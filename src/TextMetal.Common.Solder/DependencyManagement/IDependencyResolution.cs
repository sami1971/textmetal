/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// Provides the Factory Method pattern used to resolve dependencies.
	/// </summary>
	public interface IDependencyResolution
	{
		#region Methods/Operators

		/// <summary>
		/// Resolves a dependency.
		/// </summary>
		/// <returns> An instance of an object or null. </returns>
		object Resolve();

		#endregion
	}
}