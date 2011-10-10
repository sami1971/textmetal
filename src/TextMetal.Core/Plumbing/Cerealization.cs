/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Xml.Serialization;

namespace TextMetal.Core.Plumbing
{
	/// <summary>
	/// Provides common serialization scenarios.
	/// </summary>
	public static class Cerealization
	{
		#region Methods/Operators

		public static TObject GetObjectFromAssemblyResource<TObject>(Type resourceType, string resourceName)
		{
			TObject obj;
			Type targetType;

			if ((object)resourceType == null)
				throw new ArgumentNullException("resourceType");

			if ((object)resourceName == null)
				throw new ArgumentNullException("resourceName");

			if (DataType.IsWhiteSpace(resourceName))
				throw new ArgumentOutOfRangeException("resourceName");

			targetType = typeof(TObject);
			obj = (TObject)GetObjectFromAssemblyResource(resourceType, resourceName, targetType);

			return obj;
		}

		public static object GetObjectFromAssemblyResource(Type resourceType, string resourceName, Type targetType)
		{
			object obj;

			if ((object)resourceType == null)
				throw new ArgumentNullException("resourceType");

			if ((object)resourceName == null)
				throw new ArgumentNullException("resourceName");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if (DataType.IsWhiteSpace(resourceName))
				throw new ArgumentOutOfRangeException("resourceName");

			using (Stream stream = resourceType.Assembly.GetManifestResourceStream(resourceName))
				obj = GetObjectFromStream(stream, targetType);

			return obj;
		}

		public static object GetObjectFromFile(string inputFilePath, Type targetType)
		{
			object obj;

			if ((object)inputFilePath == null)
				throw new ArgumentNullException("inputFilePath");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if (DataType.IsWhiteSpace(inputFilePath))
				throw new ArgumentOutOfRangeException("inputFilePath");

			using (Stream stream = File.Open(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				obj = GetObjectFromStream(stream, targetType);

			return obj;
		}

		public static TObject GetObjectFromFile<TObject>(string inputFilePath)
		{
			TObject obj;
			Type targetType;

			if ((object)inputFilePath == null)
				throw new ArgumentNullException("inputFilePath");

			if (DataType.IsWhiteSpace(inputFilePath))
				throw new ArgumentOutOfRangeException("inputFilePath");

			targetType = typeof(TObject);
			obj = (TObject)GetObjectFromFile(inputFilePath, targetType);

			return obj;
		}

		public static object GetObjectFromStream(Stream stream, Type targetType)
		{
			XmlSerializer xmlSerializer;
			object obj;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			xmlSerializer = new XmlSerializer(targetType);
			obj = xmlSerializer.Deserialize(stream);

			return obj;
		}

		public static TObject GetObjectFromStream<TObject>(Stream stream)
		{
			TObject obj;
			Type targetType;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			targetType = typeof(TObject);
			obj = (TObject)GetObjectFromStream(stream, targetType);

			return obj;
		}

		public static void SetObjectToFile<TObject>(string outputFilePath, TObject obj)
		{
			if ((object)outputFilePath == null)
				throw new ArgumentNullException("outputFilePath");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			if (DataType.IsWhiteSpace(outputFilePath))
				throw new ArgumentOutOfRangeException("outputFilePath");

			SetObjectToFile(outputFilePath, (object)obj);
		}

		public static void SetObjectToFile(string outputFilePath, object obj)
		{
			if ((object)outputFilePath == null)
				throw new ArgumentNullException("outputFilePath");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			if (DataType.IsWhiteSpace(outputFilePath))
				throw new ArgumentOutOfRangeException("outputFilePath");

			using (Stream stream = File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
				SetObjectToStream(stream, obj);
		}

		public static void SetObjectToStream(Stream stream, object obj)
		{
			XmlSerializer xmlSerializer;
			Type targetType;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			targetType = obj.GetType();
			xmlSerializer = new XmlSerializer(targetType);
			xmlSerializer.Serialize(stream, obj);
		}

		public static void SetObjectToStream<TObject>(Stream stream, TObject obj)
		{
			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			SetObjectToStream(stream, (object)obj);
		}

		public static bool TryGetFromAssemblyResource<TObject>(Type resourceType, string resourceName, out TObject result)
		{
			Type targetType;
			bool retval;

			if ((object)resourceType == null)
				throw new ArgumentNullException("resourceType");

			if ((object)resourceName == null)
				throw new ArgumentNullException("resourceName");

			if (DataType.IsWhiteSpace(resourceName))
				throw new ArgumentOutOfRangeException("resourceName");

			result = default(TObject);
			targetType = typeof(TObject);

			using (Stream stream = resourceType.Assembly.GetManifestResourceStream(resourceName))
			{
				if (retval = ((object)stream != null))
					result = (TObject)GetObjectFromStream(stream, targetType);
			}

			return retval;
		}

		public static bool TryGetStringFromAssemblyResource(Type resourceType, string resourceName, out string result)
		{
			bool retval;

			if ((object)resourceType == null)
				throw new ArgumentNullException("resourceType");

			if ((object)resourceName == null)
				throw new ArgumentNullException("resourceName");

			if (DataType.IsWhiteSpace(resourceName))
				throw new ArgumentOutOfRangeException("resourceName");

			result = null;

			using (Stream stream = resourceType.Assembly.GetManifestResourceStream(resourceName))
			{
				if (retval = (object)stream != null)
				{
					using (StreamReader streamReader = new StreamReader(stream))
						result = streamReader.ReadToEnd();
				}
			}

			return retval;
		}

		#endregion
	}
}