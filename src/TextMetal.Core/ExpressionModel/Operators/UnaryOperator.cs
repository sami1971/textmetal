﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;

namespace TextMetal.Core.ExpressionModel
{
	[Serializable]
	public enum UnaryOperator
	{
		[Description("")]
		Undefined = 0,

		[Description("!")]
		Not,

		[Description("{is_null}")]
		IsNull,

		[Description("{is_not_null}")]
		IsNotNull, // yes, it is redundant

		[Description("{is_defined}")]
		IsDef,

		[Description("-")]
		Neg,

		[Description("+")]
		Pos,

		[Description("++")]
		Incr,

		[Description("--")]
		Decr,

		[Description("~")]
		BComp
	}
}