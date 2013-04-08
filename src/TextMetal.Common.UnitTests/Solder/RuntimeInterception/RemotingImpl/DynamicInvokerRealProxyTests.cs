/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;

using NMock2;
using NMock2.Matchers;

using NUnit.Framework;

using TestingFramework.Core.Customization;

using TextMetal.Common.Solder.RuntimeInterception;
using TextMetal.Common.UnitTests.TestingInfrastructure;

namespace TextMetal.Common.UnitTests.Solder.RuntimeInterception.RemotingImpl
{
	[TestFixture]
	public class DynamicInvokerRealProxyTests
	{
		#region Constructors/Destructors

		public DynamicInvokerRealProxyTests()
		{
		}

		#endregion

		//[Test]
		//public void _TestTart()
		//{
		//    IListItem outerli;
		//    IListItem innerli;

		//    innerli = new ListItem<int>(1110, "daniel");

		//    outerli = (IListItem)new BarDIRP<IListItem>(innerli).GetTransparentProxy();

		//    var v = outerli.Text;
		//}

		#region Methods/Operators

		[Test]
		public void ShouldCreateInstanceWithInvokeDynamicAndDisposeInnerDisposableTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			Expect.Once.On(mockDynamicInvocation).Method("Dispose").WithNoArguments();

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsFalse(mockDynamicInvokerRealProxy.Disposed);
			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			mockDynamicInvokerRealProxy.Dispose();
			Assert.IsTrue(mockDynamicInvokerRealProxy.Disposed);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedInvokeTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			mockMessage = mockery.NewMock<IMethodCallMessage>();
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Once.On(mockDynamicInvocation).Method("Dispose").WithNoArguments();

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);

			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			mockDynamicInvokerRealProxy.Dispose();

			mockDynamicInvokerRealProxy.Invoke(mockMessage);
		}

		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void ShouldFailOnGetTypeNameTest()
		{
			MockDynamicInvokerRealProxy realProxy;
			Mockery mockery;
			IDynamicInvocation mockDynamicInvocation;
			string value;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			realProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(realProxy);

			value = realProxy.TypeName;
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullInvokeDynamicCreateTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = null;

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
		}

		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void ShouldFailOnSetTypeNameTest()
		{
			MockDynamicInvokerRealProxy realProxy;
			Mockery mockery;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			realProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(realProxy);

			realProxy.TypeName = null;
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldIFailOnNullInvocationParametersInGetOutputParametersTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec =>
			                                                                               {
				                                                                               byte bdummy = default(byte);
				                                                                               int idummy = default(int);
				                                                                               string sdummy = default(string);
				                                                                               object odummy = default(object);
				                                                                               bdummy = exec.SomeMethodWithVarietyOfParameters(idummy, out sdummy, ref odummy);
			                                                                               });

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(null));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(null)).Will(Return.Value(null));

			mockDynamicInvokerRealProxy.Invoke(mockMessage);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldIFailOnNullMethodInfoInGetOutputParametersTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = null;

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(new object[] { 10, "100", (object)"1000" }));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(new object[] { 10, "100", (object)"1000" })).Will(Return.Value(null));

			mockDynamicInvokerRealProxy.Invoke(mockMessage);
		}

		[Test]
		public void ShouldInvokeAsIDiposableTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(new object[] { }));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(new object[] { })).Will(Return.Value(null));

			mockDynamicInvokerRealProxy.Invoke(mockMessage);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldInvokeAsIMockObjectTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec =>
			                                                                               {
				                                                                               byte bdummy = default(byte);
				                                                                               int idummy = default(int);
				                                                                               string sdummy = default(string);
				                                                                               object odummy = default(object);
				                                                                               bdummy = exec.SomeMethodWithVarietyOfParameters(idummy, out sdummy, ref odummy);
			                                                                               });

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(new object[] { 10, "100", (object)"1000" }));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(new object[] { 10, "100", (object)"1000" })).Will(Return.Value(null));

			mockDynamicInvokerRealProxy.Invoke(mockMessage);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldInvokeAsNonObjectTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<ICloneable>.GetLastMemberInfo(exec => exec.Clone());

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(new object[] { }));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);

			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(new object[] { })).Will(Return.Value(null));

			mockDynamicInvokerRealProxy.Invoke(mockMessage);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldInvokeAsObjectTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;
			IMethodCallMessage mockMessage;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();
			mockMessage = mockery.NewMock<IMethodCallMessage>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());

			Expect.Once.On(mockMessage).GetProperty("Args").Will(Return.Value(new object[] { }));
			Expect.Exactly(2).On(mockMessage).GetProperty("MethodBase").Will(Return.Value(invokedMethodInfo));
			Expect.Once.On(mockMessage).GetProperty("LogicalCallContext").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("Uri").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("MethodName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("TypeName").Will(Return.Value(null));
			Expect.Once.On(mockMessage).GetProperty("HasVarArgs").Will(Return.Value(false));

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(mockDynamicInvokerRealProxy.GetTransparentProxy()), new EqualMatcher(new object[] { })).Will(Return.Value(null));

			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			mockDynamicInvokerRealProxy.Invoke(mockMessage);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldNotFailOnDoubleDisposeTest()
		{
			Mockery mockery;
			MockDynamicInvokerRealProxy mockDynamicInvokerRealProxy;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			Expect.Once.On(mockDynamicInvocation).Method("Dispose").WithNoArguments();

			mockDynamicInvokerRealProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);

			Assert.IsNotNull(mockDynamicInvokerRealProxy);

			mockDynamicInvokerRealProxy.Dispose();
			mockDynamicInvokerRealProxy.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldValidateAssumptionAboutRealProxyObjectCastingTest()
		{
			MockDynamicInvokerRealProxy realProxy;
			IMockObject transparentProxy;
			Mockery mockery;
			IDynamicInvocation mockDynamicInvocation;
			IDisposable disposable;
			MethodInfo invokedMethodInfo;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());

			realProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(realProxy);

			Expect.Once.On(mockDynamicInvocation).Method("Invoke").With(new EqualMatcher(typeof(IMockObject)), new EqualMatcher(invokedMethodInfo), new EqualMatcher(realProxy.GetTransparentProxy()), new EqualMatcher(new object[] { })).Will(Return.Value(null));

			transparentProxy = (IMockObject)realProxy.GetTransparentProxy();
			Assert.IsNotNull(transparentProxy);
			Assert.IsTrue(RemotingServices.IsTransparentProxy((object)transparentProxy));
			Assert.IsTrue(RemotingServices.GetRealProxy((object)transparentProxy) == (object)realProxy);
			Assert.IsFalse((object)realProxy == (object)transparentProxy);

			Assert.IsTrue(realProxy.CanCastTo(typeof(IDisposable), null));
			Assert.IsFalse(realProxy.CanCastTo(typeof(IConvertible), null));
			Assert.IsTrue(transparentProxy is IDisposable);

			disposable = (IDisposable)transparentProxy;
			disposable.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldValidateAssumptionAboutRealProxyObjectIdTest()
		{
			MockDynamicInvokerRealProxy realProxy;
			IMockObject transparentProxy0, transparentProxy1;
			Mockery mockery;
			IDynamicInvocation mockDynamicInvocation;

			mockery = new Mockery();
			mockDynamicInvocation = mockery.NewMock<IDynamicInvocation>();

			realProxy = new MockDynamicInvokerRealProxy(mockDynamicInvocation);
			Assert.IsNotNull(realProxy);

			transparentProxy0 = (IMockObject)realProxy.GetTransparentProxy();
			Assert.IsNotNull(transparentProxy0);
			Assert.IsTrue(RemotingServices.IsTransparentProxy((object)transparentProxy0));
			Assert.IsTrue(RemotingServices.GetRealProxy((object)transparentProxy0) == (object)realProxy);
			Assert.IsFalse((object)realProxy == (object)transparentProxy0);

			transparentProxy1 = (IMockObject)realProxy.GetTransparentProxy();
			Assert.IsNotNull(transparentProxy1);
			Assert.IsTrue(RemotingServices.IsTransparentProxy((object)transparentProxy1));
			Assert.IsTrue(RemotingServices.GetRealProxy((object)transparentProxy1) == (object)realProxy);
			Assert.IsFalse((object)realProxy == (object)transparentProxy1);

			Assert.IsTrue((object)transparentProxy0 == (object)transparentProxy1);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		#endregion

		//public class BarDIRP<T> : DynamicInvokerRealProxy<T>
		//    where T : class
		//{
		//    #region Constructors/Destructors

		//    public BarDIRP(T obj)
		//        : base(new FooADI<T>(obj))
		//    {
		//    }

		//    #endregion
		//}

		//public class FooADI<T> : AspectDynamicInvoker
		//    where T : class
		//{
		//    #region Constructors/Destructors

		//    public FooADI(T obj)
		//    {
		//        if ((object)obj == null)
		//            throw new ArgumentNullException("obj");

		//        this.obj = obj;
		//    }

		//    #endregion

		//    #region Fields/Constants

		//    private readonly T obj;

		//    #endregion

		//    #region Properties/Indexers/Events

		//    public override object InterceptedInstance
		//    {
		//        get
		//        {
		//            return this.obj;
		//        }
		//    }

		//    #endregion

		//    #region Methods/Operators

		//    protected override void OnInterceptAfterInvoke(bool invocationPreceeded, Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters, ref object returnValue, ref Exception thrownException)
		//    {
		//        Console.WriteLine(string.Format("{0}::{1}", proxiedType.FullName, invokedMethodInfo.Name));
		//    }

		//    protected override void OnInterceptBeforeInvoke(out bool proceedWithInvocation, Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		//    {
		//        Console.WriteLine(string.Format("{0}::{1}", proxiedType.FullName, invokedMethodInfo.Name));
		//        proceedWithInvocation = true;
		//    }

		//    #endregion
		//}
	}
}