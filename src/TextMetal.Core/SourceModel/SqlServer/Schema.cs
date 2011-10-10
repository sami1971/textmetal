/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextMetal.Core.SourceModel.SqlServer
{
	[Serializable]
	public class Schema
	{
		#region Constructors/Destructors

		public Schema()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<Procedure> procedures = new List<Procedure>();
		private readonly List<Table> tables = new List<Table>();
		private string schemaName;
		private string schemaNameCamelCase;
		private string schemaNameConstantCase;
		private string schemaNamePascalCase;
		private string schemaNamePluralCamelCase;
		private string schemaNamePluralConstantCase;
		private string schemaNamePluralPascalCase;
		private string schemaNameSingularCamelCase;
		private string schemaNameSingularConstantCase;
		private string schemaNameSingularPascalCase;

		#endregion

		#region Properties/Indexers/Events

		[XmlArray(ElementName = "Procedures")]
		[XmlArrayItem(ElementName = "Procedure")]
		public List<Procedure> Procedures
		{
			get
			{
				return this.procedures;
			}
		}

		[XmlAttribute]
		public string SchemaName
		{
			get
			{
				return this.schemaName;
			}
			set
			{
				this.schemaName = value;
			}
		}

		[XmlAttribute]
		public string SchemaNameCamelCase
		{
			get
			{
				return this.schemaNameCamelCase;
			}
			set
			{
				this.schemaNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNameConstantCase
		{
			get
			{
				return this.schemaNameConstantCase;
			}
			set
			{
				this.schemaNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNamePascalCase
		{
			get
			{
				return this.schemaNamePascalCase;
			}
			set
			{
				this.schemaNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNamePluralCamelCase
		{
			get
			{
				return this.schemaNamePluralCamelCase;
			}
			set
			{
				this.schemaNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNamePluralConstantCase
		{
			get
			{
				return this.schemaNamePluralConstantCase;
			}
			set
			{
				this.schemaNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNamePluralPascalCase
		{
			get
			{
				return this.schemaNamePluralPascalCase;
			}
			set
			{
				this.schemaNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNameSingularCamelCase
		{
			get
			{
				return this.schemaNameSingularCamelCase;
			}
			set
			{
				this.schemaNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNameSingularConstantCase
		{
			get
			{
				return this.schemaNameSingularConstantCase;
			}
			set
			{
				this.schemaNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string SchemaNameSingularPascalCase
		{
			get
			{
				return this.schemaNameSingularPascalCase;
			}
			set
			{
				this.schemaNameSingularPascalCase = value;
			}
		}

		[XmlArray(ElementName = "Tables")]
		[XmlArrayItem(ElementName = "Table")]
		public List<Table> Tables
		{
			get
			{
				return this.tables;
			}
		}

		#endregion
	}
}