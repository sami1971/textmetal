﻿namespace TextMetal.HostImpl.WindowsTool.Forms
{
	partial class SplashForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
			this.pbAppLogo = new System.Windows.Forms.PictureBox();
			this.btnOK = new TextMetal.HostImpl.WindowsTool.Controls.TmButton();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pbMain = new System.Windows.Forms.ProgressBar();
			this.tmrMain = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbAppLogo
			// 
			this.pbAppLogo.BackColor = System.Drawing.Color.White;
			this.pbAppLogo.Location = new System.Drawing.Point(12, 12);
			this.pbAppLogo.Name = "pbAppLogo";
			this.pbAppLogo.Size = new System.Drawing.Size(300, 300);
			this.pbAppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbAppLogo.TabIndex = 12;
			this.pbAppLogo.TabStop = false;
			this.pbAppLogo.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(237, 318);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// pnlMain
			// 
			this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMain.Controls.Add(this.pbMain);
			this.pnlMain.Controls.Add(this.pbAppLogo);
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(327, 351);
			this.pnlMain.TabIndex = 0;
			this.pnlMain.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// pbMain
			// 
			this.pbMain.Location = new System.Drawing.Point(12, 317);
			this.pbMain.Maximum = 1000;
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new System.Drawing.Size(219, 23);
			this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbMain.TabIndex = 0;
			this.pbMain.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// tmrMain
			// 
			this.tmrMain.Enabled = true;
			this.tmrMain.Interval = 150;
			this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(327, 351);
			this.Controls.Add(this.pnlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Click += new System.EventHandler(this.closeFormBy_Click);
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbAppLogo;
		private TextMetal.HostImpl.WindowsTool.Controls.TmButton btnOK;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ProgressBar pbMain;
		private System.Windows.Forms.Timer tmrMain;
	}
}
