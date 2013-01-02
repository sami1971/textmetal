/*
	Copyright �2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// The exception thrown when a specific dependency resolution error occurs.
	/// </summary>
	[Serializable]
	public sealed class DependencyException : Exception
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DependencyException class.
		/// </summary>
		/// <param name="message"> The message that describes the error. </param>
		public DependencyException(string message)
			: base(message)
		{
		}

		#endregion
	}
}