//-----------------------------------------------------------------------
// <copyright file="ReShare.cs" company="Beemway">
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
	public class ReShare : SoXmlObject
	{
		#region Constructors/Destructors

		public ReShare()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "attribution", NamespaceUri = "")]
		public Attribution Attribution
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "visibility", NamespaceUri = "")]
		public Visibility Visibility
		{
			get;
			set;
		}

		#endregion
	}
}