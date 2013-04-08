/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Expressions;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "NullaryExpression", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class NullaryExpressionConstruct : ExpressionXmlObject, INullaryExpression
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the NullaryExpressionConstruct class.
		/// </summary>
		public NullaryExpressionConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly NullaryOperator nullaryOperator = NullaryOperator.Nop;

		#endregion

		#region Properties/Indexers/Events

		public NullaryOperator NullaryOperator
		{
			get
			{
				return this.nullaryOperator;
			}
		}

		#endregion

		#region Methods/Operators

		protected override object CoreEvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			return null;
		}

		#endregion
	}
}