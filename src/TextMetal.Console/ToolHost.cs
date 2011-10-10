/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using TextMetal.Core;
using TextMetal.Core.AssociativeModel;
using TextMetal.Core.InputOutputModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.SourceModel;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Console
{
	public static class ToolHost
	{
		#region Methods/Operators

		public static void Host(string templateFilePath, string sourceFilePath, string baseDirectoryPath,
		                        string sourceStrategyAssemblyQualifiedTypeName, bool strictMatching, IDictionary<string, IList<string>> properties)
		{
			IXmlPersistEngine xpe;
			TemplateConstruct template;
			object source;
			ModelConstruct modelConstruct = null;
			TemplatingContext templatingContext;
			Dictionary<string, object> globalVariableTable;
			string toolVersion;
			string templateDirectoryPath;
			Type sourceStrategyType;
			ISourceStrategy sourceStrategy;

			if ((object)templateFilePath == null)
				throw new ArgumentNullException("templateFilePath");

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)baseDirectoryPath == null)
				throw new ArgumentNullException("baseDirectoryPath");

			if ((object)sourceStrategyAssemblyQualifiedTypeName == null)
				throw new ArgumentNullException("sourceStrategyAssemblyQualifiedTypeName");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(templateFilePath))
				throw new ArgumentOutOfRangeException("templateFilePath");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			if (DataType.IsWhiteSpace(baseDirectoryPath))
				throw new ArgumentOutOfRangeException("baseDirectoryPath");

			if (DataType.IsWhiteSpace(sourceStrategyAssemblyQualifiedTypeName))
				throw new ArgumentOutOfRangeException("sourceStrategyAssemblyQualifiedTypeName");

			toolVersion = new AssemblyInformation(Assembly.GetAssembly(typeof(IXmlPersistEngine))).AssemblyVersion;
			templateFilePath = Path.GetFullPath(templateFilePath);
			templateDirectoryPath = Path.GetDirectoryName(templateFilePath);
			baseDirectoryPath = Path.GetFullPath(baseDirectoryPath);

			if (!Directory.Exists(baseDirectoryPath))
				Directory.CreateDirectory(baseDirectoryPath);

			sourceStrategyType = Type.GetType(sourceStrategyAssemblyQualifiedTypeName, false);

			if ((object)sourceStrategyType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!typeof(ISourceStrategy).IsAssignableFrom(sourceStrategyType))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			sourceStrategy = (ISourceStrategy)Activator.CreateInstance(sourceStrategyType);

			xpe = new XmlPersistEngine();
			xpe.RegisterWellKnownConstructs();

			template = (TemplateConstruct)xpe.DeserializeFromXml(templateFilePath);
			source = sourceStrategy.GetSourceObject(sourceFilePath, properties);

			modelConstruct = source as ModelConstruct;

			xpe.SerializeToXml(template, Path.Combine(baseDirectoryPath, "#template.xml"));

			if ((object)modelConstruct != null)
				xpe.SerializeToXml(modelConstruct, Path.Combine(baseDirectoryPath, "#source.xml"));
			else if ((object)source != null && (object)Reflexion.GetOneAttribute<SerializableAttribute>(source.GetType()) != null)
				Cerealization.SetObjectToFile(Path.Combine(baseDirectoryPath, "#source.xml"), source);

			using (IInputMechanism inputMechanism = new FileInputMechanism(templateDirectoryPath, xpe)) // relative to template
			{
				using (IOutputMechanism outputMechanism = new FileOutputMechanism(baseDirectoryPath))
				{
					templatingContext = new TemplatingContext(xpe, new Tokenizer(strictMatching), inputMechanism, outputMechanism);

					templatingContext.Tokenizer.TokenReplacementStrategies.Add("StaticPropertyResolver", new DynamicValueTokenReplacementStrategy(DynamicValueTokenReplacementStrategy.StaticPropertyResolver));
					templatingContext.Tokenizer.TokenReplacementStrategies.Add("StaticMethodResolver", new DynamicValueTokenReplacementStrategy(DynamicValueTokenReplacementStrategy.StaticMethodResolver));

					// globals
					templatingContext.VariableTables.Push(globalVariableTable = new Dictionary<string, object>());
					globalVariableTable.Add("ToolVersion", toolVersion);

					foreach (KeyValuePair<string, IList<string>> property in properties)
					{
						if (property.Value.Count == 0)
							continue;

						if (property.Value.Count == 1)
							globalVariableTable.Add(property.Key, property.Value[0]);
						else
							globalVariableTable.Add(property.Key, property.Value);
					}

					templatingContext.IteratorModels.Push(source);
					template.ExpandTemplate(templatingContext);
					templatingContext.IteratorModels.Pop();
					templatingContext.VariableTables.Pop();
				}
			}
		}

		#endregion
	}
}