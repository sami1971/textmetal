namespace TextMetal.HostImpl.WindowsTool.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ssMain = new System.Windows.Forms.StatusStrip();
			this.tsslMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.msMain = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNewDocument = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenDocument = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDocumentWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTopics = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.ofdMain = new System.Windows.Forms.OpenFileDialog();
			this.ssMain.SuspendLayout();
			this.msMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssMain
			// 
			this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMain});
			this.ssMain.Location = new System.Drawing.Point(0, 517);
			this.ssMain.Name = "ssMain";
			this.ssMain.Size = new System.Drawing.Size(866, 22);
			this.ssMain.TabIndex = 1;
			this.ssMain.Text = "statusStrip1";
			// 
			// tsslMain
			// 
			this.tsslMain.Name = "tsslMain";
			this.tsslMain.Size = new System.Drawing.Size(53, 17);
			this.tsslMain.Text = "%TEXT%";
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiWindow,
            this.tsmiHelp});
			this.msMain.Location = new System.Drawing.Point(0, 0);
			this.msMain.Name = "msMain";
			this.msMain.Size = new System.Drawing.Size(866, 24);
			this.msMain.TabIndex = 0;
			this.msMain.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.toolStripSeparator1,
            this.tsmiExit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(35, 20);
			this.tsmiFile.Text = "&File";
			// 
			// tsmiNew
			// 
			this.tsmiNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewDocument});
			this.tsmiNew.Name = "tsmiNew";
			this.tsmiNew.Size = new System.Drawing.Size(152, 22);
			this.tsmiNew.Text = "&New";
			// 
			// tsmiNewDocument
			// 
			this.tsmiNewDocument.Name = "tsmiNewDocument";
			this.tsmiNewDocument.ShortcutKeyDisplayString = "";
			this.tsmiNewDocument.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmiNewDocument.Size = new System.Drawing.Size(161, 22);
			this.tsmiNewDocument.Text = "&Document";
			this.tsmiNewDocument.Click += new System.EventHandler(this.tsmiNewDocument_Click);
			// 
			// tsmiOpen
			// 
			this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenDocument});
			this.tsmiOpen.Name = "tsmiOpen";
			this.tsmiOpen.Size = new System.Drawing.Size(152, 22);
			this.tsmiOpen.Text = "&Open";
			// 
			// tsmiOpenDocument
			// 
			this.tsmiOpenDocument.Name = "tsmiOpenDocument";
			this.tsmiOpenDocument.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmiOpenDocument.Size = new System.Drawing.Size(174, 22);
			this.tsmiOpenDocument.Text = "&Document...";
			this.tsmiOpenDocument.Click += new System.EventHandler(this.tsmiOpenDocument_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiExit.Size = new System.Drawing.Size(152, 22);
			this.tsmiExit.Text = "E&xit";
			this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
			// 
			// tsmiWindow
			// 
			this.tsmiWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCloseAllDocuments,
            this.toolStripSeparator2,
            this.tsmiDocumentWindows});
			this.tsmiWindow.Name = "tsmiWindow";
			this.tsmiWindow.Size = new System.Drawing.Size(57, 20);
			this.tsmiWindow.Text = "&Window";
			// 
			// tsmiCloseAllDocuments
			// 
			this.tsmiCloseAllDocuments.Name = "tsmiCloseAllDocuments";
			this.tsmiCloseAllDocuments.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
			this.tsmiCloseAllDocuments.Size = new System.Drawing.Size(263, 22);
			this.tsmiCloseAllDocuments.Text = "&Close All Documents";
			this.tsmiCloseAllDocuments.Click += new System.EventHandler(this.tsmiCloseAllDocuments_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(260, 6);
			// 
			// tsmiDocumentWindows
			// 
			this.tsmiDocumentWindows.Name = "tsmiDocumentWindows";
			this.tsmiDocumentWindows.Size = new System.Drawing.Size(263, 22);
			this.tsmiDocumentWindows.Text = "Document Windows";
			// 
			// tsmiHelp
			// 
			this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTopics,
            this.tsmiAbout});
			this.tsmiHelp.Name = "tsmiHelp";
			this.tsmiHelp.Size = new System.Drawing.Size(40, 20);
			this.tsmiHelp.Text = "&Help";
			// 
			// tsmiTopics
			// 
			this.tsmiTopics.Name = "tsmiTopics";
			this.tsmiTopics.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.tsmiTopics.Size = new System.Drawing.Size(128, 22);
			this.tsmiTopics.Text = "&Topics";
			this.tsmiTopics.Click += new System.EventHandler(this.tsmiTopics_Click);
			// 
			// tsmiAbout
			// 
			this.tsmiAbout.Name = "tsmiAbout";
			this.tsmiAbout.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.tsmiAbout.Size = new System.Drawing.Size(128, 22);
			this.tsmiAbout.Text = "&About";
			this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
			// 
			// ofdMain
			// 
			this.ofdMain.Filter = "All files|*.*";
			this.ofdMain.RestoreDirectory = true;
			this.ofdMain.Title = "Open Document...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(866, 539);
			this.Controls.Add(this.ssMain);
			this.Controls.Add(this.msMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.msMain;
			this.Name = "MainForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ssMain.ResumeLayout(false);
			this.ssMain.PerformLayout();
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip ssMain;
		private System.Windows.Forms.MenuStrip msMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiNew;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewDocument;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenDocument;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmiTopics;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseAllDocuments;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiDocumentWindows;
		private System.Windows.Forms.ToolStripStatusLabel tsslMain;
		private System.Windows.Forms.OpenFileDialog ofdMain;
	}
}

