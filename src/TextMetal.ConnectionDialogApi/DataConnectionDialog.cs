//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TextMetal.ConnectionDialogApi
{
	public partial class DataConnectionDialog : Form
	{
		#region Constructors/Destructors

		public DataConnectionDialog()
		{
			this.InitializeComponent();
			this.dataSourceTextBox.Width = 0;

			// Make sure we handle a user preference change
			this.components.Add(new UserPreferenceChangedHandler(this));

			// Configure initial label values
			ComponentResourceManager resources = new ComponentResourceManager(typeof(DataConnectionSourceDialog));
			this._chooseDataSourceTitle = resources.GetString("$this.Text");
			this._chooseDataSourceAcceptText = resources.GetString("okButton.Text");
			this._changeDataSourceTitle = Strings.DataConnectionDialog_ChangeDataSourceTitle;

			this._dataSources = new DataSourceCollection(this);
		}

		#endregion

		#region Fields/Constants

		private string _changeDataSourceHeaderLabel = String.Empty;
		private string _changeDataSourceTitle;
		private string _chooseDataSourceAcceptText;
		private string _chooseDataSourceHeaderLabel = String.Empty;
		private string _chooseDataSourceTitle;
		private IDictionary<DataSource, IDictionary<DataProvider, IDataConnectionProperties>> _connectionPropertiesTable = new Dictionary<DataSource, IDictionary<DataProvider, IDataConnectionProperties>>();
		private IDictionary<DataSource, IDictionary<DataProvider, IDataConnectionUIControl>> _connectionUIControlTable = new Dictionary<DataSource, IDictionary<DataProvider, IDataConnectionUIControl>>();
		private IDictionary<DataSource, DataProvider> _dataProviderSelections = new Dictionary<DataSource, DataProvider>();
		private ICollection<DataSource> _dataSources;
		private Label _headerLabel;
		private Size _initialContainerControlSize;
		private bool _saveSelection = true;
		private DataSource _selectedDataSource;
		private bool _showingDialog;
		private bool _translateHelpButton = true;
		private DataSource _unspecifiedDataSource = DataSource.CreateUnspecified();

		#endregion

		#region Properties/Indexers/Events

		public event EventHandler<ContextHelpEventArgs> ContextHelpRequested;

		public event ThreadExceptionEventHandler DialogException;
		public event EventHandler VerifySettings;

		public string AcceptButtonText
		{
			get
			{
				return this.acceptButton.Text;
			}
			set
			{
				this.acceptButton.Text = value;
			}
		}

		public string ChangeDataSourceHeaderLabel
		{
			get
			{
				return this._changeDataSourceHeaderLabel;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (value == null)
					value = String.Empty;
				if (value == this._changeDataSourceHeaderLabel)
					return;
				this._changeDataSourceHeaderLabel = value;
			}
		}

		public string ChangeDataSourceTitle
		{
			get
			{
				return this._changeDataSourceTitle;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (value == null)
					value = String.Empty;
				if (value == this._changeDataSourceTitle)
					return;
				this._changeDataSourceTitle = value;
			}
		}

		public string ChooseDataSourceAcceptText
		{
			get
			{
				return this._chooseDataSourceAcceptText;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (value == null)
					value = String.Empty;
				if (value == this._chooseDataSourceAcceptText)
					return;
				this._chooseDataSourceAcceptText = value;
			}
		}

		public string ChooseDataSourceHeaderLabel
		{
			get
			{
				return this._chooseDataSourceHeaderLabel;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (value == null)
					value = String.Empty;
				if (value == this._chooseDataSourceHeaderLabel)
					return;
				this._chooseDataSourceHeaderLabel = value;
			}
		}

		public string ChooseDataSourceTitle
		{
			get
			{
				return this._chooseDataSourceTitle;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (value == null)
					value = String.Empty;
				if (value == this._chooseDataSourceTitle)
					return;
				this._chooseDataSourceTitle = value;
			}
		}

		internal IDataConnectionProperties ConnectionProperties
		{
			get
			{
				if (this.SelectedDataProvider == null)
					return null;
				if (!this._connectionPropertiesTable.ContainsKey(this.SelectedDataSource))
					this._connectionPropertiesTable[this.SelectedDataSource] = new Dictionary<DataProvider, IDataConnectionProperties>();
				if (!this._connectionPropertiesTable[this.SelectedDataSource].ContainsKey(this.SelectedDataProvider))
				{
					IDataConnectionProperties properties = null;
					if (this.SelectedDataSource == this.UnspecifiedDataSource)
						properties = this.SelectedDataProvider.CreateConnectionProperties();
					else
						properties = this.SelectedDataProvider.CreateConnectionProperties(this.SelectedDataSource);
					if (properties == null)
						properties = new BasicConnectionProperties();
					properties.PropertyChanged += new EventHandler(this.ConfigureAcceptButton);
					this._connectionPropertiesTable[this.SelectedDataSource][this.SelectedDataProvider] = properties;
				}
				return this._connectionPropertiesTable[this.SelectedDataSource][this.SelectedDataProvider];
			}
		}

		public string ConnectionString
		{
			get
			{
				string s = null;
				if (this.ConnectionProperties != null)
				{
					try
					{
						s = this.ConnectionProperties.ToString();
					}
					catch
					{
					}
				}
				return (s != null) ? s : String.Empty;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (this.SelectedDataProvider == null)
					throw new InvalidOperationException(Strings.DataConnectionDialog_NoDataProviderSelected);
				Debug.Assert(this.ConnectionProperties != null);
				if (this.ConnectionProperties != null)
					this.ConnectionProperties.Parse(value);
			}
		}

		internal UserControl ConnectionUIControl
		{
			get
			{
				if (this.SelectedDataProvider == null)
					return null;
				if (!this._connectionUIControlTable.ContainsKey(this.SelectedDataSource))
					this._connectionUIControlTable[this.SelectedDataSource] = new Dictionary<DataProvider, IDataConnectionUIControl>();
				if (!this._connectionUIControlTable[this.SelectedDataSource].ContainsKey(this.SelectedDataProvider))
				{
					IDataConnectionUIControl uiControl = null;
					UserControl control = null;
					try
					{
						if (this.SelectedDataSource == this.UnspecifiedDataSource)
							uiControl = this.SelectedDataProvider.CreateConnectionUIControl();
						else
							uiControl = this.SelectedDataProvider.CreateConnectionUIControl(this.SelectedDataSource);
						control = uiControl as UserControl;
						if (control == null)
						{
							IContainerControl ctControl = uiControl as IContainerControl;
							if (ctControl != null)
								control = ctControl.ActiveControl as UserControl;
						}
					}
					catch
					{
					}
					if (uiControl == null || control == null)
					{
						uiControl = new PropertyGridUIControl();
						control = uiControl as UserControl;
					}
					control.Location = Point.Empty;
					control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
					control.AutoSize = false;
					try
					{
						uiControl.Initialize(this.ConnectionProperties);
					}
					catch
					{
					}
					this._connectionUIControlTable[this.SelectedDataSource][this.SelectedDataProvider] = uiControl;
					this.components.Add(control); // so that it is disposed when the form is disposed
				}
				UserControl result = this._connectionUIControlTable[this.SelectedDataSource][this.SelectedDataProvider] as UserControl;
				if (result == null)
					result = (this._connectionUIControlTable[this.SelectedDataSource][this.SelectedDataProvider] as IContainerControl).ActiveControl as UserControl;
				return result;
			}
		}

		public ICollection<DataSource> DataSources
		{
			get
			{
				return this._dataSources;
			}
		}

		public string DisplayConnectionString
		{
			get
			{
				string s = null;
				if (this.ConnectionProperties != null)
				{
					try
					{
						s = this.ConnectionProperties.ToDisplayString();
					}
					catch
					{
					}
				}
				return (s != null) ? s : String.Empty;
			}
		}

		public string HeaderLabel
		{
			get
			{
				return (this._headerLabel != null) ? this._headerLabel.Text : String.Empty;
			}
			set
			{
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (this._headerLabel == null && (value == null || value.Length == 0))
					return;
				if (this._headerLabel != null && value == this._headerLabel.Text)
					return;
				if (value != null && value.Length > 0)
				{
					if (this._headerLabel == null)
					{
						this._headerLabel = new Label();
						this._headerLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
						this._headerLabel.FlatStyle = FlatStyle.System;
						this._headerLabel.Location = new Point(12, 12);
						this._headerLabel.Margin = new Padding(3);
						this._headerLabel.Name = "dataSourceLabel";
						this._headerLabel.Width = this.dataSourceTableLayoutPanel.Width;
						this._headerLabel.TabIndex = 100;
						this.Controls.Add(this._headerLabel);
					}
					this._headerLabel.Text = value;
					this.MinimumSize = Size.Empty;
					this._headerLabel.Height = LayoutUtils.GetPreferredLabelHeight(this._headerLabel);
					int dy =
						this._headerLabel.Bottom +
						this._headerLabel.Margin.Bottom +
						this.dataSourceLabel.Margin.Top -
						this.dataSourceLabel.Top;
					this.containerControl.Anchor &= ~AnchorStyles.Bottom;
					this.Height += dy;
					this.containerControl.Anchor |= AnchorStyles.Bottom;
					this.containerControl.Top += dy;
					this.dataSourceTableLayoutPanel.Top += dy;
					this.dataSourceLabel.Top += dy;
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
						this.dataSourceLabel.Top -= dy;
						this.dataSourceTableLayoutPanel.Top -= dy;
						this.containerControl.Top -= dy;
						this.containerControl.Anchor &= ~AnchorStyles.Bottom;
						this.Height -= dy;
						this.containerControl.Anchor |= AnchorStyles.Bottom;
						this.MinimumSize = this.Size;
					}
				}
			}
		}

		public bool SaveSelection
		{
			get
			{
				return this._saveSelection;
			}
			set
			{
				this._saveSelection = value;
			}
		}

		public DataProvider SelectedDataProvider
		{
			get
			{
				return this.GetSelectedDataProvider(this.SelectedDataSource);
			}
			set
			{
				if (this.SelectedDataProvider != value)
				{
					if (this.SelectedDataSource == null)
						throw new InvalidOperationException(Strings.DataConnectionDialog_NoDataSourceSelected);
					this.SetSelectedDataProvider(this.SelectedDataSource, value);
				}
			}
		}

		public DataSource SelectedDataSource
		{
			get
			{
				if (this._dataSources == null)
					return null;
				switch (this._dataSources.Count)
				{
					case 0:
						Debug.Assert(this._selectedDataSource == null);
						return null;
					case 1:
						// If there is only one data source, it must be selected
						IEnumerator<DataSource> e = this._dataSources.GetEnumerator();
						e.MoveNext();
						return e.Current;
					default:
						return this._selectedDataSource;
				}
			}
			set
			{
				if (this.SelectedDataSource != value)
				{
					if (this._showingDialog)
						throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
					this.SetSelectedDataSource(value, false);
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

		public bool TranslateHelpButton
		{
			get
			{
				return this._translateHelpButton;
			}
			set
			{
				this._translateHelpButton = value;
			}
		}

		public DataSource UnspecifiedDataSource
		{
			get
			{
				return this._unspecifiedDataSource;
			}
		}

		#endregion

		#region Methods/Operators

		public static DialogResult Show(DataConnectionDialog dialog)
		{
			return Show(dialog, null);
		}

		public static DialogResult Show(DataConnectionDialog dialog, IWin32Window owner)
		{
			if (dialog == null)
				throw new ArgumentNullException("dialog");
			if (dialog.DataSources.Count == 0)
				throw new InvalidOperationException(Strings.DataConnectionDialog_NoDataSourcesAvailable);
			foreach (DataSource dataSource in dialog.DataSources)
			{
				if (dataSource.Providers.Count == 0)
					throw new InvalidOperationException(String.Format(Strings.DataConnectionDialog_NoDataProvidersForDataSource + dataSource.DisplayName.Replace("'", "''")));
			}

			Application.ThreadException += new ThreadExceptionEventHandler(dialog.HandleDialogException);
			dialog._showingDialog = true;
			try
			{
				// If there is no selected data source or provider, show the data connection source dialog
				if (dialog.SelectedDataSource == null || dialog.SelectedDataProvider == null)
				{
					DataConnectionSourceDialog sourceDialog = new DataConnectionSourceDialog(dialog);
					sourceDialog.Title = dialog.ChooseDataSourceTitle;
					sourceDialog.HeaderLabel = dialog.ChooseDataSourceHeaderLabel;
					(sourceDialog.AcceptButton as Button).Text = dialog.ChooseDataSourceAcceptText;
					if (dialog.Container != null)
						dialog.Container.Add(sourceDialog);
					try
					{
						if (owner == null)
							sourceDialog.StartPosition = FormStartPosition.CenterScreen;
						sourceDialog.ShowDialog(owner);
						if (dialog.SelectedDataSource == null || dialog.SelectedDataProvider == null)
							return DialogResult.Cancel;
					}
					finally
					{
						if (dialog.Container != null)
							dialog.Container.Remove(sourceDialog);
						sourceDialog.Dispose();
					}
				}
				else
					dialog._saveSelection = false;
				if (owner == null)
					dialog.StartPosition = FormStartPosition.CenterScreen;
				for (;;)
				{
					DialogResult result = dialog.ShowDialog(owner);
					if (result == DialogResult.Ignore)
					{
						DataConnectionSourceDialog sourceDialog = new DataConnectionSourceDialog(dialog);
						sourceDialog.Title = dialog.ChangeDataSourceTitle;
						sourceDialog.HeaderLabel = dialog.ChangeDataSourceHeaderLabel;
						if (dialog.Container != null)
							dialog.Container.Add(sourceDialog);
						try
						{
							if (owner == null)
								sourceDialog.StartPosition = FormStartPosition.CenterScreen;
							result = sourceDialog.ShowDialog(owner);
						}
						finally
						{
							if (dialog.Container != null)
								dialog.Container.Remove(sourceDialog);
							sourceDialog.Dispose();
						}
					}
					else
						return result;
				}
			}
			finally
			{
				dialog._showingDialog = false;
				Application.ThreadException -= new ThreadExceptionEventHandler(dialog.HandleDialogException);
			}
		}

		private void ChangeDataSource(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Ignore;
			this.Close();
		}

		private void ConfigureAcceptButton(object sender, EventArgs e)
		{
			try
			{
				this.acceptButton.Enabled = (this.ConnectionProperties != null) ? this.ConnectionProperties.IsComplete : false;
			}
			catch
			{
				this.acceptButton.Enabled = true;
			}
		}

		private void ConfigureChangeDataSourceButton()
		{
			this.changeDataSourceButton.Enabled = (this.DataSources.Count > 1 || this.SelectedDataSource.Providers.Count > 1);
		}

		private void ConfigureContainerControl()
		{
			if (this.containerControl.Controls.Count == 0)
				this._initialContainerControlSize = this.containerControl.Size;
			if ((this.containerControl.Controls.Count == 0 && this.ConnectionUIControl != null) ||
			    (this.containerControl.Controls.Count > 0 && this.ConnectionUIControl != this.containerControl.Controls[0]))
			{
				this.containerControl.Controls.Clear();
				if (this.ConnectionUIControl != null && this.ConnectionUIControl.PreferredSize.Width > 0 && this.ConnectionUIControl.PreferredSize.Height > 0)
				{
					// Add it to the container control
					this.containerControl.Controls.Add(this.ConnectionUIControl);

					// Size dialog appropriately
					this.MinimumSize = Size.Empty;
					Size currentSize = this.containerControl.Size;
					this.containerControl.Size = this._initialContainerControlSize;
					Size preferredSize = this.ConnectionUIControl.PreferredSize;
					this.containerControl.Size = currentSize;
					int minimumWidth =
						this._initialContainerControlSize.Width - (this.Width - this.ClientSize.Width) -
						this.Padding.Left -
						this.containerControl.Margin.Left -
						this.containerControl.Margin.Right -
						this.Padding.Right;
					minimumWidth = Math.Max(minimumWidth,
					                        this.testConnectionButton.Width +
					                        this.testConnectionButton.Margin.Right +
					                        this.buttonsTableLayoutPanel.Margin.Left +
					                        this.buttonsTableLayoutPanel.Width +
					                        this.buttonsTableLayoutPanel.Margin.Right);
					preferredSize.Width = Math.Max(preferredSize.Width, minimumWidth);
					this.Size += preferredSize - this.containerControl.Size;
					if (this.containerControl.Bottom == this.advancedButton.Top)
					{
						this.containerControl.Margin = new Padding(
							this.containerControl.Margin.Left,
							this.dataSourceTableLayoutPanel.Margin.Bottom,
							this.containerControl.Margin.Right,
							this.advancedButton.Margin.Top);
						this.Height += this.containerControl.Margin.Bottom + this.advancedButton.Margin.Top;
						this.containerControl.Height -= this.containerControl.Margin.Bottom + this.advancedButton.Margin.Top;
					}
					Size maximumSize =
						SystemInformation.PrimaryMonitorMaximizedWindowSize -
						SystemInformation.FrameBorderSize -
						SystemInformation.FrameBorderSize;
					if (this.Width > maximumSize.Width)
					{
						this.Width = maximumSize.Width;
						if (this.Height + SystemInformation.HorizontalScrollBarHeight <= maximumSize.Height)
							this.Height += SystemInformation.HorizontalScrollBarHeight;
					}
					if (this.Height > maximumSize.Height)
					{
						if (this.Width + SystemInformation.VerticalScrollBarWidth <= maximumSize.Width)
							this.Width += SystemInformation.VerticalScrollBarWidth;
						this.Height = maximumSize.Height;
					}
					this.MinimumSize = this.Size;

					// The advanced button is only enabled for actual UI controls
					this.advancedButton.Enabled = !(this.ConnectionUIControl is PropertyGridUIControl);
				}
				else
				{
					// Size dialog appropriately
					this.MinimumSize = Size.Empty;
					if (this.containerControl.Bottom != this.advancedButton.Top)
					{
						this.containerControl.Height += this.containerControl.Margin.Bottom + this.advancedButton.Margin.Top;
						this.Height -= this.containerControl.Margin.Bottom + this.advancedButton.Margin.Top;
						this.containerControl.Margin = new Padding(
							this.containerControl.Margin.Left,
							0,
							this.containerControl.Margin.Right,
							0);
					}
					this.Size -= this.containerControl.Size - new Size(300, 0);
					this.MinimumSize = this.Size;

					// The advanced button is always enabled for no UI control
					this.advancedButton.Enabled = true;
				}
			}
			if (this.ConnectionUIControl != null)
			{
				// Load properties into the connection UI control
				try
				{
					this._connectionUIControlTable[this.SelectedDataSource][this.SelectedDataProvider].LoadProperties();
				}
				catch
				{
				}
			}
		}

		private void ConfigureDataSourceTextBox()
		{
			if (this.SelectedDataSource != null)
			{
				if (this.SelectedDataSource == this.UnspecifiedDataSource)
				{
					if (this.SelectedDataProvider != null)
						this.dataSourceTextBox.Text = this.SelectedDataProvider.DisplayName;
					else
						this.dataSourceTextBox.Text = null;
					this.dataProviderToolTip.SetToolTip(this.dataSourceTextBox, null);
				}
				else
				{
					this.dataSourceTextBox.Text = this.SelectedDataSource.DisplayName;
					if (this.SelectedDataProvider != null)
					{
						if (this.SelectedDataProvider.ShortDisplayName != null)
							this.dataSourceTextBox.Text = String.Format(Strings.DataConnectionDialog_DataSourceWithShortProvider, this.dataSourceTextBox.Text, this.SelectedDataProvider.ShortDisplayName);
						this.dataProviderToolTip.SetToolTip(this.dataSourceTextBox, this.SelectedDataProvider.DisplayName);
					}
					else
						this.dataProviderToolTip.SetToolTip(this.dataSourceTextBox, null);
				}
			}
			else
			{
				this.dataSourceTextBox.Text = null;
				this.dataProviderToolTip.SetToolTip(this.dataSourceTextBox, null);
			}
			this.dataSourceTextBox.Select(0, 0);
		}

		public DataProvider GetSelectedDataProvider(DataSource dataSource)
		{
			if (dataSource == null)
				return null;
			switch (dataSource.Providers.Count)
			{
				case 0:
					return null;
				case 1:
					// If there is only one data provider, it must be selected
					IEnumerator<DataProvider> e = dataSource.Providers.GetEnumerator();
					e.MoveNext();
					return e.Current;
				default:
					return (this._dataProviderSelections.ContainsKey(dataSource)) ? this._dataProviderSelections[dataSource] : dataSource.DefaultProvider;
			}
		}

		private void HandleAccept(object sender, EventArgs e)
		{
			this.acceptButton.Focus(); // ensures connection properties are up to date
		}

		private void HandleDialogException(object sender, ThreadExceptionEventArgs e)
		{
			this.OnDialogException(e);
		}

		protected internal virtual void OnContextHelpRequested(ContextHelpEventArgs e)
		{
			if (this.ContextHelpRequested != null)
				this.ContextHelpRequested(this, e);
			if (e.Handled == false)
			{
				this.ShowError(null, Strings.DataConnectionDialog_NoHelpAvailable);
				e.Handled = true;
			}
		}

		protected virtual void OnDialogException(ThreadExceptionEventArgs e)
		{
			if (this.DialogException != null)
				this.DialogException(this, e);
			else
				ShowError(null, e.Exception);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			this.dataSourceTableLayoutPanel.Anchor &= ~AnchorStyles.Right;
			this.containerControl.Anchor &= ~AnchorStyles.Right & ~AnchorStyles.Bottom;
			this.advancedButton.Anchor |= AnchorStyles.Top | AnchorStyles.Left;
			this.advancedButton.Anchor &= ~AnchorStyles.Right & ~AnchorStyles.Bottom;
			this.separatorPanel.Anchor |= AnchorStyles.Top;
			this.separatorPanel.Anchor &= ~AnchorStyles.Right & ~AnchorStyles.Bottom;
			this.testConnectionButton.Anchor |= AnchorStyles.Top;
			this.testConnectionButton.Anchor &= ~AnchorStyles.Bottom;
			this.buttonsTableLayoutPanel.Anchor |= AnchorStyles.Top | AnchorStyles.Left;
			this.buttonsTableLayoutPanel.Anchor &= ~AnchorStyles.Right & ~AnchorStyles.Bottom;
			Size properSize = new Size(
				this.containerControl.Right +
				this.containerControl.Margin.Right +
				this.Padding.Right,
				this.buttonsTableLayoutPanel.Bottom +
				this.buttonsTableLayoutPanel.Margin.Bottom +
				this.Padding.Bottom);
			properSize = this.SizeFromClientSize(properSize);
			Size dsize = this.Size - properSize;
			this.MinimumSize -= dsize;
			this.Size -= dsize;
			this.buttonsTableLayoutPanel.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
			this.buttonsTableLayoutPanel.Anchor &= ~AnchorStyles.Top & ~AnchorStyles.Left;
			this.testConnectionButton.Anchor |= AnchorStyles.Bottom;
			this.testConnectionButton.Anchor &= ~AnchorStyles.Top;
			this.separatorPanel.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
			this.separatorPanel.Anchor &= ~AnchorStyles.Top;
			this.advancedButton.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
			this.advancedButton.Anchor &= ~AnchorStyles.Top & ~AnchorStyles.Left;
			this.containerControl.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
			this.dataSourceTableLayoutPanel.Anchor |= AnchorStyles.Right;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK)
			{
				try
				{
					this.OnVerifySettings(EventArgs.Empty);
				}
				catch (Exception ex)
				{
					ExternalException exex = ex as ExternalException;
					if (exex == null || exex.ErrorCode != NativeMethods.DB_E_CANCELED)
						ShowError(null, ex);
					e.Cancel = true;
				}
			}

			base.OnFormClosing(e);
		}

		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			// Get the active control
			Control activeControl = this;
			ContainerControl containrControl = null;
			while ((containrControl = activeControl as ContainerControl) != null &&
			       containrControl != this.ConnectionUIControl &&
			       containrControl.ActiveControl != null)
				activeControl = containrControl.ActiveControl;

			// Figure out the context
			DataConnectionDialogContext context = DataConnectionDialogContext.Main;
			if (activeControl == this.dataSourceTextBox)
				context = DataConnectionDialogContext.MainDataSourceTextBox;
			if (activeControl == this.changeDataSourceButton)
				context = DataConnectionDialogContext.MainChangeDataSourceButton;
			if (activeControl == this.ConnectionUIControl)
			{
				context = DataConnectionDialogContext.MainConnectionUIControl;
				if (this.ConnectionUIControl is SqlConnectionUIControl)
					context = DataConnectionDialogContext.MainSqlConnectionUIControl;
				if (this.ConnectionUIControl is SqlFileConnectionUIControl)
					context = DataConnectionDialogContext.MainSqlFileConnectionUIControl;
				if (this.ConnectionUIControl is OracleConnectionUIControl)
					context = DataConnectionDialogContext.MainOracleConnectionUIControl;
				if (this.ConnectionUIControl is AccessConnectionUIControl)
					context = DataConnectionDialogContext.MainAccessConnectionUIControl;
				if (this.ConnectionUIControl is OleDBConnectionUIControl)
					context = DataConnectionDialogContext.MainOleDBConnectionUIControl;
				if (this.ConnectionUIControl is OdbcConnectionUIControl)
					context = DataConnectionDialogContext.MainOdbcConnectionUIControl;
				if (this.ConnectionUIControl is PropertyGridUIControl)
					context = DataConnectionDialogContext.MainGenericConnectionUIControl;
			}
			if (activeControl == this.advancedButton)
				context = DataConnectionDialogContext.MainAdvancedButton;
			if (activeControl == this.testConnectionButton)
				context = DataConnectionDialogContext.MainTestConnectionButton;
			if (activeControl == this.acceptButton)
				context = DataConnectionDialogContext.MainAcceptButton;
			if (activeControl == this.cancelButton)
				context = DataConnectionDialogContext.MainCancelButton;

			// Call OnContextHelpRequested
			ContextHelpEventArgs e = new ContextHelpEventArgs(context, hevent.MousePos);
			this.OnContextHelpRequested(e);
			hevent.Handled = e.Handled;
			if (!e.Handled)
				base.OnHelpRequested(hevent);
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!this._showingDialog)
				throw new NotSupportedException(Strings.DataConnectionDialog_ShowDialogNotSupported);
			this.ConfigureDataSourceTextBox();
			this.ConfigureChangeDataSourceButton();
			this.ConfigureContainerControl();
			this.ConfigureAcceptButton(this, EventArgs.Empty);
			base.OnLoad(e);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			// Set focus to the connection UI control (if any)
			if (this.ConnectionUIControl != null)
				this.ConnectionUIControl.Focus();
		}

		protected virtual void OnVerifySettings(EventArgs e)
		{
			if (this.VerifySettings != null)
				this.VerifySettings(this, e);
		}

		private void PaintSeparator(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Pen dark = new Pen(ControlPaint.Dark(this.BackColor, 0f));
			Pen light = new Pen(ControlPaint.Light(this.BackColor, 1f));
			int width = this.separatorPanel.Width;

			graphics.DrawLine(dark, 0, 0, width, 0);
			graphics.DrawLine(light, 0, 1, width, 1);
		}

		private void SetConnectionUIControlDockStyle(object sender, EventArgs e)
		{
			if (this.containerControl.Controls.Count > 0)
			{
				DockStyle dockStyle = DockStyle.None;
				Size containerControlSize = this.containerControl.Size;
				Size connectionUIControlMinimumSize = this.containerControl.Controls[0].MinimumSize;
				if (containerControlSize.Width >= connectionUIControlMinimumSize.Width &&
				    containerControlSize.Height >= connectionUIControlMinimumSize.Height)
					dockStyle = DockStyle.Fill;
				if (containerControlSize.Width - SystemInformation.VerticalScrollBarWidth >= connectionUIControlMinimumSize.Width &&
				    containerControlSize.Height < connectionUIControlMinimumSize.Height)
					dockStyle = DockStyle.Top;
				if (containerControlSize.Width < connectionUIControlMinimumSize.Width &&
				    containerControlSize.Height - SystemInformation.HorizontalScrollBarHeight >= connectionUIControlMinimumSize.Height)
					dockStyle = DockStyle.Left;
				this.containerControl.Controls[0].Dock = dockStyle;
			}
		}

		public void SetSelectedDataProvider(DataSource dataSource, DataProvider dataProvider)
		{
			if (this.GetSelectedDataProvider(dataSource) != dataProvider)
			{
				if (dataSource == null)
					throw new ArgumentNullException("dataSource");
				if (this._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				this.SetSelectedDataProvider(dataSource, dataProvider, false);
			}
		}

		private void SetSelectedDataProvider(DataSource dataSource, DataProvider value, bool noSingleItemCheck)
		{
			Debug.Assert(dataSource != null);
			if (!noSingleItemCheck && dataSource.Providers.Count == 1 &&
			    ((this._dataProviderSelections.ContainsKey(dataSource) && this._dataProviderSelections[dataSource] != value) ||
			     (!this._dataProviderSelections.ContainsKey(dataSource) && value != null)))
			{
				IEnumerator<DataProvider> e = dataSource.Providers.GetEnumerator();
				e.MoveNext();
				if (value != e.Current)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotChangeSingleDataProvider);
			}
			if ((this._dataProviderSelections.ContainsKey(dataSource) && this._dataProviderSelections[dataSource] != value) ||
			    (!this._dataProviderSelections.ContainsKey(dataSource) && value != null))
			{
				if (value != null)
				{
					if (!dataSource.Providers.Contains(value))
						throw new InvalidOperationException(Strings.DataConnectionDialog_DataSourceNoAssociation);
					this._dataProviderSelections[dataSource] = value;
				}
				else if (this._dataProviderSelections.ContainsKey(dataSource))
					this._dataProviderSelections.Remove(dataSource);

				if (this._showingDialog)
					this.ConfigureContainerControl();
			}
		}

		internal void SetSelectedDataProviderInternal(DataSource dataSource, DataProvider value)
		{
			this.SetSelectedDataProvider(dataSource, value, false);
		}

		private void SetSelectedDataSource(DataSource value, bool noSingleItemCheck)
		{
			if (!noSingleItemCheck && this._dataSources.Count == 1 && this._selectedDataSource != value)
			{
				IEnumerator<DataSource> e = this._dataSources.GetEnumerator();
				e.MoveNext();
				if (value != e.Current)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotChangeSingleDataSource);
			}
			if (this._selectedDataSource != value)
			{
				if (value != null)
				{
					if (!this._dataSources.Contains(value))
						throw new InvalidOperationException(Strings.DataConnectionDialog_DataSourceNotFound);
					this._selectedDataSource = value;
					switch (this._selectedDataSource.Providers.Count)
					{
						case 0:
							this.SetSelectedDataProvider(this._selectedDataSource, null, noSingleItemCheck);
							break;
						case 1:
							IEnumerator<DataProvider> e = this._selectedDataSource.Providers.GetEnumerator();
							e.MoveNext();
							this.SetSelectedDataProvider(this._selectedDataSource, e.Current, true);
							break;
						default:
							DataProvider defaultProvider = this._selectedDataSource.DefaultProvider;
							if (this._dataProviderSelections.ContainsKey(this._selectedDataSource))
								defaultProvider = this._dataProviderSelections[this._selectedDataSource];
							this.SetSelectedDataProvider(this._selectedDataSource, defaultProvider, noSingleItemCheck);
							break;
					}
				}
				else
					this._selectedDataSource = null;

				if (this._showingDialog)
					this.ConfigureDataSourceTextBox();
			}
		}

		internal void SetSelectedDataSourceInternal(DataSource value)
		{
			this.SetSelectedDataSource(value, false);
		}

		private void ShowAdvanced(object sender, EventArgs e)
		{
			DataConnectionAdvancedDialog advancedDialog = new DataConnectionAdvancedDialog(this.ConnectionProperties, this);
			DialogResult dialogResult = DialogResult.None;
			try
			{
				if (this.Container != null)
					this.Container.Add(advancedDialog);
				dialogResult = advancedDialog.ShowDialog(this);
			}
			finally
			{
				if (this.Container != null)
					this.Container.Remove(advancedDialog);
				advancedDialog.Dispose();
			}
			if (dialogResult == DialogResult.OK && this.ConnectionUIControl != null)
			{
				try
				{
					this._connectionUIControlTable[this.SelectedDataSource][this.SelectedDataProvider].LoadProperties();
				}
				catch
				{
				}
				this.ConfigureAcceptButton(this, EventArgs.Empty);
			}
		}

		private void ShowError(string title, Exception ex)
		{
			IUIService uiService = this.GetService(typeof(IUIService)) as IUIService;
			if (uiService != null)
				uiService.ShowError(ex);
			else
				RTLAwareMessageBox.Show(title, ex.Message, MessageBoxIcon.Exclamation);
		}

		private void ShowError(string title, string message)
		{
			IUIService uiService = this.GetService(typeof(IUIService)) as IUIService;
			if (uiService != null)
				uiService.ShowError(message);
			else
				RTLAwareMessageBox.Show(title, message, MessageBoxIcon.Exclamation);
		}

		private void ShowMessage(string title, string message)
		{
			IUIService uiService = this.GetService(typeof(IUIService)) as IUIService;
			if (uiService != null)
				uiService.ShowMessage(message);
			else
				RTLAwareMessageBox.Show(title, message, MessageBoxIcon.Information);
		}

		private void TestConnection(object sender, EventArgs e)
		{
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				this.ConnectionProperties.Test();
			}
			catch (Exception ex)
			{
				Cursor.Current = currentCursor;
				ShowError(Strings.DataConnectionDialog_TestResults, ex);
				return;
			}
			Cursor.Current = currentCursor;
			this.ShowMessage(Strings.DataConnectionDialog_TestResults, Strings.DataConnectionDialog_TestConnectionSucceeded);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (this._translateHelpButton && HelpUtils.IsContextHelpMessage(ref m))
			{
				// Force the ? in the title bar to invoke the help topic
				HelpUtils.TranslateContextHelpMessage(this, ref m);
				base.DefWndProc(ref m); // pass to the active control
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class BasicConnectionProperties : IDataConnectionProperties
		{
			#region Constructors/Destructors

			public BasicConnectionProperties()
			{
			}

			#endregion

			#region Fields/Constants

			private string _s;

			#endregion

			#region Properties/Indexers/Events

			public event EventHandler PropertyChanged;

			public object this[string propertyName]
			{
				get
				{
					if (propertyName == "ConnectionString")
						return this.ConnectionString;
					else
						return null;
				}
				set
				{
					if (propertyName == "ConnectionString")
						this.ConnectionString = value as string;
				}
			}

			public string ConnectionString
			{
				get
				{
					return this.ToFullString();
				}
				set
				{
					this.Parse(value);
				}
			}

			[Browsable(false)]
			public bool IsComplete
			{
				get
				{
					return true;
				}
			}

			[Browsable(false)]
			public bool IsExtensible
			{
				get
				{
					return false;
				}
			}

			#endregion

			#region Methods/Operators

			public void Add(string propertyName)
			{
				throw new NotImplementedException();
			}

			public bool Contains(string propertyName)
			{
				return (propertyName == "ConnectionString");
			}

			public void Parse(string s)
			{
				this._s = s;
				if (this.PropertyChanged != null)
					this.PropertyChanged(this, EventArgs.Empty);
			}

			public void Remove(string propertyName)
			{
				throw new NotImplementedException();
			}

			public void Reset()
			{
				this._s = String.Empty;
			}

			public void Reset(string propertyName)
			{
				Debug.Assert(propertyName == "ConnectionString");
				this._s = String.Empty;
			}

			public void Test()
			{
			}

			public string ToDisplayString()
			{
				return this._s;
			}

			public string ToFullString()
			{
				return this._s;
			}

			#endregion
		}

		private class DataSourceCollection : ICollection<DataSource>
		{
			#region Constructors/Destructors

			public DataSourceCollection(DataConnectionDialog dialog)
			{
				Debug.Assert(dialog != null);

				this._dialog = dialog;
			}

			#endregion

			#region Fields/Constants

			private DataConnectionDialog _dialog;
			private List<DataSource> _list = new List<DataSource>();

			#endregion

			#region Properties/Indexers/Events

			public int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			public bool IsReadOnly
			{
				get
				{
					return this._dialog._showingDialog;
				}
			}

			#endregion

			#region Methods/Operators

			public void Add(DataSource item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (this._dialog._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				if (!this._list.Contains(item))
					this._list.Add(item);
			}

			public void Clear()
			{
				if (this._dialog._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				this._list.Clear();
				this._dialog.SetSelectedDataSource(null, true);
			}

			public bool Contains(DataSource item)
			{
				return this._list.Contains(item);
			}

			public void CopyTo(DataSource[] array, int arrayIndex)
			{
				this._list.CopyTo(array, arrayIndex);
			}

			public IEnumerator<DataSource> GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			public bool Remove(DataSource item)
			{
				if (this._dialog._showingDialog)
					throw new InvalidOperationException(Strings.DataConnectionDialog_CannotModifyState);
				bool result = this._list.Remove(item);
				if (item == this._dialog.SelectedDataSource)
					this._dialog.SetSelectedDataSource(null, true);
				return result;
			}

			#endregion
		}

		private class PropertyGridUIControl : UserControl, IDataConnectionUIControl
		{
			#region Constructors/Destructors

			public PropertyGridUIControl()
			{
				this.propertyGrid = new DataConnectionAdvancedDialog.SpecializedPropertyGrid();
				this.SuspendLayout();
				// 
				// propertyGrid
				// 
				this.propertyGrid.CommandsVisibleIfAvailable = true;
				this.propertyGrid.Dock = DockStyle.Fill;
				this.propertyGrid.Location = Point.Empty;
				this.propertyGrid.Margin = new Padding(0);
				this.propertyGrid.Name = "propertyGrid";
				this.propertyGrid.TabIndex = 0;
				// 
				// DataConnectionDialog
				// 
				this.Controls.Add(this.propertyGrid);
				this.Name = "PropertyGridUIControl";
				this.ResumeLayout(false);
				this.PerformLayout();
			}

			#endregion

			#region Fields/Constants

			private IDataConnectionProperties connectionProperties;
			private DataConnectionAdvancedDialog.SpecializedPropertyGrid propertyGrid;

			#endregion

			#region Methods/Operators

			public override Size GetPreferredSize(Size proposedSize)
			{
				return this.propertyGrid.GetPreferredSize(proposedSize);
			}

			public void Initialize(IDataConnectionProperties dataConnectionProperties)
			{
				this.connectionProperties = dataConnectionProperties;
			}

			public void LoadProperties()
			{
				this.propertyGrid.SelectedObject = this.connectionProperties;
			}

			#endregion
		}

		#endregion
	}
}