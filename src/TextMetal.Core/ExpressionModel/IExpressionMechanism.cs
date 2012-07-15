/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.ExpressionModel
{
	/// <summary>
	/// 	Provides for expression mechanics.
	/// </summary>
	public interface IExpressionMechanism
	{
		#region Methods/Operators

		object EvaluateExpression(TemplatingContext templatingContext);

		#endregion
	}
}