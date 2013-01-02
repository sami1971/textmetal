/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Common.Expressions
{
	/// <summary>
	/// Represents an expression with one operand.
	/// </summary>
	public interface IUnaryExpression : IExpression
	{
		#region Properties/Indexers/Events

		IExpression TheExpression
		{
			get;
		}

		UnaryOperator UnaryOperator
		{
			get;
		}

		#endregion
	}
}