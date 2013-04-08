/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Common.Solder.RuntimeInterception;

namespace TextMetal.Common.UnitTests.TestingInfrastructure
{
	public class MockDynamicInvoker : DynamicInvoker
	{
		#region Constructors/Destructors

		public MockDynamicInvoker()
		{
		}

		#endregion

		#region Fields/Constants

		private string lastOperationName;

		#endregion

		#region Properties/Indexers/Events

		public string LastOperationName
		{
			get
			{
				return this.lastOperationName;
			}
			private set
			{
				this.lastOperationName = value;
			}
		}

		#endregion

		#region Methods/Operators

		public object BypassInvokeOnObject(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		{
			return InvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		public override object Invoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		{
			this.LastOperationName = string.Format("{0}::{1}", (object)proxiedType == null ? "<null>" : proxiedType.Name, (object)invokedMethodInfo == null ? "<null>" : invokedMethodInfo.Name);

			if ((object)invokedMethodInfo != null)
			{
				if (invokedMethodInfo.DeclaringType == typeof(object))
					return this.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
				else if (invokedMethodInfo.DeclaringType == typeof(IDisposable))
				{
					if (invokedMethodInfo.Name == "Dispose")
						this.Dispose();
					else
						throw new InvalidOperationException(string.Format("Method '{0}' not supported on '{1}'.", (object)invokedMethodInfo == null ? "<null>" : invokedMethodInfo.Name, typeof(object).FullName));

					return null;
				}
			}

			return null;
		}

		#endregion
	}
}