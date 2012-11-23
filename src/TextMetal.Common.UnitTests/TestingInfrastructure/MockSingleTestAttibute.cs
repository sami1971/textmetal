/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.UnitTests.TestingInfrastructure
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	public class MockSingleTestAttibute : Attribute
	{
		#region Constructors/Destructors

		public MockSingleTestAttibute(int value)
		{
			this.value = value;
		}

		#endregion

		#region Fields/Constants

		private int value;

		#endregion

		#region Properties/Indexers/Events

		public int Value
		{
			get
			{
				return this.value;
			}
		}

		#endregion
	}
}