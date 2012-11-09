/*
	Copyright �2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using NUnit.Framework;

using TestingFramework.Core.Customization;

using TextMetal.Plumbing.UnitTests.TestingInfrastructure;

namespace TextMetal.Plumbing.UnitTests.RuntimeInterception.root
{
	[TestFixture]
	public class DynamicInvokerTests
	{
		#region Constructors/Destructors

		public DynamicInvokerTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCloneInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(ICloneable);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<ICloneable>.GetLastMemberInfo(exec => exec.Clone());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.IsNull(returnValue);
			Assert.AreEqual("ICloneable::Clone", mockDynamicInvoker.LastOperationName);
		}

		[Test]
		public void ShouldDisposeInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IDisposable);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.IsNull(returnValue);
			Assert.IsTrue(mockDynamicInvoker.Disposed);
		}

		[Test]
		public void ShouldEqualsInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.Equals(null));
			invocationParameters = new object[] { null };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.AreEqual(false, returnValue);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadDeclaringTypeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IDisposable);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IDisposable>.GetLastMemberInfo(exec => exec.Dispose());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadInputParameterCountEqualsInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.Equals(null));
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadInputParameterCountGetHashCodeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetHashCode());
			invocationParameters = new object[] { 0 };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadInputParameterCountGetTypeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());
			invocationParameters = new object[] { 0 };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadInputParameterCountToStringInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = new object[] { 0 };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnBadMethodNameInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)typeof(object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullInputInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());
			invocationParameters = null;

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullInputParameterInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = null;

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullInvokingTypeInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = null;
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullMethodInfoInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = null;
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullMethodInfoInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = null;
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullProxyTypeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = new object();
			proxiedType = null;
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTargetInstanceInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = null;
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTargetInstanceInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;

			proxyInstance = null;
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
		}

		[Test]
		public void ShouldGetHashCodeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetHashCode());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.AreEqual(0, returnValue);
		}

		[Test]
		public void ShouldGetTypeInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.GetType());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.AreEqual(typeof(IMockObject), returnValue);
		}

		[Test]
		public void ShouldNotFailOnDoubleDisposeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;

			mockDynamicInvoker = new MockDynamicInvoker();

			Assert.IsNotNull(mockDynamicInvoker);

			mockDynamicInvoker.Dispose();
			mockDynamicInvoker.Dispose();
		}

		[Test]
		public void ShouldToStringInvokeOnObjectTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.BypassInvokeOnObject(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.AreEqual(typeof(IMockObject).FullName, returnValue);
		}

		[Test]
		public void ShouldToStringInvokeTest()
		{
			MockDynamicInvoker mockDynamicInvoker;
			object proxyInstance;
			Type proxiedType;
			MethodInfo invokedMethodInfo;
			object[] invocationParameters;
			object returnValue;

			proxyInstance = new object();
			proxiedType = typeof(IMockObject);
			invokedMethodInfo = (MethodInfo)MemberInfoProxy<IMockObject>.GetLastMemberInfo(exec => exec.ToString());
			invocationParameters = new object[] { };

			mockDynamicInvoker = new MockDynamicInvoker();
			returnValue = mockDynamicInvoker.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			Assert.AreEqual(typeof(IMockObject).FullName, returnValue);
		}

		#endregion
	}
}