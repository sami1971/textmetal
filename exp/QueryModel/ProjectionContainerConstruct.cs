/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "ProjectionContainer", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class ProjectionContainerConstruct : QueryXmlObject
	{
		#region Constructors/Destructors

		public ProjectionContainerConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private bool distinct;
		private string type;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "distinct", NamespaceUri = "")]
		public bool Distinct
		{
			get
			{
				return this.distinct;
			}
			set
			{
				this.distinct = value;
			}
		}

		[XmlAttributeMapping(LocalName = "type", NamespaceUri = "")]
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		#endregion
	}
}