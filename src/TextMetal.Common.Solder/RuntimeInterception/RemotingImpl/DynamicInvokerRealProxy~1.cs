/*
	Copyright �2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

// STACK_OVERFLOW_CHECK_ENABLED is used to detect NMock2 Object.ToString() on error.

namespace TextMetal.Common.Solder.RuntimeInterception.RemotingImpl
{
	/// <summary>
	/// Provides an abstract base for real proxy objects which leverage the dynamic invoker infrastructure (i.e. IDynamicInvocation).
	/// </summary>
	/// <typeparam name="TTransparentProxy"> The type of the transparent proxy object served up by this class. </typeparam>
	public abstract class DynamicInvokerRealProxy<TTransparentProxy> : RealProxy, IDisposable, IRemotingTypeInfo
		where TTransparentProxy : class
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DynamicInvokerRealProxy`1 class.
		/// </summary>
		/// <param name="dynamicInvocation"> The dynamic invoker object to use. </param>
		protected DynamicInvokerRealProxy(IDynamicInvocation dynamicInvocation)
			: base(typeof(TTransparentProxy))
		{
			if ((object)dynamicInvocation == null)
				throw new ArgumentNullException("dynamicInvocation");

			this.dynamicInvocation = dynamicInvocation;
		}

		#endregion

		#region Fields/Constants

		private readonly IDynamicInvocation dynamicInvocation;

#if STACK_OVERFLOW_CHECK_ENABLED
		private int invokeCount = 0;
#endif
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets a value indicating whether the current instance has been disposed.
		/// </summary>
		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
			private set
			{
				this.disposed = value;
			}
		}

		/// <summary>
		/// Gets or sets the fully qualified type name of the server object in a ObjRef. No implementation is provided; any calls will throw a NotSupportedException.
		/// </summary>
		/// <returns> The fully qualified type name of the server object in a ObjRef. </returns>
		public string TypeName
		{
			get
			{
				throw new NotSupportedException("This property is not supported.");
			}
			set
			{
				throw new NotSupportedException("This property is not supported.");
			}
		}

		private IDynamicInvocation DynamicInvocation
		{
			get
			{
				return this.dynamicInvocation;
			}
		}

#if STACK_OVERFLOW_CHECK_ENABLED
		private int InvokeCount
		{
			get
			{
				return this.invokeCount;
			}
			set
			{
				this.invokeCount = value;
			}
		}
#endif

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Checks whether the proxy that represents the specified object type can be cast to the type represented by the IRemotingTypeInfo interface.
		/// </summary>
		/// <returns> true if cast will succeed; otherwise, false. </returns>
		/// <param name="fromType"> The type to cast to. </param>
		/// <param name="o"> The object for which to check casting. </param>
		public bool CanCastTo(Type fromType, object o)
		{
			return fromType == typeof(IDisposable) ||
			       fromType == typeof(TTransparentProxy);
		}

		/// <summary>
		/// Disposes of the inner dynamic invoker. Once disposed, the instance cannot be reused.
		/// </summary>
		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.DynamicInvocation.Dispose();
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Runtime real proxy invocation.
		/// </summary>
		/// <param name="msg"> Invocation call message </param>
		/// <returns> Invocation return message. </returns>
		public override IMessage Invoke(IMessage msg)
		{
			IMethodCallMessage methodCallMessage;
			MethodInfo invokedMethodInfo;
			Type proxiedType;
			object proxyInstance;
			object returnValue;
			object[] invocationParameters;
			object[] outputParameters;
			Type returnType;
			bool isVoidCall;

#if STACK_OVERFLOW_CHECK_ENABLED
			if (++this.InvokeCount >= 25)
				throw new StackOverflowException();
#endif
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DynamicInvokerRealProxy<TTransparentProxy>).FullName);

			proxiedType = typeof(TTransparentProxy);
			methodCallMessage = (IMethodCallMessage)msg;
			invocationParameters = methodCallMessage.Args;
			invokedMethodInfo = (MethodInfo)methodCallMessage.MethodBase;

			returnType = (object)invokedMethodInfo == null ? null : invokedMethodInfo.ReturnType;
			isVoidCall = ((object)returnType == null || returnType == typeof(void));

			proxyInstance = this.GetTransparentProxy();

			returnValue = this.DynamicInvocation.Invoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
			outputParameters = GetOutputParameters(invokedMethodInfo, invocationParameters);

			returnValue = isVoidCall ? null : returnValue;

			return new ReturnMessage(returnValue, outputParameters, (object)outputParameters != null ? outputParameters.Length : 0, methodCallMessage.LogicalCallContext, methodCallMessage);
		}

		private static object[] GetOutputParameters(MethodInfo methodInfo, object[] invocationParameters)
		{
			ParameterInfo[] parameterInfos;
			List<object> outputParameterValues;
			object[] outputParameters;

			if ((object)methodInfo == null)
				throw new ArgumentNullException("methodInfo");

			if ((object)invocationParameters == null)
				throw new ArgumentNullException("invocationParameters");

			// examine parameters
			parameterInfos = methodInfo.GetParameters();

			// extract output parameter values
			outputParameterValues = new List<object>();

			if ((object)parameterInfos != null)
			{
				int parameterIndex = 0;

				foreach (ParameterInfo parameterInfo in parameterInfos)
				{
					Type parameterType;

					parameterType = parameterInfo.ParameterType;

					if (!parameterInfo.IsOut && parameterType.IsByRef) // byref
						outputParameterValues.Add(invocationParameters[parameterIndex]);
					else if (parameterInfo.IsOut && parameterType.IsByRef) // 'out'
						outputParameterValues.Add(invocationParameters[parameterIndex]);
					else if (!parameterType.IsByRef) // byval
						outputParameterValues.GetType(); // nop
					else
						throw new InvalidOperationException(string.Format("The type::method::parameter '{0}::{1}::{2}' does not specify a valid parameter passing modifier.", methodInfo.DeclaringType.FullName, methodInfo.Name, parameterInfo.Name));

					// increment parameter index
					parameterIndex++;
				}
			}

			outputParameters = outputParameterValues.ToArray();

			return outputParameters;
		}

		#endregion
	}
}