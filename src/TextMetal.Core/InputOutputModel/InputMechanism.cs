/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
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

		/// <summary>
		/// 	Initializes a new instance of the InputMechanism class.
		/// </summary>
		protected InputMechanism()
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

		protected abstract Assembly CoreLoadAssembly(string assembluName);

		protected abstract string CoreLoadContent(string resourceName);

		protected abstract ITemplateXmlObject CoreLoadFragment(string resourceName);

		/// <summary>
		/// 	Dispose of the data source transaction.
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

		/// <summary>
		/// 	Loads an assembly by name.
		/// </summary>
		/// <param name="assemblyName"> The assembly name to load. </param>
		/// <returns> An assembly object or null. </returns>
		public Assembly LoadAssembly(string assemblyName)
		{
			return this.CoreLoadAssembly(assemblyName);
		}

		/// <summary>
		/// 	Loads content by resource name. Resource name semantics is implementation specific.
		/// </summary>
		/// <param name="resourceName"> The resource name to load. </param>
		/// <returns> The text content or null. </returns>
		public string LoadContent(string resourceName)
		{
			return this.CoreLoadContent(resourceName);
		}

		/// <summary>
		/// 	Loads an template fragment by resource name. Resource name semantics is implementation specific.
		/// </summary>
		/// <param name="resourceName"> The resource name to load. </param>
		/// <returns> The template fragment root object or null. </returns>
		public ITemplateXmlObject LoadFragment(string resourceName)
		{
			return this.CoreLoadFragment(resourceName);
		}

		#endregion
	}
}