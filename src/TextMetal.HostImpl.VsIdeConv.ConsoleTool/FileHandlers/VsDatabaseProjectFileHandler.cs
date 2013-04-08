/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public class VsDatabaseProjectFileHandler : FileHandler
	{
		#region Constructors/Destructors

		private VsDatabaseProjectFileHandler()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly VsDatabaseProjectFileHandler instance = new VsDatabaseProjectFileHandler();

		#endregion

		#region Properties/Indexers/Events

		public static VsDatabaseProjectFileHandler Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		public static bool PatchDatabaseProjectFile(FileInfo fileInfo, ref string[] solutionLines)
		{
			string line;
			List<string> lines;

			bool foundSccProjectName = false;
			bool foundSccLocalPath = false;
			bool foundSccAuxPath = false;
			bool foundSccProvider = false;

			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if (ConversionConfig.ConversionSettings.BackupFiles)
				fileInfo.CopyTo(fileInfo.FullName + ".bak", true);

			lines = new List<string>();

			for (int index = 0; index < solutionLines.Length; index++)
			{
				line = solutionLines[index];

				if (Regex.IsMatch(line, "\\s*SccProjectName\\s*=\\s*\"(.*)\""))
				{
					foundSccProjectName = true;

					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
						       (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccProjectName : line);
				}
				else if (Regex.IsMatch(line, "\\s*SccLocalPath\\s*=\\s*\"(.*)\""))
				{
					foundSccLocalPath = true;

					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
						       (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccLocalPath : line);
				}
				else if (Regex.IsMatch(line, "\\s*SccAuxPath\\s*=\\s*\"(.*)\""))
				{
					foundSccAuxPath = true;

					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
						       (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccAuxPath : line);
				}
				else if (Regex.IsMatch(line, "\\s*SccProvider\\s*=\\s*\"(.*)\""))
				{
					foundSccProvider = true;

					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
						       (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccProvider : line);
				}
				else
					line = line;

				lines.Add(line);
			}

			if (ConversionConfig.ConversionSettings.VersionControlBindingAction != VersionControlBindingAction.Leave &&
			    (!foundSccProjectName && !foundSccLocalPath && !foundSccAuxPath && !foundSccProvider))
				Console.WriteLine(string.Format("Warning: SccProjectName/SccLocalPath/SccAuxPath/SccProvider tags not found in project file {0}.", fileInfo.FullName));

			solutionLines = lines.ToArray();
			return true;
		}

		protected override void OnExecute(FileInfo fileInfo)
		{
			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			Debug.WriteLine("Visual Studio database project: " + fileInfo.FullName);

			switch (fileInfo.Extension.ToLower())
			{
				case ".dbp":
					this.PatchDatabaseProjectFile(fileInfo);
					break;
				default:
					throw new InvalidOperationException(string.Format("Unsupported file type: {0}", fileInfo.Extension));
			}
		}

		private void PatchDatabaseProjectFile(FileInfo fileInfo)
		{
			bool shouldCommit;
			string[] solutionLines;

			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if (ConversionConfig.ConversionSettings.BackupFiles)
				fileInfo.CopyTo(fileInfo.FullName + ".bak", true);

			solutionLines = File.ReadAllLines(fileInfo.FullName);

			shouldCommit = PatchDatabaseProjectFile(fileInfo, ref solutionLines);

			if (shouldCommit)
				File.WriteAllLines(fileInfo.FullName, solutionLines, Encoding.UTF8);
		}

		#endregion
	}
}