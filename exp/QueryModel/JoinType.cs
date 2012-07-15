/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.QueryModel
{
	public enum JoinType
	{
		Undefined = 0,
		Inner,
		LeftOuter,
		RightOuter,
		FullOuter,
		Cross
	}
}