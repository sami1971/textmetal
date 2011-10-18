//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TextMetal.ConnectionDialogApi
{
	public partial class OdbcConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		public OdbcConnectionUIControl()
		{
			this.InitializeComponent();
			this.RightToLeft = RightToLeft.Inherit;

			// WinForms automatically sets the accessible name for text boxes based on
			// a label previous to it, but does not do the same when it is proceeded
			// by a radio button.  So, simulate that behavior here.
			this.dataSourceNameComboBox.AccessibleName = TextWithoutMnemonics(this.useDataSourceNameRadioButton.Text);
			this.connectionStringTextBox.AccessibleName = TextWithoutMnemonics(this.useConnectionStringRadioButton.Text);

			this._uiThread = Thread.CurrentThread;
		}

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			if (!(connectionProperties is OdbcConnectionProperties))
				throw new ArgumentException(Strings.OdbcConnectionUIControl_InvalidConnectionProperties);

			this._connectionProperties = connectionProperties;
		}

		public void LoadProperties()
		{
			this._loading = true;

			this.EnumerateDataSourceNames();

			if (this.Properties.ToFullString().Length == 0 ||
			    (this.Properties["Dsn"] is string && (this.Properties["Dsn"] as string).Length > 0))
				this.useDataSourceNameRadioButton.Checked = true;
			else
				this.useConnectionStringRadioButton.Checked = true;
			this.UpdateControls();

			this._loading = false;
		}

		// Simulate RTL mirroring
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			if (this.ParentForm != null &&
			    this.ParentForm.RightToLeftLayout == true &&
			    this.RightToLeft == RightToLeft.Yes)
			{
				LayoutUtils.MirrorControl(this.useDataSourceNameRadioButton);
				LayoutUtils.MirrorControl(this.dataSourceNameTableLayoutPanel);
				LayoutUtils.MirrorControl(this.useConnectionStringRadioButton);
				LayoutUtils.MirrorControl(this.connectionStringTableLayoutPanel);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.connectionStringTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.useConnectionStringRadioButton);
				LayoutUtils.UnmirrorControl(this.dataSourceNameTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.useDataSourceNameRadioButton);
			}
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

		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.ActiveControl == this.useDataSourceNameRadioButton &&
			    (keyData & Keys.KeyCode) == Keys.Down)
			{
				this.useConnectionStringRadioButton.Focus();
				return true;
			}
			if (this.ActiveControl == this.useConnectionStringRadioButton &&
			    (keyData & Keys.KeyCode) == Keys.Down)
			{
				this.useDataSourceNameRadioButton.Focus();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.Parent == null)
				this.OnFontChanged(e);
		}

		private void SetDataSourceOption(object sender, EventArgs e)
		{
			if (this.useDataSourceNameRadioButton.Checked)
			{
				this.dataSourceNameTableLayoutPanel.Enabled = true;
				if (!this._loading)
				{
					string dsn = this.Properties["Dsn"] as string;
					string uid = (this.Properties.Contains("uid")) ? this.Properties["uid"] as string : null;
					string pwd = (this.Properties.Contains("pwd")) ? this.Properties["pwd"] as string : null;
					this.Properties.Parse(String.Empty);
					this.Properties["Dsn"] = dsn;
					this.Properties["uid"] = uid;
					this.Properties["pwd"] = pwd;
				}
				this.UpdateControls();
				this.connectionStringTableLayoutPanel.Enabled = false;
			}
			else /* if (useConnectionStringRadioButton.Checked) */
			{
				this.dataSourceNameTableLayoutPanel.Enabled = false;
				if (!this._loading)
				{
					string dsn = this.Properties["Dsn"] as string;
					string uid = (this.Properties.Contains("uid")) ? this.Properties["uid"] as string : null;
					string pwd = (this.Properties.Contains("pwd")) ? this.Properties["pwd"] as string : null;
					this.Properties.Parse(this.connectionStringTextBox.Text);
					this.Properties["Dsn"] = dsn;
					this.Properties["uid"] = uid;
					this.Properties["pwd"] = pwd;
				}
				this.UpdateControls();
				this.connectionStringTableLayoutPanel.Enabled = true;
			}
		}

		private void HandleComboBoxDownKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
				this.EnumerateDataSourceNames(sender, e);
		}

#if NOTUSED
		private void SettingDataSourceName(object sender, EventArgs e)
		{
			if (!_loading)
			{
				Properties["Dsn"] = (dataSourceNameComboBox.Text.Trim().Length > 0) ? dataSourceNameComboBox.Text.Trim() : null;
				if (dataSourceNameComboBox.Items.Count == 0 && _dataSourceNameEnumerationThread == null)
				{
					// Start an enumeration of data source names
					_dataSourceNameEnumerationThread = new Thread(new ThreadStart(EnumerateDataSourceNames));
					_dataSourceNameEnumerationThread.Start();
				}
			}
		}
#endif

		private void EnumerateDataSourceNames(object sender, EventArgs e)
		{
			if (this.dataSourceNameComboBox.Items.Count == 0)
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
#if NOTUSED
					if (_dataSourceNameEnumerationThread == null ||
						_dataSourceNameEnumerationThread.ThreadState == ThreadState.Stopped)
					{
#endif
					this.EnumerateDataSourceNames();
#if NOTUSED
					}
					else if (_dataSourceNameEnumerationThread.ThreadState == ThreadState.Running)
					{
						// Wait for the asynchronous enumeration to finish
						_dataSourceNameEnumerationThread.Join();

						// Populate the combo box now, rather than waiting for
						// the asynchronous call to be marshaled back to the UI
						// thread
						PopulateDataSourceNameComboBox();
					}
#endif
				}
				finally
				{
					Cursor.Current = currentCursor;
				}
			}
		}

		private void SetDataSourceName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Dsn"] = (this.dataSourceNameComboBox.Text.Length > 0) ? this.dataSourceNameComboBox.Text : null;
			this.UpdateControls();
		}

		private void RefreshDataSourceNames(object sender, EventArgs e)
		{
			this.dataSourceNameComboBox.Items.Clear();
			this.EnumerateDataSourceNames(sender, e);
		}

		private void SetConnectionString(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				string pwd = (this.Properties.Contains("pwd")) ? this.Properties["pwd"] as string : null;
				try
				{
					this.Properties.Parse(this.connectionStringTextBox.Text.Trim());
				}
				catch (ArgumentException ex)
				{
					IUIService uiService = null;
					if (this.ParentForm != null && this.ParentForm.Site != null)
						uiService = this.ParentForm.Site.GetService(typeof(IUIService)) as IUIService;
					if (uiService != null)
						uiService.ShowError(ex);
					else
						RTLAwareMessageBox.Show(null, ex.Message, MessageBoxIcon.Exclamation);
				}
				if (this.connectionStringTextBox.Text.Trim().Length > 0 &&
				    !this.Properties.Contains("pwd") && pwd != null)
					this.Properties["pwd"] = pwd;
				this.connectionStringTextBox.Text = this.Properties.ToDisplayString();
			}
			this.UpdateControls();
		}

		private void BuildConnectionString(object sender, EventArgs e)
		{
			IntPtr henv = IntPtr.Zero;
			IntPtr hdbc = IntPtr.Zero;
			short result = 0;
			try
			{
				result = NativeMethods.SQLAllocEnv(out henv);
				if (!NativeMethods.SQL_SUCCEEDED(result))
					throw new ApplicationException(Strings.OdbcConnectionUIControl_SQLAllocEnvFailed);

				result = NativeMethods.SQLAllocConnect(henv, out hdbc);
				if (!NativeMethods.SQL_SUCCEEDED(result))
					throw new ApplicationException(Strings.OdbcConnectionUIControl_SQLAllocConnectFailed);

				string currentConnectionString = this.Properties.ToFullString();
				StringBuilder newConnectionString = new StringBuilder(1024);
				short newConnectionStringLength = 0;
				result = NativeMethods.SQLDriverConnect(hdbc, this.ParentForm.Handle, currentConnectionString, (short)currentConnectionString.Length, newConnectionString, 1024, out newConnectionStringLength, NativeMethods.SQL_DRIVER_PROMPT);
				if (!NativeMethods.SQL_SUCCEEDED(result) && result != NativeMethods.SQL_NO_DATA)
				{
					// Try again without the current connection string, in case it was invalid
					result = NativeMethods.SQLDriverConnect(hdbc, this.ParentForm.Handle, null, 0, newConnectionString, 1024, out newConnectionStringLength, NativeMethods.SQL_DRIVER_PROMPT);
				}
				if (!NativeMethods.SQL_SUCCEEDED(result) && result != NativeMethods.SQL_NO_DATA)
					throw new ApplicationException(Strings.OdbcConnectionUIControl_SQLDriverConnectFailed);
				else
					NativeMethods.SQLDisconnect(hdbc);

				if (newConnectionStringLength > 0)
				{
					this.RefreshDataSourceNames(sender, e);
					this.Properties.Parse(newConnectionString.ToString());
					this.UpdateControls();
				}
			}
			finally
			{
				if (hdbc != IntPtr.Zero)
					NativeMethods.SQLFreeConnect(hdbc);
				if (henv != IntPtr.Zero)
					NativeMethods.SQLFreeEnv(henv);
			}
		}

		private void SetUserName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["uid"] = (this.userNameTextBox.Text.Trim().Length > 0) ? this.userNameTextBox.Text.Trim() : null;
			this.UpdateControls();
		}

		private void SetPassword(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties["pwd"] = (this.passwordTextBox.Text.Length > 0) ? this.passwordTextBox.Text : null;
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
			this.UpdateControls();
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
			this.UpdateControls();
		}

		private void UpdateControls()
		{
			if (this.Properties["Dsn"] is string &&
			    (this.Properties["Dsn"] as string).Length > 0 &&
			    this.dataSourceNameComboBox.Items.Contains(this.Properties["Dsn"]))
				this.dataSourceNameComboBox.Text = this.Properties["Dsn"] as string;
			else
				this.dataSourceNameComboBox.Text = null;
			this.connectionStringTextBox.Text = this.Properties.ToDisplayString();
			if (this.Properties.Contains("uid"))
				this.userNameTextBox.Text = this.Properties["uid"] as string;
			else
				this.userNameTextBox.Text = null;
			if (this.Properties.Contains("pwd"))
				this.passwordTextBox.Text = this.Properties["pwd"] as string;
			else
				this.passwordTextBox.Text = null;
		}

		private void EnumerateDataSourceNames()
		{
			// Perform the enumeration
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			try
			{
				// Use the MSDAORA enumerator
				OleDbDataReader reader = OleDbEnumerator.GetEnumerator(Type.GetTypeFromCLSID(NativeMethods.CLSID_MSDASQL_ENUMERATOR));
				using (reader)
					dataTable.Load(reader);
			}
			catch
			{
			}

			// Create the object array of data source names (with instances appended)
			this._dataSourceNames = new object[dataTable.Rows.Count];
			for (int i = 0; i < this._dataSourceNames.Length; i++)
				this._dataSourceNames[i] = dataTable.Rows[i]["SOURCES_NAME"] as string;

			// Sort the list
			Array.Sort(this._dataSourceNames);

			// Populate the server combo box items (must occur on the UI thread)
			if (Thread.CurrentThread == this._uiThread)
				this.PopulateDataSourceNameComboBox();
			else if (this.IsHandleCreated)
				this.BeginInvoke(new ThreadStart(this.PopulateDataSourceNameComboBox));
		}

		private void PopulateDataSourceNameComboBox()
		{
			if (this.dataSourceNameComboBox.Items.Count == 0)
			{
				if (this._dataSourceNames.Length > 0)
					this.dataSourceNameComboBox.Items.AddRange(this._dataSourceNames);
				else
					this.dataSourceNameComboBox.Items.Add(String.Empty);
			}
		}

		private static string TextWithoutMnemonics(string text)
		{
			if (text == null)
				return null;

			int index = text.IndexOf('&');
			if (index == -1)
				return text;

			StringBuilder str = new StringBuilder(text.Substring(0, index));
			for (; index < text.Length; ++index)
			{
				if (text[index] == '&')
				{
					// Skip this & and copy the next character instead
					index++;
				}
				if (index < text.Length)
					str.Append(text[index]);
			}

			return str.ToString();
		}

		private IDataConnectionProperties Properties
		{
			get
			{
				return this._connectionProperties;
			}
		}

		private bool _loading;
		private object[] _dataSourceNames;
		private Thread _uiThread;
		//		private Thread _dataSourceNameEnumerationThread;
		private IDataConnectionProperties _connectionProperties;
	}
}