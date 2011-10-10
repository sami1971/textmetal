﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Xml.Serialization;

namespace TextMetal.Core.SourceModel.SqlServer
{
	[Serializable]
	public class UniqueKeyColumnRef
	{
		#region Constructors/Destructors

		public UniqueKeyColumnRef()
		{
		}

		#endregion

		#region Fields/Constants

		private bool uniqueKeyColumnDescendingSort;
		private byte uniqueKeyColumnOrdinal;
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
		public byte UniqueKeyColumnOrdinal
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