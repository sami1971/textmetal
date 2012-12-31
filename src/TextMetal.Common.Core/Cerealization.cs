/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Xml.Serialization;

namespace TextMetal.Common.Core
{
	/// <summary>
	/// Provides static helper and/or extension methods for serialization/deserialization.
	/// </summary>
	public static class Cerealization
	{
		#region Methods/Operators

		/// <summary>
		/// Deserializes an object from the specified input file.
		/// </summary>
		/// <param name="inputFilePath"> The input file path to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
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

		/// <summary>
		/// Deserializes an object from the specified input file. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="inputFilePath"> The input file path to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
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

		/// <summary>
		/// Deserializes an object from the specified readable stream.
		/// </summary>
		/// <param name="stream"> The readable stream to deserialize. </param>
		/// <param name="targetType"> The target run-time type of the root of the deserialized object graph. </param>
		/// <returns> An object of the target type or null. </returns>
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

		/// <summary>
		/// Deserializes an object from the specified readable stream. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the deserialized object graph. </typeparam>
		/// <param name="stream"> The readable stream to deserialize. </param>
		/// <returns> An object of the target type or null. </returns>
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

		/// <summary>
		/// Serializes an object to the specified output file.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the object graph to serialize. </typeparam>
		/// <param name="outputFilePath"> The output file path to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
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

		/// <summary>
		/// Serializes an object to the specified output file.
		/// </summary>
		/// <param name="outputFilePath"> The output file path to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
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

		/// <summary>
		/// Serializes an object to the specified writable stream.
		/// </summary>
		/// <param name="stream"> The writable stream to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
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

		/// <summary>
		/// Serializes an object to the specified writable stream.
		/// </summary>
		/// <typeparam name="TObject"> The target run-time type of the root of the object graph to serialize. </typeparam>
		/// <param name="stream"> The writable stream to serialize. </param>
		/// <param name="obj"> The object graph to serialize. </param>
		public static void SetObjectToStream<TObject>(Stream stream, TObject obj)
		{
			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			if ((object)obj == null)
				throw new ArgumentNullException("obj");

			SetObjectToStream(stream, (object)obj);
		}

		/// <summary>
		/// Deserializes an object from an assembly manifest resource.
		/// </summary>
		/// <typeparam name="TObject"> The run-time type of the object root to deserialize. </typeparam>
		/// <param name="resourceType"> A type within the source assembly where the manifest resource lives. </param>
		/// <param name="resourceName"> The fully qualified manifest resource name to load. </param>
		/// <param name="result"> A valid object of the specified type or null if the manifest resource name was not found in the assembly of the resource type. </param>
		/// <returns> A value indicating whether the manifest resource name was found in the target type's assembly. </returns>
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

		/// <summary>
		/// Deserializes a string from an assembly manifest resource.
		/// </summary>
		/// <param name="resourceType"> A type within the source assembly where the manifest resource lives. </param>
		/// <param name="resourceName"> The fully qualified manifest resource name to load. </param>
		/// <param name="result"> A valid string or null if the manifest resource name was not found in the assembly of the resource type. </param>
		/// <returns> A value indicating whether the manifest resource name was found in the target type's assembly. </returns>
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