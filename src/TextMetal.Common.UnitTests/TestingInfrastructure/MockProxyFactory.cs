/*
	Copyright �2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Reflection;

using TextMetal.Common.Solder.RuntimeInterception;

namespace TextMetal.Common.UnitTests.TestingInfrastructure
{
	public class MockProxyFactory : ProxyFactory<string>
	{
		#region Constructors/Destructors

		public MockProxyFactory()
		{
		}

		#endregion

		#region Methods/Operators

		public IMockObject CreateInstance(string cacheKey)
		{
			//if (base.Disposed)
			//    throw new ObjectDisposedException(typeof(MockProxyFactory).FullName);

			return this.CreateInstance(cacheKey, new MockInvokeDynamicFactory());
		}

		public IMockObject CreateInstance(string cacheKey, IInvokeDynamicFactory invokeDynamicFactory)
		{
			return this.CreateInstance(cacheKey, invokeDynamicFactory, false);
		}

		public IMockObject CreateInstance(string cacheKey, IInvokeDynamicFactory invokeDynamicFactory, bool skipCacheCheck)
		{
			IMockObject objectContract;
			KeyValuePair<Type, string> keyValuePair;
			IDynamicInvocation dynamicInvocation = null;
			Type objectType;

			//if (base.Disposed)
			//    throw new ObjectDisposedException(typeof(MockProxyFactory).FullName);

			if ((object)cacheKey == null)
				throw new ArgumentNullException("cacheKey");

			if ((object)invokeDynamicFactory == null)
				throw new ArgumentNullException("invokeDynamicFactory");

			keyValuePair = new KeyValuePair<Type, string>(typeof(IMockObject), cacheKey);

			if (!skipCacheCheck && !base.IsInstanceInCache(keyValuePair))
			{
				objectType = typeof(IMockObject);

				// spin up an (Wcf)Service instance
				dynamicInvocation = invokeDynamicFactory.GetDynamicInvoker(cacheKey, objectType);
			}

			objectContract = base.CreateOrGetInstance(keyValuePair, () => this.CreateInstance(dynamicInvocation));

			return objectContract;
		}

		public IMockObject CreateInstance(IDynamicInvocation dynamicInvocation)
		{
			IMockObject proxy;

			if (base.Disposed)
				throw new ObjectDisposedException(typeof(MockProxyFactory).FullName);

			if ((object)dynamicInvocation == null)
				throw new ArgumentNullException("invokeDynamic");

			proxy = new MockObject();

			return proxy;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public interface IInvokeDynamicFactory
		{
			#region Methods/Operators

			IDynamicInvocation GetDynamicInvoker(string cacheKey, Type objectType);

			#endregion
		}

		public class MockInvokeDynamic : IDynamicInvocation
		{
			#region Methods/Operators

			public void Dispose()
			{
			}

			public object Invoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
			{
				return null;
			}

			#endregion
		}

		public class MockInvokeDynamicFactory : IInvokeDynamicFactory
		{
			#region Methods/Operators

			public IDynamicInvocation GetDynamicInvoker(string cacheKey, Type objectType)
			{
				return new MockInvokeDynamic();
			}

			#endregion
		}

		public class MockObject : IMockObject, IDisposable
		{
			#region Properties/Indexers/Events

			public object SomeProp
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			#endregion

			#region Methods/Operators

			public void Dispose()
			{
			}

			public byte SomeMethodWithVarietyOfParameters(int inparam, out string outparam, ref object refparam)
			{
				throw new NotImplementedException();
			}

			#endregion
		}

		#endregion
	}
}