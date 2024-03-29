﻿/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.HostImpl.WindowsTool
{
	/// <summary>
	/// Represents a list item with a strongly typed value.
	/// </summary>
	[Serializable]
	public class ListItem<TValue> : ListItem, IListItem<TValue>
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ListItem`1 class.
		/// </summary>
		/// <param name="value"> The value of the list item. </param>
		/// <param name="text"> The text of the list item. </param>
		public ListItem(TValue value, string text)
			: base(value, text)
		{
		}

		public ListItem()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly ListItem<TValue> empty = new ListItem<TValue>(default(TValue), null);

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the singleton 'empty' instance.
		/// </summary>
		public new static ListItem<TValue> Empty
		{
			get
			{
				return empty;
			}
		}

		/// <summary>
		/// Gets the list item value.
		/// </summary>
		public new TValue Value
		{
			get
			{
				return (TValue)base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		public override string ToString()
		{
			return this.Text;
		}

		#endregion
	}
}