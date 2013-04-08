/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

using TextMetal.Common.Core;
using TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public abstract class FileHandler : IFileHandler
	{
		#region Methods/Operators

		public void Execute(FileInfo fileInfo)
		{
			try
			{
				this.OnExecute(fileInfo);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("Exception occured in file handler type '{2}' in project file {0}:\r\n\t{1}.", fileInfo.FullName, Reflexion.GetErrors(ex, 1), this.GetType().FullName));

				if (!ConversionConfig.ConversionSettings.SupressFileHandlerExceptions)
					throw;
			}
		}

		protected abstract void OnExecute(FileInfo fileInfo);

		#endregion
	}
}