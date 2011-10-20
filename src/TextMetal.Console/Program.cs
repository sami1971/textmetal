/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TextMetal.Core.Plumbing;

namespace TextMetal.Console
{
	internal class Program
	{
		#region Methods/Operators

		private static bool GetDirectoryPathInteractive(out string directoryPath)
		{
			directoryPath = null;

			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				folderBrowserDialog.ShowNewFolderButton = true;

				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					directoryPath = folderBrowserDialog.SelectedPath;

					return true;
				}
			}

			return false;
		}

		private static bool GetFilePathInteractive(out string filePath)
		{
			filePath = null;

			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Multiselect = false;
				openFileDialog.RestoreDirectory = true;
				openFileDialog.Title = "Choose File...";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					filePath = openFileDialog.FileName;

					return true;
				}
			}

			return false;
		}

		[STAThread]
		private static int Main(string[] args)
		{
#if !DEBUG
			return TryStartup(args);
#else
			return Startup(args);
#endif
		}

		private static int Startup(string[] args)
		{
			IDictionary<string, IList<string>> arguments;
			string templateFilePath;
			string sourceFilePath;
			string baseDirectoryPath;
			string sourceStrategyAssemblyQualifiedTypeName;
			bool strictMatching;
			IDictionary<string, IList<string>> properties;
			IList<string> _arguments;

			const string CMDLN_TOKEN_TEMPLATEFILE = "templatefile";
			const string CMDLN_TOKEN_SOURCEFILE = "sourcefile";
			const string CMDLN_TOKEN_BASEDIR = "basedir";
			const string CMDLN_TOKEN_SOURCESTRATEGY_AQTN = "sourcestrategy";
			const string CMDLN_TOKEN_STRICT = "strict";
			const string CMDLN_TOKEN_PROPERTY = "property";

			arguments = AppConfig.ParseCommandLineArguments(args);

			if ((object)arguments == null ||
			    !arguments.ContainsKey(CMDLN_TOKEN_TEMPLATEFILE) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_SOURCEFILE) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_BASEDIR) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_SOURCESTRATEGY_AQTN) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_STRICT))
			{
				System.Console.WriteLine("USAGE: textmetal.exe\r\n\t-{0}:\"...\" -{1}:\"...\" -{2}:\"...\"\r\n\t-{3}:\"...\" -{4}:\"true|false\"",
				                         CMDLN_TOKEN_TEMPLATEFILE,
				                         CMDLN_TOKEN_SOURCEFILE,
				                         CMDLN_TOKEN_BASEDIR,
				                         CMDLN_TOKEN_SOURCESTRATEGY_AQTN,
				                         CMDLN_TOKEN_STRICT);

				return -1;
			}

			// required
			templateFilePath = arguments[CMDLN_TOKEN_TEMPLATEFILE].Single();
			sourceFilePath = arguments[CMDLN_TOKEN_SOURCEFILE].Single();
			baseDirectoryPath = arguments[CMDLN_TOKEN_BASEDIR].Single();
			sourceStrategyAssemblyQualifiedTypeName = arguments[CMDLN_TOKEN_SOURCESTRATEGY_AQTN].Single();
			DataType.TryParse<bool>(arguments[CMDLN_TOKEN_STRICT].Single(), out strictMatching);

			properties = new Dictionary<string, IList<string>>();

			if (arguments.TryGetValue(CMDLN_TOKEN_PROPERTY, out _arguments))
			{
				if ((object)_arguments != null)
				{
					foreach (string argument in _arguments)
					{
						IList<string> propertyValues;
						string key, value;

						if (!AppConfig.TryParseCommandLineArgumentProperty(argument, out key, out value))
							continue;

						if (!properties.ContainsKey(key))
							properties.Add(key, new List<string>());

						propertyValues = properties[key];

						// duplicate values are ignored
						if (propertyValues.Contains(value))
							continue;

						propertyValues.Add(value);
					}
				}
			}

			if (sourceFilePath == "?")
			{
				if (!GetFilePathInteractive(out sourceFilePath))
					return 0;
			}

			if (baseDirectoryPath == "?")
			{
				if (!GetDirectoryPathInteractive(out baseDirectoryPath))
					return 0;
			}

			ToolHost.Host(templateFilePath, sourceFilePath, baseDirectoryPath, sourceStrategyAssemblyQualifiedTypeName, strictMatching, properties);

			return 0;
		}

		private static int TryStartup(string[] args)
		{
			try
			{
				return Startup(args);
			}
			catch (Exception ex)
			{
				string message;

				message = Reflexion.GetErrors(ex, 0);

				System.Console.Error.WriteLine(message);
			}

			return -1;
		}

		#endregion
	}
}