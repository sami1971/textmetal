//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using Microsoft.Win32;

namespace TextMetal.ConnectionDialogApi
{
	public partial class OleDBConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		#region Constructors/Destructors

		public OleDBConnectionUIControl()
		{
			this.InitializeComponent();
			this.RightToLeft = RightToLeft.Inherit;
			this._uiThread = Thread.CurrentThread;
		}

		#endregion

		#region Fields/Constants

		private Thread _catalogEnumerationThread;
		private object[] _catalogs;
		private IDataConnectionProperties _connectionProperties;
		private bool _loading;
		private Thread _uiThread;

		#endregion

		#region Properties/Indexers/Events

		private IDataConnectionProperties Properties
		{
			get
			{
				return this._connectionProperties;
			}
		}

		#endregion

		#region Methods/Operators

		private void EnumerateCatalogs(object sender, EventArgs e)
		{
			if (this.initialCatalogComboBox.Items.Count == 0)
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					if (this._catalogEnumerationThread == null ||
					    this._catalogEnumerationThread.ThreadState == ThreadState.Stopped)
						this.EnumerateCatalogs();
					else if (this._catalogEnumerationThread.ThreadState == ThreadState.Running)
					{
						// Wait for the asynchronous enumeration to finish
						this._catalogEnumerationThread.Join();

						// Populate the combo box now, rather than waiting for
						// the asynchronous call to be marshaled back to the UI
						// thread
						this.PopulateInitialCatalogComboBox();
					}
				}
				finally
				{
					Cursor.Current = currentCursor;
				}
			}
		}

		private void EnumerateCatalogs()
		{
			// Perform the enumeration
			DataTable dataTable = null;
			OleDbConnection connection = null;
			try
			{
				// Create a connection string without initial catalog
				OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder(this.Properties.ToFullString());
				builder.Remove("Initial Catalog");

				// Create a connection
				connection = new OleDbConnection(builder.ConnectionString);

				// Open the connection
				connection.Open();

				// Try to get the DBSCHEMA_CATALOGS schema rowset
				dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, null);
			}
			catch
			{
				dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
			}
			finally
			{
				if (connection != null)
					connection.Dispose();
			}

			// Create the object array of catalog names
			this._catalogs = new object[dataTable.Rows.Count];
			for (int i = 0; i < this._catalogs.Length; i++)
				this._catalogs[i] = dataTable.Rows[i]["CATALOG_NAME"];

			// Populate the initial catalog combo box items (must occur on the UI thread)
			if (Thread.CurrentThread == this._uiThread)
				this.PopulateInitialCatalogComboBox();
			else if (this.IsHandleCreated)
				this.BeginInvoke(new ThreadStart(this.PopulateInitialCatalogComboBox));
		}

		private void EnumerateProviders()
		{
			Cursor currentCursor = Cursor.Current;
			OleDbDataReader dr = null;
			try
			{
				Cursor.Current = Cursors.WaitCursor;

				// Get the sources rowset for the root OLE DB enumerator
				dr = OleDbEnumerator.GetEnumerator(Type.GetTypeFromCLSID(NativeMethods.CLSID_OLEDB_ENUMERATOR));

				// Get the CLSIDs and descriptions of each data source (not binders or enumerators)
				Dictionary<string, string> sources = new Dictionary<string, string>(); // avoids duplicate entries
				while (dr.Read())
				{
					int type = dr.GetInt32(dr.GetOrdinal("SOURCES_TYPE"));
					if (type == NativeMethods.DBSOURCETYPE_DATASOURCE_TDP ||
					    type == NativeMethods.DBSOURCETYPE_DATASOURCE_MDP)
					{
						string clsId = dr["SOURCES_CLSID"].ToString();
						string description = dr["SOURCES_DESCRIPTION"].ToString();
						sources[clsId] = description;
					}
				}

				// Get the full ProgID for each data source
				Dictionary<string, string> sourceProgIds = new Dictionary<string, string>(sources.Count);
				RegistryKey key = Registry.ClassesRoot.OpenSubKey("CLSID");
				using (key)
				{
					foreach (KeyValuePair<string, string> source in sources)
					{
						RegistryKey subKey = key.OpenSubKey(source.Key + "\\ProgID");
						if (subKey != null)
						{
							using (subKey)
							{
								string progId = key.OpenSubKey(source.Key + "\\ProgID").GetValue(null) as string;
								if (progId != null &&
								    !progId.Equals("MSDASQL", StringComparison.OrdinalIgnoreCase) &&
								    !progId.StartsWith("MSDASQL.", StringComparison.OrdinalIgnoreCase) &&
								    !progId.Equals("Microsoft OLE DB Provider for ODBC Drivers"))
									sourceProgIds[progId] = source.Key;
							} // subKey is disposed here
						}
					}
				} // key is disposed here

				// Populate the combo box
				foreach (KeyValuePair<string, string> entry in sourceProgIds)
					this.providerComboBox.Items.Add(new ProviderStruct(entry.Key, sources[entry.Value]));
			}
			finally
			{
				if (dr != null)
					dr.Dispose();

				Cursor.Current = currentCursor;
			}
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size preferredSize = base.GetPreferredSize(proposedSize);

			// We have these "Blank Password" and "Allow Saving Password"
			// check boxes on the same line.  In long text languages, this
			// doesn't always fit.  Tweak the preferred size to account for
			// this.
			int preferredWidth =
				this.logonGroupBox.Padding.Left +
				this.loginTableLayoutPanel.Margin.Left +
				this.blankPasswordCheckBox.Margin.Left +
				this.blankPasswordCheckBox.Width +
				this.blankPasswordCheckBox.Margin.Right +
				this.allowSavingPasswordCheckBox.Margin.Left +
				this.allowSavingPasswordCheckBox.Width +
				this.allowSavingPasswordCheckBox.Margin.Right +
				this.loginTableLayoutPanel.Margin.Right +
				this.logonGroupBox.Padding.Right;
			if (preferredWidth > preferredSize.Width)
				preferredSize = new Size(preferredWidth, preferredSize.Height);

			return preferredSize;
		}

		private void HandleComboBoxDownKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
				this.EnumerateCatalogs(sender, e);
		}

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			this.Initialize(connectionProperties, false);
		}

		public void Initialize(IDataConnectionProperties connectionProperties, bool disableProviderSelection)
		{
			if (!(connectionProperties is OleDBConnectionProperties))
				throw new ArgumentException(Strings.OleDBConnectionUIControl_InvalidConnectionProperties);

			this.EnumerateProviders();
			this.providerComboBox.Enabled = !disableProviderSelection;
			this.dataLinksButton.Enabled = false;
			this.dataSourceGroupBox.Enabled = false;
			this.logonGroupBox.Enabled = false;
			this.initialCatalogLabel.Enabled = false;
			this.initialCatalogComboBox.Enabled = false;

			this._connectionProperties = connectionProperties;
		}

		public void LoadProperties()
		{
			this._loading = true;

			string provider = this.Properties["Provider"] as string;
			if (provider != null && provider.Length > 0)
			{
				object candidate = null;
				foreach (ProviderStruct providerStruct in this.providerComboBox.Items)
				{
					if (providerStruct.ProgId.Equals(provider))
					{
						candidate = providerStruct;
						break;
					}
					if (providerStruct.ProgId.StartsWith(provider + ".", StringComparison.OrdinalIgnoreCase))
					{
						if (candidate == null ||
						    providerStruct.ProgId.CompareTo(((ProviderStruct)candidate).ProgId) > 0)
							candidate = providerStruct;
					}
				}
				this.providerComboBox.SelectedItem = candidate;
			}
			else
				this.providerComboBox.SelectedItem = null;

			if (this.Properties.Contains("Data Source") &&
			    this.Properties["Data Source"] is string)
				this.dataSourceTextBox.Text = this.Properties["Data Source"] as string;
			else
				this.dataSourceTextBox.Text = null;
			if (this.Properties.Contains("Location") &&
			    this.Properties["Location"] is string)
				this.locationTextBox.Text = this.Properties["Location"] as string;
			else
				this.locationTextBox.Text = null;
			if (this.Properties.Contains("Integrated Security") &&
			    this.Properties["Integrated Security"] is string &&
			    (this.Properties["Integrated Security"] as string).Length > 0)
				this.integratedSecurityRadioButton.Checked = true;
			else
				this.nativeSecurityRadioButton.Checked = true;
			if (this.Properties.Contains("User ID") &&
			    this.Properties["User ID"] is string)
				this.userNameTextBox.Text = this.Properties["User ID"] as string;
			else
				this.userNameTextBox.Text = null;
			if (this.Properties.Contains("Password") &&
			    this.Properties["Password"] is string)
			{
				this.passwordTextBox.Text = this.Properties["Password"] as string;
				this.blankPasswordCheckBox.Checked = (this.passwordTextBox.Text.Length == 0);
			}
			else
			{
				this.passwordTextBox.Text = null;
				this.blankPasswordCheckBox.Checked = false;
			}
			if (this.Properties.Contains("Persist Security Info") &&
			    this.Properties["Persist Security Info"] is bool)
				this.allowSavingPasswordCheckBox.Checked = (bool)this.Properties["Persist Security Info"];
			else
				this.allowSavingPasswordCheckBox.Checked = false;
			if (this.Properties.Contains("Initial Catalog") &&
			    this.Properties["Initial Catalog"] is string)
				this.initialCatalogComboBox.Text = this.Properties["Initial Catalog"] as string;
			else
				this.initialCatalogComboBox.Text = null;

			this._loading = false;
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.Parent == null)
				this.OnFontChanged(e);
		}

		// Simulate RTL mirroring
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			if (this.ParentForm != null &&
			    this.ParentForm.RightToLeftLayout == true &&
			    this.RightToLeft == RightToLeft.Yes)
			{
				LayoutUtils.MirrorControl(this.providerLabel, this.providerTableLayoutPanel);
				LayoutUtils.MirrorControl(this.integratedSecurityRadioButton);
				LayoutUtils.MirrorControl(this.nativeSecurityRadioButton);
				LayoutUtils.MirrorControl(this.loginTableLayoutPanel);
				LayoutUtils.MirrorControl(this.initialCatalogLabel, this.initialCatalogComboBox);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.initialCatalogLabel, this.initialCatalogComboBox);
				LayoutUtils.UnmirrorControl(this.loginTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.nativeSecurityRadioButton);
				LayoutUtils.UnmirrorControl(this.integratedSecurityRadioButton);
				LayoutUtils.UnmirrorControl(this.providerLabel, this.providerTableLayoutPanel);
			}
		}

		private void PopulateInitialCatalogComboBox()
		{
			if (this.initialCatalogComboBox.Items.Count == 0)
			{
				if (this._catalogs.Length > 0)
					this.initialCatalogComboBox.Items.AddRange(this._catalogs);
				else
					this.initialCatalogComboBox.Items.Add(String.Empty);
			}
			this._catalogEnumerationThread = null;
		}

		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			Size baseSize = this.Size;
			this.MinimumSize = Size.Empty;
			base.ScaleControl(factor, specified);
			this.MinimumSize = new Size(
				(int)Math.Round((float)baseSize.Width * factor.Width),
				(int)Math.Round((float)baseSize.Height * factor.Height));
		}

		private void SetAllowSavingPassword(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Persist Security Info"] = this.allowSavingPasswordCheckBox.Checked;
		}

		private void SetBlankPassword(object sender, EventArgs e)
		{
			if (this.blankPasswordCheckBox.Checked)
			{
				if (!this._loading)
					this.Properties["Password"] = String.Empty;
				this.passwordLabel.Enabled = false;
				this.passwordTextBox.Enabled = false;
			}
			else
			{
				if (!this._loading)
					this.SetPassword(sender, e);
				this.passwordLabel.Enabled = true;
				this.passwordTextBox.Enabled = true;
			}
		}

		private void SetDataSource(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Data Source"] = (this.dataSourceTextBox.Text.Trim().Length > 0) ? this.dataSourceTextBox.Text.Trim() : null;
			this.initialCatalogComboBox.Items.Clear(); // a server change requires a refresh here
		}

		private void SetInitialCatalog(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties["Initial Catalog"] = (this.initialCatalogComboBox.Text.Trim().Length > 0) ? this.initialCatalogComboBox.Text.Trim() : null;
				if (this.initialCatalogComboBox.Items.Count == 0 && this._catalogEnumerationThread == null)
				{
					// Start an enumeration of initial catalogs
					this._catalogEnumerationThread = new Thread(new ThreadStart(this.EnumerateCatalogs));
					this._catalogEnumerationThread.Start();
				}
			}
		}

		private void SetLocation(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Location"] = this.locationTextBox.Text;
			this.initialCatalogComboBox.Items.Clear(); // a server change requires a refresh here
		}

		private void SetPassword(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties["Password"] = (this.passwordTextBox.Text.Length > 0) ? this.passwordTextBox.Text : null;
				if (this.passwordTextBox.Text.Length == 0)
					this.Properties.Remove("Password");
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
			this.initialCatalogComboBox.Items.Clear(); // a password change requires a refresh here
		}

		private void SetProvider(object sender, EventArgs e)
		{
			if (this.providerComboBox.SelectedItem is ProviderStruct)
			{
				// Set the provider to initialize the correct set of properties
				if (!this._loading)
					this.Properties["Provider"] = ((ProviderStruct)this.providerComboBox.SelectedItem).ProgId;

				// Remove all miscellaneous properties because they just get in the way
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(this.Properties))
				{
					if (descriptor.Category.Equals(CategoryAttribute.Default.Category, StringComparison.CurrentCulture))
						this.Properties.Remove(descriptor.DisplayName);
				}

				// Enable data links button
				this.dataLinksButton.Enabled = true;

				// Enable all container controls
				this.dataSourceGroupBox.Enabled = true;
				this.logonGroupBox.Enabled = true;
				this.loginTableLayoutPanel.Enabled = true;
				this.initialCatalogLabel.Enabled = true;
				this.initialCatalogComboBox.Enabled = true;

				// Initially disable all end user controls
				this.dataSourceLabel.Enabled = false;
				this.dataSourceTextBox.Enabled = false;
				this.locationLabel.Enabled = false;
				this.locationTextBox.Enabled = false;
				this.integratedSecurityRadioButton.Enabled = false;
				this.nativeSecurityRadioButton.Enabled = false;
				this.userNameLabel.Enabled = false;
				this.userNameTextBox.Enabled = false;
				this.passwordLabel.Enabled = false;
				this.passwordTextBox.Enabled = false;
				this.blankPasswordCheckBox.Enabled = false;
				this.allowSavingPasswordCheckBox.Enabled = false;
				this.initialCatalogLabel.Enabled = false;
				this.initialCatalogComboBox.Enabled = false;

				// Now selectively enable those that are supported
				PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(this.Properties);
				PropertyDescriptor propertyDescriptor = null;
				if ((propertyDescriptor = propertyDescriptors["DataSource"]) != null &&
				    propertyDescriptor.IsBrowsable)
				{
					this.dataSourceLabel.Enabled = true;
					this.dataSourceTextBox.Enabled = true;
				}
				if ((propertyDescriptor = propertyDescriptors["Location"]) != null &&
				    propertyDescriptor.IsBrowsable)
				{
					this.locationLabel.Enabled = true;
					this.locationTextBox.Enabled = true;
				}
				this.dataSourceGroupBox.Enabled = (this.dataSourceTextBox.Enabled || this.locationTextBox.Enabled);
				if ((propertyDescriptor = propertyDescriptors["Integrated Security"]) != null &&
				    propertyDescriptor.IsBrowsable)
					this.integratedSecurityRadioButton.Enabled = true;
				if ((propertyDescriptor = propertyDescriptors["User ID"]) != null &&
				    propertyDescriptor.IsBrowsable)
				{
					this.userNameLabel.Enabled = true;
					this.userNameTextBox.Enabled = true;
				}
				if ((propertyDescriptor = propertyDescriptors["Password"]) != null &&
				    propertyDescriptor.IsBrowsable)
				{
					this.passwordLabel.Enabled = true;
					this.passwordTextBox.Enabled = true;
					this.blankPasswordCheckBox.Enabled = true;
				}
				if (this.passwordTextBox.Enabled &&
				    (propertyDescriptor = propertyDescriptors["PersistSecurityInfo"]) != null &&
				    propertyDescriptor.IsBrowsable)
					this.allowSavingPasswordCheckBox.Enabled = true;
				this.loginTableLayoutPanel.Enabled = (this.userNameTextBox.Enabled || this.passwordTextBox.Enabled);
				this.nativeSecurityRadioButton.Enabled = this.loginTableLayoutPanel.Enabled;
				this.logonGroupBox.Enabled = (this.integratedSecurityRadioButton.Enabled || this.nativeSecurityRadioButton.Enabled);
				if ((propertyDescriptor = propertyDescriptors["Initial Catalog"]) != null &&
				    propertyDescriptor.IsBrowsable)
				{
					this.initialCatalogLabel.Enabled = true;
					this.initialCatalogComboBox.Enabled = true;
				}
			}
			else
			{
				if (!this._loading)
					this.Properties["Provider"] = null;

				// Disable data links button
				this.dataLinksButton.Enabled = false;

				// Disable all container controls
				this.dataSourceGroupBox.Enabled = false;
				this.logonGroupBox.Enabled = false;
				this.initialCatalogLabel.Enabled = false;
				this.initialCatalogComboBox.Enabled = false;
			}

			if (!this._loading)
				this.LoadProperties();

			this.initialCatalogComboBox.Items.Clear(); // a provider change requires a refresh here
		}

		private void SetProviderDropDownWidth(object sender, EventArgs e)
		{
			if (this.providerComboBox.Items.Count > 0)
			{
				int largestWidth = 0;
				using (Graphics g = Graphics.FromHwnd(this.providerComboBox.Handle))
				{
					foreach (ProviderStruct providerStruct in this.providerComboBox.Items)
					{
						int width = TextRenderer.MeasureText(
							g,
							providerStruct.Description,
							this.providerComboBox.Font,
							new Size(Int32.MaxValue, Int32.MaxValue),
							TextFormatFlags.WordBreak
							).Width;
						if (width > largestWidth)
							largestWidth = width;
					}
				}
				this.providerComboBox.DropDownWidth = largestWidth + 3; // give a little extra margin
				if (this.providerComboBox.Items.Count > this.providerComboBox.MaxDropDownItems)
					this.providerComboBox.DropDownWidth += SystemInformation.VerticalScrollBarWidth;
			}
			else
				this.providerComboBox.DropDownWidth = this.providerComboBox.Width;
		}

		private void SetSecurityOption(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				if (this.integratedSecurityRadioButton.Checked)
				{
					this.Properties["Integrated Security"] = "SSPI";
					this.Properties.Reset("User ID");
					this.Properties.Reset("Password");
					this.Properties.Reset("Persist Security Info");
				}
				else
				{
					this.Properties.Reset("Integrated Security");
					this.SetUserName(sender, e);
					this.SetPassword(sender, e);
					this.SetBlankPassword(sender, e);
					this.SetAllowSavingPassword(sender, e);
				}
			}
			this.loginTableLayoutPanel.Enabled = !this.integratedSecurityRadioButton.Checked;
			this.initialCatalogComboBox.Items.Clear(); // an authentication change requires a refresh here
		}

		private void SetUserName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["User ID"] = (this.userNameTextBox.Text.Trim().Length > 0) ? this.userNameTextBox.Text.Trim() : null;
			this.initialCatalogComboBox.Items.Clear(); // a user name change requires a refresh here
		}

		private void ShowDataLinks(object sender, EventArgs e)
		{
			try
			{
				// Create data links object as IDataInitialize
				Type dataLinksType = Type.GetTypeFromCLSID(NativeMethods.CLSID_DataLinks);
				NativeMethods.IDataInitialize dataInitialize = Activator.CreateInstance(dataLinksType) as NativeMethods.IDataInitialize;

				// Create data source object from connection string
				object dataSource = null;
				dataInitialize.GetDataSource(null,
				                             NativeMethods.CLSCTX_INPROC_SERVER,
				                             this.Properties.ToFullString(),
				                             ref NativeMethods.IID_IUnknown,
				                             ref dataSource);

				// Get IDBPromptInitialize interface from data links object
				NativeMethods.IDBPromptInitialize promptInitialize = (NativeMethods.IDBPromptInitialize)dataInitialize;

				// Display the data links dialog using this data source
				promptInitialize.PromptDataSource(
					null,
					this.ParentForm.Handle,
					NativeMethods.DBPROMPTOPTIONS_PROPERTYSHEET | NativeMethods.DBPROMPTOPTIONS_DISABLE_PROVIDER_SELECTION,
					0,
					IntPtr.Zero,
					null,
					ref NativeMethods.IID_IUnknown,
					ref dataSource);

				// Retrieve the new connection string from the data source
				string newConnectionString = null;
				dataInitialize.GetInitializationString(dataSource, true, out newConnectionString);

				// Parse the new connection string into the connection properties object
				this.Properties.Parse(newConnectionString);

				// Reload the control with the modified connection properties
				this.LoadProperties();
			}
			catch (Exception ex)
			{
				COMException comex = ex as COMException;
				if (comex == null || comex.ErrorCode != NativeMethods.DB_E_CANCELED)
				{
					IUIService uiService = this.GetService(typeof(IUIService)) as IUIService;
					if (uiService != null)
						uiService.ShowError(ex);
					else
						RTLAwareMessageBox.Show(null, ex.Message, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private struct ProviderStruct
		{
			#region Constructors/Destructors

			public ProviderStruct(string progId, string description)
			{
				this._progId = progId;
				this._description = description;
			}

			#endregion

			#region Fields/Constants

			private string _description;
			private string _progId;

			#endregion

			#region Properties/Indexers/Events

			public string Description
			{
				get
				{
					return this._description;
				}
			}

			public string ProgId
			{
				get
				{
					return this._progId;
				}
			}

			#endregion

			#region Methods/Operators

			public override string ToString()
			{
				return this._description;
			}

			#endregion
		}

		#endregion
	}
}