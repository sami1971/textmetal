﻿/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Binary
{
	public abstract class BinaryOperator : Operator, IBinaryOperator
	{
		#region Constructors/Destructors

		protected BinaryOperator()
		{
		}

		#endregion

		#region Methods/Operators

		public object Eval(object leftObj, Func<object> onDemandRightExpressionEvaluator)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}