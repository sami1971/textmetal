﻿/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
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
	/// <summary>
	/// 	A set of extension methods to manage the XML Persist Engine model. NOTE: This file must be updated when adding or removing constructs.
	/// </summary>
	public static class XpeExtensions
	{
		#region Methods/Operators

		/// <summary>
		/// 	Quickly register all well-known constructs within this framework.
		/// </summary>
		/// <param name="xpe"> The target XML Persist Engine instance. </param>
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

			xpe.RegisterKnownXmlObject<AliasConstruct>();
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
		}

		#endregion
	}
}