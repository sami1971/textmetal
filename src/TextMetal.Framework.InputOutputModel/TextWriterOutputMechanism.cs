/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.Framework.InputOutputModel
{
	public class TextWriterOutputMechanism : OutputMechanism
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the TextWriterOutputMechanism class.
		/// </summary>
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