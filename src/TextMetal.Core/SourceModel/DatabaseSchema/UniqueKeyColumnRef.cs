/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Xml.Serialization;

namespace TextMetal.Core.SourceModel.DatabaseSchema
{
	[Serializable]
	public class UniqueKeyColumnRef
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the UniqueKeyColumnRef class.
		/// </summary>
		public UniqueKeyColumnRef()
		{
		}

		#endregion

		#region Fields/Constants

		private bool uniqueKeyColumnDescendingSort;
		private int uniqueKeyColumnOrdinal;
		private int uniqueKeyParentColumnOrdinal;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public bool UniqueKeyColumnDescendingSort
		{
			get
			{
				return this.uniqueKeyColumnDescendingSort;
			}
			set
			{
				this.uniqueKeyColumnDescendingSort = value;
			}
		}

		[XmlAttribute]
		public int UniqueKeyColumnOrdinal
		{
			get
			{
				return this.uniqueKeyColumnOrdinal;
			}
			set
			{
				this.uniqueKeyColumnOrdinal = value;
			}
		}

		[XmlAttribute]
		public int UniqueKeyParentColumnOrdinal
		{
			get
			{
				return this.uniqueKeyParentColumnOrdinal;
			}
			set
			{
				this.uniqueKeyParentColumnOrdinal = value;
			}
		}

		#endregion
	}
}