/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Binary
{
	/// <summary>
	/// 	Defines the contract an binary operator must expose.
	/// </summary>
	public interface IBinaryOperator : IOperator
	{
		#region Methods/Operators

		/// <summary>
		/// 	Evaluate this unary operator with two arguments.
		/// </summary>
		/// <param name="leftObj"> The left hand side argument. </param>
		/// <param name="onDemandRightExpressionEvaluator"> The rigght hand side argument, wrapped as an on-demand, lazy evaluated delegate. </param>
		/// <returns> The evaluated value (can be null). </returns>
		object Eval(object leftObj, Func<object> onDemandRightExpressionEvaluator);

		#endregion
	}
}