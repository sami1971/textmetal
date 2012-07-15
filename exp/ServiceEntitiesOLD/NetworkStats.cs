//-----------------------------------------------------------------------
// <copyright file="NetworkStats.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "network-stats", NamespaceUri = "")]
	public class NetworkStats : PagedCollection<NetworkStatsProperty>
	{
		#region Constructors/Destructors

		public NetworkStats()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "property", NamespaceUri = "")]
		public IList<NetworkStatsProperty> Items
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<NetworkStatsProperty>().ToList();
			}
		}

		#endregion
	}
}