/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	public abstract class ExpressionXmlObject : XmlObject, IExpressionXmlObject
	{
		#region Constructors/Destructors

		protected ExpressionXmlObject()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreEvaluateExpression(TemplatingContext templatingContext);

		public object EvaluateExpression(TemplatingContext templatingContext)
		{
			return this.CoreEvaluateExpression(templatingContext);
		}

		#endregion
	}
}