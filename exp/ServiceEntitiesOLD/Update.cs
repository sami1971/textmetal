//-----------------------------------------------------------------------
// <copyright file="Update.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "update", NamespaceUri = "")]
	public class Update : SoXmlObject
	{
		#region Constructors/Destructors

		public Update()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "is-commentable", NamespaceUri = "")]
		public bool IsCommentable
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "is-likable", NamespaceUri = "")]
		public bool IsLikable
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "is-liked", NamespaceUri = "")]
		public bool IsLiked
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "likes", NamespaceUri = "")]
		public Likes Likes
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "num-likes", NamespaceUri = "")]
		public int NumberOfLikes
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "update-comments", NamespaceUri = "")]
		public UpdateComments UpdateComments
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "update-content", NamespaceUri = "")]
		public UpdateContent UpdateContent
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "update-key", NamespaceUri = "")]
		public string UpdateKey
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "update-type", NamespaceUri = "")]
		public string UpdateType
		{
			get;
			set;
		}

		#endregion
	}
}