/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.Core.InputOutputModel
{
	/// <summary>
	/// 	Provides for output mechanics.
	/// </summary>
	public interface IOutputMechanism : IDisposable
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the current text writer instance.
		/// </summary>
		TextWriter CurrentTextWriter
		{
			get;
		}

		/// <summary>
		/// 	Gets the current log text writer instance.
		/// </summary>
		TextWriter LogTextWriter
		{
			get;
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Enters (pushes) an output scope as deliniated by scope name. Scope name semantics is implementation specific.
		/// </summary>
		/// <param name="scopeName"> The scope name to push. </param>
		void EnterScope(string scopeName);

		/// <summary>
		/// 	Leaves (pops) an output scope as deliniated by scope name. Scope name semantics is implementation specific.
		/// </summary>
		/// <param name="scopeName"> The scope name to pop. </param>
		void LeaveScope(string scopeName);

		#endregion
	}
}