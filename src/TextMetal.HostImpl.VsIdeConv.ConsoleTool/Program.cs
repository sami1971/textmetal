/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Common.Core;
using TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers;
using TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool
{
	internal class Program
	{
		#region Fields/Constants

		private static readonly Dictionary<string, IFileHandler> registeredFileExtensionHandlers = new Dictionary<string, IFileHandler>(StringComparer.InvariantCultureIgnoreCase);
		private static readonly Dictionary<string, IFileHandler> registeredFileNameHandlers = new Dictionary<string, IFileHandler>(StringComparer.InvariantCultureIgnoreCase);

		#endregion

		#region Properties/Indexers/Events

		private static Dictionary<string, IFileHandler> RegisteredFileExtensionHandlers
		{
			get
			{
				return registeredFileExtensionHandlers;
			}
		}

		private static Dictionary<string, IFileHandler> RegisteredFileNameHandlers
		{
			get
			{
				return registeredFileNameHandlers;
			}
		}

		#endregion

		#region Methods/Operators

		private static int Main(string[] args)
		{
			FileSystemEnumerator fse;
			DateTime start, end;
			TimeSpan duration;

			start = DateTime.Now;

			RegisteredFileExtensionHandlers.Add(".testrunconfig", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".vsmdi", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance));

			RegisteredFileExtensionHandlers.Add(".suo", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".user", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".cache", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));

			RegisteredFileExtensionHandlers.Add(".resharper", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".VisualState.xml", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));

			RegisteredFileExtensionHandlers.Add(".dbp", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, VsDatabaseProjectFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".vdproj", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, VsDeployProjectFileHandler.Instance));

			if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove)
			{
				RegisteredFileExtensionHandlers.Add(".vspscc", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
				RegisteredFileExtensionHandlers.Add(".vssscc", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
				RegisteredFileExtensionHandlers.Add(".vsscc", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
				RegisteredFileNameHandlers.Add("MSSCCPRJ.SCC", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
				RegisteredFileNameHandlers.Add("VssVer.scc", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
				RegisteredFileNameHandlers.Add("VssVer2.scc", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, FsDeleteFileHandler.Instance));
			}

			RegisteredFileExtensionHandlers.Add(".sln", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, VsSolutionFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".csproj", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, MsBuildProjectFileHandler.Instance));
			RegisteredFileExtensionHandlers.Add(".vbproj", ChainFileHandler.GetChain(FsClearRoFileHandler.Instance, MsBuildProjectFileHandler.Instance));

			try
			{
				if (DataType.IsNullOrWhiteSpace(ConversionConfig.ConversionSettings.RootWorkingCopyPath))
					throw new InvalidOperationException("Invalid root working copy path");

				if (!File.Exists(ConversionConfig.ConversionSettings.RootWorkingCopyPath) &&
				    !Directory.Exists(ConversionConfig.ConversionSettings.RootWorkingCopyPath))
					throw new InvalidOperationException("Root working copy path directory path does not exist");

				fse = new FileSystemEnumerator();
				fse.FileFound += fse_FileFound;
				fse.DirectoryFound += fse_DirectoryFound;

				fse.EnumerateFileSystem(ConversionConfig.ConversionSettings.RootWorkingCopyPath);

				fse.FileFound -= fse_FileFound;
				fse.DirectoryFound -= fse_DirectoryFound;
			}
			catch (Exception ex)
			{
				Console.WriteLine("******************** Unhandled Exception ********************");
				Console.WriteLine(Reflexion.GetErrors(ex, 0));
				Console.WriteLine("******************** Unhandled Exception ********************");

				return -1;
			}
			finally
			{
				end = DateTime.Now;
				duration = end - start;
				Console.WriteLine("Operation duration: {0}", duration);
			}

			return 0;
		}

		private static void fse_DirectoryFound(DirectoryInfo directoryInfo)
		{
		}

		private static void fse_FileFound(FileInfo fileInfo)
		{
			IFileHandler fileHandler;

			FsClearRoFileHandler.Instance.Execute(fileInfo);

			if (RegisteredFileNameHandlers.ContainsKey(fileInfo.Name))
				fileHandler = RegisteredFileNameHandlers[fileInfo.Name];
			else
			{
				if (RegisteredFileExtensionHandlers.ContainsKey(fileInfo.Extension))
					fileHandler = RegisteredFileExtensionHandlers[fileInfo.Extension];
				else
					fileHandler = null;
			}

			if ((object)fileHandler != null)
				fileHandler.Execute(fileInfo);
		}

		#endregion
	}
}