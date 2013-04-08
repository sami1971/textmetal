/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Solder.RuntimeInterception;
using TextMetal.Common.Solder.RuntimeInterception.RemotingImpl;

namespace TextMetal.Common.UnitTests.TestingInfrastructure
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