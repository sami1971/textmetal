//-----------------------------------------------------------------------
// <copyright file="Recipient.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Xml;
using System.Xml.Schema;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "recipient", NamespaceUri = "")]
	public class Recipient : SoXmlObject
	{
		#region Constructors/Destructors

		public Recipient()
		{
		}

		public Recipient(string path)
		{
			this.Path = path;
		}

		#endregion

		#region Properties/Indexers/Events

		public string Path
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("person");

			if (string.IsNullOrEmpty(this.Path) == false)
				writer.WriteAttributeString("path", this.Path);

			writer.WriteEndElement(); // person
		}

		#endregion
	}
}