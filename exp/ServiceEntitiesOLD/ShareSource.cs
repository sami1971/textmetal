//-----------------------------------------------------------------------
// <copyright file="ShareSource.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "source", NamespaceUri = "")]
	public class ShareSource : SoXmlObject
	{
		#region Constructors/Destructors

		public ShareSource()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "application", NamespaceUri = "")]
		public Application Application
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "service-provider", NamespaceUri = "")]
		public ServiceProvider ServiceProvider
		{
			get;
			set;
		}

		#endregion
	}
}