/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "Projection", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ProjectionConstruct : QueryXmlObject
	{
		#region Constructors/Destructors

		public ProjectionConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string alias;
		private ExpressionContainerConstruct theExpression;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "alias", NamespaceUri = "")]
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "TheExpression", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ExpressionContainerConstruct TheExpression
		{
			get
			{
				return this.theExpression;
			}
			set
			{
				this.theExpression = value;
			}
		}

		#endregion
	}
}