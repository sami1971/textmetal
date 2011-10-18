//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	public partial class OracleConnectionUIControl : UserControl, IDataConnectionUIControl
	{
		#region Constructors/Destructors

		public OracleConnectionUIControl()
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

		private string PasswordProperty
		{
			get
			{
				if (!(this.Properties is OdbcConnectionProperties))
					return "Password";
				else
					return "PWD";
			}
		}

		private IDataConnectionProperties Properties
		{
			get
			{
				return this._connectionProperties;
			}
		}

		private string ServerProperty
		{
			get
			{
				if (!(this.Properties is OdbcConnectionProperties))
					return "Data Source";
				else
					return "SERVER";
			}
		}

		private string UserNameProperty
		{
			get
			{
				if (!(this.Properties is OdbcConnectionProperties))
					return "User ID";
				else
					return "UID";
			}
		}

		#endregion

		#region Methods/Operators

		public void Initialize(IDataConnectionProperties connectionProperties)
		{
			if (connectionProperties == null)
				throw new ArgumentNullException("connectionProperties");

			if (!(connectionProperties is OracleConnectionProperties) &&
			    !(connectionProperties is OleDBOracleConnectionProperties))
				throw new ArgumentException(Strings.OracleConnectionUIControl_InvalidConnectionProperties);

			if (connectionProperties is OdbcConnectionProperties)
			{
				// ODBC does not support saving the password
				this.savePasswordCheckBox.Enabled = false;
			}

			this._connectionProperties = connectionProperties;
		}

		public void LoadProperties()
		{
			this._loading = true;

			this.serverTextBox.Text = this.Properties[this.ServerProperty] as string;
			this.userNameTextBox.Text = this.Properties[this.UserNameProperty] as string;
			this.passwordTextBox.Text = this.Properties[this.PasswordProperty] as string;
			if (!(this.Properties is OdbcConnectionProperties))
				this.savePasswordCheckBox.Checked = (bool)this.Properties["Persist Security Info"];
			else
				this.savePasswordCheckBox.Checked = false;

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
				LayoutUtils.MirrorControl(this.serverLabel, this.serverTextBox);
			else
				LayoutUtils.UnmirrorControl(this.serverLabel, this.serverTextBox);
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

		private void SetPassword(object sender, EventArgs e)
		{
			if (!this._loading)
			{
				this.Properties[this.PasswordProperty] = (this.passwordTextBox.Text.Length > 0) ? this.passwordTextBox.Text : null;
				this.passwordTextBox.Text = this.passwordTextBox.Text; // forces reselection of all text
			}
		}

		private void SetSavePassword(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties["Persist Security Info"] = this.savePasswordCheckBox.Checked;
		}

		private void SetServer(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties[this.ServerProperty] = (this.serverTextBox.Text.Trim().Length > 0) ? this.serverTextBox.Text.Trim() : null;
		}

		private void SetUserName(object sender, EventArgs e)
		{
			if (!this._loading)
				this.Properties[this.UserNameProperty] = (this.userNameTextBox.Text.Trim().Length > 0) ? this.userNameTextBox.Text.Trim() : null;
		}

		private void TrimControlText(object sender, EventArgs e)
		{
			Control c = sender as Control;
			c.Text = c.Text.Trim();
		}

		#endregion
	}
}