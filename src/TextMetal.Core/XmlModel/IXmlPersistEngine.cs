/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Xml;

namespace TextMetal.Core.XmlModel
{
	public interface IXmlPersistEngine
	{
		#region Methods/Operators

		/// <summary>
		/// Clears all XML object registrations.
		/// </summary>
		void ClearAllKnowns();

		IXmlObject DeserializeFromXml(string fileName);

		IXmlObject DeserializeFromXml(Stream stream);

		void RegisterKnownXmlObject<TObject>()
			where TObject : IXmlObject;

		void RegisterKnownXmlObject(Type targetType);

		void RegisterKnownXmlTextObject<TObject>()
			where TObject : IXmlTextObject;

		void RegisterKnownXmlTextObject(Type targetType);

		void SerializeToXml(IXmlObject document, string fileName);

		void SerializeToXml(IXmlObject document, Stream stream);

		void SerializeToXml(IXmlObject document, XmlTextWriter xmlTextWriter);

		#endregion
	}
}