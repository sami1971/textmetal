/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace TextMetal.Core.UnitTests.TestingFramework
{
	/// <summary>
	/// 	Provides a mechanism to obtain the MemberInfo of a member using a dummy invocation inside of an anonymous delegate call, useful for mocking scenarios.
	/// </summary>
	/// <typeparam name="T"> The type of the interface declaring the member (method, property, event) for which to obtain a MemberInfo. </typeparam>
	public sealed class MemberInfoProxy<T> : RealProxy
		where T : class
	{
		#region Constructors/Destructors

		private MemberInfoProxy()
			: base(typeof(T))
		{
		}

		#endregion

		#region Fields/Constants

		private MemberInfo lastMemberInfo;

		#endregion

		#region Properties/Indexers/Events

		private MemberInfo LastMemberInfo
		{
			get
			{
				return this.lastMemberInfo;
			}
			set
			{
				this.lastMemberInfo = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Obtains the MemberInfo of a member using a dummy invocation inside of the spcified anonymous delegate call, useful for mocking scenarios.
		/// </summary>
		/// <param name="exec"> A method which makes a dummy call onto a member of the provided action parameter. </param>
		/// <returns> A MemberInfo of the dummy invocation. </returns>
		public static MemberInfo GetLastMemberInfo(Action<T> exec)
		{
			T instance;
			MemberInfoProxy<T> proxy;

			if ((object)exec == null)
				throw new ArgumentNullException("exec");

			proxy = new MemberInfoProxy<T>();
			instance = proxy.GetTransparentProxy() as T;

			exec(instance);

			return proxy.LastMemberInfo;
		}

		private static MemberInfo GetRealMemberInfo(MethodInfo methodInfo)
		{
			PropertyInfo[] propertyInfos;
			EventInfo[] eventInfos;
			MethodInfo accessorMethodInfo = null;

			if ((object)methodInfo == null)
				throw new ArgumentNullException("methodInfo");

			propertyInfos = methodInfo.DeclaringType.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

			if ((object)propertyInfos != null)
			{
				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					accessorMethodInfo = propertyInfo.GetGetMethod(true);

					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return propertyInfo;

					accessorMethodInfo = propertyInfo.GetSetMethod(true);
					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return propertyInfo;
				}
			}

			eventInfos = methodInfo.DeclaringType.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

			if ((object)eventInfos != null)
			{
				foreach (EventInfo eventInfo in eventInfos)
				{
					accessorMethodInfo = eventInfo.GetAddMethod(true);

					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return eventInfo;

					accessorMethodInfo = eventInfo.GetRemoveMethod(true);
					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return eventInfo;
				}
			}

			return methodInfo;
		}

		/// <summary>
		/// 	Run-time real proxy invocation.
		/// </summary>
		/// <param name="msg"> Invocation call message </param>
		/// <returns> Invocation return message. </returns>
		public override IMessage Invoke(IMessage msg)
		{
			IMethodCallMessage methodCallMessage;
			MethodInfo methodInfo;
			Type returnType;
			bool isVoidCall;
			object returnValue;
			List<object> outputParameters;

			if ((object)this.LastMemberInfo != null)
				throw new InvalidOperationException("A member has already been executed on this instance.");

			methodCallMessage = (IMethodCallMessage)msg;
			methodInfo = (MethodInfo)methodCallMessage.MethodBase;
			returnType = methodInfo.ReturnType;
			isVoidCall = ((object)returnType == null || returnType == typeof(void));
			outputParameters = new List<object>(methodCallMessage.ArgCount);

			this.LastMemberInfo = GetRealMemberInfo(methodInfo);

			returnValue = isVoidCall || !returnType.IsValueType ? null : (object)Activator.CreateInstance(returnType);

			return new ReturnMessage(returnValue, outputParameters.ToArray(), outputParameters.Count, methodCallMessage.LogicalCallContext, methodCallMessage);
		}

		#endregion
	}
}