//-----------------------------------------------------------------------
// <copyright file="Location.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "location", NamespaceUri = "")]
	public class Location : SoXmlObject
	{
		#region Constructors/Destructors

		public Location()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "country", NamespaceUri = "")]
		public Country Country
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

		#endregion
	}
}