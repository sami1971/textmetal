/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// 	Provides the Factory Method pattern used to resolve dependencies.
	/// </summary>
	/// <typeparam name="TObject"> The actual type of the resolution. </typeparam>
	public sealed class ConstructorDependencyResolution<TObject> : IDependencyResolution
		where TObject : new()
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ConstructorDependencyResolution`1 class.
		/// </summary>
		public ConstructorDependencyResolution()
		{
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Resolves a dependency.
		/// </summary>
		/// <returns> An instance of an object or null. </returns>
		public object Resolve()
		{
			return new TObject();
		}

		#endregion
	}
}