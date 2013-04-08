/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	[Serializable]
	public class ForeignKey
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ForeignKey class.
		/// </summary>
		public ForeignKey()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<ForeignKeyColumnRef> foreignKeyColumnRef = new List<ForeignKeyColumnRef>();
		private bool foreignKeyIsDisabled;
		private bool foreignKeyIsForReplication;
		private string foreignKeyName;
		private string foreignKeyNameCamelCase;
		private string foreignKeyNameConstantCase;
		private string foreignKeyNamePascalCase;
		private string foreignKeyNamePluralCamelCase;
		private string foreignKeyNamePluralConstantCase;
		private string foreignKeyNamePluralPascalCase;
		private string foreignKeyNameSingularCamelCase;
		private string foreignKeyNameSingularConstantCase;
		private string foreignKeyNameSingularPascalCase;
		private byte foreignKeyOnDeleteRefIntAction;
		private string foreignKeyOnDeleteRefIntActionSqlName;
		private byte foreignKeyOnUpdateRefIntAction;
		private string foreignKeyOnUpdateRefIntActionSqlName;

		#endregion

		#region Properties/Indexers/Events

		[XmlArray(ElementName = "ForeignKeyColumnRefs")]
		[XmlArrayItem(ElementName = "ForeignKeyColumnRef")]
		public List<ForeignKeyColumnRef> ForeignKeyColumnRefs
		{
			get
			{
				return this.foreignKeyColumnRef;
			}
		}

		[XmlAttribute]
		public bool ForeignKeyIsDisabled
		{
			get
			{
				return this.foreignKeyIsDisabled;
			}
			set
			{
				this.foreignKeyIsDisabled = value;
			}
		}

		[XmlAttribute]
		public bool ForeignKeyIsForReplication
		{
			get
			{
				return this.foreignKeyIsForReplication;
			}
			set
			{
				this.foreignKeyIsForReplication = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyName
		{
			get
			{
				return this.foreignKeyName;
			}
			set
			{
				this.foreignKeyName = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNameCamelCase
		{
			get
			{
				return this.foreignKeyNameCamelCase;
			}
			set
			{
				this.foreignKeyNameCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNameConstantCase
		{
			get
			{
				return this.foreignKeyNameConstantCase;
			}
			set
			{
				this.foreignKeyNameConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNamePascalCase
		{
			get
			{
				return this.foreignKeyNamePascalCase;
			}
			set
			{
				this.foreignKeyNamePascalCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNamePluralCamelCase
		{
			get
			{
				return this.foreignKeyNamePluralCamelCase;
			}
			set
			{
				this.foreignKeyNamePluralCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNamePluralConstantCase
		{
			get
			{
				return this.foreignKeyNamePluralConstantCase;
			}
			set
			{
				this.foreignKeyNamePluralConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNamePluralPascalCase
		{
			get
			{
				return this.foreignKeyNamePluralPascalCase;
			}
			set
			{
				this.foreignKeyNamePluralPascalCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNameSingularCamelCase
		{
			get
			{
				return this.foreignKeyNameSingularCamelCase;
			}
			set
			{
				this.foreignKeyNameSingularCamelCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNameSingularConstantCase
		{
			get
			{
				return this.foreignKeyNameSingularConstantCase;
			}
			set
			{
				this.foreignKeyNameSingularConstantCase = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyNameSingularPascalCase
		{
			get
			{
				return this.foreignKeyNameSingularPascalCase;
			}
			set
			{
				this.foreignKeyNameSingularPascalCase = value;
			}
		}

		[XmlAttribute]
		public byte ForeignKeyOnDeleteRefIntAction
		{
			get
			{
				return this.foreignKeyOnDeleteRefIntAction;
			}
			set
			{
				this.foreignKeyOnDeleteRefIntAction = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyOnDeleteRefIntActionSqlName
		{
			get
			{
				return this.foreignKeyOnDeleteRefIntActionSqlName;
			}
			set
			{
				this.foreignKeyOnDeleteRefIntActionSqlName = value;
			}
		}

		[XmlAttribute]
		public byte ForeignKeyOnUpdateRefIntAction
		{
			get
			{
				return this.foreignKeyOnUpdateRefIntAction;
			}
			set
			{
				this.foreignKeyOnUpdateRefIntAction = value;
			}
		}

		[XmlAttribute]
		public string ForeignKeyOnUpdateRefIntActionSqlName
		{
			get
			{
				return this.foreignKeyOnUpdateRefIntActionSqlName;
			}
			set
			{
				this.foreignKeyOnUpdateRefIntActionSqlName = value;
			}
		}

		#endregion
	}
}