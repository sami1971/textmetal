//-----------------------------------------------------------------------
// <copyright file="Visibility.cs" company="Beemway">
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

using LinkedInform.LinkedInRestApi.Utility;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "visibility", NamespaceUri = "")]
	public class Visibility : SoXmlObject
	{
		#region Constructors/Destructors

		public Visibility()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly string CodeElementName = "code";
		private readonly string RootElementName = "visibility";

		#endregion

		#region Properties/Indexers/Events

		public VisibilityCode Code
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
			// Read the opening tag of the encapsulating element
			reader.ReadStartElement();

			reader.ReadStartElement(this.CodeElementName);
			string codeString = reader.ReadString();
			switch (codeString)
			{
				case "anyone":
					this.Code = VisibilityCode.Anyone;
					break;
				case "connections-only":
					this.Code = VisibilityCode.ConnectionsOnly;
					break;
			}

			reader.ReadEndElement();

			// Read the end tag of the encapsulating element
			reader.ReadEndElement();
		}

		public void WriteContractBody(XmlWriter writer, bool isRoot)
		{
			if (isRoot == false)
				writer.WriteStartElement(this.RootElementName);

			writer.WriteElementString(this.CodeElementName, EnumHelper.GetDescription(this.Code));

			if (isRoot == false)
				writer.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			this.WriteContractBody(writer, true);
		}

		#endregion
	}
}