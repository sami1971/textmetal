/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

namespace TextMetal.Core.Plumbing
{
	/// <summary>
	/// 	Represents an atomic set of data operations on a single connection/transaction.
	/// </summary>
	public sealed class UnitOfWorkContext : IDisposable
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the UnitOfWorkContext class.
		/// </summary>
		private UnitOfWorkContext()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly string UNIT_OF_WORK_CONTEXT_CURRENT_KEY = typeof(UnitOfWorkContext).GUID.SafeToString();
		private bool completed;
		private IDbConnection connection;
		private IDisposable context;
		private bool disposed;
		private bool diverged;
		private IDbTransaction transaction;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the current ambient unit of work context active on the current thread and application domain.
		/// </summary>
		public static UnitOfWorkContext Current
		{
			get
			{
				return (UnitOfWorkContext)ExecutionPathStorage.GetValue(UNIT_OF_WORK_CONTEXT_CURRENT_KEY);
			}
			set
			{
				ExecutionPathStorage.SetValue(UNIT_OF_WORK_CONTEXT_CURRENT_KEY, value);
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether the current instance has been completed.
		/// </summary>
		public bool Completed
		{
			get
			{
				return this.completed;
			}
			private set
			{
				this.completed = value;
			}
		}

		/// <summary>
		/// 	Gets the underlying ADO.NET connection.
		/// </summary>
		public IDbConnection Connection
		{
			get
			{
				if (this.Disposed)
					throw new ObjectDisposedException(typeof(UnitOfWorkContext).FullName);

				return this.connection;
			}
			private set
			{
				this.connection = value;
			}
		}

		/// <summary>
		/// 	Gets the context object.
		/// </summary>
		public IDisposable Context
		{
			get
			{
				if (this.Disposed)
					throw new ObjectDisposedException(typeof(UnitOfWorkContext).FullName);

				return this.context;
			}
			set
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
		/// 	Gets a value indicating whether the current instance has been diverged.
		/// </summary>
		public bool Diverged
		{
			get
			{
				return this.diverged;
			}
			private set
			{
				this.diverged = value;
			}
		}

		/// <summary>
		/// 	Gets the underlying ADO.NET transaction.
		/// </summary>
		public IDbTransaction Transaction
		{
			get
			{
				if (this.Disposed)
					throw new ObjectDisposedException(typeof(UnitOfWorkContext).FullName);

				return this.transaction;
			}
			private set
			{
				this.transaction = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Creates a new unit of work context (and opens the underlying connection) for the given connection type and connection string with an optional transaction started.
		/// </summary>
		/// <param name="connectionType"> The run-time type of the connection to use. </param>
		/// <param name="connectionString"> The ADO.NET provider connection string to use. </param>
		/// <param name="transactional"> A value indicating whether a new local data source transaction isstarted on the connection. </param>
		/// <returns> An instance of teh UnitOfWorkContext ready for execution of operations. This should be wrapped in a using(...){} block for an optimal usage scenario. </returns>
		public static UnitOfWorkContext Create(Type connectionType, string connectionString, bool transactional)
		{
			UnitOfWorkContext unitOfWorkContext;
			const bool OPEN = true;

			if ((object)connectionType == null)
				throw new ArgumentNullException("connectionType");

			if ((object)connectionString == null)
				throw new ArgumentNullException("connectionString");

			if (DataType.IsWhiteSpace(connectionString))
				throw new ArgumentOutOfRangeException("connectionString");

			unitOfWorkContext = new UnitOfWorkContext();
			unitOfWorkContext.Connection = (IDbConnection)Activator.CreateInstance(connectionType);

			if (OPEN)
			{
				unitOfWorkContext.Connection.ConnectionString = connectionString;
				unitOfWorkContext.Connection.Open();

				if (transactional)
					unitOfWorkContext.Transaction = unitOfWorkContext.Connection.BeginTransaction();
			}

			return unitOfWorkContext;
		}

		/// <summary>
		/// 	Contains the logic to 'adjudicate' or realize a transaction based on state of the current unit of work context instance.
		/// </summary>
		private void Adjudicate()
		{
			try
			{
				if ((object)this.Transaction != null)
				{
					if (this.Completed && !this.Diverged)
						this.Transaction.Commit();
					else
						this.Transaction.Rollback();
				}
			}
			finally
			{
				// destroy and tear-down the context
				if ((object)this.Context != null)
				{
					this.Context.Dispose();
					this.Context = null;
				}

				// destroy and tear-down the transaction
				if ((object)this.Transaction != null)
				{
					this.Transaction.Dispose();
					this.Transaction = null;
				}

				// destroy and tear-down the connection
				if ((object)this.Connection != null)
				{
					this.Connection.Dispose();
					this.Connection = null;
				}
			}
		}

		/// <summary>
		/// 	Indicates that all operations within the unit of work context have completed successfully. This method should only be called once.
		/// </summary>
		public void Complete()
		{
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(UnitOfWorkContext).FullName);

			if (this.Completed)
				throw new InvalidOperationException("The current unit of work context is already complete. You should dispose of the unit of work context.");

			this.Completed = true;
		}

		/// <summary>
		/// 	Dispose of the unit of work context.
		/// </summary>
		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.Adjudicate();
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// 	Indicates that at least one operation within the unit of work context cause a failure in data concurrency or idempotency. This forces the entire unit of work to yield an incomplete status. This method can be called any number of times.
		/// </summary>
		public void Divergent()
		{
			if (this.Disposed)
				throw new ObjectDisposedException(typeof(UnitOfWorkContext).FullName);

			this.Diverged = true;
		}

		#endregion
	}
}