/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// Provides the Factory Method pattern used to resolve dependencies.
	/// </summary>
	public sealed class SingletonDependencyResolution : IDependencyResolution
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SingletonDependencyResolution class.
		/// </summary>
		/// <param name="instance"> The singleton instance. </param>
		public SingletonDependencyResolution(object instance)
		{
			this.instance = instance;
		}

		#endregion

		#region Fields/Constants

		private readonly object instance;

		#endregion

		#region Properties/Indexers/Events

		private object Instance
		{
			get
			{
				return this.instance;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Gets an instance of SingletonDependencyResolution from the specified object instance.
		/// </summary>
		/// <typeparam name="TObject"> The target type of resolution. </typeparam>
		/// <param name="instance"> The singleton instance. </param>
		/// <returns> A SingletonDependencyResolution instance. </returns>
		public static SingletonDependencyResolution OfType<TObject>(TObject instance)
		{
			return new SingletonDependencyResolution(instance);
		}

		/// <summary>
		/// Resolves a dependency.
		/// </summary>
		/// <returns> An instance of an object or null. </returns>
		public object Resolve()
		{
			return this.Instance;
		}

		#endregion
	}
}