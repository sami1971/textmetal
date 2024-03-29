﻿/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using TextMetal.Common.Core;

namespace TextMetal.Framework.SourceModel.Primative
{
	public class JsonSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the JsonSourceStrategy class.
		/// </summary>
		public JsonSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object LoadFrom(Stream stream)
		{
			object obj;
			//DataContractJsonSerializer serializer;
			JsonSerializer serializer;
			string jsonText;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			//serializer = new DataContractJsonSerializer(typeof(Sketch), knownShapes);
			serializer = JsonSerializer.Create(new JsonSerializerSettings()
			                                   {
				                                   TypeNameHandling = TypeNameHandling.Objects
			                                   });

			using (StreamReader streamReader = new StreamReader(stream))
			{
				using (JsonReader jsonReader = new JsonTextReader(streamReader))
					obj = (object)serializer.Deserialize<object>(jsonReader);
			}

			return obj;
		}

		private static object LoadFromFile(string filePath)
		{
			object obj;

			if ((object)filePath == null)
				throw new ArgumentNullException("filePath");

			if (DataType.IsWhiteSpace(filePath))
				throw new ArgumentOutOfRangeException("filePath");

			if (!File.Exists(filePath))
				return null;

			using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				obj = LoadFrom(stream);

			return obj;
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			object retval;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			retval = LoadFromFile(sourceFilePath);

			return retval;
		}

		#endregion
	}
}