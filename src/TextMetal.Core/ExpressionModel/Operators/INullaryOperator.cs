﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators
{
	public interface INullaryOperator : IOperator
	{
		#region Methods/Operators

		object Eval();

		#endregion
	}
}