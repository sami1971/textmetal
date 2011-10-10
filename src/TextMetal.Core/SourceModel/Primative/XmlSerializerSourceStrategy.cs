/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.SourceModel.Primative
{
	public class XmlSerializerSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		public XmlSerializerSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			const string CMDLN_TOKEN_XML_SERIALIZED_AQTN = "XmlSerializedType";
			string xmlSerializedObjectAqtn;
			Type xmlSerializedObjectType = null;
			IList<string> values;
			object retval;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			if (properties.TryGetValue(CMDLN_TOKEN_XML_SERIALIZED_AQTN, out values))
			{
				if ((object)values != null && values.Count == 1)
				{
					xmlSerializedObjectAqtn = values[0];
					xmlSerializedObjectType = Type.GetType(xmlSerializedObjectAqtn, false);
				}
			}

			if ((object)xmlSerializedObjectType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			retval = Cerealization.GetObjectFromFile(sourceFilePath, xmlSerializedObjectType);

			return retval;
		}

		#endregion
	}
}