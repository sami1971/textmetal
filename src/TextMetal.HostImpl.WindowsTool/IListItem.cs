/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.HostImpl.WindowsTool
{
	/// <summary>
	///     Represents a list item.
	/// </summary>
	public interface IListItem
	{
		#region Properties/Indexers/Events

		/// <summary>
		///     Gets the list item text.
		/// </summary>
		string Text
		{
			get;
			set;
		}

		/// <summary>
		///     Gets the list item value.
		/// </summary>
		object Value
		{
			get;
			set;
		}

		#endregion
	}
}