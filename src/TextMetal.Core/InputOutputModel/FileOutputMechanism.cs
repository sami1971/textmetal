/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Text;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.InputOutputModel
{
	public class FileOutputMechanism : OutputMechanism
	{
		#region Constructors/Destructors

		public FileOutputMechanism(string baseDirectoryPath)
		{
			if ((object)baseDirectoryPath == null)
				throw new ArgumentNullException("baseDirectoryPath");

			this.baseDirectoryPath = baseDirectoryPath;

			this.SetupLogger();
		}

		#endregion

		#region Fields/Constants

		private readonly string baseDirectoryPath;

		#endregion

		#region Properties/Indexers/Events

		private string BaseDirectoryPath
		{
			get
			{
				return this.baseDirectoryPath;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreEnter(string scopeName)
		{
			FileStream stream;
			TextWriter textWriter;
			string fullFilePath;
			string fullDirectoryPath;

			if (DataType.IsNullOrWhiteSpace(scopeName))
				return;

			fullFilePath = Path.GetFullPath(Path.Combine(this.BaseDirectoryPath, scopeName));
			fullDirectoryPath = Path.GetDirectoryName(fullFilePath);
			//Console.Error.WriteLine(fullFilePath);

			if (!Directory.Exists(fullDirectoryPath))
				Directory.CreateDirectory(fullDirectoryPath);

			// do not dispose here!
			stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
			textWriter = new StreamWriter(stream, Encoding.UTF8);

			base.TextWriters.Push(textWriter);
		}

		protected override void CoreLeave(string scopeName)
		{
			TextWriter textWriter;

			if (DataType.IsNullOrWhiteSpace(scopeName))
				return;

			textWriter = base.TextWriters.Pop();
			textWriter.Flush();
			textWriter.Dispose();
		}

		private void SetupLogger()
		{
			FileStream stream;
			TextWriter textWriter;
			string fullFilePath;
			string fullDirectoryPath;
			const string LOG_FILE_NAME = "#textmetal.log";

			fullFilePath = Path.GetFullPath(Path.Combine(this.BaseDirectoryPath, LOG_FILE_NAME));
			fullDirectoryPath = Path.GetDirectoryName(fullFilePath);
			//Console.Error.WriteLine(fullFilePath);

			if (!Directory.Exists(fullDirectoryPath))
				Directory.CreateDirectory(fullDirectoryPath);

			// do not dispose here!
			stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
			textWriter = new StreamWriter(stream, Encoding.UTF8);

			this.LogTextWriter = textWriter;
		}

		#endregion
	}
}