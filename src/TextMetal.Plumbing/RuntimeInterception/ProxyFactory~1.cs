/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace TextMetal.Plumbing.RuntimeInterception
{
	/// <summary>
	/// 	Acts as an abstract factory for proxy objects, with cache support. Uses reader-writer lock for asynchronous protection (i.e. thread-safety). Provides agregated disposal of all cached proxies in the cache by calling Dispose() (if IDisposable).
	/// </summary>
	/// <typeparam name="TCacheTrait"> The type of the cache trait. </typeparam>
	public abstract class ProxyFactory<TCacheTrait> : IDisposable
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ProxyFactory`1 class.
		/// </summary>
		protected ProxyFactory()
		{
		}

		#endregion

		#region Fields/Constants

		private const int LOCK_AQUIRE_TIMEOUT_MILLISECONDS = 500;
		private readonly IDictionary<KeyValuePair<Type, TCacheTrait>, object> proxyCache = new Dictionary<KeyValuePair<Type, TCacheTrait>, object>();
		private readonly ReaderWriterLock readerWriterLock = new ReaderWriterLock();
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

		private IDictionary<KeyValuePair<Type, TCacheTrait>, object> ProxyCache
		{
			get
			{
				return this.proxyCache;
			}
		}

		private ReaderWriterLock ReaderWriterLock
		{
			get
			{
				return this.readerWriterLock;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Disposes of the inner proxies, if present. Once disposed, the instance cannot be reused.
		/// </summary>
		/// <param name="proxy"> The proxy to dispose of. </param>
		protected static void DisposeOf(object proxy)
		{
			if (proxy is IDisposable)
				((IDisposable)proxy).Dispose();
		}

		/// <summary>
		/// 	Returns an instance of the specified proxy type for the given key-value pair for a type and cache trait. If the instance is in the cache, it will be returned; otherwise a new instacne is created, added to the cache, and returned. This method is thread-safe: a reader-writer-lock is used to serialize access in the case of a new addition to the cahce.
		/// </summary>
		/// <typeparam name="TProxy"> The type of proxy object to return. </typeparam>
		/// <param name="keyValuePair"> The cahce trait used to check cache existence. </param>
		/// <param name="proxyFactoryMethod"> A callback is specified to provide a factory method mechanism. This allows inheritors the abillity to customize the object being created. This method is only called if a new object is required; i.e. the object does not exist in the cache with the specified type and cahce trait. </param>
		/// <returns> An instance of the specified proxy type. </returns>
		protected TProxy CreateOrGetInstance<TProxy>(KeyValuePair<Type, TCacheTrait> keyValuePair, Func<TProxy> proxyFactoryMethod) where TProxy : class
		{
			TProxy proxy;
			LockCookie lockCookie;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(ProxyFactory<TCacheTrait>).FullName);

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				if (this.ProxyCache.ContainsKey(keyValuePair))
				{
					// get proxy from cache
					proxy = (TProxy)this.ProxyCache[keyValuePair];
				}
				else
				{
					// cop a writer lock
					lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

					try
					{
						// get proxy (indirect)
						proxy = proxyFactoryMethod();

						// add proxy to cache
						this.ProxyCache.Add(keyValuePair, proxy);
					}
					finally
					{
						this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}

			return proxy;
		}

		/// <summary>
		/// 	Notifies any registered listeners to the DisposeInstances event to dispose then clears the internal cache. Once disposed, the instance cannot be reused.
		/// </summary>
		public void Dispose()
		{
			LockCookie lockCookie;

			if (this.Disposed)
				return;

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					foreach (KeyValuePair<KeyValuePair<Type, TCacheTrait>, object> keyValuePair in this.ProxyCache)
						DisposeOf(keyValuePair.Value);

					this.ProxyCache.Clear();
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether an object of a type exists in the cache with the specified cache trait.
		/// </summary>
		/// <param name="keyValuePair"> A key-value pair object representing the cache trait for a given type. </param>
		/// <returns> True if an object exists in the cache with the given cache trait for the typr; otherwise false. </returns>
		protected bool IsInstanceInCache(KeyValuePair<Type, TCacheTrait> keyValuePair)
		{
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(ProxyFactory<TCacheTrait>).FullName);

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				return this.ProxyCache.ContainsKey(keyValuePair);
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		#endregion
	}
}