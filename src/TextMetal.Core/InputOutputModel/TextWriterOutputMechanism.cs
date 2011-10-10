/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.Core.InputOutputModel
{
	public class TextWriterOutputMechanism : OutputMechanism
	{
		#region Constructors/Destructors

		public TextWriterOutputMechanism(TextWriter textWriter)
		{
			if ((object)textWriter == null)
				throw new ArgumentNullException("textWriter");

			base.TextWriters.Push(textWriter);
		}

		#endregion

		#region Methods/Operators

		protected override void CoreEnter(string scopeName)
		{
		}

		protected override void CoreLeave(string scopeName)
		{
		}

		#endregion
	}
}