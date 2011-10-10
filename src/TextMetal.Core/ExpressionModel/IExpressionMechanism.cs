/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.ExpressionModel
{
	public interface IExpressionMechanism
	{
		#region Methods/Operators

		object EvaluateExpression(TemplatingContext templatingContext);

		#endregion
	}
}