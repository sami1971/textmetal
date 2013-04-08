/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Text;

using TextMetal.Common.Core;

namespace TextMetal.Framework.InputOutputModel
{
	public class FileOutputMechanism : OutputMechanism
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the FileOutputMechanism class.
		/// </summary>
		/// <param name="baseDirectoryPath"> The base output directory path. </param>
		/// <param name="logFileName"> The file name of the log file (relative to base directory path) or empty string for console output. </param>
		public FileOutputMechanism(string baseDirectoryPath, string logFileName)
		{
			if ((object)baseDirectoryPath == null)
				throw new ArgumentNullException("baseDirectoryPath");

			if ((object)logFileName == null)
				throw new ArgumentNullException("logFileName");

			this.baseDirectoryPath = baseDirectoryPath;
			this.logFileName = logFileName;

			this.SetupLogger();
		}

		#endregion

		#region Fields/Constants

		private readonly string baseDirectoryPath;
		private readonly string logFileName;

		#endregion

		#region Properties/Indexers/Events

		private string BaseDirectoryPath
		{
			get
			{
				return this.baseDirectoryPath;
			}
		}

		private string LogFileName
		{
			get
			{
				return this.logFileName;
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
			TextWriter textWriter = null;
			string fullFilePath;
			string fullDirectoryPath;

			if (!DataType.IsWhiteSpace(this.LogFileName))
			{
				fullFilePath = Path.GetFullPath(Path.Combine(this.BaseDirectoryPath, this.LogFileName));
				fullDirectoryPath = Path.GetDirectoryName(fullFilePath);
				//Console.Error.WriteLine(fullFilePath);

				if (!Directory.Exists(fullDirectoryPath))
					Directory.CreateDirectory(fullDirectoryPath);

				// do not dispose here!
				stream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
				textWriter = new StreamWriter(stream, Encoding.UTF8);
			}

			base.SetLogTextWriter(textWriter);
		}

		#endregion
	}
}