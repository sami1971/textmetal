//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	public partial class SqlFileConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		#region Constructors/Destructors

		public SqlFileConnectionUIControl()
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
		}

		#endregion

		#region Fields/Constants

		private IDataConnectionProperties _connectionProperties;
		private bool _loading;

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

		private void Browse(object sender, EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Title = Strings.SqlConnectionUIControl_BrowseFileTitle;
			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = false;
			fileDialog.RestoreDirectory = true;
			fileDialog.Filter = Strings.SqlConnectionUIControl_BrowseFileFilter;
			fileDialog.DefaultExt = Strings.SqlConnectionUIControl_BrowseFileDefaultExt;
			fileDialog.FileName = this.Properties["AttachDbFilename"] as string;
			if (this.Container != null)
				this.Container.Add(fileDialog);
			try
			{
				DialogResult result = fileDialog.ShowDialog(this.ParentForm);
				if (result == DialogResult.OK)
					this.databaseFileTextBox.Text = fileDialog.FileName.Trim();
			}
			finally
			{
				if (this.Container != null)
					this.Container.Remove(fileDialog);
				fileDialog.Dispose();
			}
		}

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			if (!(connectionProperties is SqlFileConnectionProperties))
				throw new ArgumentException(Strings.SqlFileConnectionUIControl_InvalidConnectionProperties);

			this._connectionProperties = connectionProperties;
		}

		public void LoadProperties()
		{
			this._loading = true;

			this.databaseFileTextBox.Text = this.Properties["AttachDbFilename"] as string;
			string myDocumentsDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			if (this.databaseFileTextBox.Text.StartsWith(myDocumentsDir, StringComparison.OrdinalIgnoreCase))
				this.databaseFileTextBox.Text = this.databaseFileTextBox.Text.Substring(myDocumentsDir.Length + 1);
			if ((bool)this.Properties["Integrated Security"])
				this.windowsAuthenticationRadioButton.Checked = true;
			else
				this.sqlAuthenticationRadioButton.Checked = true;
			this.userNameTextBox.Text = this.Properties["User ID"] as string;
			this.passwordTextBox.Text = this.Properties["Password"] as string;
			this.savePasswordCheckBox.Checked = (bool)this.Properties["Persist Security Info"];

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
				LayoutUtils.MirrorControl(this.databaseFileLabel, this.databaseFileTableLayoutPanel);
				LayoutUtils.MirrorControl(this.windowsAuthenticationRadioButton);
				LayoutUtils.MirrorControl(this.sqlAuthenticationRadioButton);
				LayoutUtils.MirrorControl(this.loginTableLayoutPanel);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.loginTableLayoutPanel);
				LayoutUtils.UnmirrorControl(this.sqlAuthenticationRadioButton);
				LayoutUtils.UnmirrorControl(this.windowsAuthenticationRadioButton);
				LayoutUtils.UnmirrorControl(this.databaseFileLabel, this.databaseFileTableLayoutPanel);
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

		private void SetAuthenticationOption(object sender, EventArgs e)
		{
			if (this.windowsAuthenticationRadioButton.Checked)
			{
				if (!this._loading)
				{
					this.Properties["Integrated Security"] = true;
					this.Properties.Reset("User ID");
					this.Properties.Reset("Password");
					this.Properties.Reset("Persist Security Info");
				}
				this.loginTableLayoutPanel.Enabled = false;
			}
			else /* if (sqlAuthenticationRadioButton.Checked) */
			{
				if (!this._loading)
				{
					this.Properties["Integrated Security"] = false;
					this.SetUserName(sender, e);
					this.SetPassword(sender, e);
					this.SetSavePassword(sender, e);
				}
				this.loginTableLayoutPanel.Enabled = true;
			}
		}

		private void SetDatabaseFile(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["AttachDbFilename"] = (this.databaseFileTextBox.Text.Trim().Length > 0) ? this.databaseFileTextBox.Text.Trim() : null;
		}

		private void SetPassword(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties["Password"] = (this.passwordTextBox.Text.Length > 0) ? this.passwordTextBox.Text : null;
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
		}

		private void SetSavePassword(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Persist Security Info"] = this.savePasswordCheckBox.Checked;
		}

		private void SetUserName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["User ID"] = (this.userNameTextBox.Text.Trim().Length > 0) ? this.userNameTextBox.Text.Trim() : null;
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
		}

		private void UpdateDatabaseFile(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				string attachDbFilename = (this.databaseFileTextBox.Text.Trim().Length > 0) ? this.databaseFileTextBox.Text.Trim() : null;
				if (attachDbFilename != null)
				{
					if (!attachDbFilename.EndsWith(".mdf", StringComparison.OrdinalIgnoreCase))
						attachDbFilename += ".mdf";
					try
					{
						if (!Path.IsPathRooted(attachDbFilename))
						{
							// Simulate a default directory as My Documents by appending this to the front
							attachDbFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), attachDbFilename);
						}
					}
					catch
					{
					}
				}
				this.Properties["AttachDbFilename"] = attachDbFilename;
			}
		}

		#endregion
	}
}