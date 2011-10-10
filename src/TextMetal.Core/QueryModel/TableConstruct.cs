/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "Table", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class TableConstruct : XmlSterileObject<IQueryXmlObject>, IQueryXmlObject
	{
		#region Constructors/Destructors

		public TableConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string alias;
		private JoinType join;
		private string name;
		private ExpressionContainerConstruct onExpression;

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

		[XmlAttributeMapping(LocalName = "join", NamespaceUri = "")]
		public JoinType Join
		{
			get
			{
				return this.join;
			}
			set
			{
				this.join = value;
			}
		}

		[XmlAttributeMapping(LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "OnExpression", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct OnExpression
		{
			get
			{
				return this.onExpression;
			}
			set
			{
				this.onExpression = value;
			}
		}

		#endregion
	}
}