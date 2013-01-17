/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities
{
	public class FileSystemEnumerator
	{
		#region Constructors/Destructors

		public FileSystemEnumerator()
		{
		}

		#endregion

		#region Fields/Constants

		private bool cancel = false;

		#endregion

		#region Properties/Indexers/Events

		public event DirectoryFoundHandler DirectoryFound;
		public event FileFoundHandler FileFound;

		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			private set
			{
				this.cancel = true;
			}
		}

		#endregion

		#region Methods/Operators

		public void EnumerateFileSystem(string directoryPath)
		{
			string[] directories;
			string[] files;

			if (this.Cancel)
				return;

			if (File.Exists(directoryPath))
			{
				files = new string[] { Path.GetFullPath(directoryPath) };
				directories = null;
			}
			else
			{
				files = Directory.GetFiles(directoryPath);
				directories = Directory.GetDirectories(directoryPath);
			}

			if ((object)files != null)
			{
				foreach (string file in files)
				{
					if (this.Cancel)
						return;

					if (this.FileFound != null)
						this.FileFound(new FileInfo(file));
				}
			}

			if ((object)directories != null)
			{
				foreach (string directory in directories)
				{
					if (this.Cancel)
						return;

					if (this.DirectoryFound != null)
						this.DirectoryFound(new DirectoryInfo(directory));

					this.EnumerateFileSystem(directory);
				}
			}
		}

		public void SignalCancel()
		{
			if (this.Cancel)
				throw new InvalidOperationException("Already canceled");

			this.Cancel = true;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public delegate void DirectoryFoundHandler(DirectoryInfo directoryInfo);

		public delegate void FileFoundHandler(FileInfo fileInfo);

		#endregion
	}
}