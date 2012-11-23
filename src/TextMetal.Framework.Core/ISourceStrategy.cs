/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Framework.Core
{
	/// <summary>
	/// 	Provides a strategy pattern around aquiring source objects.
	/// </summary>
	public interface ISourceStrategy
	{
		#region Methods/Operators

		/// <summary>
		/// 	Gets the source object.
		/// </summary>
		/// <param name="sourceFilePath"> The source file path or lossely, a URI to the source repository (e.g. database). </param>
		/// <param name="properties"> A list of arbitrary properties (key/value pairs). </param>
		/// <returns> An source object instance or null. </returns>
		object GetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties);

		#endregion
	}
}