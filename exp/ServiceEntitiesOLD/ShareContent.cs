//-----------------------------------------------------------------------
// <copyright file="ShareContent.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "content", NamespaceUri = "")]
	public class ShareContent : SoXmlObject
	{
		#region Constructors/Destructors

		public ShareContent()
		{
		}

		#endregion

		#region Fields/Constants

		private const string DescriptionElementName = "description";
		private const string IdElementName = "id";
		private const string ResolvedUrlElementName = "resolved-url";
		private const string RootElementName = "content";
		private const string ShortenedUrlElementName = "shortened-url";
		private const string SubmittedImageUrlElementName = "submitted-image-url";
		private const string SubmittedUrlElementName = "submitted-url";
		private const string ThumbnailUrlElementName = "thumbnail-url";
		private const string TitleElementName = "title";

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "description", NamespaceUri = "")]
		public string Description
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "resolved-url", NamespaceUri = "")]
		public string ResolvedUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "shortened-url", NamespaceUri = "")]
		public string ShortenedUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "submitted-image-url", NamespaceUri = "")]
		public string SubmittedImageUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "submitted-url", NamespaceUri = "")]
		public string SubmittedUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "thumbnail-url", NamespaceUri = "")]
		public string ThumbnailUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "title", NamespaceUri = "")]
		public string Title
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
			while (reader.Read())
			{
				if (reader.IsStartElement())
				{
					switch (reader.Name)
					{
						case IdElementName:
							this.Id = reader.ReadString();
							break;
						case TitleElementName:
							this.Title = reader.ReadString();
							break;
						case DescriptionElementName:
							this.Description = reader.ReadString();
							break;
						case SubmittedUrlElementName:
							this.SubmittedUrl = reader.ReadString();
							break;
						case ShortenedUrlElementName:
							this.ShortenedUrl = reader.ReadString();
							break;
						case ResolvedUrlElementName:
							this.ResolvedUrl = reader.ReadString();
							break;
						case SubmittedImageUrlElementName:
							this.SubmittedImageUrl = reader.ReadString();
							break;
						case ThumbnailUrlElementName:
							this.ThumbnailUrl = reader.ReadString();
							break;
					}
				}
			}
		}

		public void WriteContractBody(XmlWriter writer, bool isRoot)
		{
			if (isRoot == false)
				writer.WriteStartElement(RootElementName);

			if (string.IsNullOrEmpty(this.Id) == false)
				writer.WriteElementString(IdElementName, this.Id);

			if (string.IsNullOrEmpty(this.Title) == false)
				writer.WriteElementString(TitleElementName, this.Title);

			if (string.IsNullOrEmpty(this.Description) == false)
				writer.WriteElementString(DescriptionElementName, this.Description);

			if (string.IsNullOrEmpty(this.SubmittedUrl) == false)
				writer.WriteElementString(SubmittedUrlElementName, this.SubmittedUrl);

			if (string.IsNullOrEmpty(this.ShortenedUrl) == false)
				writer.WriteElementString(ShortenedUrlElementName, this.ShortenedUrl);

			if (string.IsNullOrEmpty(this.ResolvedUrl) == false)
				writer.WriteElementString(ResolvedUrlElementName, this.ResolvedUrl);

			if (string.IsNullOrEmpty(this.SubmittedImageUrl) == false)
				writer.WriteElementString(SubmittedImageUrlElementName, this.SubmittedImageUrl);

			if (string.IsNullOrEmpty(this.ThumbnailUrl) == false)
				writer.WriteElementString(ThumbnailUrlElementName, this.ThumbnailUrl);

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