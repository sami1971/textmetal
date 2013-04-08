/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Common.Solder.RuntimeInterception
{
	/// <summary>
	/// Represents a dynamic invocation used for AOP. Provides hooks into pre and post processing of invocations.
	/// </summary>
	public abstract class AspectDynamicInvoker : DynamicInvoker
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the AspectDynamicInvoker class.
		/// </summary>
		protected AspectDynamicInvoker()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the intercepted instance.
		/// </summary>
		public abstract object InterceptedInstance
		{
			get;
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Represnts a dynamic invocation of a proxied type member.
		/// </summary>
		/// <param name="proxiedType"> The run-time type of the proxied type (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo of the invoked member. </param>
		/// <param name="proxyInstance"> The proxy object instance. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <returns> The return value from the invoked member, if appliable. </returns>
		public override sealed object Invoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		{
			Type contractType;
			Type returnType;
			object returnValue = null;
			Exception thrownException = null;

			bool proceed = false;

			if (!((object)invokedMethodInfo != null &&
			      invokedMethodInfo.DeclaringType == typeof(IDisposable)) &&
			    base.Disposed) // always forward dispose invocations
				throw new ObjectDisposedException(typeof(AspectDynamicInvoker).FullName);

			// sanity checks
			if ((object)proxiedType == null)
				throw new ArgumentNullException("proxiedType");

			if ((object)invokedMethodInfo == null)
				throw new ArgumentNullException("invokedMethodInfo");

			if ((object)proxyInstance == null)
				throw new ArgumentNullException("proxyInstance");

			if ((object)invocationParameters == null)
				throw new ArgumentNullException("invocationParameters");

			// obtain the contract type
			contractType = invokedMethodInfo.DeclaringType;

			// are we executing an interface (contract)?
			//if (!contractType.IsInterface)
			//	throw new InvalidOperationException(string.Format("The type '{0}' is not an interface.", contractType.FullName));

			// get the operation return type
			returnType = invokedMethodInfo.ReturnType;

			this.OnInterceptBeforeInvoke(out proceed, proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);

			if (proceed)
			{
				try
				{
					returnValue = this.OnInterceptProceedInvoke(proxiedType, invokedMethodInfo, proxyInstance, invocationParameters);
				}
				catch (Exception ex)
				{
					thrownException = ex;
				}
			}

			this.OnInterceptAfterInvoke(proceed, proxiedType, invokedMethodInfo, proxyInstance, invocationParameters, ref returnValue, ref thrownException);

			if ((object)thrownException != null)
				throw thrownException;

			return returnValue;
		}

		/// <summary>
		/// This method is executed after the invoking the intercepted member on the intercepted instance. NOTE: The intercepted member can be accessed via the InterceptedInstance property. Do not confuse the intercepted member for the target instance. The target instance is the underlying proxy object.
		/// </summary>
		/// <param name="invocationPreceeded"> A value indicating whether the invocation of the member preceeded this interception point. </param>
		/// <param name="proxiedType"> The runtime type of the invoking instance (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo that was to be invoked. </param>
		/// <param name="proxyInstance"> The target object instance of the invocation. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <param name="returnValue"> The return value from the invoked member, if appliable. </param>
		/// <param name="thrownException"> The exception thrown from the invoked member or null if no error (as input); an exception to rethrow or null (as output). The implementation can choose to swallow a thrown exception or project an exception where none was thrown at the taret invocation. </param>
		/// <returns> A value indicating whether to proceed with the invoking the member. </returns>
		protected abstract void OnInterceptAfterInvoke(bool invocationPreceeded, Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters, ref object returnValue, ref Exception thrownException);

		/// <summary>
		/// This method is executed before the invoking the intercepted member on the intercepted instance. NOTE: The intercepted member can be accessed via the InterceptedInstance property. Do not confuse the intercepted member for the target instance. The target instance is the underlying proxy object.
		/// </summary>
		/// <param name="proceedWithInvocation"> A value indicating whether to proceed with the invoking the member. </param>
		/// <param name="proxiedType"> The runtime type of the invoking instance (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo to invoke. </param>
		/// <param name="proxyInstance"> The target object instance of the invocation. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		protected abstract void OnInterceptBeforeInvoke(out bool proceedWithInvocation, Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters);

		/// <summary>
		/// This method is executes the the intercepted member on the intercepted instance. NOTE: The intercepted member can be accessed via the InterceptedInstance property. Do not confuse the intercepted member for the target instance. The target instance is the underlying proxy object. Generally, inheritors will use the default implementation of this method but advanced scenarios can override this method ensuring a base call.
		/// </summary>
		/// <param name="proxiedType"> The run-time type of the proxied type (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo of the invoked member. </param>
		/// <param name="proxyInstance"> The proxy object instance. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <returns> The return value from the invoked member, if appliable. </returns>
		protected virtual object OnInterceptProceedInvoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		{
			object returnValue = null;

			if ((object)this.InterceptedInstance != null)
				returnValue = invokedMethodInfo.Invoke(this.InterceptedInstance, invocationParameters);

			return returnValue;
		}

		#endregion
	}
}