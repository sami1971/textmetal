/*
	Copyright �2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using NMock2;

using NUnit.Framework;

using TestingFramework.Core.Customization;

using TextMetal.Common.Solder.RuntimeInterception;
using TextMetal.Common.UnitTests.TestingInfrastructure;

namespace TextMetal.Common.UnitTests.Solder.RuntimeInterception.root
{
	[TestFixture]
	public class ProxyFactoryTests
	{
		#region Constructors/Destructors

		public ProxyFactoryTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateInstanceWithCacheKeyAndInvokeDynamicFactoryTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;
			IDynamicInvocation mockDynamicInvocation;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Once.On(mockInvokeDynamicFactory).Method("GetDynamicInvoker").With("myCacheKey", typeof(IMockObject)).Will(Return.Value(mockDynamicInvocation));

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);
			Assert.IsNotNull(objectContract);

			factory.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCreateInstanceWithCacheKeyTest()
		{
			MockProxyFactory factory;
			IMockObject objectContract;

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);
			Assert.IsFalse(factory.Disposed);

			objectContract = factory.CreateInstance("myCacheKey");

			Assert.IsNotNull(objectContract);

			factory.Dispose();
			Assert.IsTrue(factory.Disposed);
		}

		[Test]
		public void ShouldCreateInstanceWithInvokeDynamicTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance(mockDynamicInvocation);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldEnsureCachingTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract0, objectContract1, objectContract2, objectContract3;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;
			IDynamicInvocation mockDynamicInvocation;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Exactly(1).On(mockInvokeDynamicFactory).Method("GetDynamicInvoker").With("myCacheKey", typeof(IMockObject)).Will(Return.Value(mockDynamicInvocation));
			Expect.Exactly(1).On(mockInvokeDynamicFactory).Method("GetDynamicInvoker").With("myCacheKey_different", typeof(IMockObject)).Will(Return.Value(mockDynamicInvocation));

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract0 = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);
			objectContract1 = factory.CreateInstance("myCacheKey_different", mockInvokeDynamicFactory);
			objectContract2 = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);
			objectContract3 = factory.CreateInstance(mockDynamicInvocation);

			Assert.IsNotNull(objectContract0);
			Assert.IsNotNull(objectContract1);
			Assert.IsNotNull(objectContract2);
			Assert.IsNotNull(objectContract3);

			Assert.AreNotSame(objectContract0, objectContract1);
			Assert.AreSame(objectContract0, objectContract2);
			Assert.AreNotSame(objectContract1, objectContract2);

			Assert.AreNotSame(objectContract0, objectContract3);
			Assert.AreNotSame(objectContract1, objectContract3);
			Assert.AreNotSame(objectContract2, objectContract3);

			factory.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldEnsureNoCachingTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract0, objectContract1, objectContract2;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;
			IDynamicInvocation mockDynamicInvocation;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Once.On(mockInvokeDynamicFactory).Method("GetDynamicInvoker").With("myCacheKey", typeof(IMockObject)).Will(Return.Value(mockDynamicInvocation));

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract0 = factory.CreateInstance(mockDynamicInvocation);
			objectContract1 = factory.CreateInstance(mockDynamicInvocation);
			objectContract2 = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);

			Assert.IsNotNull(objectContract0);
			Assert.IsNotNull(objectContract1);
			Assert.IsNotNull(objectContract2);

			Assert.AreNotSame(objectContract0, objectContract1);
			Assert.AreNotSame(objectContract0, objectContract2);
			Assert.AreNotSame(objectContract1, objectContract2);

			factory.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedCreateInstanceWithCacheKeyAndInvokeDynamicFactoryTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;

			mockery = new Mockery();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			factory.Dispose();

			factory.CreateInstance("test", mockInvokeDynamicFactory, true);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedCreateInstanceWithCacheKeyTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;

			mockery = new Mockery();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			factory.Dispose();

			factory.CreateInstance("test");
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedCreateInstanceWithInvokeDynamicTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			factory.Dispose();

			factory.CreateInstance(mockDynamicInvocation);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullCacheKeyCreateInstanceWithCacheKeyAndInvokeDynamicFactoryTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;

			mockery = new Mockery();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance(null, mockInvokeDynamicFactory);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullInvokeDynamicFactoryCreateInstanceWithCacheKeyAndInvokeDynamicFactoryTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;

			mockery = new Mockery();
			mockInvokeDynamicFactory = null;

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullServiceCreateInstanceWithInvokeDynamicTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = null;

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance(mockDynamicInvocation);
		}

		[Test]
		public void ShouldNotFailOnDoubleDisposeTest()
		{
			Mockery mockery;
			MockProxyFactory factory;
			IMockObject objectContract;
			MockProxyFactory.IInvokeDynamicFactory mockInvokeDynamicFactory;
			IDynamicInvocation mockDynamicInvocation;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockInvokeDynamicFactory = mockery.NewMock<MockProxyFactory.IInvokeDynamicFactory>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Once.On(mockInvokeDynamicFactory).Method("GetDynamicInvoker").With("myCacheKey", typeof(IMockObject)).Will(Return.Value(mockDynamicInvocation));

			factory = new MockProxyFactory();

			Assert.IsNotNull(factory);

			objectContract = factory.CreateInstance("myCacheKey", mockInvokeDynamicFactory);
			Assert.IsNotNull(objectContract);

			factory.Dispose();
			factory.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		#endregion
	}
}