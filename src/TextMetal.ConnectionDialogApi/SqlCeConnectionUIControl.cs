//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Forms;

using Microsoft.SqlServerCe.Client;
using Microsoft.Win32;

namespace TextMetal.ConnectionDialogApi
{
	/// <summary>
	/// Represents the connection UI control for the SQL Server Compact provider.
	/// </summary>
	internal partial class SqlCeConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		#region Constructors/Destructors

		public SqlCeConnectionUIControl()
		{
			this.InitializeComponent();
			this.RightToLeft = RightToLeft.Inherit;

			// Disable the active sync radio button for standalone connection dialog.
			this.activeSyncRadioButton.Enabled = false;
			this.createButton.Enabled = false;
		}

		#endregion

		#region Fields/Constants

		private bool _loading;

		private SqlCeConnectionProperties _properties;

		#endregion

		#region Properties/Indexers/Events

		private static string InitialDirectory
		{
			get
			{
				string path = null;
				RegistryKey sqlCEBaseRegKey = Registry.LocalMachine.OpenSubKey(
					@"SOFTWARE\Microsoft\Microsoft SQL Server Compact Edition\v3.5");
				if (sqlCEBaseRegKey != null)
				{
					using (sqlCEBaseRegKey)
					{
						path = sqlCEBaseRegKey.GetValue("InstallDir") as string;
						if (path != null)
							path = Path.Combine(path, "Samples");
					}
				}
				return path;
			}
		}

		public static string MobileDevicePrefix
		{
			get
			{
				return ConStringUtil.MobileDevicePrefix + @"\";
			}
		}

		private string DataSourceProperty
		{
			get
			{
				return "Data Source";
			}
		}

		private string PasswordProperty
		{
			get
			{
				return "Password";
			}
		}

		public string PersistSecurityInfoProperty
		{
			get
			{
				return "Persist Security Info";
			}
		}

		#endregion

		#region Methods/Operators

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			if (connectionProperties == null)
				throw new ArgumentNullException("connectionProperties");
			SqlCeConnectionProperties properties = connectionProperties as SqlCeConnectionProperties;
			if (properties == null)
				throw new ArgumentException(Resources.SqlCeConnectionUIControl_InvalidConnectionProperties);
			this._properties = properties;
		}

		public void LoadProperties()
		{
			this._loading = true;

			string dataSource = this._properties[this.DataSourceProperty] as string;
			this.myComputerRadioButton.Checked = true;
			this.databaseTextBox.Text = dataSource;
			this.passwordTextBox.Text = this._properties[this.PasswordProperty] as string;
			this.savePasswordCheckBox.Checked = (bool)this._properties[this.PersistSecurityInfoProperty];

			this._loading = false;
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
		}

		private void activeSyncRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.databaseTextBox_TextChanged(sender, e);
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			if (this.myComputerRadioButton.Checked)
			{
				//
				// We're exploring the desktop, let's use an OpenFileDialog
				//
				using (OpenFileDialog fileDialog = new OpenFileDialog())
				{
					fileDialog.Title = Resources.SqlConnectionUIControl_BrowseFileTitle;
					fileDialog.Multiselect = false;
					if (String.IsNullOrEmpty(this._properties[this.DataSourceProperty] as string))
						fileDialog.InitialDirectory = InitialDirectory;
					fileDialog.RestoreDirectory = true;
					fileDialog.Filter = Resources.SqlConnectionUIControl_BrowseFileFilter;
					fileDialog.DefaultExt = Resources.SqlConnectionUIControl_BrowseFileDefaultExt;
					if (fileDialog.ShowDialog() == DialogResult.OK)
					{
						this._properties[this.DataSourceProperty] = fileDialog.FileName.Trim();
						this.LoadProperties();
					}
				}
			}
		}

		private void databaseTextBox_TextChanged(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				string dataSource = this.databaseTextBox.Text.Trim();
				if (this.activeSyncRadioButton.Checked)
					dataSource = Path.Combine(MobileDevicePrefix, dataSource);
				if (dataSource.Length == 0)
					dataSource = null;
				this._properties[this.DataSourceProperty] = dataSource;
			}
		}

		private void myComputerRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			this.databaseTextBox_TextChanged(sender, e);
		}

		private void passwordTextBox_TextChanged(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this._properties[this.PasswordProperty] = (this.passwordTextBox.Text.Length > 0) ? this.passwordTextBox.Text : null;
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
		}

		private void savePasswordCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!this._loading)
				this._properties[this.PersistSecurityInfoProperty] = this.savePasswordCheckBox.Checked;
		}

		#endregion
	}
}