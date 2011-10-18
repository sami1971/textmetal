//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ThreadState = System.Threading.ThreadState;

namespace TextMetal.ConnectionDialogApi
{
	public partial class SqlConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		#region Constructors/Destructors

		public SqlConnectionUIControl()
		{
			this.InitializeComponent();
			this.RightToLeft = RightToLeft.Inherit;

			int requiredHeight = LayoutUtils.GetPreferredCheckBoxHeight(this.savePasswordCheckBox);
			if (this.savePasswordCheckBox.Height < requiredHeight)
			{
				this.savePasswordCheckBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
				this.loginTableLayoutPanel.Height += this.loginTableLayoutPanel.Margin.Bottom;
				this.loginTableLayoutPanel.Margin = new Padding(this.loginTableLayoutPanel.Margin.Left, this.loginTableLayoutPanel.Margin.Top, this.loginTableLayoutPanel.Margin.Right, 0);
			}

			// Apparently WinForms automatically sets the accessible name for text boxes
			// based on a label previous to it, but does not do the same when it is
			// proceeded by a radio button.  So, simulate that behavior here
			this.selectDatabaseComboBox.AccessibleName = TextWithoutMnemonics(this.selectDatabaseRadioButton.Text);
			this.attachDatabaseTextBox.AccessibleName = TextWithoutMnemonics(this.attachDatabaseRadioButton.Text);

			this._uiThread = Thread.CurrentThread;
		}

		#endregion

		#region Fields/Constants

		private ControlProperties _controlProperties;
		private Thread _databaseEnumerationThread;
		private object[] _databases;
		private bool _loading;
		private Thread _serverEnumerationThread;
		private object[] _servers;
		private Thread _uiThread;
		private string currentOleDBProvider;
		private bool currentUserInstanceSetting;

		#endregion

		#region Properties/Indexers/Events

		private ControlProperties Properties
		{
			get
			{
				return this._controlProperties;
			}
		}

		#endregion

		#region Methods/Operators

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

		private void Browse(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Title = Strings.SqlConnectionUIControl_BrowseFileTitle;
			fileDialog.Multiselect = false;
			fileDialog.RestoreDirectory = true;
			fileDialog.Filter = Strings.SqlConnectionUIControl_BrowseFileFilter;
			fileDialog.DefaultExt = Strings.SqlConnectionUIControl_BrowseFileDefaultExt;
			if (this.Container != null)
				this.Container.Add(fileDialog);
			try
			{
				DialogResult result = fileDialog.ShowDialog(this.ParentForm);
				if (result == DialogResult.OK)
					this.attachDatabaseTextBox.Text = fileDialog.FileName.Trim();
			}
			finally
			{
				if (this.Container != null)
					this.Container.Remove(fileDialog);
				fileDialog.Dispose();
			}
		}

		private void EnumerateDatabases(object sender, EventArgs e)
		{
			if (this.selectDatabaseComboBox.Items.Count == 0)
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					if (this._databaseEnumerationThread == null ||
					    this._databaseEnumerationThread.ThreadState == ThreadState.Stopped)
						this.EnumerateDatabases();
					else if (this._databaseEnumerationThread.ThreadState == ThreadState.Running)
					{
						// Wait for the asynchronous enumeration to finish
						this._databaseEnumerationThread.Join();

						// Populate the combo box now, rather than waiting for
						// the asynchronous call to be marshaled back to the UI
						// thread
						this.PopulateDatabaseComboBox();
					}
				}
				finally
				{
					Cursor.Current = currentCursor;
				}
			}
		}

		private void EnumerateDatabases()
		{
			// Perform the enumeration
			DataTable dataTable = null;
			IDbConnection connection = null;
			IDataReader reader = null;
			try
			{
				// Get a basic connection
				connection = this.Properties.GetBasicConnection();

				// Create a command to check if the database is on SQL AZure.
				IDbCommand command = connection.CreateCommand();
				command.CommandText = "SELECT CASE WHEN SERVERPROPERTY(N'EDITION') = 'SQL Data Services' OR SERVERPROPERTY(N'EDITION') = 'SQL Azure' THEN 1 ELSE 0 END";

				// Open the connection
				connection.Open();

				// SQL AZure doesn't support HAS_DBACCESS at this moment.
				// Change the command text to get database names accordingly
				if ((Int32)(command.ExecuteScalar()) == 1)
					command.CommandText = "SELECT name FROM master.dbo.sysdatabases ORDER BY name";
				else
					command.CommandText = "SELECT name FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1 ORDER BY name";

				// Execute the command
				reader = command.ExecuteReader();

				// Read into the data table
				dataTable = new DataTable();
				dataTable.Locale = CultureInfo.CurrentCulture;
				dataTable.Load(reader);
			}
			catch
			{
				dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
			}
			finally
			{
				if (reader != null)
					reader.Dispose();
				if (connection != null)
					connection.Dispose();
			}

			// Create the object array of database names
			this._databases = new object[dataTable.Rows.Count];
			for (int i = 0; i < this._databases.Length; i++)
				this._databases[i] = dataTable.Rows[i]["name"];

			// Populate the database combo box items (must occur on the UI thread)
			if (Thread.CurrentThread == this._uiThread)
				this.PopulateDatabaseComboBox();
			else if (this.IsHandleCreated)
				this.BeginInvoke(new ThreadStart(this.PopulateDatabaseComboBox));
		}

		private void EnumerateServers(object sender, EventArgs e)
		{
			if (this.serverComboBox.Items.Count == 0)
			{
				Cursor currentCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				try
				{
					if (this._serverEnumerationThread == null ||
					    this._serverEnumerationThread.ThreadState == ThreadState.Stopped)
						this.EnumerateServers();
					else if (this._serverEnumerationThread.ThreadState == ThreadState.Running)
					{
						// Wait for the asynchronous enumeration to finish
						this._serverEnumerationThread.Join();

						// Populate the combo box now, rather than waiting for
						// the asynchronous call to be marshaled back to the UI
						// thread
						this.PopulateServerComboBox();
					}
				}
				finally
				{
					Cursor.Current = currentCursor;
				}
			}
		}

		private void EnumerateServers()
		{
			// Perform the enumeration
			DataTable dataTable = null;
			try
			{
				dataTable = SqlDataSourceEnumerator.Instance.GetDataSources();
			}
			catch
			{
				dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
			}

			// Create the object array of server names (with instances appended)
			this._servers = new object[dataTable.Rows.Count];
			for (int i = 0; i < this._servers.Length; i++)
			{
				string name = dataTable.Rows[i]["ServerName"].ToString();
				string instance = dataTable.Rows[i]["InstanceName"].ToString();
				if (instance.Length == 0)
					this._servers[i] = name;
				else
					this._servers[i] = name + "\\" + instance;
			}

			// Sort the list
			Array.Sort(this._servers);

			// Populate the server combo box items (must occur on the UI thread)
			if (Thread.CurrentThread == this._uiThread)
				this.PopulateServerComboBox();
			else if (this.IsHandleCreated)
				this.BeginInvoke(new ThreadStart(this.PopulateServerComboBox));
		}

		private void HandleComboBoxDownKey(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				if (sender == this.serverComboBox)
					this.EnumerateServers(sender, e);
				if (sender == this.selectDatabaseComboBox)
					this.EnumerateDatabases(sender, e);
			}
		}

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			if (connectionProperties == null)
				throw new ArgumentNullException("connectionProperties");

			if (!(connectionProperties is SqlConnectionProperties) &&
			    !(connectionProperties is OleDBSqlConnectionProperties))
				throw new ArgumentException(Strings.SqlConnectionUIControl_InvalidConnectionProperties);

			if (connectionProperties is OleDBSqlConnectionProperties)
				this.currentOleDBProvider = connectionProperties["Provider"] as string;

			if (connectionProperties is OdbcConnectionProperties)
			{
				// ODBC does not support saving the password
				this.savePasswordCheckBox.Enabled = false;
			}

			this._controlProperties = new ControlProperties(connectionProperties);
		}

		public void LoadProperties()
		{
			this._loading = true;

			if (this.currentOleDBProvider != this.Properties.Provider)
			{
				this.selectDatabaseComboBox.Items.Clear(); // a provider change requires a refresh here
				this.currentOleDBProvider = this.Properties.Provider;
			}

			this.serverComboBox.Text = this.Properties.ServerName;
			if (this.Properties.UseWindowsAuthentication)
				this.windowsAuthenticationRadioButton.Checked = true;
			else
				this.sqlAuthenticationRadioButton.Checked = true;
			if (this.currentUserInstanceSetting != this.Properties.UserInstance)
				this.selectDatabaseComboBox.Items.Clear(); // this change requires a refresh here
			this.currentUserInstanceSetting = this.Properties.UserInstance;
			this.userNameTextBox.Text = this.Properties.UserName;
			this.passwordTextBox.Text = this.Properties.Password;
			this.savePasswordCheckBox.Checked = this.Properties.SavePassword;
			if (this.Properties.DatabaseFile == null || this.Properties.DatabaseFile.Length == 0)
			{
				this.selectDatabaseRadioButton.Checked = true;
				this.selectDatabaseComboBox.Text = this.Properties.DatabaseName;
				this.attachDatabaseTextBox.Text = null;
				this.logicalDatabaseNameTextBox.Text = null;
			}
			else
			{
				this.attachDatabaseRadioButton.Checked = true;
				this.selectDatabaseComboBox.Text = null;
				this.attachDatabaseTextBox.Text = this.Properties.DatabaseFile;
				this.logicalDatabaseNameTextBox.Text = this.Properties.LogicalDatabaseName;
			}

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
				LayoutUtils.MirrorControl(this.serverLabel, this.serverTableLayoutPanel);
				LayoutUtils.MirrorControl(this.windowsAuthenticationRadioButton);
				LayoutUtils.MirrorControl(this.sqlAuthenticationRadioButton);
				LayoutUtils.MirrorControl(this.loginTableLayoutPanel);
				LayoutUtils.MirrorControl(this.selectDatabaseRadioButton);
				LayoutUtils.MirrorControl(this.selectDatabaseComboBox);
				LayoutUtils.MirrorControl(this.attachDatabaseRadioButton);
				LayoutUtils.MirrorControl(this.attachDatabaseTableLayoutPanel);
				LayoutUtils.MirrorControl(this.logicalDatabaseNameLabel);
				LayoutUtils.MirrorControl(this.logicalDatabaseNameTextBox);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.logicalDatabaseNameTextBox);
				LayoutUtils.UnmirrorControl(this.logicalDatabaseNameLabel);
				LayoutUtils.UnmirrorControl(this.attachDatabaseTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.attachDatabaseRadioButton);
				LayoutUtils.UnmirrorControl(this.selectDatabaseComboBox);
				LayoutUtils.UnmirrorControl(this.selectDatabaseRadioButton);
				LayoutUtils.UnmirrorControl(this.loginTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.sqlAuthenticationRadioButton);
				LayoutUtils.UnmirrorControl(this.windowsAuthenticationRadioButton);
				LayoutUtils.UnmirrorControl(this.serverLabel, this.serverTableLayoutPanel);
			}
		}

		private void PopulateDatabaseComboBox()
		{
			if (this.selectDatabaseComboBox.Items.Count == 0)
			{
				if (this._databases.Length > 0)
					this.selectDatabaseComboBox.Items.AddRange(this._databases);
				else
					this.selectDatabaseComboBox.Items.Add(String.Empty);
			}
			this._databaseEnumerationThread = null;
		}

		private void PopulateServerComboBox()
		{
			if (this.serverComboBox.Items.Count == 0)
			{
				if (this._servers.Length > 0)
					this.serverComboBox.Items.AddRange(this._servers);
				else
					this.serverComboBox.Items.Add(String.Empty);
			}
		}

		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.ActiveControl == this.selectDatabaseRadioButton &&
			    (keyData & Keys.KeyCode) == Keys.Down)
			{
				this.attachDatabaseRadioButton.Focus();
				return true;
			}
			if (this.ActiveControl == this.attachDatabaseRadioButton &&
			    (keyData & Keys.KeyCode) == Keys.Down)
			{
				this.selectDatabaseRadioButton.Focus();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		private void RefreshServers(object sender, EventArgs e)
		{
			this.serverComboBox.Items.Clear();
			this.EnumerateServers(sender, e);
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

		private void SetAttachDatabase(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				if (this.selectDatabaseRadioButton.Checked)
					this.Properties.DatabaseFile = null;
				else /* if (attachDatabaseRadioButton.Checked) */
					this.Properties.DatabaseFile = this.attachDatabaseTextBox.Text;
			}
		}

		private void SetAuthenticationOption(object sender, EventArgs e)
		{
			if (this.windowsAuthenticationRadioButton.Checked)
			{
				if (!this._loading)
				{
					this.Properties.UseWindowsAuthentication = true;
					this.Properties.UserName = null;
					this.Properties.Password = null;
					this.Properties.SavePassword = false;
				}
				this.loginTableLayoutPanel.Enabled = false;
			}
			else /* if (sqlAuthenticationRadioButton.Checked) */
			{
				if (!this._loading)
				{
					this.Properties.UseWindowsAuthentication = false;
					this.SetUserName(sender, e);
					this.SetPassword(sender, e);
					this.SetSavePassword(sender, e);
				}
				this.loginTableLayoutPanel.Enabled = true;
			}
			this.SetDatabaseGroupBoxStatus(sender, e);
			this.selectDatabaseComboBox.Items.Clear(); // an authentication change requires a refresh here
		}

		private void SetDatabase(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties.DatabaseName = this.selectDatabaseComboBox.Text;
				if (this.selectDatabaseComboBox.Items.Count == 0 && this._databaseEnumerationThread == null)
				{
					// Start an enumeration of databases
					this._databaseEnumerationThread = new Thread(new ThreadStart(this.EnumerateDatabases));
					this._databaseEnumerationThread.Start();
				}
			}
		}

		private void SetDatabaseGroupBoxStatus(object sender, EventArgs e)
		{
			if (this.serverComboBox.Text.Trim().Length > 0 &&
			    (this.windowsAuthenticationRadioButton.Checked ||
			     this.userNameTextBox.Text.Trim().Length > 0))
				this.databaseGroupBox.Enabled = true;
			else
				this.databaseGroupBox.Enabled = false;
		}

		private void SetDatabaseOption(object sender, EventArgs e)
		{
			if (this.selectDatabaseRadioButton.Checked)
			{
				this.SetDatabase(sender, e);
				this.SetAttachDatabase(sender, e);
				this.selectDatabaseComboBox.Enabled = true;
				this.attachDatabaseTableLayoutPanel.Enabled = false;
				this.logicalDatabaseNameLabel.Enabled = false;
				this.logicalDatabaseNameTextBox.Enabled = false;
			}
			else /* if (attachDatabaseRadioButton.Checked) */
			{
				this.SetAttachDatabase(sender, e);
				this.SetLogicalFilename(sender, e);
				this.selectDatabaseComboBox.Enabled = false;
				this.attachDatabaseTableLayoutPanel.Enabled = true;
				this.logicalDatabaseNameLabel.Enabled = true;
				this.logicalDatabaseNameTextBox.Enabled = true;
			}
		}

		private void SetLogicalFilename(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				if (this.selectDatabaseRadioButton.Checked)
					this.Properties.LogicalDatabaseName = null;
				else /* if (attachDatabaseRadioButton.Checked) */
					this.Properties.LogicalDatabaseName = this.logicalDatabaseNameTextBox.Text;
			}
		}

		private void SetPassword(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties.Password = this.passwordTextBox.Text;
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
			this.selectDatabaseComboBox.Items.Clear(); // a password change requires a refresh here
		}

		private void SetSavePassword(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties.SavePassword = this.savePasswordCheckBox.Checked;
		}

		private void SetServer(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties.ServerName = this.serverComboBox.Text;
				if (this.serverComboBox.Items.Count == 0 && this._serverEnumerationThread == null)
				{
					// Start an enumeration of servers
					this._serverEnumerationThread = new Thread(new ThreadStart(this.EnumerateServers));
					this._serverEnumerationThread.Start();
				}
			}
			this.SetDatabaseGroupBoxStatus(sender, e);
			this.selectDatabaseComboBox.Items.Clear(); // a server change requires a refresh here
		}

		private void SetUserName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties.UserName = this.userNameTextBox.Text;
			this.SetDatabaseGroupBoxStatus(sender, e);
			this.selectDatabaseComboBox.Items.Clear(); // a user name change requires a refresh here
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class ControlProperties
		{
			#region Constructors/Destructors

			public ControlProperties(IDataConnectionProperties properties)
			{
				this._properties = properties;
			}

			#endregion

			#region Fields/Constants

			private IDataConnectionProperties _properties;

			#endregion

			#region Properties/Indexers/Events

			public string DatabaseFile
			{
				get
				{
					return this._properties[this.DatabaseFileProperty] as string;
				}
				set
				{
					if (value != null && value.Trim().Length > 0)
						this._properties[this.DatabaseFileProperty] = value.Trim();
					else
						this._properties.Reset(this.DatabaseFileProperty);
				}
			}

			private string DatabaseFileProperty
			{
				get
				{
					return
						(this._properties is SqlConnectionProperties) ? "AttachDbFilename" :
						                                                                   	(this._properties is OleDBConnectionProperties) ? "Initial File Name" :
						                                                                   	                                                                      	(this._properties is OdbcConnectionProperties) ? "AttachDBFileName" : null;
				}
			}

			public string DatabaseName
			{
				get
				{
					return this._properties[this.DatabaseNameProperty] as string;
				}
				set
				{
					if (value != null && value.Trim().Length > 0)
						this._properties[this.DatabaseNameProperty] = value.Trim();
					else
						this._properties.Reset(this.DatabaseNameProperty);
				}
			}

			private string DatabaseNameProperty
			{
				get
				{
					return
						(this._properties is SqlConnectionProperties) ? "Initial Catalog" :
						                                                                  	(this._properties is OleDBConnectionProperties) ? "Initial Catalog" :
						                                                                  	                                                                    	(this._properties is OdbcConnectionProperties) ? "DATABASE" : null;
				}
			}

			public string LogicalDatabaseName
			{
				get
				{
					return this.DatabaseName;
				}
				set
				{
					this.DatabaseName = value;
				}
			}

			public string Password
			{
				get
				{
					return this._properties[this.PasswordProperty] as string;
				}
				set
				{
					if (value != null && value.Length > 0)
						this._properties[this.PasswordProperty] = value;
					else
						this._properties.Reset(this.PasswordProperty);
				}
			}

			private string PasswordProperty
			{
				get
				{
					return
						(this._properties is SqlConnectionProperties) ? "Password" :
						                                                           	(this._properties is OleDBConnectionProperties) ? "Password" :
						                                                           	                                                             	(this._properties is OdbcConnectionProperties) ? "PWD" : null;
				}
			}

			public string Provider
			{
				get
				{
					if (this._properties is OleDBSqlConnectionProperties)
						return this._properties["Provider"] as string;
					return null;
				}
			}

			public bool SavePassword
			{
				get
				{
					if (this._properties is OdbcConnectionProperties)
						return false;
					return (bool)this._properties["Persist Security Info"];
				}
				set
				{
					Debug.Assert(!(this._properties is OdbcConnectionProperties));
					if (value)
						this._properties["Persist Security Info"] = value;
					else
						this._properties.Reset("Persist Security Info");
				}
			}

			public string ServerName
			{
				get
				{
					return this._properties[this.ServerNameProperty] as string;
				}
				set
				{
					if (value != null && value.Trim().Length > 0)
						this._properties[this.ServerNameProperty] = value.Trim();
					else
						this._properties.Reset(this.ServerNameProperty);
				}
			}

			private string ServerNameProperty
			{
				get
				{
					return
						(this._properties is SqlConnectionProperties) ? "Data Source" :
						                                                              	(this._properties is OleDBConnectionProperties) ? "Data Source" :
						                                                              	                                                                	(this._properties is OdbcConnectionProperties) ? "SERVER" : null;
				}
			}

			public bool UseWindowsAuthentication
			{
				get
				{
					if (this._properties is SqlConnectionProperties)
						return (bool)this._properties["Integrated Security"];
					if (this._properties is OleDBConnectionProperties)
					{
						return this._properties.Contains("Integrated Security") &&
						       this._properties["Integrated Security"] is string &&
						       (this._properties["Integrated Security"] as string).Equals("SSPI", StringComparison.OrdinalIgnoreCase);
					}
					if (this._properties is OdbcConnectionProperties)
					{
						return this._properties.Contains("Trusted_Connection") &&
						       this._properties["Trusted_Connection"] is string &&
						       (this._properties["Trusted_Connection"] as string).Equals("Yes", StringComparison.OrdinalIgnoreCase);
					}
					return false;
				}
				set
				{
					if (this._properties is SqlConnectionProperties)
					{
						if (value)
							this._properties["Integrated Security"] = value;
						else
							this._properties.Reset("Integrated Security");
					}
					if (this._properties is OleDBConnectionProperties)
					{
						if (value)
							this._properties["Integrated Security"] = "SSPI";
						else
							this._properties.Reset("Integrated Security");
					}
					if (this._properties is OdbcConnectionProperties)
					{
						if (value)
							this._properties["Trusted_Connection"] = "Yes";
						else
							this._properties.Remove("Trusted_Connection");
					}
				}
			}

			public bool UserInstance
			{
				get
				{
					if (this._properties is SqlConnectionProperties)
						return (bool)this._properties["User Instance"];
					return false;
				}
			}

			public string UserName
			{
				get
				{
					return this._properties[this.UserNameProperty] as string;
				}
				set
				{
					if (value != null && value.Trim().Length > 0)
						this._properties[this.UserNameProperty] = value.Trim();
					else
						this._properties.Reset(this.UserNameProperty);
				}
			}

			private string UserNameProperty
			{
				get
				{
					return
						(this._properties is SqlConnectionProperties) ? "User ID" :
						                                                          	(this._properties is OleDBConnectionProperties) ? "User ID" :
						                                                          	                                                            	(this._properties is OdbcConnectionProperties) ? "UID" : null;
				}
			}

			#endregion

			#region Methods/Operators

			public IDbConnection GetBasicConnection()
			{
				IDbConnection connection = null;

				string connectionString = String.Empty;
				if (this._properties is SqlConnectionProperties || this._properties is OleDBConnectionProperties)
				{
					if (this._properties is OleDBConnectionProperties)
						connectionString += "Provider=" + this._properties["Provider"].ToString() + ";";
					connectionString += "Data Source='" + this.ServerName.Replace("'", "''") + "';";
					if (this.UserInstance)
						connectionString += "User Instance=true;";
					if (this.UseWindowsAuthentication)
						connectionString += "Integrated Security=" + this._properties["Integrated Security"].ToString() + ";";
					else
					{
						connectionString += "User ID='" + this.UserName.Replace("'", "''") + "';";
						connectionString += "Password='" + this.Password.Replace("'", "''") + "';";
					}
					if (this._properties is SqlConnectionProperties)
						connectionString += "Pooling=False;";
				}
				if (this._properties is OdbcConnectionProperties)
				{
					connectionString += "DRIVER={SQL Server};";
					connectionString += "SERVER={" + this.ServerName.Replace("}", "}}") + "};";
					if (this.UseWindowsAuthentication)
						connectionString += "Trusted_Connection=Yes;";
					else
					{
						connectionString += "UID={" + this.UserName.Replace("}", "}}") + "};";
						connectionString += "PWD={" + this.Password.Replace("}", "}}") + "};";
					}
				}

				if (this._properties is SqlConnectionProperties)
					connection = new SqlConnection(connectionString);
				if (this._properties is OleDBConnectionProperties)
					connection = new OleDbConnection(connectionString);
				if (this._properties is OdbcConnectionProperties)
					connection = new OdbcConnection(connectionString);

				return connection;
			}

			#endregion
		}

		#endregion
	}
}