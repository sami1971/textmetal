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
	public class ActivatorDependencyResolutionTests
	{
		#region Constructors/Destructors

		public ActivatorDependencyResolutionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			ActivatorDependencyResolution activatorDependencyResolution;
			Type value;
			object result;

			value = typeof(int);

			activatorDependencyResolution = new ActivatorDependencyResolution(value);

			Assert.IsNotNull(activatorDependencyResolution);

			result = activatorDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(0, result);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullValueCreateTest()
		{
			ActivatorDependencyResolution activatorDependencyResolution;
			Type value;

			value = null;

			activatorDependencyResolution = new ActivatorDependencyResolution(value);
		}

		#endregion
	}
}