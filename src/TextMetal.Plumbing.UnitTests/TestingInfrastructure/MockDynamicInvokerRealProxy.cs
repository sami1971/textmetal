/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Plumbing.RuntimeInterception;
using TextMetal.Plumbing.RuntimeInterception.RemotingImpl;

namespace TextMetal.Plumbing.UnitTests.TestingInfrastructure
{
	public class MockDynamicInvokerRealProxy : DynamicInvokerRealProxy<IMockObject>
	{
		#region Constructors/Destructors

		public MockDynamicInvokerRealProxy(IDynamicInvocation dynamicInvocation)
			: base(dynamicInvocation)
		{
		}

		#endregion
	}
}