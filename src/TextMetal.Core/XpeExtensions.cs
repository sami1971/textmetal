/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.AssociativeModel;
using TextMetal.Core.ExpressionModel;
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

			xpe.RegisterKnownXmlObject<DebuggerBreakpointConstruct>();

			xpe.RegisterKnownXmlTextObject<TemplateXmlTextObject>();

			xpe.RegisterKnownXmlObject<ArrayConstruct>();
			xpe.RegisterKnownXmlObject<ModelConstruct>();
			xpe.RegisterKnownXmlObject<ObjectConstruct>();
			xpe.RegisterKnownXmlObject<PropertyConstruct>();
			xpe.RegisterKnownXmlObject<ProxyConstruct>();

			xpe.RegisterKnownXmlObject<AspectConstruct>();
			xpe.RegisterKnownXmlObject<BinaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<FacetConstruct>();
			xpe.RegisterKnownXmlObject<NullaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<ExpressionContainerConstruct>();
			xpe.RegisterKnownXmlObject<PowerShellConstruct>();
			xpe.RegisterKnownXmlObject<UnaryExpressionConstruct>();
			xpe.RegisterKnownXmlObject<ValueConstruct>();

			xpe.RegisterKnownXmlObject<AscendingConstruct>();
			xpe.RegisterKnownXmlObject<DescendingConstruct>();
			xpe.RegisterKnownXmlObject<SortContainerConstruct>();

			xpe.RegisterKnownXmlObject<AllocConstruct>();
			xpe.RegisterKnownXmlObject<AssignConstruct>();
			xpe.RegisterKnownXmlObject<FreeConstruct>();
			xpe.RegisterKnownXmlObject<ForConstruct>();
			xpe.RegisterKnownXmlObject<ForEachConstruct>();
			xpe.RegisterKnownXmlObject<IfConstruct>();
			xpe.RegisterKnownXmlObject<ImportConstruct>();
			xpe.RegisterKnownXmlObject<IncludeConstruct>();
			xpe.RegisterKnownXmlObject<InvokeSourceStrategyConstruct>();
			xpe.RegisterKnownXmlObject<LogConstruct>();
			xpe.RegisterKnownXmlObject<OutputScopeConstruct>();
			xpe.RegisterKnownXmlObject<ReferenceConstruct>();
			xpe.RegisterKnownXmlObject<TemplateContainerConstruct>();
			xpe.RegisterKnownXmlObject<TemplateConstruct>();
			xpe.RegisterKnownXmlObject<WriteConstruct>();
		}

		#endregion
	}
}