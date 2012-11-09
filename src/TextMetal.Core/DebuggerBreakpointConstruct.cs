/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
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
	/// <summary>
	/// 	Allows an author of a TextMetal template file to declaratively set a CLR breakpoint anywhere in the object tree.
	/// </summary>
	[XmlElementMapping(LocalName = "DebuggerBreakpoint", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public class DebuggerBreakpointConstruct : ITemplateXmlObject, IExpressionXmlObject, IAssociativeXmlObject, ISortXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the DebuggerBreakpointConstruct class.
		/// </summary>
		public DebuggerBreakpointConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets an array of allowed child XML object types.
		/// </summary>
		public Type[] AllowedChildTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		/// <summary>
		/// 	Gets an array of allowed parent XML object types.
		/// </summary>
		public Type[] AllowedParentTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		///<summary>
		///	Gets or sets the optional single XML object content. This implementation always return null.
		///</summary>
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

		///<summary>
		///	Gets a list of XML object items. This implementation always return null.
		///</summary>
		public IXmlObjectCollection<IXmlObject> Items
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// 	Gets the associative name of the current associative XML object.
		/// </summary>
		public string Name
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// 	Gets or sets the parent XML object or null if this is the document root.
		/// </summary>
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

		/// <summary>
		/// 	Evaluates at run-time, an expression tree yielding an object value result.
		/// </summary>
		/// <param name="templatingContext"> The templating context. </param>
		/// <returns> An expression return value or null. </returns>
		public object EvaluateExpression(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return null;
		}

		/// <summary>
		/// 	Re-orders an enumerable of values, yielding a re-ordered enumerable.
		/// </summary>
		/// <param name="templatingContext"> The templating context. </param>
		/// <param name="values"> </param>
		/// <returns> </returns>
		public IEnumerable EvaluateSort(TemplatingContext templatingContext, IEnumerable values)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return values;
		}

		/// <summary>
		/// 	Expands the template tree into the templating context current output.
		/// </summary>
		/// <param name="templatingContext"> The templating context. </param>
		public void ExpandTemplate(TemplatingContext templatingContext)
		{
			if (!Debugger.IsAttached)
				Debugger.Break();
		}

		/// <summary>
		/// 	Gets the enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator or null. </returns>
		public IEnumerator GetAssociativeObjectEnumerator()
		{
			return null;
		}

		/// <summary>
		/// 	Gets the dictionary enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IDictionaryEnumerator or null. </returns>
		public IDictionaryEnumerator GetAssociativeObjectEnumeratorDict()
		{
			return null;
		}

		/// <summary>
		/// 	Gets the enumerator (tick one) for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator`1 or null. </returns>
		public IEnumerator<KeyValuePair<string, object>> GetAssociativeObjectEnumeratorTickOne()
		{
			return null;
		}

		/// <summary>
		/// 	Gets the value of the current associative object instance.
		/// </summary>
		/// <returns> A value or null. </returns>
		public object GetAssociativeObjectValue()
		{
			if (!Debugger.IsAttached)
				Debugger.Break();

			return this;
		}

		#endregion
	}
}