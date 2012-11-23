/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Common.Solder.RuntimeInterception
{
	/// <summary>
	/// 	Represents a dynamic invocation. Provides mechanisms to handle System.Object and System.IDisposable message propagation.
	/// </summary>
	public abstract class DynamicInvoker : IDynamicInvocation
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the DynamicInvoker class.
		/// </summary>
		protected DynamicInvoker()
		{
		}

		#endregion

		#region Fields/Constants

		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets a value indicating whether the current instance has been disposed.
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

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Represnts a dynamic invocation of a proxied type member.
		/// </summary>
		/// <param name="proxiedType"> The run-time type of the proxied type (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo of the invoked member. </param>
		/// <param name="proxyInstance"> The proxy object instance. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <returns> The return value from the invoked member, if appliable. </returns>
		protected static object InvokeOnObject(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters)
		{
			Type returnType;
			object returnValue;

			if ((object)proxiedType == null)
				throw new ArgumentNullException("proxiedType");

			if ((object)invokedMethodInfo == null)
				throw new ArgumentNullException("invokedMethodInfo");

			if ((object)proxyInstance == null)
				throw new ArgumentNullException("proxyInstance");

			if ((object)invocationParameters == null)
				throw new ArgumentNullException("invocationParameters");

			if (invokedMethodInfo.DeclaringType != typeof(object))
				throw new InvalidOperationException(string.Format("Declaring type for method '{0}' is not '{1}'.", invokedMethodInfo.Name, typeof(object).FullName));

			// get the operation return type
			returnType = invokedMethodInfo.ReturnType;

			switch (invokedMethodInfo.Name)
			{
				case "GetType":

					if (invocationParameters.Length != 0)
						throw new InvalidOperationException(string.Format("Method '{0}' on type '{1}' takes 0 parameter(s).", invokedMethodInfo.Name, typeof(object).FullName));

					returnValue = proxiedType;
					break;
				case "ToString":

					if (invocationParameters.Length != 0)
						throw new InvalidOperationException(string.Format("Method '{0}' on type '{1}' takes 0 parameter(s).", invokedMethodInfo.Name, typeof(object).FullName));

					returnValue = proxiedType.FullName;
					break;
				case "GetHashCode":

					if (invocationParameters.Length != 0)
						throw new InvalidOperationException(string.Format("Method '{0}' on type '{1}' takes 0 parameter(s).", invokedMethodInfo.Name, typeof(object).FullName));

					returnValue = 0;
					break;
				case "Equals":

					if (invocationParameters.Length != 1)
						throw new InvalidOperationException(string.Format("Method '{0}' on type '{1}' takes 1 parameter(s).", invokedMethodInfo.Name, typeof(object).FullName));

					returnValue = ReferenceEquals((object)proxyInstance, (object)invocationParameters[0]);
					break;
				default:
					throw new InvalidOperationException(string.Format("Method '{0}' not supported on '{1}'.", invokedMethodInfo.Name, typeof(object).FullName));
			}

			if (returnType == typeof(void))
				returnValue = null;

			return returnValue;
		}

		/// <summary>
		/// 	Disposes of the inner resources, if present. Once disposed, the instance cannot be reused.
		/// </summary>
		public virtual void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// 	Represnts a dynamic invocation of a proxied type member.
		/// </summary>
		/// <param name="proxiedType"> The run-time type of the proxied type (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo of the invoked member. </param>
		/// <param name="proxyInstance"> The proxy object instance. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <returns> The return value from the invoked member, if appliable. </returns>
		public abstract object Invoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters);

		#endregion
	}
}