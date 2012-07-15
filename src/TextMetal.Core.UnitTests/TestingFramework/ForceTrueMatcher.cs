/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

using NMock2;

namespace TextMetal.Core.UnitTests.TestingFramework
{
	/// <summary>
	/// 	Forces a true match.
	/// </summary>
	public class ForceTrueMatcher : Matcher
	{
		#region Constructors/Destructors

		private ForceTrueMatcher()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly ForceTrueMatcher instance = new ForceTrueMatcher();

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the singleton instance.
		/// </summary>
		public static ForceTrueMatcher Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Not documented on purpose.
		/// </summary>
		/// <param name="writer"> Not documented on purpose. </param>
		public override void DescribeTo(TextWriter writer)
		{
		}

		/// <summary>
		/// 	Not documented on purpose.
		/// </summary>
		/// <param name="o"> Not documented on purpose. </param>
		/// <returns> Not documented on purpose. </returns>
		public override bool Matches(object o)
		{
			return true;
		}

		#endregion
	}
}