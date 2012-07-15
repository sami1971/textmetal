/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators
{
	public abstract class Operator : IOperator
	{
		#region Constructors/Destructors

		protected Operator()
		{
		}

		#endregion

		#region Methods/Operators

		public OperatorMneumonicAttribute GetMneumonic()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}