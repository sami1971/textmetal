/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TextMetal.Core.SourceModel.SqlServer
{
	[Serializable]
	public class Table
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the Table class.
		/// </summary>
		public Table()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<Column> columns = new List<Column>();
		private readonly List<ForeignKey> foreignKeys = new List<ForeignKey>();
		private readonly List<UniqueKey> uniqueKeys = new List<UniqueKey>();
		private bool isView;
		private string tableName;
		private string tableNameCamelCase;
		private string tableNameConstantCase;
		private string tableNamePascalCase;
		private string tableNamePluralCamelCase;
		private string tableNamePluralConstantCase;
		private string tableNamePluralPascalCase;
		private string tableNameSingularCamelCase;
		private string tableNameSingularConstantCase;
		private string tableNameSingularPascalCase;

		#endregion

		#region Properties/Indexers/Events

		[XmlArray(ElementName = "Columns")]
		[XmlArrayItem(ElementName = "Column")]
		public List<Column> Columns
		{
			get
			{
				return this.columns;
			}
		}

		[XmlArray(ElementName = "ForeignKeys")]
		[XmlArrayItem(ElementName = "ForeignKey")]
		public List<ForeignKey> ForeignKeys
		{
			get
			{
				return this.foreignKeys;
			}
		}

		[XmlIgnore]
		public bool HasIdentityColumns
		{
			get
			{
				return this.Columns.Count(c => c.ColumnIsIdentity) > 0;
			}
		}

		[XmlAttribute]
		public bool IsView
		{
			get
			{
				return this.isView;
			}
			set
			{
				this.isView = value;
			}
		}

		[XmlAttribute]
		public string TableName
		{
			get
			{
				return this.tableName;
			}
			set
			{
				this.tableName = value;
			}
		}

		[XmlAttribute]
		public string TableNameCamelCase
		{
			get
			{
				return this.tableNameCamelCase;
			}
			set
			{
				this.tableNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string TableNameConstantCase
		{
			get
			{
				return this.tableNameConstantCase;
			}
			set
			{
				this.tableNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string TableNamePascalCase
		{
			get
			{
				return this.tableNamePascalCase;
			}
			set
			{
				this.tableNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string TableNamePluralCamelCase
		{
			get
			{
				return this.tableNamePluralCamelCase;
			}
			set
			{
				this.tableNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string TableNamePluralConstantCase
		{
			get
			{
				return this.tableNamePluralConstantCase;
			}
			set
			{
				this.tableNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string TableNamePluralPascalCase
		{
			get
			{
				return this.tableNamePluralPascalCase;
			}
			set
			{
				this.tableNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string TableNameSingularCamelCase
		{
			get
			{
				return this.tableNameSingularCamelCase;
			}
			set
			{
				this.tableNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string TableNameSingularConstantCase
		{
			get
			{
				return this.tableNameSingularConstantCase;
			}
			set
			{
				this.tableNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string TableNameSingularPascalCase
		{
			get
			{
				return this.tableNameSingularPascalCase;
			}
			set
			{
				this.tableNameSingularPascalCase = value;
			}
		}

		[XmlArray(ElementName = "UniqueKeys")]
		[XmlArrayItem(ElementName = "UniqueKey")]
		public List<UniqueKey> UniqueKeys
		{
			get
			{
				return this.uniqueKeys;
			}
		}

		#endregion
	}
}