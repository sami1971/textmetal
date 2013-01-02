/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Xml.Serialization;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	[Serializable]
	public class UniqueKeyColumnRef
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the UniqueKeyColumnRef class.
		/// </summary>
		public UniqueKeyColumnRef()
		{
		}

		#endregion

		#region Fields/Constants

		private string columnName;
		private int columnOrdinal;

		private bool uniqueKeyColumnDescendingSort;
		private int uniqueKeyOrdinal;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
			set
			{
				this.columnName = value;
			}
		}

		[XmlAttribute]
		public int ColumnOrdinal
		{
			get
			{
				return this.columnOrdinal;
			}
			set
			{
				this.columnOrdinal = value;
			}
		}

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
		public int UniqueKeyOrdinal
		{
			get
			{
				return this.uniqueKeyOrdinal;
			}
			set
			{
				this.uniqueKeyOrdinal = value;
			}
		}

		#endregion
	}
}