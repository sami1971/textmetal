/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Plumbing.UnitTests.TestingInfrastructure
{
	public class MockObject
	{
		#region Constructors/Destructors

		public MockObject()
		{
		}

		#endregion

		#region Fields/Constants

		private string firstName;
		private string lastName;
		private string middleName;
		private int? nameId;
		private string suffix;

		#endregion

		#region Properties/Indexers/Events

		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
			set
			{
				this.middleName = value;
			}
		}

		public int? NameId
		{
			get
			{
				return this.nameId;
			}
			set
			{
				this.nameId = value;
			}
		}

		public object NoGetter
		{
			set
			{
			}
		}

		public object NoSetter
		{
			get
			{
				return 1;
			}
		}

		public object PersistentId
		{
			get
			{
				return this.nameId;
			}
			set
			{
				this.nameId = (int?)value;
			}
		}

		public string Suffix
		{
			get
			{
				return this.suffix;
			}
			set
			{
				this.suffix = value;
			}
		}

		#endregion
	}
}