/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

using TextMetal.HostImpl.VsIdeConv.ConsoleTool.Utilities;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public class MsBuildProjectFileHandler : FileHandler
	{
		#region Constructors/Destructors

		private MsBuildProjectFileHandler()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly MsBuildProjectFileHandler instance = new MsBuildProjectFileHandler();

		#endregion

		#region Properties/Indexers/Events

		public static MsBuildProjectFileHandler Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		public static bool PatchMsBuildProjectFile(FileInfo fileInfo, XmlDocument projectXml)
		{
			XmlElement element, e;
			XmlAttribute attribute;
			XmlAttribute a;
			XmlNodeList elements;

			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if ((object)projectXml == null)
				throw new ArgumentNullException("projectXml");

			// Project
			element = (XmlElement)projectXml.SelectSingleNode("/*[local-name() = 'Project' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']");

			if ((object)element == null)
			{
				Console.WriteLine(string.Format("Warning: Project element not found in project file {0}.", fileInfo.FullName));
				return false;
			}
			else
			{
				foreach (string key in ConversionConfig.ProjectAttributes.Keys)
				{
					string value;

					value = (string)ConversionConfig.ProjectAttributes[key];
					a = element.Attributes[key];

					if ((object)a == null)
					{
						a = projectXml.CreateAttribute("", key, "");
						element.Attributes.Append(a);
					}

					a.Value = value;
				}
			}

			foreach (string key in ConversionConfig.ProjectAssemblyReferences.Keys)
			{
				string value;

				value = (string)ConversionConfig.ProjectAssemblyReferences[key];
				a = (XmlAttribute)element.SelectSingleNode(string.Format("./*[local-name() = 'ItemGroup' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/*[local-name() = 'Reference' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/@Include[../@Include = '{0}']", key));

				if ((object)a != null)
					a.Value = value;
			}

			foreach (string key in ConversionConfig.ProjectTargetImports.Keys)
			{
				string value;

				value = (string)ConversionConfig.ProjectTargetImports[key];
				a = (XmlAttribute)element.SelectSingleNode(string.Format("./*[local-name() = 'Import' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/@Project[../@Project = '{0}']", key));

				if ((object)a != null)
					a.Value = value;
			}

			// ItemGroup/Reference/RequiredTargetFramework
			if (ConversionConfig.ConversionSettings.ProjectRemoveRequiredTargetFrameworkNodes)
			{
				elements = element.SelectNodes("./*[local-name() = 'ItemGroup' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/*[local-name() = 'Reference' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/*[local-name() = 'RequiredTargetFramework' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']");

				if ((object)elements != null)
				{
					foreach (XmlElement ee in elements)
						ee.ParentNode.RemoveChild(ee);
				}
			}

			// Project/PropertyGroup
			element = (XmlElement)projectXml.SelectSingleNode("/*[local-name() = 'Project' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']/*[local-name() = 'PropertyGroup' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003' and not(@*)]");

			// version control
			if (ConversionConfig.ConversionSettings.VersionControlBindingAction != VersionControlBindingAction.Leave)
			{
				string key;

				key = "SccAuxPath";
				e = (XmlElement)element.SelectSingleNode(string.Format("./*[local-name() = '{0}' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']", key));

				if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove)
				{
					if ((object)e != null)
						element.RemoveChild(e);
				}
				else if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify)
				{
					if ((object)e == null)
					{
						e = projectXml.CreateElement("", key, "http://schemas.microsoft.com/developer/msbuild/2003");
						element.AppendChild(e);
					}

					e.InnerText = ConversionConfig.ConversionSettings.SccAuxPath;
				}

				key = "SccLocalPath";
				e = (XmlElement)element.SelectSingleNode(string.Format("./*[local-name() = '{0}' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']", key));

				if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove)
				{
					if ((object)e != null)
						element.RemoveChild(e);
				}
				else if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify)
				{
					if ((object)e == null)
					{
						e = projectXml.CreateElement("", key, "http://schemas.microsoft.com/developer/msbuild/2003");
						element.AppendChild(e);
					}

					e.InnerText = ConversionConfig.ConversionSettings.SccLocalPath;
				}

				key = "SccProvider";
				e = (XmlElement)element.SelectSingleNode(string.Format("./*[local-name() = '{0}' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']", key));

				if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove)
				{
					if ((object)e != null)
						element.RemoveChild(e);
				}
				else if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify)
				{
					if ((object)e == null)
					{
						e = projectXml.CreateElement("", key, "http://schemas.microsoft.com/developer/msbuild/2003");
						element.AppendChild(e);
					}

					e.InnerText = ConversionConfig.ConversionSettings.SccProvider;
				}

				key = "SccProjectName";
				e = (XmlElement)element.SelectSingleNode(string.Format("./*[local-name() = '{0}' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']", key));

				if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Remove)
				{
					if ((object)e != null)
						element.RemoveChild(e);
				}
				else if (ConversionConfig.ConversionSettings.VersionControlBindingAction == VersionControlBindingAction.Modify)
				{
					if ((object)e == null)
					{
						e = projectXml.CreateElement("", key, "http://schemas.microsoft.com/developer/msbuild/2003");
						element.AppendChild(e);
					}

					e.InnerText = ConversionConfig.ConversionSettings.SccProjectName;
				}
			}

			foreach (string key in ConversionConfig.ProjectProperties.Keys)
			{
				string value;

				if (key == "SccAuxPath" || key == "SccLocalPath" || key == "SccProvider" || key == "SccProjectName")
					continue; // ignore SCC values

				value = (string)ConversionConfig.ProjectProperties[key];
				e = (XmlElement)element.SelectSingleNode(string.Format("./*[local-name() = '{0}' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']", key));

				if ((object)e == null)
				{
					e = projectXml.CreateElement("", key, "http://schemas.microsoft.com/developer/msbuild/2003");
					element.AppendChild(e);
				}

				e.InnerText = value;
			}

			e = (XmlElement)element.SelectSingleNode("./*[local-name() = 'ProjectTypeGuids' and namespace-uri() = 'http://schemas.microsoft.com/developer/msbuild/2003']");

			if ((object)e != null)
			{
				string[] projectTypeGuids;
				string oldProjectTypeGuid, newProjectTypeGuid;

				projectTypeGuids = (e.InnerXml ?? "").Split(';');

				if ((object)projectTypeGuids != null)
				{
					for (int i = 0; i < projectTypeGuids.Length; i++)
					{
						oldProjectTypeGuid = projectTypeGuids[i];

						if (ConversionConfig.ProjectTypeGuids.TryGetValue(oldProjectTypeGuid, out newProjectTypeGuid))
							projectTypeGuids[i] = newProjectTypeGuid;
					}
				}

				e.InnerText = string.Join(";", projectTypeGuids);
			}

			return true;
		}

		protected override void OnExecute(FileInfo fileInfo)
		{
			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			Debug.WriteLine("MSBuild project file: " + fileInfo.FullName);

			switch (fileInfo.Extension.ToLower())
			{
				case ".csproj":
				case ".vbproj":
					this.PatchMsBuildProjectFile(fileInfo);
					break;
				default:
					throw new InvalidOperationException(string.Format("Unsupported file type: {0}", fileInfo.Extension));
			}
		}

		private void PatchMsBuildProjectFile(FileInfo fileInfo)
		{
			XmlDocument projectXml;
			XmlElement element, e;
			XmlAttribute attribute;
			XmlAttribute a;
			StreamReader streamReader;
			bool shouldCommit;

			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			if (ConversionConfig.ConversionSettings.BackupFiles)
				fileInfo.CopyTo(fileInfo.FullName + ".bak", true);

			projectXml = new XmlDocument();

			using (streamReader = fileInfo.OpenText())
				projectXml.Load(streamReader);

			shouldCommit = PatchMsBuildProjectFile(fileInfo, projectXml);

			if (shouldCommit)
				projectXml.Save(fileInfo.FullName);
		}

		#endregion
	}
}