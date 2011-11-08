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
	[XmlElementMapping(LocalName = "DebuggerBreakpoint", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public class DebuggerBreakpointConstruct : ITemplateXmlObject, IExpressionXmlObject, IAssociativeXmlObject, ISortXmlObject
	{
		#region Constructors/Destructors

		public DebuggerBreakpointConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		public Type[] AllowedChildTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		public Type[] AllowedParentTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		public IXmlObject Content
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

		public IList<IXmlObject> Items
		{
			get
			{
				return null;
			}
		}

		public string Name
		{
			get
			{
				return null;
			}
		}

		public IXmlObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public object EvaluateExpression(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return null;
		}

		public IEnumerable EvaluateSort(TemplatingContext templatingContext, IEnumerable values)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return values;
		}

		public void ExpandTemplate(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();
		}

		public IEnumerator GetAssociativeObjectEnumerator()
		{
			return null;
		}

		public object GetAssociativeObjectValue()
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return this;
		}

		#endregion
	}
}