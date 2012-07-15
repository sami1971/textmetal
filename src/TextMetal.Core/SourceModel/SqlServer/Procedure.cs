/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextMetal.Core.SourceModel.SqlServer
{
	[Serializable]
	public class Procedure
	{
		#region Constructors/Destructors

		public Procedure()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<Column> columns = new List<Column>();
		private readonly List<Parameter> parameters = new List<Parameter>();
		private string procedureName;
		private string procedureNameCamelCase;
		private string procedureNameConstantCase;
		private string procedureNamePascalCase;
		private string procedureNamePluralCamelCase;
		private string procedureNamePluralConstantCase;
		private string procedureNamePluralPascalCase;
		private string procedureNameSingularCamelCase;
		private string procedureNameSingularConstantCase;
		private string procedureNameSingularPascalCase;

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

		[XmlIgnore]
		public bool HasResults
		{
			get
			{
				return this.Columns.Count > 0;
			}
		}

		[XmlArray(ElementName = "Parameters")]
		[XmlArrayItem(ElementName = "Parameter")]
		public List<Parameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		[XmlAttribute]
		public string ProcedureName
		{
			get
			{
				return this.procedureName;
			}
			set
			{
				this.procedureName = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNameCamelCase
		{
			get
			{
				return this.procedureNameCamelCase;
			}
			set
			{
				this.procedureNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNameConstantCase
		{
			get
			{
				return this.procedureNameConstantCase;
			}
			set
			{
				this.procedureNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNamePascalCase
		{
			get
			{
				return this.procedureNamePascalCase;
			}
			set
			{
				this.procedureNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNamePluralCamelCase
		{
			get
			{
				return this.procedureNamePluralCamelCase;
			}
			set
			{
				this.procedureNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNamePluralConstantCase
		{
			get
			{
				return this.procedureNamePluralConstantCase;
			}
			set
			{
				this.procedureNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNamePluralPascalCase
		{
			get
			{
				return this.procedureNamePluralPascalCase;
			}
			set
			{
				this.procedureNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNameSingularCamelCase
		{
			get
			{
				return this.procedureNameSingularCamelCase;
			}
			set
			{
				this.procedureNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNameSingularConstantCase
		{
			get
			{
				return this.procedureNameSingularConstantCase;
			}
			set
			{
				this.procedureNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ProcedureNameSingularPascalCase
		{
			get
			{
				return this.procedureNameSingularPascalCase;
			}
			set
			{
				this.procedureNameSingularPascalCase = value;
			}
		}

		#endregion
	}
}