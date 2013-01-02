/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;

namespace TextMetal.Common.Expressions
{
	[Serializable]
	public enum NullaryOperator
	{
		[Description("{nop}")]
		Nop = 0
	}
}