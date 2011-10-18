//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	internal partial class AddPropertyDialog : Form
	{
		#region Constructors/Destructors

		public AddPropertyDialog()
		{
			this.InitializeComponent();

			// Make sure we handle a user preference change
			if (this.components == null)
				this.components = new Container();
			this.components.Add(new UserPreferenceChangedHandler(this));
		}

		public AddPropertyDialog(DataConnectionDialog mainDialog)
			: this()
		{
			Debug.Assert(mainDialog != null);

			this._mainDialog = mainDialog;
		}

		#endregion

		#region Fields/Constants

		private DataConnectionDialog _mainDialog;

		#endregion

		#region Properties/Indexers/Events

		public string PropertyName
		{
			get
			{
				return this.propertyTextBox.Text;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			this.propertyTextBox.Width =
				this.buttonsTableLayoutPanel.Right -
				this.propertyTextBox.Left;

			// Resize the dialog appropriately so that OK/Cancel buttons fit
			int preferredClientWidth =
				this.Padding.Left +
				this.buttonsTableLayoutPanel.Margin.Left +
				this.buttonsTableLayoutPanel.Width +
				this.buttonsTableLayoutPanel.Margin.Right +
				this.Padding.Right;
			if (this.ClientSize.Width < preferredClientWidth)
				this.ClientSize = new Size(preferredClientWidth, this.ClientSize.Height);
		}

		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the active control
			Control activeControl = HelpUtils.GetActiveControl(this);

			// Figure out the context
			DataConnectionDialogContext context = DataConnectionDialogContext.AddProperty;
			if (activeControl == this.propertyTextBox)
				context = DataConnectionDialogContext.AddPropertyTextBox;
			if (activeControl == this.okButton)
				context = DataConnectionDialogContext.AddPropertyOkButton;
			if (activeControl == this.cancelButton)
				context = DataConnectionDialogContext.AddPropertyCancelButton;

			// Call OnContextHelpRequested
			ContextHelpEventArgs e = new ContextHelpEventArgs(context, hevent.MousePos);
			this._mainDialog.OnContextHelpRequested(e);
			hevent.Handled = e.Handled;
			if (!e.Handled)
				base.OnHelpRequested(hevent);
		}

		private void SetOkButtonStatus(object sender, EventArgs e)
		{
			this.okButton.Enabled = (this.propertyTextBox.Text.Trim().Length > 0);
		}

		protected override void WndProc(ref Message m)
		{
			if (this._mainDialog.TranslateHelpButton && HelpUtils.IsContextHelpMessage(ref m))
			{
				// Force the ? in the title bar to invoke the help topic
				HelpUtils.TranslateContextHelpMessage(this, ref m);
			}
			base.WndProc(ref m);
		}

		#endregion
	}
}