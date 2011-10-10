/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using TextMetal.Core.AssociativeModel;
using TextMetal.Core.ExpressionModel;
using TextMetal.Core.SortModel;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core
{
	[XmlElementMapping(LocalName = "DebuggerBreakpoint", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class DebuggerBreakpointConstruct : ITemplateXmlObject, IExpressionXmlObject, IAssociativeXmlObject, ISortXmlObject
	{
		#region Constructors/Destructors

		public DebuggerBreakpointConstruct()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		IList<IXmlObject> IXmlObject.AnonymousChildren
		{
			get
			{
				return null;
			}
		}

		string IAssociativeXmlObject.Name
		{
			get
			{
				return null;
			}
		}

		IXmlObject IXmlObject.Parent
		{
			get
			{
				return null;
			}
			set
			{
				// do nothing
			}
		}

		#endregion

		#region Methods/Operators

		object IExpressionMechanism.EvaluateExpression(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return null;
		}

		IEnumerable ISortMechanism.EvaluateSort(TemplatingContext templatingContext, IEnumerable values)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return values;
		}

		void ITemplateMechanism.ExpandTemplate(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();
		}

		public Type GetAllowedAnonymousChildrenTypes()
		{
			return null;
		}

		public Type GetAllowedParentTypes()
		{
			return null;
		}

		object IAssociativeMechanism.GetAssociativeObjectValue()
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return this;
		}

		#endregion
	}
}