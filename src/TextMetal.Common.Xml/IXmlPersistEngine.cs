/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Xml;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// 	Provides custom optimized XML serializer/deserializer behavior.
	/// </summary>
	public interface IXmlPersistEngine
	{
		#region Methods/Operators

		/// <summary>
		/// 	Clears all known XML object registrations.
		/// </summary>
		void ClearAllKnowns();

		/// <summary>
		/// 	Deserialize an XML object graph from the specified XML file.
		/// </summary>
		/// <param name="fileName"> The XML file to load. </param>
		/// <returns> An XML object graph. </returns>
		IXmlObject DeserializeFromXml(string fileName);

		/// <summary>
		/// 	Deserialize an XML object graph from the specified stream.
		/// </summary>
		/// <param name="stream"> The stream to load. </param>
		/// <returns> An XML object graph. </returns>
		IXmlObject DeserializeFromXml(Stream stream);

		/// <summary>
		/// 	Deserialize an XML object graph from the specified XML text reader.
		/// </summary>
		/// <param name="xmlTextReader"> The XML text reader to load. </param>
		/// <returns> An XML object graph. </returns>
		IXmlObject DeserializeFromXml(XmlTextReader xmlTextReader);

		/// <summary>
		/// 	Registers a known XML object by target type and explicit XML name (local name and namespace URI). This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type to register. </typeparam>
		/// <param name="xmlName"> The XML name (local name and namespace URI). </param>
		void RegisterKnownXmlObject<TObject>(XmlName xmlName)
			where TObject : IXmlObject;

		/// <summary>
		/// 	Registers a known XML object by target type and explicit XML name (local name and namespace URI). This is the non-generic overload.
		/// </summary>
		/// <param name="xmlName"> The XML name (local name and namespace URI). </param>
		/// <param name="targetType"> The target type to register. </param>
		void RegisterKnownXmlObject(XmlName xmlName, Type targetType);

		/// <summary>
		/// 	Registers a known XML object by target type and implicit attribute-based XML name (local name and namespace URI). This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type to register. </typeparam>
		void RegisterKnownXmlObject<TObject>()
			where TObject : IXmlObject;

		/// <summary>
		/// 	Registers a known XML object by target type. This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type to register. </param>
		void RegisterKnownXmlObject(Type targetType);

		/// <summary>
		/// 	Registers a known XML text object by target type. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type to register. </typeparam>
		void RegisterKnownXmlTextObject<TObject>()
			where TObject : IXmlTextObject;

		/// <summary>
		/// 	Registers a known XML text object by target type and implicit attribute-based XML name (local name and namespace URI). This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type to register. </param>
		void RegisterKnownXmlTextObject(Type targetType);

		/// <summary>
		/// 	Serializes an XML object graph to the specified XML file.
		/// </summary>
		/// <param name="document"> The document root XML object. </param>
		/// <param name="fileName"> The XML file to save. </param>
		void SerializeToXml(IXmlObject document, string fileName);

		/// <summary>
		/// 	Serializes an XML object graph to the specified stream.
		/// </summary>
		/// <param name="document"> The document root XML object. </param>
		/// <param name="stream"> The stream to save. </param>
		void SerializeToXml(IXmlObject document, Stream stream);

		/// <summary>
		/// 	Serializes an XML object graph to the specified XmlTextWriter.
		/// </summary>
		/// <param name="document"> The document root XML object. </param>
		/// <param name="xmlTextWriter"> The XmlTextWriter to save. </param>
		void SerializeToXml(IXmlObject document, XmlTextWriter xmlTextWriter);

		/// <summary>
		/// 	Unregisters a known XML object by target type. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type to unregister. </typeparam>
		/// <returns> A value indicating if the registration was present. </returns>
		bool UnregisterKnownXmlObject<TObject>()
			where TObject : IXmlObject;

		/// <summary>
		/// 	Unregisters a known XML object by target type. This is the generic overload.
		/// </summary>
		/// <param name="targetType"> The target type to unregister. </param>
		/// <returns> A value indicating if the registration was present. </returns>
		bool UnregisterKnownXmlObject(Type targetType);

		/// <summary>
		/// 	Unregisters a known XML text object by target type. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type to unregister. </typeparam>
		/// <returns> A value indicating if the registration was present. </returns>
		bool UnregisterKnownXmlTextObject<TObject>()
			where TObject : IXmlTextObject;

		/// <summary>
		/// 	Unregisters a known XML text object by target type. This is the generic overload.
		/// </summary>
		/// <param name="targetType"> The target type to unregister. </param>
		/// <returns> A value indicating if the registration was present. </returns>
		bool UnregisterKnownXmlTextObject(Type targetType);

		#endregion
	}
}