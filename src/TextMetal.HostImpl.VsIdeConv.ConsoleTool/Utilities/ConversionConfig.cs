/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities
{
	public enum VersionControlBindingAction
	{
		Leave = 0,
		Modify = 1,
		Remove = 2
	}

	public static class ConversionConfig
	{
		#region Properties/Indexers/Events

		public static IDictionary<string, string> ProjectAssemblyReferences
		{
			get
			{
				return (IDictionary<string, string>)GetConversionDictionary("projectAssemblyReferences");
			}
		}

		public static IDictionary<string, string> ProjectAttributes
		{
			get
			{
				return (IDictionary<string, string>)GetConversionDictionary("projectAttributes");
			}
		}

		public static IDictionary<string, string> ProjectProperties
		{
			get
			{
				return (IDictionary<string, string>)GetConversionDictionary("projectProperties");
			}
		}

		public static IDictionary<string, string> ProjectTargetImports
		{
			get
			{
				return (IDictionary<string, string>)GetConversionDictionary("projectTargetImports");
			}
		}

		public static IDictionary<string, string> ProjectTypeGuids
		{
			get
			{
				return (IDictionary<string, string>)GetConversionDictionary("projectTypeGuids");
			}
		}

		#endregion

		#region Methods/Operators

		private static IDictionary<string, string> GetConversionDictionary(string section)
		{
			NameValueCollection nvc;
			Dictionary<string, string> dictionary;

			nvc = (NameValueCollection)ConfigurationManager.GetSection(string.Format("conversionConfig/{0}", section));
			dictionary = new Dictionary<string, string>();

			foreach (string key in nvc.AllKeys)
			{
				if (dictionary.ContainsKey(key))
					throw new InvalidOperationException(string.Format("Key {0} already in app.cofig", key));
				dictionary.Add(key, nvc[key]);
			}

			return dictionary;
		}

		private static string GetConversionSettings(string key)
		{
			IDictionary<string, string> dictionary;
			string value;

			dictionary = GetConversionDictionary("conversionSettings");

			if (!dictionary.TryGetValue(key, out value))
				throw new InvalidOperationException(string.Format("Key {0} not found in app.cofig", key));

			return value;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public static class ConversionSettings
		{
			#region Properties/Indexers/Events

			public static bool BackupFiles
			{
				get
				{
					return bool.Parse(GetConversionSettings("BackupFiles"));
				}
			}

			public static bool ProjectRemoveRequiredTargetFrameworkNodes
			{
				get
				{
					return bool.Parse(GetConversionSettings("ProjectRemoveRequiredTargetFrameworkNodes"));
				}
			}

			public static string RootWorkingCopyPath
			{
				get
				{
					return GetConversionSettings("RootWorkingCopyPath");
				}
			}

			public static string SccAuxPath
			{
				get
				{
					return GetConversionSettings("SccAuxPath");
				}
			}

			public static string SccLocalPath
			{
				get
				{
					return GetConversionSettings("SccLocalPath");
				}
			}

			public static string SccProjectName
			{
				get
				{
					return GetConversionSettings("SccProjectName");
				}
			}

			public static string SccProvider
			{
				get
				{
					return GetConversionSettings("SccProvider");
				}
			}

			public static string SolutionExternalVersion
			{
				get
				{
					return GetConversionSettings("SolutionExternalVersion");
				}
			}

			public static string SolutionInternalVersion
			{
				get
				{
					return GetConversionSettings("SolutionInternalVersion");
				}
			}

			public static string SolutionStatePersistenceKeySccBinding
			{
				get
				{
					return GetConversionSettings("SolutionStatePersistenceKeySccBinding");
				}
			}

			public static bool SupressFileHandlerExceptions
			{
				get
				{
					return bool.Parse(GetConversionSettings("SupressFileHandlerExceptions"));
				}
			}

			public static VersionControlBindingAction VersionControlBindingAction
			{
				get
				{
					return (VersionControlBindingAction)Enum.Parse(typeof(VersionControlBindingAction), GetConversionSettings("VersionControlBindingAction"));
				}
			}

			#endregion
		}

		#endregion
	}
}