/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	internal partial class SplashForm : TmForm
	{
		#region Constructors/Destructors

		public SplashForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Methods/Operators

		protected override void CoreSetup()
		{
			Stream stream;
			Image image;

			base.CoreSetup();

			this.CoreText = string.Format("{0}", Program.AssemblyInformation.Product);

			stream = this.GetType().Assembly.GetManifestResourceStream("TextMetal.HostImpl.WindowsTool.Images.SplashScreen.png");

			if ((object)stream == null)
				throw new InvalidOperationException("");

			image = Image.FromStream(stream);

			this.pbAppLogo.Image = image;
			// DO NOT DISPOSE (owner cleans up)
		}

		private void Okay()
		{
			this.DialogResult = DialogResult.Cancel; // yes, this is correct
			this.Close(); // direct
		}

		private void closeFormBy_Click(object sender, EventArgs e)
		{
			this.Okay();
		}

		private void tmrMain_Tick(object sender, EventArgs e)
		{
			this.pbMain.Value += (int)(this.pbMain.Maximum * 0.10);

			if (this.pbMain.Value >= this.pbMain.Maximum)
			{
				this.tmrMain.Enabled = false;
				this.closeFormBy_Click(sender, e);
			}
		}

		#endregion
	}
}