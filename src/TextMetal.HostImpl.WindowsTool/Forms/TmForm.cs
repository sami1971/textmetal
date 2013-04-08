/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using TextMetal.Common.Core;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class TmForm : Form
	{
		#region Constructors/Destructors

		public TmForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private bool coreHasShown;
		private bool coreIsDirty;
		private char? coreIsDirtyIndicator;
		private string coreText;

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool CoreHasShow
		{
			get
			{
				return this.coreHasShown;
			}
			private set
			{
				if (this.coreHasShown || !value)
					throw new InvalidOperationException("");

				this.coreHasShown = true;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CoreIsDirty
		{
			get
			{
				return this.coreIsDirty;
			}
			set
			{
				this.coreIsDirty = value;

				this.CoreUpdateFormText();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public char? CoreIsDirtyIndicator
		{
			get
			{
				return this.coreIsDirtyIndicator;
			}
			set
			{
				this.coreIsDirtyIndicator = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CoreText
		{
			get
			{
				return this.coreText;
			}
			set
			{
				this.coreText = value;

				this.CoreUpdateFormText();
			}
		}

		#endregion

		#region Methods/Operators

		protected virtual void CoreQuit(out bool cancel)
		{
			cancel = false;
			// do nothing
		}

		public void CoreSetToolTipText(Control control, string caption)
		{
			this.ttMain.SetToolTip(control, caption);
		}

		protected virtual void CoreSetup()
		{
			Stream stream;
			Icon icon;

			stream = this.GetType().Assembly.GetManifestResourceStream("TextMetal.HostImpl.WindowsTool.Icons.TextMetal.ico");

			if ((object)stream == null)
				throw new InvalidOperationException("TextMetal.HostImpl.WindowsTool.Icons.TextMetal.ico");

			icon = new Icon(stream);
			this.Icon = icon;
			// DO NOT DISPOSE (owner cleans up)
		}

		protected virtual void CoreShown()
		{
			this.CoreHasShow = true;
			this.CoreIsDirty = false;
		}

		protected virtual void CoreTeardown()
		{
			// do nothing
		}

		private void CoreUpdateFormText()
		{
			this.Text = string.Format("{0}{1}", this.CoreText.SafeToString(), this.CoreIsDirty ? this.CoreIsDirtyIndicator.SafeToString() : "");
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.CoreTeardown();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			bool cancel;

			base.OnClosing(e);

			if (e.Cancel)
				return;

			this.CoreQuit(out cancel);

			e.Cancel = cancel;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CoreSetup();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.CoreShown();
		}

		#endregion
	}
}