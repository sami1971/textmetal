//-----------------------------------------------------------------------
// <copyright file="SiteRequest.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "site-standard-profile-request", NamespaceUri = "")]
	public class SiteRequest : SoXmlObject
	{
		#region Constructors/Destructors

		public SiteRequest()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "url", NamespaceUri = "")]
		public string Url
		{
			get;
			set;
		}

		#endregion
	}
}