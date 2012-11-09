/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Plumbing.CommonFacilities.LinqToSql
{
	/// <summary>
	/// 	Used to 'wrap' a context (e.g. DataContext, ObjectContext, SessionImpl, etc.) in a manner such that consuming code can leverage a 'using' block which respects an ambient DataSourceTransaction context if one is present. Essentially, the disposal of this object forwards disposal to the wrapped context if no ambient DataSourceTransaction context if one is present; otherwise, no action is performed leaving disposal of the context up to the adjudication of the ambient DataSourceTransaction context.
	/// </summary>
	/// <typeparam name="TContext"> The type of the underlying or 'wrapped' context. </typeparam>
	public sealed class ContextWrapper<TContext> : IDisposable
		where TContext : class, IDisposable
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ContextWrapper`1 class.
		/// </summary>
		/// <param name="context"> The underlying or 'wrapped' context. </param>
		public ContextWrapper(TContext context)
		{
			if ((object)context == null)
				throw new ArgumentNullException("context");

			this.context = context;
		}

		#endregion

		#region Fields/Constants

		private TContext context;
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the underlying or 'wrapped' context.
		/// </summary>
		public TContext Context
		{
			get
			{
				if (this.Disposed)
					throw new ObjectDisposedException(typeof(ContextWrapper<TContext>).FullName);

				return this.context;
			}
			private set
			{
				this.context = value;
			}
		}

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

		/// <summary>
		/// 	Gets a value indicating whether resources need to be disposed of.
		/// </summary>
		private bool ShouldDisposeResources
		{
			get
			{
				return (object)UnitOfWorkContext.Current == null;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Disposes of the inner context. Once disposed, the instance cannot be reused.
		/// </summary>
		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				if (this.ShouldDisposeResources)
				{
					this.Context.Dispose();
					this.Context = null;
				}
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		#endregion
	}
}