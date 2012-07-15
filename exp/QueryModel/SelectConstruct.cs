/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.SortModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	/// <summary>
	/// This class uses the C# compiler style of numeric promotions.
	/// </summary>
	[XmlElementMapping(LocalName = "Select", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class SelectConstruct : QueryXmlObject
	{
		#region Constructors/Destructors

		public SelectConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private JoinSourceContainerConstruct joinSource;
		private SortContainerConstruct orderBySort;
		private ProjectionContainerConstruct projections;
		private ExpressionContainerConstruct whereExpression;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "JoinSource", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public JoinSourceContainerConstruct JoinSource
		{
			get
			{
				return this.joinSource;
			}
			set
			{
				this.joinSource = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "OrderBySort", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public SortContainerConstruct OrderBySort
		{
			get
			{
				return this.orderBySort;
			}
			set
			{
				this.orderBySort = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Projections", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ProjectionContainerConstruct Projections
		{
			get
			{
				return this.projections;
			}
			set
			{
				this.projections = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "WhereExpression", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ExpressionContainerConstruct WhereExpression
		{
			get
			{
				return this.whereExpression;
			}
			set
			{
				this.whereExpression = value;
			}
		}

		#endregion
	}
}