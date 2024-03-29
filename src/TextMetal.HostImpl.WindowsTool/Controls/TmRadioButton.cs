﻿/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Controls
{
	public class TmRadioButton : RadioButton
	{
		#region Constructors/Destructors

		public TmRadioButton()
		{
			this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
		}

		#endregion

		#region Properties/Indexers/Events

		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public new event MouseEventHandler MouseDoubleClick;

		#endregion

		#region Methods/Operators

		protected override void OnCheckedChanged(EventArgs e)
		{
			base.OnCheckedChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			this.CoreSetParentFormDirty(true);

			if ((object)this.MouseDoubleClick != null)
				this.MouseDoubleClick(this, e);
		}

		#endregion
	}
}