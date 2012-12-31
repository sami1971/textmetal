/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Common.Expressions
{
	/// <summary>
	/// Represents an expression with zero operands.
	/// </summary>
	public interface INullaryExpression : IExpression
	{
		#region Properties/Indexers/Events

		NullaryOperator NullaryOperator
		{
			get;
		}

		#endregion
	}
}