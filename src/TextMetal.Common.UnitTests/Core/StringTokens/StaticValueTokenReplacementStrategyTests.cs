/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using NUnit.Framework;

using TextMetal.Common.Core.StringTokens;

namespace TextMetal.Common.UnitTests.Core.StringTokens
{
	/// <summary>
	/// Unit tests.
	/// </summary>
	[TestFixture]
	public class StaticValueTokenReplacementStrategyTests
	{
		#region Constructors/Destructors

		public StaticValueTokenReplacementStrategyTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			StaticValueTokenReplacementStrategy tokenReplacementStrategy;
			int value;
			object result;

			value = 10;

			tokenReplacementStrategy = new StaticValueTokenReplacementStrategy(value);

			Assert.IsNotNull(tokenReplacementStrategy);
			Assert.IsNotNull(tokenReplacementStrategy.Value);

			result = tokenReplacementStrategy.Evaluate(new string[] { "10" });

			Assert.IsNotNull(result);
			Assert.AreEqual(10, result);
		}

		#endregion
	}
}