/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Expressions;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	public abstract class ExpressionXmlObject : XmlObject, IExpressionXmlObject, IExpression
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ExpressionXmlObject class.
		/// </summary>
		protected ExpressionXmlObject()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreEvaluateExpression(ITemplatingContext templatingContext);

		public object EvaluateExpression(ITemplatingContext templatingContext)
		{
			return this.CoreEvaluateExpression(templatingContext);
		}

		#endregion
	}
}