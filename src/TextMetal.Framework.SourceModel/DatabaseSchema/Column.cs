/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;
using System.Xml.Serialization;

using TextMetal.Common.Core;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	[Serializable]
	public class Column
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the Column class.
		/// </summary>
		public Column()
		{
		}

		#endregion

		#region Fields/Constants

		private string columnCSharpClrNonNullableType;
		private string columnCSharpClrNullableType;
		private string columnCSharpClrType;
		private string columnCSharpDbType;
		private string columnCSharpNullableLiteral;
		private Type columnClrNonNullableType;
		private Type columnClrNullableType;
		private Type columnClrType;
		private DbType columnDbType;
		private bool columnHasCheck;
		private bool columnHasDefault;
		private bool columnIsComputed;
		private bool columnIsIdentity;
		private bool columnIsPrimaryKey;
		private string columnName;
		private string columnNameCamelCase;
		private string columnNameConstantCase;
		private string columnNamePascalCase;
		private string columnNamePluralCamelCase;
		private string columnNamePluralConstantCase;
		private string columnNamePluralPascalCase;
		private string columnNameSingularCamelCase;
		private string columnNameSingularConstantCase;
		private string columnNameSingularPascalCase;
		private bool columnNullable;
		private int columnOrdinal;
		private int columnPrecision;
		private int columnScale;
		private int columnSize;
		private string columnSqlType;
		private int primaryKeyColumnOrdinal;
		private string primaryKeyName;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public string ColumnCSharpClrNonNullableType
		{
			get
			{
				return this.columnCSharpClrNonNullableType;
			}
			set
			{
				this.columnCSharpClrNonNullableType = value;
			}
		}

		[XmlAttribute]
		public string ColumnCSharpClrNullableType
		{
			get
			{
				return this.columnCSharpClrNullableType;
			}
			set
			{
				this.columnCSharpClrNullableType = value;
			}
		}

		[XmlAttribute]
		public string ColumnCSharpClrType
		{
			get
			{
				return this.columnCSharpClrType;
			}
			set
			{
				this.columnCSharpClrType = value;
			}
		}

		[XmlAttribute]
		public string ColumnCSharpDbType
		{
			get
			{
				return this.columnCSharpDbType;
			}
			set
			{
				this.columnCSharpDbType = value;
			}
		}

		[XmlAttribute]
		public string ColumnCSharpNullableLiteral
		{
			get
			{
				return this.columnCSharpNullableLiteral;
			}
			set
			{
				this.columnCSharpNullableLiteral = value;
			}
		}

		[XmlIgnore]
		public Type ColumnClrNonNullableType
		{
			get
			{
				return this.columnClrNonNullableType;
			}
			set
			{
				this.columnClrNonNullableType = value;
			}
		}

		[XmlIgnore]
		public Type ColumnClrNullableType
		{
			get
			{
				return this.columnClrNullableType;
			}
			set
			{
				this.columnClrNullableType = value;
			}
		}

		[XmlIgnore]
		public Type ColumnClrType
		{
			get
			{
				return this.columnClrType;
			}
			set
			{
				this.columnClrType = value;
			}
		}

		[XmlAttribute]
		public DbType ColumnDbType
		{
			get
			{
				return this.columnDbType;
			}
			set
			{
				this.columnDbType = value;
			}
		}

		[XmlAttribute]
		public bool ColumnHasCheck
		{
			get
			{
				return this.columnHasCheck;
			}
			set
			{
				this.columnHasCheck = value;
			}
		}

		[XmlAttribute]
		public bool ColumnHasDefault
		{
			get
			{
				return this.columnHasDefault;
			}
			set
			{
				this.columnHasDefault = value;
			}
		}

		[XmlAttribute]
		public bool ColumnIsComputed
		{
			get
			{
				return this.columnIsComputed;
			}
			set
			{
				this.columnIsComputed = value;
			}
		}

		[XmlAttribute]
		public bool ColumnIsIdentity
		{
			get
			{
				return this.columnIsIdentity;
			}
			set
			{
				this.columnIsIdentity = value;
			}
		}

		[XmlAttribute]
		public bool ColumnIsPrimaryKey
		{
			get
			{
				return this.columnIsPrimaryKey;
			}
			set
			{
				this.columnIsPrimaryKey = value;
			}
		}

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
		public string ColumnNameCamelCase
		{
			get
			{
				return this.columnNameCamelCase;
			}
			set
			{
				this.columnNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNameConstantCase
		{
			get
			{
				return this.columnNameConstantCase;
			}
			set
			{
				this.columnNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNamePascalCase
		{
			get
			{
				return this.columnNamePascalCase;
			}
			set
			{
				this.columnNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNamePluralCamelCase
		{
			get
			{
				return this.columnNamePluralCamelCase;
			}
			set
			{
				this.columnNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNamePluralConstantCase
		{
			get
			{
				return this.columnNamePluralConstantCase;
			}
			set
			{
				this.columnNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNamePluralPascalCase
		{
			get
			{
				return this.columnNamePluralPascalCase;
			}
			set
			{
				this.columnNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNameSingularCamelCase
		{
			get
			{
				return this.columnNameSingularCamelCase;
			}
			set
			{
				this.columnNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNameSingularConstantCase
		{
			get
			{
				return this.columnNameSingularConstantCase;
			}
			set
			{
				this.columnNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ColumnNameSingularPascalCase
		{
			get
			{
				return this.columnNameSingularPascalCase;
			}
			set
			{
				this.columnNameSingularPascalCase = value;
			}
		}

		[XmlAttribute]
		public bool ColumnNullable
		{
			get
			{
				return this.columnNullable;
			}
			set
			{
				this.columnNullable = value;
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
		public int ColumnPrecision
		{
			get
			{
				return this.columnPrecision;
			}
			set
			{
				this.columnPrecision = value;
			}
		}

		[XmlAttribute]
		public int ColumnScale
		{
			get
			{
				return this.columnScale;
			}
			set
			{
				this.columnScale = value;
			}
		}

		[XmlAttribute]
		public int ColumnSize
		{
			get
			{
				return this.columnSize;
			}
			set
			{
				this.columnSize = value;
			}
		}

		[XmlAttribute]
		public string ColumnSqlType
		{
			get
			{
				return this.columnSqlType;
			}
			set
			{
				this.columnSqlType = value;
			}
		}

		public bool IsColumnServerGeneratedPrimaryKey
		{
			get
			{
				return this.ColumnIsPrimaryKey && this.ColumnIsIdentity;
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

		[XmlAttribute("ColumnClrNonNullableType")]
		public string _ColumnClrNotNullableType
		{
			get
			{
				return this.ColumnClrNonNullableType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ColumnClrNonNullableType = null;
				else
					this.ColumnClrNonNullableType = Type.GetType(value, false);
			}
		}

		[XmlAttribute("ColumnClrNullableType")]
		public string _ColumnClrNullableType
		{
			get
			{
				return this.ColumnClrNullableType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ColumnClrNullableType = null;
				else
					this.ColumnClrNullableType = Type.GetType(value, false);
			}
		}

		[XmlAttribute("ColumnClrType")]
		public string _ColumnClrType
		{
			get
			{
				return this.ColumnClrType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ColumnClrType = null;
				else
					this.ColumnClrType = Type.GetType(value, false);
			}
		}

		#endregion
	}
}