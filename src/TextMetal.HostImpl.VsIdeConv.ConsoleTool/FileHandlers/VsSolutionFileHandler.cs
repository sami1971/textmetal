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

using TextMetal.Common.Core;
using TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public class VsSolutionFileHandler : FileHandler
	{
		#region Constructors/Destructors

		private VsSolutionFileHandler()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly VsSolutionFileHandler instance = new VsSolutionFileHandler();

		#endregion

		#region Properties/Indexers/Events

		public static VsSolutionFileHandler Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		public static bool PatchSolutionFile(FileInfo fileInfo, ref string[] solutionLines)
		{
			string line;
			List<string> lines;

			bool insideProject = false;
			bool insideWebSiteProject = false;
			bool insideGSSCC = false;
			bool foundGSSCC = false;
			bool foundEGSforSCC = false;
			bool foundSlnFileVer = false;
			bool foundVsVer = false;
			Match matchSlnFileVer, matchVsVer, matchProjectPath;
			List<string> projectPaths;

			if (fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if (solutionLines == null)
				throw new ArgumentNullException("solutionLines");

			lines = new List<string>();
			projectPaths = new List<string>();

			for (int index = 0; index < solutionLines.Length; index++)
			{
				line = solutionLines[index];

				matchProjectPath = Regex.Match(line, string.Format(@"Project\({0}\)\s=\s{1},\s{2},\s{3}", new object[] { "\"\\{(?<keyGuid>[A-Fa-f0-9\\-]+)\\}\"", "\"(?<prjName>[^\"]+)\"", "\"(?<prjPath>[^\"]+)\"", "\"\\{(?<prjGuid>[A-Fa-f0-9\\-]+)\\}\"" }));
				matchSlnFileVer = Regex.Match(line, @"Microsoft Visual Studio Solution File\, Format Version ([0-9]{1,2}\.[0-9]{2,2})");
				matchVsVer = Regex.Match(line, @"\# Visual Studio ([0-9]{4,4})");

				if (foundSlnFileVer && foundVsVer && (object)matchProjectPath != null && matchProjectPath.Success)
				{
					projectPaths.Add(matchProjectPath.Groups["prjPath"].Value);
					insideProject = true;
				}
				else if ((object)matchSlnFileVer != null && matchSlnFileVer.Success)
				{
					line = !DataType.IsNullOrWhiteSpace(ConversionConfig.ConversionSettings.SolutionInternalVersion) ?
					                                                                                                 	"Microsoft Visual Studio Solution File, Format Version " + ConversionConfig.ConversionSettings.SolutionInternalVersion : line;

					foundSlnFileVer = true;
				}
				else if ((object)matchVsVer != null && matchVsVer.Success)
				{
					line = !DataType.IsNullOrWhiteSpace(ConversionConfig.ConversionSettings.SolutionExternalVersion) ?
					                                                                                                 	"# Visual Studio " + ConversionConfig.ConversionSettings.SolutionExternalVersion : line;

					foundVsVer = true;
				}

				else if (foundSlnFileVer && foundVsVer && insideProject && Regex.IsMatch(line, @"\s*EndProject"))
					insideProject = false;

				else if (foundSlnFileVer && foundVsVer && insideProject && Regex.IsMatch(line, @"\s*ProjectSection\(WebsiteProperties\) = preProject"))
					insideWebSiteProject = true;

				else if (foundSlnFileVer && foundVsVer && insideProject && insideWebSiteProject && Regex.IsMatch(line, "\\s*SccProjectName\\s*=\\s*\"(.*)\""))
				{
					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
					                                                                                                                  	(ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccProjectName : line);
				}
				else if (foundSlnFileVer && foundVsVer && insideProject && insideWebSiteProject && Regex.IsMatch(line, "\\s*SccAuxPath\\s*=\\s*\"(.*)\""))
				{
					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
					                                                                                                                  	(ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccAuxPath : line);
				}
				else if (foundSlnFileVer && foundVsVer && insideProject && insideWebSiteProject && Regex.IsMatch(line, "\\s*SccLocalPath\\s*=\\s*\"(.*)\""))
				{
					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
					                                                                                                                  	(ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccLocalPath : line);
				}
				else if (foundSlnFileVer && foundVsVer && insideProject && insideWebSiteProject && Regex.IsMatch(line, "\\s*SccProvider\\s*=\\s*\"(.*)\""))
				{
					line = ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove ? "" :
					                                                                                                                  	(ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify ? ConversionConfig.ConversionSettings.SccProvider : line);
				}

				else if (foundSlnFileVer && foundVsVer && insideProject && insideWebSiteProject && Regex.IsMatch(line, @"\s*EndProjectSection"))
					insideWebSiteProject = false;
				else if (foundSlnFileVer && foundVsVer && !insideProject && Regex.IsMatch(line, @"\s*GlobalSection\(" + ConversionConfig.ConversionSettings.SolutionStatePersistenceKeySccBinding + @"\)"))
					foundGSSCC = insideGSSCC = true;
				else if (foundSlnFileVer && foundVsVer && !insideProject && foundGSSCC && insideGSSCC && Regex.IsMatch(line, @"\s*EndGlobalSection"))
				{
					// write nothing
					foundEGSforSCC = true;
				}

				if (!insideGSSCC || ConversionConfig.ConversionSettings.VersionControlBindingAction != VersionControlBindingAction.Remove)
					lines.Add(line);

				if (foundEGSforSCC && insideGSSCC)
					insideGSSCC = false;
			}

			if (!foundSlnFileVer)
			{
				Console.WriteLine(string.Format("Warning: 'Microsoft Visual Studio Solution File, Format Version' not found in solution file {0}.", fileInfo.FullName));
				return false;
			}

			if (!foundVsVer)
			{
				Console.WriteLine(string.Format("Warning: '# Visual Studio' not found in solution file {0}.", fileInfo.FullName));
				return false;
			}

			if (ConversionConfig.ConversionSettings.VersionControlBindingAction != VersionControlBindingAction.Leave && !foundGSSCC)
				Console.WriteLine(string.Format("Warning: GlobalSection({1}) section not found in solution file {0}.", fileInfo.FullName, ConversionConfig.ConversionSettings.SolutionStatePersistenceKeySccBinding));

			if (ConversionConfig.ConversionSettings.VersionControlBindingAction != VersionControlBindingAction.Leave && !foundEGSforSCC)
				Console.WriteLine(string.Format("Warning: EndGlobalSection for GlobalSection({1}) section not found in solution file {0}.", fileInfo.FullName, ConversionConfig.ConversionSettings.SolutionStatePersistenceKeySccBinding));

			Debug.WriteLine("Info: The following solution Project entities were found: {0}", string.Join(" | ", projectPaths.ToArray()));

			solutionLines = lines.ToArray();
			return true;
		}

		protected override void OnExecute(FileInfo fileInfo)
		{
			if (fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			Debug.WriteLine("Visual Studio solution file: " + fileInfo.FullName);

			switch (fileInfo.Extension.ToLower())
			{
				case ".sln":
					this.PatchSolutionFile(fileInfo);
					break;
				default:
					throw new InvalidOperationException(string.Format("Unsupported file type: {0}", fileInfo.Extension));
			}
		}

		private void PatchSolutionFile(FileInfo fileInfo)
		{
			bool shouldCommit;
			string[] solutionLines;

			if (fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if (ConversionConfig.ConversionSettings.BackupFiles)
				fileInfo.CopyTo(fileInfo.FullName + ".bak", true);

			solutionLines = File.ReadAllLines(fileInfo.FullName);

			shouldCommit = PatchSolutionFile(fileInfo, ref solutionLines);

			if (shouldCommit)
				File.WriteAllLines(fileInfo.FullName, solutionLines, Encoding.UTF8);
		}

		#endregion
	}
}