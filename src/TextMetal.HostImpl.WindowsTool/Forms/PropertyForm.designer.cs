/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class PropertyForm
	{
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
					this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pgShape = new PropertyGrid();
			this.SuspendLayout();
			// 
			// pgShape
			// 
			this.pgShape.CommandsVisibleIfAvailable = true;
			this.pgShape.Dock = DockStyle.Fill;
			this.pgShape.LargeButtons = false;
			this.pgShape.LineColor = SystemColors.ScrollBar;
			this.pgShape.Location = new Point(0, 0);
			this.pgShape.Name = "pgShape";
			this.pgShape.PropertySort = PropertySort.Alphabetical;
			this.pgShape.Size = new Size(200, 328);
			this.pgShape.TabIndex = 0;
			this.pgShape.Text = "PropertyGrid";
			this.pgShape.ToolbarVisible = false;
			this.pgShape.ViewBackColor = SystemColors.Window;
			this.pgShape.ViewForeColor = SystemColors.WindowText;
			this.pgShape.PropertyValueChanged += new PropertyValueChangedEventHandler(this.pgShape_PropertyValueChanged);
			// 
			// SketchShapeBasePropertiesForm
			// 
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(200, 328);
			this.Controls.Add(this.pgShape);
			this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			this.Name = "PropertyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Sketch Shape Properties";
			this.Load += new EventHandler(this.PropertyForm_Load);
			this.ResumeLayout(false);
		}


		private Container components = null;
		private PropertyGrid pgShape;
	}
}