/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.TemplateModel
{
	public abstract class TemplateXmlObject : XmlObject, ITemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the TemplateXmlObject class.
		/// </summary>
		protected TemplateXmlObject()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		protected virtual bool IsScopeBlock
		{
			get
			{
				return false;
			}
		}

		#endregion

		#region Methods/Operators

		protected abstract void CoreExpandTemplate(TemplatingContext templatingContext);

		public void ExpandTemplate(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if (this.IsScopeBlock)
				templatingContext.VariableTables.Push(new Dictionary<string, object>());

			this.CoreExpandTemplate(templatingContext);

			if (this.IsScopeBlock)
				templatingContext.VariableTables.Pop();
		}

		#endregion
	}
}