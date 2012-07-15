//-----------------------------------------------------------------------
// <copyright file="Attribution.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "attribution", NamespaceUri = "")]
	public class Attribution : SoXmlObject
	{
		#region Constructors/Destructors

		public Attribution()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "share", NamespaceUri = "")]
		public Share Share
		{
			get;
			set;
		}

		#endregion
	}
}