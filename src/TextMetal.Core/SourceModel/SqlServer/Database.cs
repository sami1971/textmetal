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
	[XmlRoot(ElementName = "Database", Namespace = "http://www.textmetal.com/api/v4.4.0")]
	public class Database
	{
		#region Constructors/Destructors

		public Database()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<Schema> schemas = new List<Schema>();
		private string connectionString;
		private string connectionType;
		private string initialCatalogName;
		private string initialCatalogNameCamelCase;
		private string initialCatalogNameConstantCase;
		private string initialCatalogNamePascalCase;
		private string initialCatalogNamePluralCamelCase;
		private string initialCatalogNamePluralConstantCase;
		private string initialCatalogNamePluralPascalCase;
		private string initialCatalogNameSingularCamelCase;
		private string initialCatalogNameSingularConstantCase;
		private string initialCatalogNameSingularPascalCase;
		private string instanceName;
		private string machineName;
		private string serverEdition;
		private string serverLevel;
		private string serverVersion;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		[XmlAttribute]
		public string ConnectionType
		{
			get
			{
				return this.connectionType;
			}
			set
			{
				this.connectionType = value;
			}
		}

		[XmlIgnore]
		public string DatabaseName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.MachineName) &&
				    !string.IsNullOrEmpty(this.MachineName))
					return string.Format("{0}\\{1}", this.MachineName, this.InstanceName);
				else
					return this.MachineName;
			}
		}

		[XmlIgnore]
		public bool HasProcedures
		{
			get
			{
				return this.Schemas.Count(s => s.Procedures.Count() > 0) > 0;
			}
		}

		[XmlIgnore]
		public bool HasTables
		{
			get
			{
				return this.Schemas.Count(s => s.Tables.Count(t => !t.IsView) > 0) > 0;
			}
		}

		[XmlIgnore]
		public bool HasViews
		{
			get
			{
				return this.Schemas.Count(s => s.Tables.Count(t => t.IsView) > 0) > 0;
			}
		}

		[XmlAttribute]
		public string InitialCatalogName
		{
			get
			{
				return this.initialCatalogName;
			}
			set
			{
				this.initialCatalogName = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNameCamelCase
		{
			get
			{
				return this.initialCatalogNameCamelCase;
			}
			set
			{
				this.initialCatalogNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNameConstantCase
		{
			get
			{
				return this.initialCatalogNameConstantCase;
			}
			set
			{
				this.initialCatalogNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNamePascalCase
		{
			get
			{
				return this.initialCatalogNamePascalCase;
			}
			set
			{
				this.initialCatalogNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNamePluralCamelCase
		{
			get
			{
				return this.initialCatalogNamePluralCamelCase;
			}
			set
			{
				this.initialCatalogNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNamePluralConstantCase
		{
			get
			{
				return this.initialCatalogNamePluralConstantCase;
			}
			set
			{
				this.initialCatalogNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNamePluralPascalCase
		{
			get
			{
				return this.initialCatalogNamePluralPascalCase;
			}
			set
			{
				this.initialCatalogNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNameSingularCamelCase
		{
			get
			{
				return this.initialCatalogNameSingularCamelCase;
			}
			set
			{
				this.initialCatalogNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNameSingularConstantCase
		{
			get
			{
				return this.initialCatalogNameSingularConstantCase;
			}
			set
			{
				this.initialCatalogNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string InitialCatalogNameSingularPascalCase
		{
			get
			{
				return this.initialCatalogNameSingularPascalCase;
			}
			set
			{
				this.initialCatalogNameSingularPascalCase = value;
			}
		}

		[XmlAttribute]
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
			set
			{
				this.instanceName = value;
			}
		}

		[XmlAttribute]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				this.machineName = value;
			}
		}

		[XmlArray(ElementName = "Schemas")]
		[XmlArrayItem(ElementName = "Schema")]
		public List<Schema> Schemas
		{
			get
			{
				return this.schemas;
			}
		}

		[XmlAttribute]
		public string ServerEdition
		{
			get
			{
				return this.serverEdition;
			}
			set
			{
				this.serverEdition = value;
			}
		}

		[XmlAttribute]
		public string ServerLevel
		{
			get
			{
				return this.serverLevel;
			}
			set
			{
				this.serverLevel = value;
			}
		}

		[XmlAttribute]
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
			set
			{
				this.serverVersion = value;
			}
		}

		#endregion
	}
}