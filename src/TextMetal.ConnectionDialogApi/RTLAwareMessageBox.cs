//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	internal sealed class RTLAwareMessageBox
	{
		#region Constructors/Destructors

		private RTLAwareMessageBox()
		{
		}

		#endregion

		#region Methods/Operators

		public static DialogResult Show(string caption, string text, MessageBoxIcon icon)
		{
			MessageBoxOptions options = 0;
			if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
				options = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
			return MessageBox.Show(text, caption, MessageBoxButtons.OK, icon, MessageBoxDefaultButton.Button1, options);
		}

		#endregion
	}
}