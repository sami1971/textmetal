/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Unary
{
	public abstract class UnaryOperator : Operator, IUnaryOperator
	{
		#region Constructors/Destructors

		protected UnaryOperator()
		{
		}

		#endregion

		#region Methods/Operators

		public object Eval(object theObj)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}