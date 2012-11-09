/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Plumbing.DependencyManagement
{
	/// <summary>
	/// 	Provides the Factory Method pattern used to resolve dependencies.
	/// </summary>
	public sealed class ActivatorDependencyResolution : IDependencyResolution
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ConstructorDependencyResolution class.
		/// </summary>
		/// <param name="actualType"> The actual type of the resolution. </param>
		public ActivatorDependencyResolution(Type actualType)
		{
			if ((object)actualType == null)
				throw new ArgumentNullException("actualType");

			this.actualType = actualType;
		}

		#endregion

		#region Fields/Constants

		private readonly Type actualType;

		#endregion

		#region Properties/Indexers/Events

		private Type ActualType
		{
			get
			{
				return this.actualType;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Resolves a dependency.
		/// </summary>
		/// <returns> An instance of an object or null. </returns>
		public object Resolve()
		{
			return Activator.CreateInstance(this.ActualType);
		}

		#endregion
	}
}