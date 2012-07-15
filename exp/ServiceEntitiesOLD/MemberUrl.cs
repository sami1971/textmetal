//-----------------------------------------------------------------------
// <copyright file="MemberUrl.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "member-url", NamespaceUri = "")]
	public class MemberUrl : SoXmlObject
	{
		#region Constructors/Destructors

		public MemberUrl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "url", NamespaceUri = "")]
		public string Url
		{
			get;
			set;
		}

		#endregion
	}
}