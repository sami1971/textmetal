/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Unary
{
	/// <summary>
	/// 	Defines the contract an unary operator must expose.
	/// </summary>
	public interface IUnaryOperator : IOperator
	{
		#region Methods/Operators

		/// <summary>
		/// 	Evaluate this unary operator with the single argument.
		/// </summary>
		/// <param name="theObj"> The single argument. </param>
		/// <returns> The evaluated value (can be null). </returns>
		object Eval(object theObj);

		#endregion
	}
}