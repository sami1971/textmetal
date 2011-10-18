//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	internal partial class DataConnectionSourceDialog : Form
	{
		#region Constructors/Destructors

		public DataConnectionSourceDialog()
		{
			this.InitializeComponent();

			// Make sure we handle a user preference change
			if (this.components == null)
				this.components = new Container();
			this.components.Add(new UserPreferenceChangedHandler(this));
		}

		public DataConnectionSourceDialog(DataConnectionDialog mainDialog)
			: this()
		{
			Debug.Assert(mainDialog != null);

			this._mainDialog = mainDialog;
		}

		#endregion

		#region Fields/Constants

		private Label _headerLabel;
		private DataConnectionDialog _mainDialog;
		private Dictionary<DataSource, DataProvider> _providerSelections = new Dictionary<DataSource, DataProvider>();

		#endregion

		#region Properties/Indexers/Events

		public string HeaderLabel
		{
			get
			{
				return (this._headerLabel != null) ? this._headerLabel.Text : String.Empty;
			}
			set
			{
				if (this._headerLabel == null && (value == null || value.Length == 0))
					return;
				if (this._headerLabel != null && value == this._headerLabel.Text)
					return;
				if (value != null)
				{
					if (this._headerLabel == null)
					{
						this._headerLabel = new Label();
						this._headerLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
						this._headerLabel.FlatStyle = FlatStyle.System;
						this._headerLabel.Location = new Point(12, 12);
						this._headerLabel.Margin = new Padding(3);
						this._headerLabel.Name = "dataSourceLabel";
						this._headerLabel.Width = this.mainTableLayoutPanel.Width;
						this._headerLabel.TabIndex = 100;
						this.Controls.Add(this._headerLabel);
					}
					this._headerLabel.Text = value;
					this.MinimumSize = Size.Empty;
					this._headerLabel.Height = LayoutUtils.GetPreferredLabelHeight(this._headerLabel);
					int dy =
						this._headerLabel.Bottom +
						this._headerLabel.Margin.Bottom +
						this.mainTableLayoutPanel.Margin.Top -
						this.mainTableLayoutPanel.Top;
					this.mainTableLayoutPanel.Anchor &= ~AnchorStyles.Bottom;
					this.Height += dy;
					this.mainTableLayoutPanel.Anchor |= AnchorStyles.Bottom;
					this.mainTableLayoutPanel.Top += dy;
					this.MinimumSize = this.Size;
				}
				else
				{
					if (this._headerLabel != null)
					{
						int dy = this._headerLabel.Height;
						try
						{
							this.Controls.Remove(this._headerLabel);
						}
						finally
						{
							this._headerLabel.Dispose();
							this._headerLabel = null;
						}
						this.MinimumSize = Size.Empty;
						this.mainTableLayoutPanel.Top -= dy;
						this.mainTableLayoutPanel.Anchor &= ~AnchorStyles.Bottom;
						this.Height -= dy;
						this.mainTableLayoutPanel.Anchor |= AnchorStyles.Bottom;
						this.MinimumSize = this.Size;
					}
				}
			}
		}

		public string Title
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ChangeDataProvider(object sender, EventArgs e)
		{
			if (this.dataSourceListBox.SelectedItem != null)
				this._providerSelections[this.dataSourceListBox.SelectedItem as DataSource] = this.dataProviderComboBox.SelectedItem as DataProvider;
			this.ConfigureDescription();
			this.SetOkButtonStatus();
		}

		private void ChangeDataSource(object sender, EventArgs e)
		{
			DataSource newDataSource = this.dataSourceListBox.SelectedItem as DataSource;
			this.dataProviderComboBox.Items.Clear();
			if (newDataSource != null)
			{
				foreach (DataProvider dataProvider in newDataSource.Providers)
					this.dataProviderComboBox.Items.Add(dataProvider);
				if (!this._providerSelections.ContainsKey(newDataSource))
					this._providerSelections.Add(newDataSource, newDataSource.DefaultProvider);
				this.dataProviderComboBox.SelectedItem = this._providerSelections[newDataSource];
			}
			else
				this.dataProviderComboBox.Items.Add(String.Empty);
			this.ConfigureDescription();
			this.SetOkButtonStatus();
		}

		private void ConfigureDescription()
		{
			if (this.dataProviderComboBox.SelectedItem is DataProvider)
			{
				if (this.dataSourceListBox.SelectedItem == this._mainDialog.UnspecifiedDataSource)
					this.descriptionLabel.Text = (this.dataProviderComboBox.SelectedItem as DataProvider).Description;
				else
					this.descriptionLabel.Text = (this.dataProviderComboBox.SelectedItem as DataProvider).GetDescription(this.dataSourceListBox.SelectedItem as DataSource);
			}
			else
				this.descriptionLabel.Text = null;
		}

		private void DoOk(object sender, EventArgs e)
		{
			this._mainDialog.SetSelectedDataSourceInternal(this.dataSourceListBox.SelectedItem as DataSource);
			foreach (DataSource dataSource in this.dataSourceListBox.Items)
			{
				DataProvider selectedProvider = (this._providerSelections.ContainsKey(dataSource)) ? this._providerSelections[dataSource] : null;
				this._mainDialog.SetSelectedDataProviderInternal(dataSource, selectedProvider);
			}
		}

		private void FormatDataProvider(object sender, ListControlConvertEventArgs e)
		{
			if (e.DesiredType == typeof(string))
				e.Value = (e.ListItem is DataProvider) ? (e.ListItem as DataProvider).DisplayName : e.ListItem.ToString();
		}

		private void FormatDataSource(object sender, ListControlConvertEventArgs e)
		{
			if (e.DesiredType == typeof(string))
				e.Value = (e.ListItem as DataSource).DisplayName;
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			this.dataProviderComboBox.Top =
				this.leftPanel.Height -
				this.leftPanel.Padding.Bottom -
				this.dataProviderComboBox.Margin.Bottom -
				this.dataProviderComboBox.Height;
			this.dataProviderLabel.Top =
				this.dataProviderComboBox.Top -
				this.dataProviderComboBox.Margin.Top -
				this.dataProviderLabel.Margin.Bottom -
				this.dataProviderLabel.Height;

			int dx =
				(this.saveSelectionCheckBox.Right + this.saveSelectionCheckBox.Margin.Right) -
				(this.buttonsTableLayoutPanel.Left - this.buttonsTableLayoutPanel.Margin.Left);
			if (dx > 0)
			{
				this.Width += dx;
				this.MinimumSize = new Size(this.MinimumSize.Width + dx, this.MinimumSize.Height);
			}
			this.mainTableLayoutPanel.Anchor &= ~AnchorStyles.Bottom;
			this.saveSelectionCheckBox.Anchor &= ~AnchorStyles.Bottom;
			this.saveSelectionCheckBox.Anchor |= AnchorStyles.Top;
			this.buttonsTableLayoutPanel.Anchor &= ~AnchorStyles.Bottom;
			this.buttonsTableLayoutPanel.Anchor |= AnchorStyles.Top;
			int height =
				this.buttonsTableLayoutPanel.Top +
				this.buttonsTableLayoutPanel.Height +
				this.buttonsTableLayoutPanel.Margin.Bottom +
				this.Padding.Bottom;
			int dy = this.Height - this.SizeFromClientSize(new Size(0, height)).Height;
			this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height - dy);
			this.Height -= dy;
			this.buttonsTableLayoutPanel.Anchor &= ~AnchorStyles.Top;
			this.buttonsTableLayoutPanel.Anchor |= AnchorStyles.Bottom;
			this.saveSelectionCheckBox.Anchor &= ~AnchorStyles.Top;
			this.saveSelectionCheckBox.Anchor |= AnchorStyles.Bottom;
			this.mainTableLayoutPanel.Anchor |= AnchorStyles.Bottom;
		}

		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the active control
			Control activeControl = HelpUtils.GetActiveControl(this);

			// Figure out the context
			DataConnectionDialogContext context = DataConnectionDialogContext.Source;
			if (activeControl == this.dataSourceListBox)
				context = DataConnectionDialogContext.SourceListBox;
			if (activeControl == this.dataProviderComboBox)
				context = DataConnectionDialogContext.SourceProviderComboBox;
			if (activeControl == this.okButton)
				context = DataConnectionDialogContext.SourceOkButton;
			if (activeControl == this.cancelButton)
				context = DataConnectionDialogContext.SourceCancelButton;

			// Call OnContextHelpRequested
			ContextHelpEventArgs e = new ContextHelpEventArgs(context, hevent.MousePos);
			this._mainDialog.OnContextHelpRequested(e);
			hevent.Handled = e.Handled;
			if (!e.Handled)
				base.OnHelpRequested(hevent);
		}

		protected override void OnLoad(EventArgs e)
		{
			// If a main dialog was associated with this dialog, get its data sources
			if (this._mainDialog != null)
			{
				foreach (DataSource dataSource in this._mainDialog.DataSources)
				{
					if (dataSource == this._mainDialog.UnspecifiedDataSource)
						continue;
					this.dataSourceListBox.Items.Add(dataSource);
				}
				if (this._mainDialog.DataSources.Contains(this._mainDialog.UnspecifiedDataSource))
				{
					// We want to put the unspecified data source at the end of the list
					this.dataSourceListBox.Sorted = false;
					this.dataSourceListBox.Items.Add(this._mainDialog.UnspecifiedDataSource);
				}

				// Figure out the correct width for the data source list box and size dialog
				int dataSourceListBoxWidth = this.dataSourceListBox.Width - (this.dataSourceListBox.Width - this.dataSourceListBox.ClientSize.Width);
				foreach (object item in this.dataSourceListBox.Items)
				{
					Size size = TextRenderer.MeasureText((item as DataSource).DisplayName, this.dataSourceListBox.Font);
					size.Width += 3; // otherwise text is crammed up against right edge
					dataSourceListBoxWidth = Math.Max(dataSourceListBoxWidth, size.Width);
				}
				dataSourceListBoxWidth = dataSourceListBoxWidth + (this.dataSourceListBox.Width - this.dataSourceListBox.ClientSize.Width);
				dataSourceListBoxWidth = Math.Max(dataSourceListBoxWidth, this.dataSourceListBox.MinimumSize.Width);
				int dx = dataSourceListBoxWidth - this.dataSourceListBox.Size.Width;
				this.Width += dx * 2; // * 2 because the description group box resizes as well
				this.MinimumSize = this.Size;

				if (this._mainDialog.SelectedDataSource != null)
				{
					this.dataSourceListBox.SelectedItem = this._mainDialog.SelectedDataSource;
					if (this._mainDialog.SelectedDataProvider != null)
						this.dataProviderComboBox.SelectedItem = this._mainDialog.SelectedDataProvider;
				}

				// Configure the initial data provider selections
				foreach (DataSource dataSource in this.dataSourceListBox.Items)
				{
					DataProvider selectedProvider = this._mainDialog.GetSelectedDataProvider(dataSource);
					if (selectedProvider != null)
						this._providerSelections[dataSource] = selectedProvider;
				}
			}

			// Set the save selection check box
			this.saveSelectionCheckBox.Checked = this._mainDialog.SaveSelection;

			this.SetOkButtonStatus();

			base.OnLoad(e);
		}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			if (this.RightToLeftLayout == true &&
			    this.RightToLeft == RightToLeft.Yes)
			{
				LayoutUtils.MirrorControl(this.dataSourceLabel, this.dataSourceListBox);
				LayoutUtils.MirrorControl(this.dataProviderLabel, this.dataProviderComboBox);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.dataProviderLabel, this.dataProviderComboBox);
				LayoutUtils.UnmirrorControl(this.dataSourceLabel, this.dataSourceListBox);
			}
		}

		protected override void OnRightToLeftLayoutChanged(EventArgs e)
		{
			base.OnRightToLeftLayoutChanged(e);
			if (this.RightToLeftLayout == true &&
			    this.RightToLeft == RightToLeft.Yes)
			{
				LayoutUtils.MirrorControl(this.dataSourceLabel, this.dataSourceListBox);
				LayoutUtils.MirrorControl(this.dataProviderLabel, this.dataProviderComboBox);
			}
			else
			{
				LayoutUtils.UnmirrorControl(this.dataProviderLabel, this.dataProviderComboBox);
				LayoutUtils.UnmirrorControl(this.dataSourceLabel, this.dataSourceListBox);
			}
		}

		private void SelectDataSource(object sender, EventArgs e)
		{
			if (this.okButton.Enabled)
			{
				this.DialogResult = DialogResult.OK;
				this.DoOk(sender, e);
				this.Close();
			}
		}

		private void SetDataProviderDropDownWidth(object sender, EventArgs e)
		{
			if (this.dataProviderComboBox.Items.Count > 0 &&
			    !(this.dataProviderComboBox.Items[0] is string))
			{
				int largestWidth = 0;
				using (Graphics g = Graphics.FromHwnd(this.dataProviderComboBox.Handle))
				{
					foreach (DataProvider dataProvider in this.dataProviderComboBox.Items)
					{
						int width = TextRenderer.MeasureText(
							g,
							dataProvider.DisplayName,
							this.dataProviderComboBox.Font,
							new Size(Int32.MaxValue, Int32.MaxValue),
							TextFormatFlags.WordBreak
							).Width;
						if (width > largestWidth)
							largestWidth = width;
					}
				}
				this.dataProviderComboBox.DropDownWidth = largestWidth + 3; // give a little extra margin
				if (this.dataProviderComboBox.Items.Count > this.dataProviderComboBox.MaxDropDownItems)
					this.dataProviderComboBox.DropDownWidth += SystemInformation.VerticalScrollBarWidth;
			}
			else
				this.dataProviderComboBox.DropDownWidth = this.dataProviderComboBox.Width;
		}

		private void SetOkButtonStatus()
		{
			this.okButton.Enabled =
				this.dataSourceListBox.SelectedItem is DataSource &&
				this.dataProviderComboBox.SelectedItem is DataProvider;
		}

		private void SetSaveSelection(object sender, EventArgs e)
		{
			this._mainDialog.SaveSelection = this.saveSelectionCheckBox.Checked;
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