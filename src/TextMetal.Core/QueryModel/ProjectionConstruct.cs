﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "Projection", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class ProjectionConstruct : XmlSterileObject<IQueryXmlObject>, IQueryXmlObject
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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "TheExpression", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
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