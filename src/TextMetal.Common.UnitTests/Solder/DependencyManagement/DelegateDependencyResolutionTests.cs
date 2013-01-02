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
	public class DelegateDependencyResolutionTests
	{
		#region Constructors/Destructors

		public DelegateDependencyResolutionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateAndEvaluateFromFuncTest()
		{
			DelegateDependencyResolution delegateDependencyResolution;
			Func<object> value;
			object result;

			value = () => 11;

			delegateDependencyResolution = DelegateDependencyResolution.FromFunc<object>(value);

			Assert.IsNotNull(delegateDependencyResolution);

			result = delegateDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(11, result);
		}

		[Test]
		public void ShouldCreateAndEvaluateTest()
		{
			DelegateDependencyResolution delegateDependencyResolution;
			Func<object> value;
			object result;

			value = () => 11;

			delegateDependencyResolution = new DelegateDependencyResolution(value);

			Assert.IsNotNull(delegateDependencyResolution);

			result = delegateDependencyResolution.Resolve();

			Assert.IsNotNull(result);
			Assert.AreEqual(11, result);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullValueCreateFromFuncTest()
		{
			DelegateDependencyResolution delegateDependencyResolution;
			Func<object> value;

			value = null;

			delegateDependencyResolution = DelegateDependencyResolution.FromFunc<object>(value);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullValueCreateTest()
		{
			DelegateDependencyResolution delegateDependencyResolution;
			Func<object[], object> value;

			value = null;

			delegateDependencyResolution = new DelegateDependencyResolution(value);
		}

		#endregion
	}
}