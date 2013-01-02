/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Framework.Core
{
	/// <summary>
	/// Provides for input mechanics.
	/// </summary>
	public interface IInputMechanism : IDisposable
	{
		#region Methods/Operators

		/// <summary>
		/// Loads an assembly by name.
		/// </summary>
		/// <param name="assemblyName"> The assembly name to load. </param>
		/// <returns> An assembly object or null. </returns>
		Assembly LoadAssembly(string assemblyName);

		/// <summary>
		/// Loads content by resource name. Resource name semantics is implementation specific.
		/// </summary>
		/// <param name="resourceName"> The resource name to load. </param>
		/// <returns> The text content or null. </returns>
		string LoadContent(string resourceName);

		/// <summary>
		/// Loads an template fragment by resource name. Resource name semantics is implementation specific.
		/// </summary>
		/// <param name="resourceName"> The resource name to load. </param>
		/// <returns> The template fragment root object or null. </returns>
		ITemplateXmlObject LoadFragment(string resourceName);

		#endregion
	}
}