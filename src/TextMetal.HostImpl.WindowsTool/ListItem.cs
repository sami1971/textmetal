﻿/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.HostImpl.WindowsTool
{
	/// <summary>
	/// Represents a list item.
	/// </summary>
	[Serializable]
	public class ListItem : IListItem
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ListItem class.
		/// </summary>
		/// <param name="value"> The value of the list item. </param>
		/// <param name="text"> The text of the list item. </param>
		public ListItem(object value, string text)
		{
			this.value = value;
			this.text = text;
		}

		public ListItem()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly ListItem empty = new ListItem(null, null);
		private string text;
		private object value;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the singleton 'empty' instance.
		/// </summary>
		public static ListItem Empty
		{
			get
			{
				return empty;
			}
		}

		/// <summary>
		/// Gets the list item text.
		/// </summary>
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>
		/// Gets the list item value.
		/// </summary>
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
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