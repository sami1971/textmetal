/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Plumbing.CommonFacilities;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SourceModel.Primative
{
	public class XmlPersistEngineSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the XmlPersistEngineSourceStrategy class.
		/// </summary>
		public XmlPersistEngineSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			IXmlPersistEngine xpe;
			object retval;

			xpe = this.GetXmlPersistEngine(properties);

			if ((object)xpe == null)
				throw new ArgumentNullException("xpe");

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			retval = xpe.DeserializeFromXml(sourceFilePath);

			return retval;
		}

		protected virtual IXmlPersistEngine GetXmlPersistEngine(IDictionary<string, IList<string>> properties)
		{
			const string CMDLN_TOKEN_KNOWN_XML_OBJECT_AQTN = "KnownXmlObjectType";
			const string CMDLN_TOKEN_KNOWN_XML_TEXT_OBJECT_AQTN = "KnownXmlTextObjectType";
			IXmlPersistEngine xpe;
			IList<string> values;
			string xmlObjectAqtn;
			Type xmlObjectType = null;

			if (properties == null)
				throw new ArgumentNullException("properties");

			xpe = new XmlPersistEngine();

			xmlObjectAqtn = null;
			if (properties.TryGetValue(CMDLN_TOKEN_KNOWN_XML_TEXT_OBJECT_AQTN, out values))
			{
				if ((object)values != null && values.Count == 1)
				{
					xmlObjectAqtn = values[0];
					xmlObjectType = Type.GetType(xmlObjectAqtn, false);
				}
			}

			if ((object)xmlObjectType == null)
				throw new InvalidOperationException(string.Format("Failed to load the XML text object type '{0}' via Type.GetType(..).", xmlObjectAqtn));

			if (!typeof(IXmlTextObject).IsAssignableFrom(xmlObjectType))
				throw new InvalidOperationException(string.Format("The XML text object type is not assignable to type '{0}'.", typeof(IXmlTextObject).FullName));

			xpe.RegisterKnownXmlTextObject(xmlObjectType);
			xmlObjectType = null;

			if (properties.TryGetValue(CMDLN_TOKEN_KNOWN_XML_OBJECT_AQTN, out values))
			{
				if ((object)values != null)
				{
					foreach (string value in values)
					{
						xmlObjectAqtn = value;
						xmlObjectType = Type.GetType(xmlObjectAqtn, false);

						if ((object)xmlObjectType == null)
							throw new InvalidOperationException(string.Format("Failed to load the XML object type '{0}' via Type.GetType(..).", xmlObjectAqtn));

						if (!typeof(IXmlObject).IsAssignableFrom(xmlObjectType))
							throw new InvalidOperationException(string.Format("The XML object type is not assignable to type '{0}'.", typeof(IXmlObject).FullName));

						xpe.RegisterKnownXmlObject(xmlObjectType);
					}
				}
			}

			// dpbullington@gmail.com@2012-08-01: is this needed?
			//if ((object)xmlObjectType == null)
			//throw new InvalidOperationException("???");

			return xpe;
		}

		#endregion
	}
}