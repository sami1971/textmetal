/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core
{
	internal class ComparableComparer : IComparer<IComparable>
	{
		#region Constructors/Destructors

		public ComparableComparer()
		{
		}

		#endregion

		#region Methods/Operators

		public int Compare(IComparable x, IComparable y)
		{
			int? compareResult;

			compareResult = ((object)x != null ? x.CompareTo(y) : ((object)y != null ? (y.CompareTo(x) * -1) : (int?)null));

			if ((object)compareResult == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message | type mismatch: at least one System.IComparable expected");

			return (int)compareResult;
		}

		#endregion
	}
}