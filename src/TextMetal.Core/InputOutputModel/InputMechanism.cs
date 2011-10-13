/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.InputOutputModel
{
	public abstract class InputMechanism : IInputMechanism
	{
		#region Constructors/Destructors

		protected InputMechanism()
		{
		}

		#endregion

		#region Fields/Constants

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

		#endregion

		#region Methods/Operators

		protected abstract Assembly CoreLoadAssembly(string assembluName);

		protected abstract string CoreLoadContent(string resourceName);

		protected abstract ITemplateXmlObject CoreLoadFragment(string resourceName);

		/// <summary>
		/// Dispose of the data source transaction.
		/// </summary>
		public void Dispose()
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

		public Assembly LoadAssembly(string assembluName)
		{
			return this.CoreLoadAssembly(assembluName);
		}

		public string LoadContent(string resourceName)
		{
			return this.CoreLoadContent(resourceName);
		}

		public ITemplateXmlObject LoadFragment(string resourceName)
		{
			return this.CoreLoadFragment(resourceName);
		}

		#endregion
	}
}