/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Xml.Serialization;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	[Serializable]
	public class Catalog
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the Catalog class.
		/// </summary>
		public Catalog()
		{
		}

		#endregion

		#region Fields/Constants

		private string catalogName;
		private string catalogNameCamelCase;
		private string catalogNameConstantCase;
		private string catalogNamePascalCase;
		private string catalogNamePluralCamelCase;
		private string catalogNamePluralConstantCase;
		private string catalogNamePluralPascalCase;
		private string catalogNameSingularCamelCase;
		private string catalogNameSingularConstantCase;
		private string catalogNameSingularPascalCase;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute]
		public string CatalogName
		{
			get
			{
				return this.catalogName;
			}
			set
			{
				this.catalogName = value;
			}
		}

		[XmlAttribute]
		public string CatalogNameCamelCase
		{
			get
			{
				return this.catalogNameCamelCase;
			}
			set
			{
				this.catalogNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNameConstantCase
		{
			get
			{
				return this.catalogNameConstantCase;
			}
			set
			{
				this.catalogNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNamePascalCase
		{
			get
			{
				return this.catalogNamePascalCase;
			}
			set
			{
				this.catalogNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNamePluralCamelCase
		{
			get
			{
				return this.catalogNamePluralCamelCase;
			}
			set
			{
				this.catalogNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNamePluralConstantCase
		{
			get
			{
				return this.catalogNamePluralConstantCase;
			}
			set
			{
				this.catalogNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNamePluralPascalCase
		{
			get
			{
				return this.catalogNamePluralPascalCase;
			}
			set
			{
				this.catalogNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNameSingularCamelCase
		{
			get
			{
				return this.catalogNameSingularCamelCase;
			}
			set
			{
				this.catalogNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNameSingularConstantCase
		{
			get
			{
				return this.catalogNameSingularConstantCase;
			}
			set
			{
				this.catalogNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string CatalogNameSingularPascalCase
		{
			get
			{
				return this.catalogNameSingularPascalCase;
			}
			set
			{
				this.catalogNameSingularPascalCase = value;
			}
		}

		#endregion
	}
}