/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.Framework.InputOutputModel
{
	public class StringOutputMechanism : OutputMechanism
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the StringOutputMechanism class.
		/// </summary>
		public StringOutputMechanism()
		{
			this.RecycleOutput();
		}

		#endregion

		#region Methods/Operators

		protected override void CoreEnter(string scopeName)
		{
		}

		protected override void CoreLeave(string scopeName)
		{
		}

		public string RecycleOutput()
		{
			string value = null;

			if (base.TextWriters.Count > 0)
			{
				base.CurrentTextWriter.Flush();

				value = base.CurrentTextWriter.ToString();

				base.TextWriters.Pop().Dispose();
			}

			base.TextWriters.Push(new StringWriter());

			return value;
		}

		#endregion
	}
}