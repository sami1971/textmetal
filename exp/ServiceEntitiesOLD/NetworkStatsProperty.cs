//-----------------------------------------------------------------------
// <copyright file="NetworkStatsProperty.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "property", NamespaceUri = "")]
	public class NetworkStatsProperty : SoXmlObject
	{
		#region Constructors/Destructors

		public NetworkStatsProperty()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "key", NamespaceUri = "")]
		public string Key
		{
			get;
			set;
		}

		#endregion

		/*[XmlText]
    public string Value
    {
      get;
      set;
    }*/
	}
}