/*
	Copyright �2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using NUnit.Framework;

using TextMetal.Common.Solder.DependencyManagement;

namespace TextMetal.Common.UnitTests.Solder.DependencyManagement
{
	[TestFixture]
	public class ConstructorDependencyResolutionTests
	{
		#region Constructors/Destructors

		public ConstructorDependencyResolutionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			ConstructorDependencyResolution<int> constructorDependencyResolution;
			object result;

			constructorDependencyResolution = new ConstructorDependencyResolution<int>();

			Assert.IsNotNull(constructorDependencyResolution);

			result = constructorDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(0, result);
		}

		#endregion
	}
}