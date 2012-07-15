//-----------------------------------------------------------------------
// <copyright file="Share.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "share", NamespaceUri = "")]
	public class Share : SoXmlObject
	{
		#region Constructors/Destructors

		public Share()
		{
		}

		public Share(string comment, string title, string description, VisibilityCode visibilityCode)
		{
			this.Comment = comment;
			if (string.IsNullOrEmpty(title) == false || string.IsNullOrEmpty(description) == false)
			{
				this.Content = new ShareContent
				               {
				               	Title = title,
				               	Description = description
				               };
			}

			this.Visibility = new Visibility { Code = visibilityCode };
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "author", NamespaceUri = "")]
		public Person Author
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "comment", NamespaceUri = "")]
		public string Comment
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "content", NamespaceUri = "")]
		public ShareContent Content
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "source", NamespaceUri = "")]
		public ShareSource Source
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "visibility", NamespaceUri = "")]
		public Visibility Visibility
		{
			get;
			set;
		}

		#endregion
	}
}