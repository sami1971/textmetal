/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Controls
{
	public class TmListViewItem : ListViewItem
	{
		#region Constructors/Destructors

		public TmListViewItem(string[] strings)
			: base(Clean(strings))
		{
		}

		#endregion

		#region Methods/Operators

		private static string[] Clean(string[] strings)
		{
			if ((object)strings == null)
				throw new ArgumentNullException("strings");

			for (int i = 0; i < strings.Length; i++)
				strings[i] = (strings[i] ?? "").Trim().ToUpper();

			return strings;
		}

		#endregion
	}
}