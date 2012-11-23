/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.AssociativeModel;
using TextMetal.Framework.Core;
using TextMetal.Framework.HostingModel;
using TextMetal.Framework.InputOutputModel;
using TextMetal.Framework.TemplateModel;

namespace TextMetal.HostImpl.Tool
{
	/// <summary>
	/// 	This class contains code to bootstrap TextMetal proper. This code is a specific implementation for TextMetal 'tool' hosting, concerned with leveraging file paths. Other host implementations will vary (see web host sample for instance). This code can be used by any interactive or batch application (console, windows, WPF, service, etc.).
	/// </summary>
	public sealed class ToolHost
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ToolHost class.
		/// </summary>
		public ToolHost()
		{
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Provides a hosting shim between a 'tool' host and the underlying TextMetal run-time.
		/// </summary>
		/// <param name="templateFilePath"> The file path of the input TextMetal template file to execute. </param>
		/// <param name="sourceFilePath"> The file path (or source specific URI) of the input data source to leverage. </param>
		/// <param name="baseDirectoryPath"> The root output directory path to place output arifacts (since this implementation uses file output mechanics). </param>
		/// <param name="sourceStrategyAssemblyQualifiedTypeName"> The assembly qualified type name for the ISourceStrategy to instantiate and execute. </param>
		/// <param name="strictMatching"> A value indicating whether to use strict matching semantics for tokens. </param>
		/// <param name="properties"> Arbitrary dictionary of string lists used to further customize the text templating process. The individual components or template files can use the properties as they see fit. </param>
		public void Host(string templateFilePath, string sourceFilePath, string baseDirectoryPath,
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

			if ((object)source == null)
				return;

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