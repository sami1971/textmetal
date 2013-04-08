/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

using TextMetal.HostImpl.WindowsTool.Controls;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class DocumentChooserForm : TmForm
	{
		#region Constructors/Destructors

		public DocumentChooserForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		public DocumentForm.DocumentSpecific.IDocumentStrategy DocumentStrategy
		{
			get
			{
				if (this.DialogResult != DialogResult.OK)
					return null;

				if (this.rdoAssociativeModel.CoreGetValue() ?? false)
					return DocumentForm.DocumentSpecific.AssociativeModelDocumentStrategy.Instance;
				else if (this.rdoSqlQuery.CoreGetValue() ?? false)
					return DocumentForm.DocumentSpecific.SqlQueryDocumentStrategy.Instance;
				else if (this.rdoTemplate.CoreGetValue() ?? false)
					return DocumentForm.DocumentSpecific.TemplateDocumentStrategy.Instance;
				else
					return null;
			}
		}

		#endregion

		#region Methods/Operators

		private void Cancel()
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close(); // direct
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.CoreText = string.Format("{0} Studio", Program.AssemblyInformation.Product);
		}

		private void Okay()
		{
			this.DialogResult = DialogResult.OK;
			this.Close(); // direct
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Cancel();
		}

		private void btnOkay_Click(object sender, EventArgs e)
		{
			this.Okay();
		}

		private void rdoXXX_DoubleClick(object sender, MouseEventArgs e)
		{
			this.Okay();
		}

		#endregion
	}
}