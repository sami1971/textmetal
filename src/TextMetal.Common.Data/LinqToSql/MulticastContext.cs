/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Common.Data.LinqToSql
{
	/// <summary>
	/// Used as a context for a data source transaction. Allows multiple contexts to be associated to a single transaction for differing actual types. An exception is throw if duplicate context actual types are registered. When disposed, all underlying contexts will also be disposed.
	/// </summary>
	/// <typeparam name="TContextBase"> The base type (not actual type) of the underlying context. </typeparam>
	public sealed class MulticastContext<TContextBase> : IDisposable
		where TContextBase : class, IDisposable
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the MulticastContext`1 class.
		/// </summary>
		public MulticastContext()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<Type, TContextBase> contexts = new Dictionary<Type, TContextBase>();
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		private IDictionary<Type, TContextBase> Contexts
		{
			get
			{
				return this.contexts;
			}
		}

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

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Disposes of the inner contexts. Once disposed, the instance cannot be reused.
		/// </summary>
		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				foreach (TContextBase context in this.Contexts.Values)
					context.Dispose();
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Gets the single context of the specified actual context type. An exception is thrown if the requested actual type has not previously been registered.
		/// </summary>
		/// <param name="contextActualType"> The actual context type requested. </param>
		/// <returns> An instance of an actual context type. </returns>
		public TContextBase GetContext(Type contextActualType)
		{
			TContextBase contextActualInstance;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(MulticastContext<TContextBase>).FullName);

			if ((object)contextActualType == null)
				throw new ArgumentNullException("contextActualType");

			if (!this.Contexts.ContainsKey(contextActualType))
				throw new InvalidOperationException(string.Format("The actual context type '{0}' is not yet registered on multicast context type '{1}'.", contextActualType.FullName, typeof(MulticastContext<TContextBase>).FullName));

			contextActualInstance = this.Contexts[contextActualType];

			return contextActualInstance;
		}

		/// <summary>
		/// Gets a value indicating whether a context of the specified actual context type has been previously registered.
		/// </summary>
		/// <param name="contextActualType"> The actual context type requested. </param>
		/// <returns> A value indicating whether a context of the specified actual context type has been previously registered. </returns>
		public bool HasContext(Type contextActualType)
		{
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(MulticastContext<TContextBase>).FullName);

			if ((object)contextActualType == null)
				throw new ArgumentNullException("contextActualType");

			return this.Contexts.ContainsKey(contextActualType);
		}

		/// <summary>
		/// Sets (or registers) a single context instance of the specified actual context type. An exception is thrown if the requested actual type has already previously been registered.
		/// </summary>
		/// <param name="contextActualType"> The actual context type requested. </param>
		/// <param name="contextActualInstance"> The actual context instance to register. </param>
		public void SetContext(Type contextActualType, TContextBase contextActualInstance)
		{
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(MulticastContext<TContextBase>).FullName);

			if ((object)contextActualType == null)
				throw new ArgumentNullException("contextActualType");

			if ((object)contextActualInstance == null)
				throw new ArgumentNullException("contextActualInstance");

			if (this.Contexts.ContainsKey(contextActualType))
				throw new InvalidOperationException(string.Format("The actual context type '{0}' is already registered on multicast context type '{1}'.", contextActualType.FullName, typeof(MulticastContext<TContextBase>).FullName));

			this.Contexts.Add(contextActualType, contextActualInstance);
		}

		#endregion
	}
}