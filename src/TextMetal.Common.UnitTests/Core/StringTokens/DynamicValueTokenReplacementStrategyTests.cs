/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using NUnit.Framework;

using TextMetal.Common.Core.StringTokens;

namespace TextMetal.Common.UnitTests.Core.StringTokens
{
	/// <summary>
	/// Unit tests.
	/// </summary>
	[TestFixture]
	public class DynamicValueTokenReplacementStrategyTests
	{
		#region Constructors/Destructors

		public DynamicValueTokenReplacementStrategyTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			DynamicValueTokenReplacementStrategy tokenReplacementStrategy;
			Func<string[], object> value;
			object result;

			value = p => int.Parse(p[0]) + 1;

			tokenReplacementStrategy = new DynamicValueTokenReplacementStrategy(value);

			Assert.IsNotNull(tokenReplacementStrategy);
			Assert.IsNotNull(tokenReplacementStrategy.Method);

			result = tokenReplacementStrategy.Evaluate(new string[] { "10" });

			Assert.IsNotNull(result);
			Assert.AreEqual(11, result);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullValueCreateTest()
		{
			DynamicValueTokenReplacementStrategy tokenReplacementStrategy;
			Func<string[], object> value;

			value = null;

			tokenReplacementStrategy = new DynamicValueTokenReplacementStrategy(value);
		}

		#endregion
	}
}