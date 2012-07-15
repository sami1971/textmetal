//-----------------------------------------------------------------------
// <copyright file="Invitation.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

using LinkedInform.LinkedInRestApi.Utility;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "invitation-request", NamespaceUri = "")]
	public class Invitation : SoXmlObject
	{
		#region Constructors/Destructors

		public Invitation()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public KeyValuePair<string, string> Authorization
		{
			get;
			set;
		}

		public ConnectionType ConnectType
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
			writer.WriteElementString("connect-type", EnumHelper.GetDescription(this.ConnectType));
			writer.WriteStartElement("authorization");
			writer.WriteElementString("name", this.Authorization.Key);
			writer.WriteElementString("value", this.Authorization.Value);
			writer.WriteEndElement(); // authorization
		}

		#endregion
	}
}