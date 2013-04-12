/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	partial class DocumentChooserForm
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentChooserForm));
			this.btnOkay = new TextMetal.HostImpl.WindowsTool.Controls.TmButton();
			this.btnCancel = new TextMetal.HostImpl.WindowsTool.Controls.TmButton();
			this.gbChoice = new System.Windows.Forms.GroupBox();
			this.rdoSqlQuery = new TextMetal.HostImpl.WindowsTool.Controls.TmRadioButton();
			this.rdoAssociativeModel = new TextMetal.HostImpl.WindowsTool.Controls.TmRadioButton();
			this.rdoTemplate = new TextMetal.HostImpl.WindowsTool.Controls.TmRadioButton();
			this.gbChoice.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOkay
			// 
			this.btnOkay.Location = new System.Drawing.Point(59, 120);
			this.btnOkay.Name = "btnOkay";
			this.btnOkay.Size = new System.Drawing.Size(75, 23);
			this.btnOkay.TabIndex = 2;
			this.btnOkay.Text = "OK";
			this.btnOkay.UseVisualStyleBackColor = true;
			this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(140, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// gbChoice
			// 
			this.gbChoice.Controls.Add(this.rdoSqlQuery);
			this.gbChoice.Controls.Add(this.rdoAssociativeModel);
			this.gbChoice.Controls.Add(this.rdoTemplate);
			this.gbChoice.Location = new System.Drawing.Point(13, 13);
			this.gbChoice.Name = "gbChoice";
			this.gbChoice.Size = new System.Drawing.Size(202, 96);
			this.gbChoice.TabIndex = 0;
			this.gbChoice.TabStop = false;
			this.gbChoice.Text = "Choose a document type to proceed";
			// 
			// rdoSqlQuery
			// 
			this.rdoSqlQuery.AutoSize = true;
			this.rdoSqlQuery.Location = new System.Drawing.Point(7, 66);
			this.rdoSqlQuery.Name = "rdoSqlQuery";
			this.rdoSqlQuery.Size = new System.Drawing.Size(77, 17);
			this.rdoSqlQuery.TabIndex = 2;
			this.rdoSqlQuery.TabStop = true;
			this.rdoSqlQuery.Text = "SQL Query";
			this.rdoSqlQuery.UseVisualStyleBackColor = true;
			this.rdoSqlQuery.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rdoXXX_DoubleClick);
			// 
			// rdoAssociativeModel
			// 
			this.rdoAssociativeModel.AutoSize = true;
			this.rdoAssociativeModel.Location = new System.Drawing.Point(6, 43);
			this.rdoAssociativeModel.Name = "rdoAssociativeModel";
			this.rdoAssociativeModel.Size = new System.Drawing.Size(111, 17);
			this.rdoAssociativeModel.TabIndex = 1;
			this.rdoAssociativeModel.TabStop = true;
			this.rdoAssociativeModel.Text = "Associative Model";
			this.rdoAssociativeModel.UseVisualStyleBackColor = true;
			this.rdoAssociativeModel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rdoXXX_DoubleClick);
			// 
			// rdoTemplate
			// 
			this.rdoTemplate.AutoSize = true;
			this.rdoTemplate.Location = new System.Drawing.Point(7, 20);
			this.rdoTemplate.Name = "rdoTemplate";
			this.rdoTemplate.Size = new System.Drawing.Size(69, 17);
			this.rdoTemplate.TabIndex = 0;
			this.rdoTemplate.TabStop = true;
			this.rdoTemplate.Text = "Template";
			this.rdoTemplate.UseVisualStyleBackColor = true;
			this.rdoTemplate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.rdoXXX_DoubleClick);
			// 
			// DocumentChooserForm
			// 
			this.AcceptButton = this.btnOkay;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(228, 155);
			this.Controls.Add(this.gbChoice);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOkay);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DocumentChooserForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.gbChoice.ResumeLayout(false);
			this.gbChoice.PerformLayout();
			this.ResumeLayout(false);

		}
		internal TextMetal.HostImpl.WindowsTool.Controls.TmButton btnOkay;
		internal TextMetal.HostImpl.WindowsTool.Controls.TmButton btnCancel;
		private System.Windows.Forms.GroupBox gbChoice;
		private Controls.TmRadioButton rdoSqlQuery;
		private Controls.TmRadioButton rdoAssociativeModel;
		private Controls.TmRadioButton rdoTemplate;
	}
}