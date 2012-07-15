/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SourceModel.Primative
{
	public class XmlPersistEngineSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

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

			if (properties.TryGetValue(CMDLN_TOKEN_KNOWN_XML_TEXT_OBJECT_AQTN, out values))
			{
				if ((object)values != null && values.Count == 1)
				{
					xmlObjectAqtn = values[0];
					xmlObjectType = Type.GetType(xmlObjectAqtn, false);
				}
			}

			if ((object)xmlObjectType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!typeof(IXmlTextObject).IsAssignableFrom(xmlObjectType))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

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
							throw new InvalidOperationException("TODO (enhancement): add meaningful message");

						if (!typeof(IXmlObject).IsAssignableFrom(xmlObjectType))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message");

						xpe.RegisterKnownXmlObject(xmlObjectType);
					}
				}
			}

			if ((object)xmlObjectType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return xpe;
		}

		#endregion
	}
}