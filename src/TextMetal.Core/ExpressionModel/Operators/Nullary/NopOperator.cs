/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators.Nullary
{
	[OperatorMneumonic(Symbol = "{nop}", Description = "")]
	public sealed class NopOperator : NullaryOperator
	{
		#region Constructors/Destructors

		public NopOperator()
		{
		}

		#endregion
	}
}