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
	public class DependencyRegistrationAttributeTests
	{
		#region Constructors/Destructors

		public DependencyRegistrationAttributeTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateTest()
		{
			DependencyRegistrationAttribute attribute;

			attribute = new DependencyRegistrationAttribute();

			Assert.IsNotNull(attribute);
		}

		#endregion
	}
}