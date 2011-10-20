/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TextMetal.ConnectionDialogApi;
using TextMetal.Core.Plumbing;

namespace TextMetal.Console
{
	internal enum InteractiveSourceType
	{
		None = 0,
		DatabaseConnection = 1,
		FileSystem = 2,
		ConsoleTextInput = 3
	}

	internal class Program
	{
		#region Methods/Operators

		private static bool GetConsoleTextInputInteractive(out string consoleInputLine)
		{
			string value;

			consoleInputLine = null;

			System.Console.WriteLine();
			System.Console.Write("?>");
			value = System.Console.ReadLine();

			if (DataType.IsNullOrWhiteSpace(value))
				return false;

			consoleInputLine = value;
			return true;
		}

		private static bool GetDatabaseConnectionInteractive(out string connectionString /*, out Type connectionType*/)
		{
			connectionString = null;
			//connectionType = null;

			using (DataConnectionDialog dataConnectionDialog = new DataConnectionDialog())
			{
				DataSource.AddStandardDataSources(dataConnectionDialog);

				if (DataConnectionDialog.Show(dataConnectionDialog) == DialogResult.OK)
				{
					connectionString = dataConnectionDialog.ConnectionString;
					//connectionType = dataConnectionDialog.SelectedDataProvider.TargetConnectionType;

					return true;
				}
			}

			return false;
		}

		private static bool GetFileSystemInteractive(out string filePath)
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
			InteractiveSourceType interactiveSourceType;
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
			const string CMDLN_TOKEN_INTERACTIVE_SOURCE = "interactivesource";

			arguments = AppConfig.ParseCommandLineArguments(args);

			if ((object)arguments == null ||
			    !arguments.ContainsKey(CMDLN_TOKEN_TEMPLATEFILE) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_SOURCEFILE) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_BASEDIR) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_SOURCESTRATEGY_AQTN) ||
			    !arguments.ContainsKey(CMDLN_TOKEN_STRICT))
			{
				System.Console.WriteLine("USAGE: textmetal.exe\r\n\t-{0}:\"...\" -{1}:\"...\" -{2}:\"...\"\r\n\t-{3}:\"...\" -{4}:\"true|false\"\r\n\t[-{5}:\"none|databaseconnection|\r\n\t\tfilesystem|consoletextinput\"]",
				                         CMDLN_TOKEN_TEMPLATEFILE,
				                         CMDLN_TOKEN_SOURCEFILE,
				                         CMDLN_TOKEN_BASEDIR,
				                         CMDLN_TOKEN_SOURCESTRATEGY_AQTN,
				                         CMDLN_TOKEN_STRICT,
										 CMDLN_TOKEN_INTERACTIVE_SOURCE);

				return -1;
			}

			// required
			templateFilePath = arguments[CMDLN_TOKEN_TEMPLATEFILE].Single();
			sourceFilePath = arguments[CMDLN_TOKEN_SOURCEFILE].Single();
			baseDirectoryPath = arguments[CMDLN_TOKEN_BASEDIR].Single();
			sourceStrategyAssemblyQualifiedTypeName = arguments[CMDLN_TOKEN_SOURCESTRATEGY_AQTN].Single();
			DataType.TryParse<bool>(arguments[CMDLN_TOKEN_STRICT].Single(), out strictMatching);

			// optional
			interactiveSourceType = InteractiveSourceType.None;
			if (arguments.ContainsKey(CMDLN_TOKEN_INTERACTIVE_SOURCE))
				DataType.TryParse<InteractiveSourceType>(arguments[CMDLN_TOKEN_INTERACTIVE_SOURCE].Single(), out interactiveSourceType);

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
				switch (interactiveSourceType)
				{
					case InteractiveSourceType.None:
						return 0;
					case InteractiveSourceType.DatabaseConnection:

						if (!GetDatabaseConnectionInteractive(out sourceFilePath))
							return 0;

						break;
					case InteractiveSourceType.FileSystem:

						if (!GetFileSystemInteractive(out sourceFilePath))
							return 0;

						break;
					case InteractiveSourceType.ConsoleTextInput:

						if (!GetConsoleTextInputInteractive(out sourceFilePath))
							return 0;

						break;
					default:
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");
				}
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