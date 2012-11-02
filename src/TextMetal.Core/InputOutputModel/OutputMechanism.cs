/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace TextMetal.Core.InputOutputModel
{
	public abstract class OutputMechanism : IOutputMechanism
	{
		#region Constructors/Destructors

		protected OutputMechanism()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly Stack<TextWriter> textWriters = new Stack<TextWriter>();
		private bool disposed;
		private TextWriter logTextWriter = Console.Out;

		#endregion

		#region Properties/Indexers/Events

		public TextWriter CurrentTextWriter
		{
			get
			{
				return this.TextWriters.Count > 0 ? this.TextWriters.Peek() : Console.Out;
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

		public TextWriter LogTextWriter
		{
			get
			{
				return this.logTextWriter;
			}
			protected set
			{
				if (this.Disposed &&
				    this.LogTextWriter != value)
				{
					this.LogTextWriter.Flush();
					this.LogTextWriter.Dispose();
				}

				this.logTextWriter = value ?? Console.Out;
				this.Disposed = (object)value == null;
			}
		}

		protected Stack<TextWriter> TextWriters
		{
			get
			{
				return this.textWriters;
			}
		}

		#endregion

		#region Methods/Operators

		protected abstract void CoreEnter(string scopeName);

		protected abstract void CoreLeave(string scopeName);

		/// <summary>
		/// 	Dispose of the data source transaction.
		/// </summary>
		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				if ((object)this.TextWriters != null)
				{
					foreach (TextWriter textWriter in this.TextWriters)
					{
						textWriter.Flush();
						textWriter.Dispose();
					}
				}

				this.LogTextWriter.Flush();
				this.LogTextWriter.Dispose();
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public void EnterScope(string scopeName)
		{
			this.CoreEnter(scopeName);
		}

		public void LeaveScope(string scopeName)
		{
			this.CoreLeave(scopeName);
		}

		#endregion
	}
}