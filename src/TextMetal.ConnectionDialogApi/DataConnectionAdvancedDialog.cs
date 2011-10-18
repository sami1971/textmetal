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
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace TextMetal.ConnectionDialogApi
{
	internal partial class DataConnectionAdvancedDialog : Form
	{
		#region Constructors/Destructors

		public DataConnectionAdvancedDialog()
		{
			this.InitializeComponent();

			// Make sure we handle a user preference change
			if (this.components == null)
				this.components = new Container();
			this.components.Add(new UserPreferenceChangedHandler(this));
		}

		public DataConnectionAdvancedDialog(IDataConnectionProperties connectionProperties, DataConnectionDialog mainDialog)
			: this()
		{
			Debug.Assert(connectionProperties != null);
			Debug.Assert(mainDialog != null);

			this._savedConnectionString = connectionProperties.ToFullString();

			this.propertyGrid.SelectedObject = connectionProperties;

			this._mainDialog = mainDialog;
		}

		#endregion

		#region Fields/Constants

		private DataConnectionDialog _mainDialog;
		private string _savedConnectionString;

		#endregion

		#region Methods/Operators

		private void ConfigureTextBox()
		{
			if (this.propertyGrid.SelectedObject is IDataConnectionProperties)
			{
				try
				{
					this.textBox.Text = (this.propertyGrid.SelectedObject as IDataConnectionProperties).ToDisplayString();
				}
				catch
				{
					this.textBox.Text = null;
				}
			}
			else
				this.textBox.Text = null;
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			this.textBox.Width = this.propertyGrid.Width;
		}

		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the active control
			Control activeControl = this;
			ContainerControl containerControl = null;
			while ((containerControl = activeControl as ContainerControl) != null &&
			       containerControl != this.propertyGrid &&
			       containerControl.ActiveControl != null)
				activeControl = containerControl.ActiveControl;

			// Figure out the context
			DataConnectionDialogContext context = DataConnectionDialogContext.Advanced;
			if (activeControl == this.propertyGrid)
				context = DataConnectionDialogContext.AdvancedPropertyGrid;
			if (activeControl == this.textBox)
				context = DataConnectionDialogContext.AdvancedTextBox;
			if (activeControl == this.okButton)
				context = DataConnectionDialogContext.AdvancedOkButton;
			if (activeControl == this.cancelButton)
				context = DataConnectionDialogContext.AdvancedCancelButton;

			// Call OnContextHelpRequested
			ContextHelpEventArgs e = new ContextHelpEventArgs(context, hevent.MousePos);
			this._mainDialog.OnContextHelpRequested(e);
			hevent.Handled = e.Handled;
			if (!e.Handled)
				base.OnHelpRequested(hevent);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ConfigureTextBox();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.propertyGrid.Focus();
		}

		private void RevertProperties(object sender, EventArgs e)
		{
			try
			{
				(this.propertyGrid.SelectedObject as IDataConnectionProperties).Parse(this._savedConnectionString);
			}
			catch
			{
			}
		}

		private void SetTextBox(object s, PropertyValueChangedEventArgs e)
		{
			this.ConfigureTextBox();
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

		#region Classes/Structs/Interfaces/Enums/Delegates

		internal class SpecializedPropertyGrid : PropertyGrid
		{
			#region Constructors/Destructors

			public SpecializedPropertyGrid()
			{
				this._contextMenu = new ContextMenuStrip();

				this._contextMenu.Items.AddRange(new ToolStripItem[]
				                                 {
				                                 	new ToolStripMenuItem(),
				                                 	new ToolStripSeparator(),
				                                 	new ToolStripMenuItem(),
				                                 	new ToolStripMenuItem(),
				                                 	new ToolStripSeparator(),
				                                 	new ToolStripMenuItem()
				                                 });
				this._contextMenu.Items[0].Text = Strings.DataConnectionAdvancedDialog_Reset;
				this._contextMenu.Items[0].Click += new EventHandler(this.ResetProperty);
				this._contextMenu.Items[2].Text = Strings.DataConnectionAdvancedDialog_Add;
				this._contextMenu.Items[2].Click += new EventHandler(this.AddProperty);
				this._contextMenu.Items[3].Text = Strings.DataConnectionAdvancedDialog_Remove;
				this._contextMenu.Items[3].Click += new EventHandler(this.RemoveProperty);
				this._contextMenu.Items[5].Text = Strings.DataConnectionAdvancedDialog_Description;
				this._contextMenu.Items[5].Click += new EventHandler(this.ToggleDescription);
				(this._contextMenu.Items[5] as ToolStripMenuItem).Checked = this.HelpVisible;
				this._contextMenu.Opened += new EventHandler(this.SetupContextMenu);

				this.ContextMenuStrip = this._contextMenu;
				this.DrawFlatToolbar = true;
				this.Size = new Size(270, 250); // magic numbers, but a reasonable starting point
				this.MinimumSize = this.Size;
			}

			#endregion

			#region Fields/Constants

			private ContextMenuStrip _contextMenu;

			#endregion

			#region Methods/Operators

			private void AddProperty(object sender, EventArgs e)
			{
				DataConnectionDialog mainDialog = this.ParentForm as DataConnectionDialog;
				if (mainDialog == null)
				{
					Debug.Assert(this.ParentForm is DataConnectionAdvancedDialog);
					mainDialog = (this.ParentForm as DataConnectionAdvancedDialog)._mainDialog;
					Debug.Assert(mainDialog != null);
				}
				AddPropertyDialog dialog = new AddPropertyDialog(mainDialog);
				try
				{
					if (this.ParentForm.Container != null)
						this.ParentForm.Container.Add(dialog);
					DialogResult result = dialog.ShowDialog(this.ParentForm);
					if (result == DialogResult.OK)
					{
						(this.SelectedObject as IDataConnectionProperties).Add(dialog.PropertyName);
						this.Refresh();
						GridItem rootItem = this.SelectedGridItem;
						while (rootItem.Parent != null)
							rootItem = rootItem.Parent;
						GridItem newItem = this.LocateGridItem(rootItem, dialog.PropertyName);
						if (newItem != null)
							this.SelectedGridItem = newItem;
					}
				}
				finally
				{
					if (this.ParentForm.Container != null)
						this.ParentForm.Container.Remove(dialog);
					dialog.Dispose();
				}
			}

			private GridItem LocateGridItem(GridItem currentItem, string propertyName)
			{
				if (currentItem.GridItemType == GridItemType.Property &&
				    currentItem.Label.Equals(propertyName, StringComparison.CurrentCulture))
					return currentItem;

				GridItem foundItem = null;
				foreach (GridItem childItem in currentItem.GridItems)
				{
					foundItem = this.LocateGridItem(childItem, propertyName);
					if (foundItem != null)
						break;
				}

				return foundItem;
			}

			protected override void OnFontChanged(EventArgs e)
			{
				base.OnFontChanged(e);
				this.LargeButtons = (this.Font.SizeInPoints >= 15.0);
			}

			protected override void OnHandleCreated(EventArgs e)
			{
				ProfessionalColorTable colorTable = (this.ParentForm != null && this.ParentForm.Site != null) ? this.ParentForm.Site.GetService(typeof(ProfessionalColorTable)) as ProfessionalColorTable : null;
				if (colorTable != null)
					this.ToolStripRenderer = new ToolStripProfessionalRenderer(colorTable);
				base.OnHandleCreated(e);
			}

			private void RemoveProperty(object sender, EventArgs e)
			{
				(this.SelectedObject as IDataConnectionProperties).Remove(this.SelectedGridItem.Label);
				this.Refresh();
				this.OnPropertyValueChanged(new PropertyValueChangedEventArgs(null, null));
			}

			private void ResetProperty(object sender, EventArgs e)
			{
				object oldValue = this.SelectedGridItem.Value;
				object propertyOwner = this.SelectedObject;
				if (this.SelectedObject is ICustomTypeDescriptor)
					propertyOwner = (this.SelectedObject as ICustomTypeDescriptor).GetPropertyOwner(this.SelectedGridItem.PropertyDescriptor);
				this.SelectedGridItem.PropertyDescriptor.ResetValue(propertyOwner);
				this.Refresh();
				this.OnPropertyValueChanged(new PropertyValueChangedEventArgs(this.SelectedGridItem, oldValue));
			}

			private void SetupContextMenu(object sender, EventArgs e)
			{
				// Decide if reset should be enabled
				this._contextMenu.Items[0].Enabled = (this.SelectedGridItem.GridItemType == GridItemType.Property);
				if (this._contextMenu.Items[0].Enabled && this.SelectedGridItem.PropertyDescriptor != null)
				{
					object propertyOwner = this.SelectedObject;
					if (this.SelectedObject is ICustomTypeDescriptor)
						propertyOwner = (this.SelectedObject as ICustomTypeDescriptor).GetPropertyOwner(this.SelectedGridItem.PropertyDescriptor);
					this._contextMenu.Items[0].Enabled = this._contextMenu.Items[3].Enabled = this.SelectedGridItem.PropertyDescriptor.CanResetValue(propertyOwner);
				}

				// Decide if we are allowed to add/remove custom properties
				this._contextMenu.Items[2].Visible = this._contextMenu.Items[3].Visible = (this.SelectedObject as IDataConnectionProperties).IsExtensible;
				if (this._contextMenu.Items[3].Visible)
				{
					this._contextMenu.Items[3].Enabled = (this.SelectedGridItem.GridItemType == GridItemType.Property);
					if (this._contextMenu.Items[3].Enabled && this.SelectedGridItem.PropertyDescriptor != null)
						this._contextMenu.Items[3].Enabled = !this.SelectedGridItem.PropertyDescriptor.IsReadOnly;
				}

				// Hide the first separator if there is no need for it
				this._contextMenu.Items[1].Visible = (this._contextMenu.Items[2].Visible || this._contextMenu.Items[3].Visible);
			}

			private void ToggleDescription(object sender, EventArgs e)
			{
				this.HelpVisible = !this.HelpVisible;
				(this._contextMenu.Items[5] as ToolStripMenuItem).Checked = !(this._contextMenu.Items[5] as ToolStripMenuItem).Checked;
			}

			protected override void WndProc(ref Message m)
			{
				switch (m.Msg)
				{
					case NativeMethods.WM_SETFOCUS:
						// Make sure the property grid view has proper focus
						this.Focus();
						((IComPropertyBrowser)this).HandleF4();
						break;
				}
				base.WndProc(ref m);
			}

			#endregion
		}

		#endregion
	}
}