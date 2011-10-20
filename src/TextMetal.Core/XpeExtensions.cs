/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.AssociativeModel;
using TextMetal.Core.ExpressionModel;
using TextMetal.Core.QueryModel;
using TextMetal.Core.SortModel;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core
{
	public static class XpeExtensions
	{
		#region Methods/Operators

		public static void RegisterWellKnownConstructs(this IXmlPersistEngine xpe)
		{
			if ((object)xpe == null)
				throw new ArgumentNullException("xpe");

			xpe.RegisterKnownXmlTextObject<TemplateXmlTextObject>();
			
			xpe.RegisterKnownXmlObject<DebuggerBreakpointConstruct>();		

			xpe.RegisterKnownXmlObject<ArrayConstruct>();
			xpe.RegisterKnownXmlObject<ModelConstruct>();
			xpe.RegisterKnownXmlObject<ObjectConstruct>();
			xpe.RegisterKnownXmlObject<PropertyConstruct>();
			xpe.RegisterKnownXmlObject<ProxyConstruct>();

			xpe.RegisterKnownXmlObject<AspectConstruct>();
			xpe.RegisterKnownXmlObject<BinaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<ExpressionContainerConstruct>();
			xpe.RegisterKnownXmlObject<FacetConstruct>();
			xpe.RegisterKnownXmlObject<NullaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<PowerShellConstruct>();
			xpe.RegisterKnownXmlObject<UnaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<ValueConstruct>();

			xpe.RegisterKnownXmlObject<AscendingConstruct>();
			xpe.RegisterKnownXmlObject<DescendingConstruct>();
			xpe.RegisterKnownXmlObject<SortContainerConstruct>();

			xpe.RegisterKnownXmlObject<AllocConstruct>();
			xpe.RegisterKnownXmlObject<AssignConstruct>();
			xpe.RegisterKnownXmlObject<ForConstruct>();
			xpe.RegisterKnownXmlObject<ForEachConstruct>();
			xpe.RegisterKnownXmlObject<FreeConstruct>();
			xpe.RegisterKnownXmlObject<IfConstruct>();
			xpe.RegisterKnownXmlObject<ImportConstruct>();
			xpe.RegisterKnownXmlObject<IncludeConstruct>();
			xpe.RegisterKnownXmlObject<InvokeSourceStrategyConstruct>();
			xpe.RegisterKnownXmlObject<LogConstruct>();
			xpe.RegisterKnownXmlObject<OutputScopeConstruct>();
			xpe.RegisterKnownXmlObject<ReferenceConstruct>();
			xpe.RegisterKnownXmlObject<TemplateConstruct>();
			xpe.RegisterKnownXmlObject<TemplateContainerConstruct>();
			xpe.RegisterKnownXmlObject<WriteConstruct>();
			
			xpe.RegisterKnownXmlObject<JoinSourceContainerConstruct>();
			xpe.RegisterKnownXmlObject<ProjectionConstruct>();
			xpe.RegisterKnownXmlObject<ProjectionContainerConstruct>();
			xpe.RegisterKnownXmlObject<QueryContainerConstruct>();
			xpe.RegisterKnownXmlObject<SelectConstruct>();
			xpe.RegisterKnownXmlObject<TableConstruct>();
		}

		#endregion
	}
}