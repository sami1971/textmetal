//-----------------------------------------------------------------------
// <copyright file="Activity.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "activity", NamespaceUri = "")]
	public class Activity : SoXmlObject
	{
		#region Constructors/Destructors

		public Activity()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "body", NamespaceUri = "")]
		public string AppId
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "app-id", NamespaceUri = "")]
		public string Body
		{
			get;
			set;
		}

		[XmlAttributeMapping(LocalName = "locale", NamespaceUri = "")]
		public string CultureName
		{
			get;
			set;
		}

		#endregion
	}
}