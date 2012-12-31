using System.Collections.Generic;

using TextMetal.Framework.AssociativeModel;
using TextMetal.Framework.SourceModel.Primative;
using TextMetal.Framework.TemplateModel;

namespace TextMetal.HostImpl.Tool
{
	public interface IToolHost
	{
		#region Methods/Operators

		/// <summary>
		/// Provides a hosting shim between a 'tool' host and the underlying TextMetal run-time.
		/// </summary>
		/// <param name="templateFilePath"> The file path of the input TextMetal template file to execute. </param>
		/// <param name="sourceFilePath"> The file path (or source specific URI) of the input data source to leverage. </param>
		/// <param name="baseDirectoryPath"> The root output directory path to place output arifacts (since this implementation uses file output mechanics). </param>
		/// <param name="sourceStrategyAssemblyQualifiedTypeName"> The assembly qualified type name for the ISourceStrategy to instantiate and execute. </param>
		/// <param name="strictMatching"> A value indicating whether to use strict matching semantics for tokens. </param>
		/// <param name="properties"> Arbitrary dictionary of string lists used to further customize the text templating process. The individual components or template files can use the properties as they see fit. </param>
		void Host(string templateFilePath, string sourceFilePath, string baseDirectoryPath,
		          string sourceStrategyAssemblyQualifiedTypeName, bool strictMatching, IDictionary<string, IList<string>> properties);

		object LoadModelOnly(string filePath);

		object LoadSqlQueryOnly(string filePath);

		TemplateConstruct LoadTemplateOnly(string templateFilePath);

		void SaveModelOnly(ModelConstruct document, string filePath);

		void SaveSqlQueryOnly(SqlQuery document, string filePath);

		void SaveTemplateOnly(TemplateConstruct template, string templateFilePath);

		#endregion
	}
}