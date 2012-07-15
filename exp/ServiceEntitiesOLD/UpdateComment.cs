//-----------------------------------------------------------------------
// <copyright file="UpdateComment.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "update-comment", NamespaceUri = "")]
	public class UpdateComment : SoXmlObject
	{
		#region Constructors/Destructors

		public UpdateComment()
		{
		}

		#endregion

		#region Fields/Constants

		private const string CommentElementName = "comment";
		private const string IdElementName = "id";
		private const string PersonElementName = "person";
		private const string RootElementName = "update-comment";
		private const string SequenceNumberElementName = "sequence-number";
		private const string TimestampElementName = "timestamp";

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "comment", NamespaceUri = "")]
		public string Comment
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "id", NamespaceUri = "")]
		public string Id
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "person", NamespaceUri = "")]
		public Person Person
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "sequence-number", NamespaceUri = "")]
		public int SequenceNumber
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "timestamp", NamespaceUri = "")]
		public long Timestamp
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
			int initialDepth = reader.Depth;

			while (reader.Read() && reader.Depth >= initialDepth)
			{
				while (reader.IsStartElement())
				{
					switch (reader.Name)
					{
						case IdElementName:
							this.Id = reader.ReadString();
							break;
						case SequenceNumberElementName:
							this.SequenceNumber = int.Parse(reader.ReadString());
							break;
						case CommentElementName:
							this.Comment = reader.ReadString();
							break;
						case PersonElementName:
							this.Person = Utilities.DeserializeXml<Person>(string.Format("<person>{0}</person>", reader.ReadInnerXml()));
							break;
						case TimestampElementName:
							this.Timestamp = long.Parse(reader.ReadString());
							break;
						default:
							reader.Read();
							break;
					}
				}
			}
		}

		public void WriteContractBody(XmlWriter writer, bool isRoot)
		{
			if (isRoot == false)
				writer.WriteStartElement(RootElementName);

			writer.WriteElementString(CommentElementName, this.Comment);

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