/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Common.Core
{
	/// <summary>
	/// Internal IComparer`1 implementation using IComparable.
	/// </summary>
	public class ComparableComparer : IComparer<IComparable>
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ComparableComparer class.
		/// </summary>
		public ComparableComparer()
		{
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <returns>
		/// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" /> , as shown in the following table.Value Meaning Less than zero
		///     <paramref
		///         name="x" />
		/// is less than <paramref name="y" /> .Zero <paramref name="x" /> equals <paramref name="y" /> .Greater than zero
		///     <paramref
		///         name="x" />
		/// is greater than <paramref name="y" /> .
		/// </returns>
		/// <param name="x"> The first object to compare. </param>
		/// <param name="y"> The second object to compare. </param>
		public int Compare(IComparable x, IComparable y)
		{
			int? compareResult;

			compareResult = ((object)x != null ? x.CompareTo(y) : ((object)y != null ? (y.CompareTo(x) * -1) : (int?)null));

			if ((object)compareResult == null)
				throw new InvalidOperationException(string.Format("A null result propagated from the evaluation of a '{0}' instance using values '{1}' and '{2}'.", typeof(ComparableComparer).FullName, x, y));

			return (int)compareResult;
		}

		#endregion
	}
}