//-----------------------------------------------------------------------
// <copyright file="MemberGroup.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "member-group", NamespaceUri = "")]
	public class MemberGroup : SoXmlObject
	{
		#region Constructors/Destructors

		public MemberGroup()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "id", NamespaceUri = "")]
		public int Id
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "site-group-request", NamespaceUri = "")]
		public SiteRequest SiteGroupRequest
		{
			get;
			set;
		}

		#endregion
	}
}