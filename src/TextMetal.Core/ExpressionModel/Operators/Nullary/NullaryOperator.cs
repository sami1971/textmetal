/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Nullary
{
	public abstract class NullaryOperator : Operator, INullaryOperator
	{
		#region Constructors/Destructors

		protected NullaryOperator()
		{
		}

		#endregion

		#region Methods/Operators

		public virtual object Eval()
		{
			return null;
		}

		#endregion
	}
}