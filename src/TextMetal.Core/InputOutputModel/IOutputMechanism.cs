/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.Core.InputOutputModel
{
	public interface IOutputMechanism : IDisposable
	{
		#region Properties/Indexers/Events

		TextWriter CurrentTextWriter
		{
			get;
		}

		TextWriter LogTextWriter
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void EnterScope(string scopeName);

		void LeaveScope(string scopeName);

		#endregion
	}
}