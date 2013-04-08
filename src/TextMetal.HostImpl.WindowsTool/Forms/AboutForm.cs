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
	internal partial class AboutForm : TmForm
	{
		#region Constructors/Destructors

		public AboutForm()
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

			this.CoreText = string.Format("About {0} Studio", Program.AssemblyInformation.Product);

			this.lblVersion.Text = Program.AssemblyInformation.AssemblyVersion;
			this.lblCompany.Text = Program.AssemblyInformation.Company;
			this.lblConfiguration.Text = Program.AssemblyInformation.Configuration;
			this.lblCopyright.Text = Program.AssemblyInformation.Copyright;
			this.lblInformationalVersion.Text = Program.AssemblyInformation.InformationalVersion;
			this.lblProduct.Text = string.Format("{0} Studio", Program.AssemblyInformation.Product);
			this.lblTitle.Text = Program.AssemblyInformation.Title;
			this.lblTrademark.Text = Program.AssemblyInformation.Trademark;
			this.lblWin32FileVersion.Text = Program.AssemblyInformation.Win32FileVersion;
			this.txtBxDescription.Text = Program.AssemblyInformation.Description;

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

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Okay();
		}

		#endregion
	}
}