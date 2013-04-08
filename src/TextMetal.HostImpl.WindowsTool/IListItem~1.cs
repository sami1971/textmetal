/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.HostImpl.WindowsTool
{
	/// <summary>
	/// Represents a list item with a strongly typed value.
	/// </summary>
	/// <typeparam name="TValue"> The type of the list item value. </typeparam>
	public interface IListItem<TValue> : IListItem
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the list item value.
		/// </summary>
		new TValue Value
		{
			get;
			set;
		}

		#endregion
	}
}