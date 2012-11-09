/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	[XmlElementMapping(LocalName = "NullaryExpression", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class NullaryExpressionConstruct : ExpressionXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the NullaryExpressionConstruct class.
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