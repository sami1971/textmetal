/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using NUnit.Framework;

using TextMetal.Core.TokenModel;

namespace TextMetal.Core.UnitTests.TokenModel
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
			Assert.AreEqual("10", result);
		}

		#endregion
	}
}