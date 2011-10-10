/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Reflection;

using TextMetal.Core.Plumbing;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.InputOutputModel
{
	public class FileInputMechanism : InputMechanism
	{
		#region Constructors/Destructors

		public FileInputMechanism(string baseDirectoryPath, IXmlPersistEngine xpe)
		{
			if ((object)baseDirectoryPath == null)
				throw new ArgumentNullException("baseDirectoryPath");

			if ((object)xpe == null)
				throw new ArgumentNullException("xpe");

			this.baseDirectoryPath = baseDirectoryPath;
			this.xpe = xpe;
		}

		#endregion

		#region Fields/Constants

		private readonly string baseDirectoryPath;
		private readonly IXmlPersistEngine xpe;

		#endregion

		#region Properties/Indexers/Events

		private string BaseDirectoryPath
		{
			get
			{
				return this.baseDirectoryPath;
			}
		}

		private IXmlPersistEngine Xpe
		{
			get
			{
				return this.xpe;
			}
		}

		#endregion

		#region Methods/Operators

		public override Assembly LoadAssembly(string assembluName)
		{
			Assembly assembly;

			if (DataType.IsNullOrWhiteSpace(assembluName))
				return null;

			assembluName = Path.GetFullPath(assembluName);
			assembly = Assembly.LoadFile(assembluName);

			return assembly;
		}

		public override string LoadContent(string resourceName)
		{
			string fullFilePath;
			string value;

			if (DataType.IsNullOrWhiteSpace(resourceName))
				return null;

			fullFilePath = Path.GetFullPath(Path.Combine(this.BaseDirectoryPath, resourceName));
			//Console.Error.WriteLine(fullFilePath);

			using (Stream stream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (TextReader textReader = new StreamReader(stream))
					value = textReader.ReadToEnd();
			}

			return value;
		}

		public override ITemplateXmlObject LoadFragment(string resourceName)
		{
			string fullFilePath;
			ITemplateXmlObject value;

			if (DataType.IsNullOrWhiteSpace(resourceName))
				return null;

			fullFilePath = Path.GetFullPath(Path.Combine(this.BaseDirectoryPath, resourceName));
			//Console.Error.WriteLine(fullFilePath);

			value = (ITemplateXmlObject)this.Xpe.DeserializeFromXml(fullFilePath);

			return value;
		}

		#endregion
	}
}