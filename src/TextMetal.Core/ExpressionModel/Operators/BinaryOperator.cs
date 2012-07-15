/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;

namespace TextMetal.Core.ExpressionModel
{
	[Obsolete]
	[Serializable]
	public enum BinaryOperator
	{
		[Description("")]
		Undefined = 0,

		[Description("+")]
		Add,

		[Description("-")]
		Sub,

		[Description("/")]
		Div,

		[Description("*")]
		Mul,

		[Description("%")]
		Mod,

		[Description("&&")]
		And,

		[Description("||")]
		Or,

		[Description("^^")]
		Xor,

		[Description("==")]
		Eq,

		[Description("!=")]
		Ne,

		[Description("<")]
		Lt,

		[Description("<=")]
		Le,

		[Description(">")]
		Gt,

		[Description(">=")]
		Ge,

		[Description("{like}")]
		StrLk,

		[Description("{as}")]
		ObjAs,

		[Description("{is}")]
		ObjIs,

		[Description(":=")]
		VarPut,

		[Description("&")]
		Band,

		[Description("|")]
		Bor,

		[Description("^")]
		Bxor,

		[Description("<<")]
		Bls,

		[Description(">>")]
		Brs,

		[Description(">^>")]
		Bsr,

		[Description(">_>")]
		Bur,

		[Description("{parse}")]
		Parse,
	}
}