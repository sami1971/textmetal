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
	public class Parameter
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the Parameter class.
		/// </summary>
		public Parameter()
		{
		}

		#endregion

		#region Fields/Constants

		private string parameterCSharpClrNonNullableType;
		private string parameterCSharpClrNullableType;
		private string parameterCSharpClrType;
		private string parameterCSharpDbType;
		private string parameterCSharpNullableLiteral;
		private Type parameterClrNonNullableType;
		private Type parameterClrNullableType;
		private Type parameterClrType;
		private DbType parameterDbType;
		private string parameterDefaultValue;
		private ParameterDirection parameterDirection;
		private bool parameterIsCursorRef;
		private bool parameterIsOutput;
		private bool parameterIsReadOnly;
		private bool parameterIsResultColumn;
		private bool parameterIsReturnValue;
		private string parameterName;
		private string parameterNameCamelCase;
		private string parameterNameConstantCase;
		private string parameterNamePascalCase;
		private string parameterNamePluralCamelCase;
		private string parameterNamePluralConstantCase;
		private string parameterNamePluralPascalCase;
		private string parameterNameSingularCamelCase;
		private string parameterNameSingularConstantCase;
		private string parameterNameSingularPascalCase;
		private bool parameterNullable;
		private int parameterOrdinal;
		private int parameterPrecision;
		private string parameterPrefix;
		private int parameterScale;
		private int parameterSize;
		private string parameterSqlType;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public string ParameterCSharpClrNonNullableType
		{
			get
			{
				return this.parameterCSharpClrNonNullableType;
			}
			set
			{
				this.parameterCSharpClrNonNullableType = value;
			}
		}

		[XmlAttribute]
		public string ParameterCSharpClrNullableType
		{
			get
			{
				return this.parameterCSharpClrNullableType;
			}
			set
			{
				this.parameterCSharpClrNullableType = value;
			}
		}

		[XmlAttribute]
		public string ParameterCSharpClrType
		{
			get
			{
				return this.parameterCSharpClrType;
			}
			set
			{
				this.parameterCSharpClrType = value;
			}
		}

		[XmlAttribute]
		public string ParameterCSharpDbType
		{
			get
			{
				return this.parameterCSharpDbType;
			}
			set
			{
				this.parameterCSharpDbType = value;
			}
		}

		[XmlAttribute]
		public string ParameterCSharpNullableLiteral
		{
			get
			{
				return this.parameterCSharpNullableLiteral;
			}
			set
			{
				this.parameterCSharpNullableLiteral = value;
			}
		}

		[XmlIgnore]
		public Type ParameterClrNonNullableType
		{
			get
			{
				return this.parameterClrNonNullableType;
			}
			set
			{
				this.parameterClrNonNullableType = value;
			}
		}

		[XmlIgnore]
		public Type ParameterClrNullableType
		{
			get
			{
				return this.parameterClrNullableType;
			}
			set
			{
				this.parameterClrNullableType = value;
			}
		}

		[XmlIgnore]
		public Type ParameterClrType
		{
			get
			{
				return this.parameterClrType;
			}
			set
			{
				this.parameterClrType = value;
			}
		}

		[XmlAttribute]
		public DbType ParameterDbType
		{
			get
			{
				return this.parameterDbType;
			}
			set
			{
				this.parameterDbType = value;
			}
		}

		[XmlAttribute]
		public string ParameterDefaultValue
		{
			get
			{
				return this.parameterDefaultValue;
			}
			set
			{
				this.parameterDefaultValue = value;
			}
		}

		[XmlAttribute]
		public ParameterDirection ParameterDirection
		{
			get
			{
				return this.parameterDirection;
			}
			set
			{
				this.parameterDirection = value;
			}
		}

		[XmlAttribute]
		public bool ParameterIsCursorRef
		{
			get
			{
				return this.parameterIsCursorRef;
			}
			set
			{
				this.parameterIsCursorRef = value;
			}
		}

		[XmlAttribute]
		public bool ParameterIsOutput
		{
			get
			{
				return this.parameterIsOutput;
			}
			set
			{
				this.parameterIsOutput = value;
			}
		}

		[XmlAttribute]
		public bool ParameterIsReadOnly
		{
			get
			{
				return this.parameterIsReadOnly;
			}
			set
			{
				this.parameterIsReadOnly = value;
			}
		}

		[XmlAttribute]
		public bool ParameterIsResultColumn
		{
			get
			{
				return this.parameterIsResultColumn;
			}
			set
			{
				this.parameterIsResultColumn = value;
			}
		}

		[XmlAttribute]
		public bool ParameterIsReturnValue
		{
			get
			{
				return this.parameterIsReturnValue;
			}
			set
			{
				this.parameterIsReturnValue = value;
			}
		}

		[XmlAttribute]
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
			set
			{
				this.parameterName = value;
			}
		}

		[XmlAttribute]
		public string ParameterNameCamelCase
		{
			get
			{
				return this.parameterNameCamelCase;
			}
			set
			{
				this.parameterNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNameConstantCase
		{
			get
			{
				return this.parameterNameConstantCase;
			}
			set
			{
				this.parameterNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNamePascalCase
		{
			get
			{
				return this.parameterNamePascalCase;
			}
			set
			{
				this.parameterNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNamePluralCamelCase
		{
			get
			{
				return this.parameterNamePluralCamelCase;
			}
			set
			{
				this.parameterNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNamePluralConstantCase
		{
			get
			{
				return this.parameterNamePluralConstantCase;
			}
			set
			{
				this.parameterNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNamePluralPascalCase
		{
			get
			{
				return this.parameterNamePluralPascalCase;
			}
			set
			{
				this.parameterNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNameSingularCamelCase
		{
			get
			{
				return this.parameterNameSingularCamelCase;
			}
			set
			{
				this.parameterNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNameSingularConstantCase
		{
			get
			{
				return this.parameterNameSingularConstantCase;
			}
			set
			{
				this.parameterNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ParameterNameSingularPascalCase
		{
			get
			{
				return this.parameterNameSingularPascalCase;
			}
			set
			{
				this.parameterNameSingularPascalCase = value;
			}
		}

		[XmlAttribute]
		public bool ParameterNullable
		{
			get
			{
				return this.parameterNullable;
			}
			set
			{
				this.parameterNullable = value;
			}
		}

		[XmlAttribute]
		public int ParameterOrdinal
		{
			get
			{
				return this.parameterOrdinal;
			}
			set
			{
				this.parameterOrdinal = value;
			}
		}

		[XmlAttribute]
		public int ParameterPrecision
		{
			get
			{
				return this.parameterPrecision;
			}
			set
			{
				this.parameterPrecision = value;
			}
		}

		[XmlAttribute]
		public string ParameterPrefix
		{
			get
			{
				return this.parameterPrefix;
			}
			set
			{
				this.parameterPrefix = value;
			}
		}

		[XmlAttribute]
		public int ParameterScale
		{
			get
			{
				return this.parameterScale;
			}
			set
			{
				this.parameterScale = value;
			}
		}

		[XmlAttribute]
		public int ParameterSize
		{
			get
			{
				return this.parameterSize;
			}
			set
			{
				this.parameterSize = value;
			}
		}

		[XmlAttribute]
		public string ParameterSqlType
		{
			get
			{
				return this.parameterSqlType;
			}
			set
			{
				this.parameterSqlType = value;
			}
		}

		[XmlAttribute("ParameterClrNonNullableType")]
		public string _ParameterClrNonNullableType
		{
			get
			{
				return this.ParameterClrNonNullableType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ParameterClrNonNullableType = null;
				else
					this.ParameterClrNonNullableType = Type.GetType(value, false);
			}
		}

		[XmlAttribute("ParameterClrNullableType")]
		public string _ParameterClrNullableType
		{
			get
			{
				return this.ParameterClrNullableType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ParameterClrNullableType = null;
				else
					this.ParameterClrNullableType = Type.GetType(value, false);
			}
		}

		[XmlAttribute("ParameterClrType")]
		public string _ParameterClrType
		{
			get
			{
				return this.ParameterClrType.SafeToString();
			}
			set
			{
				if (DataType.IsNullOrWhiteSpace(value))
					this.ParameterClrType = null;
				else
					this.ParameterClrType = Type.GetType(value, false);
			}
		}

		#endregion

		#region Methods/Operators

		public override int GetHashCode()
		{
			return this.ParameterName.SafeToString().GetHashCode();
		}

		#endregion
	}
}