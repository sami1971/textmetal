//-----------------------------------------------------------------------
// <copyright file="TwitterAccount.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "twitter-account", NamespaceUri = "")]
	public class TwitterAccount : SoXmlObject
	{
		#region Constructors/Destructors

		public TwitterAccount()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "provider-account-id", NamespaceUri = "")]
		public string Id
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "provider-account-name", NamespaceUri = "")]
		public string Name
		{
			get;
			set;
		}

		#endregion
	}
}