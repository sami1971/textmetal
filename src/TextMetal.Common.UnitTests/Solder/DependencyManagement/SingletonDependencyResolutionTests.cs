/*
	Copyright �2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using NUnit.Framework;

using TextMetal.Common.Solder.DependencyManagement;

namespace TextMetal.Common.UnitTests.Solder.DependencyManagement
{
	[TestFixture]
	public class SingletonDependencyResolutionTests
	{
		#region Constructors/Destructors

		public SingletonDependencyResolutionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateOfTypeTest()
		{
			SingletonDependencyResolution singletonDependencyResolution;
			object value;
			object result;

			value = 11;

			singletonDependencyResolution = SingletonDependencyResolution.OfType<object>(value);

			Assert.IsNotNull(singletonDependencyResolution);

			result = singletonDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(11, result);
		}

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			SingletonDependencyResolution singletonDependencyResolution;
			object value;
			object result;

			value = 11;

			singletonDependencyResolution = new SingletonDependencyResolution(value);

			Assert.IsNotNull(singletonDependencyResolution);

			result = singletonDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(11, result);
		}

		#endregion
	}
}