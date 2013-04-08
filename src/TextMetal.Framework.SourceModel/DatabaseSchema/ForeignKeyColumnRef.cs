/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Xml.Serialization;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	[Serializable]
	public class ForeignKeyColumnRef
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ForeignKeyColumnRef class.
		/// </summary>
		public ForeignKeyColumnRef()
		{
		}

		#endregion

		#region Fields/Constants

		private string columnName;
		private int columnOrdinal;
		private int foreignKeyOrdinal;
		private string primaryKeyColumnName;
		private int primaryKeyColumnOrdinal;
		private string primaryKeyName;
		private int primaryKeyOrdinal;
		private string primarySchemaName;
		private string primaryTableName;

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
		public int ForeignKeyOrdinal
		{
			get
			{
				return this.foreignKeyOrdinal;
			}
			set
			{
				this.foreignKeyOrdinal = value;
			}
		}

		[XmlAttribute]
		public string PrimaryKeyColumnName
		{
			get
			{
				return this.primaryKeyColumnName;
			}
			set
			{
				this.primaryKeyColumnName = value;
			}
		}

		[XmlAttribute]
		public int PrimaryKeyColumnOrdinal
		{
			get
			{
				return this.primaryKeyColumnOrdinal;
			}
			set
			{
				this.primaryKeyColumnOrdinal = value;
			}
		}

		[XmlAttribute]
		public string PrimaryKeyName
		{
			get
			{
				return this.primaryKeyName;
			}
			set
			{
				this.primaryKeyName = value;
			}
		}

		[XmlAttribute]
		public int PrimaryKeyOrdinal
		{
			get
			{
				return this.primaryKeyOrdinal;
			}
			set
			{
				this.primaryKeyOrdinal = value;
			}
		}

		[XmlAttribute]
		public string PrimarySchemaName
		{
			get
			{
				return this.primarySchemaName;
			}
			set
			{
				this.primarySchemaName = value;
			}
		}

		[XmlAttribute]
		public string PrimaryTableName
		{
			get
			{
				return this.primaryTableName;
			}
			set
			{
				this.primaryTableName = value;
			}
		}

		#endregion
	}
}