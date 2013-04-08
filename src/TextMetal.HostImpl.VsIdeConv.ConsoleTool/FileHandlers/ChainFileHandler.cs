/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;
using System.IO;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public class ChainFileHandler : FileHandler
	{
		#region Constructors/Destructors

		private ChainFileHandler(IFileHandler[] fileHandlerChain)
		{
			if ((object)fileHandlerChain == null)
				throw new ArgumentNullException("fileHandlerChain");

			this.fileHandlerChain = fileHandlerChain;
		}

		#endregion

		#region Fields/Constants

		private readonly IFileHandler[] fileHandlerChain;

		#endregion

		#region Properties/Indexers/Events

		private IFileHandler[] FileHandlerChain
		{
			get
			{
				return this.fileHandlerChain;
			}
		}

		#endregion

		#region Methods/Operators

		public static ChainFileHandler GetChain(params IFileHandler[] fileHandlerChain)
		{
			if ((object)fileHandlerChain == null)
				throw new ArgumentNullException("fileHandlerChain");

			return new ChainFileHandler(fileHandlerChain);
		}

		protected override void OnExecute(FileInfo fileInfo)
		{
			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			Debug.WriteLine("<< Enter: {0}", fileInfo.FullName);

			if ((object)this.FileHandlerChain != null)
			{
				foreach (IFileHandler fileHandler in this.FileHandlerChain)
					fileHandler.Execute(fileInfo);
			}

			Debug.WriteLine(">> Leave: {0}", fileInfo.FullName);
		}

		#endregion
	}
}