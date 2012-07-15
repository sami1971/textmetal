//-----------------------------------------------------------------------
// <copyright file="Network.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "network", NamespaceUri = "")]
	public class Network : SoXmlObject
	{
		#region Constructors/Destructors

		public Network()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IList<NetworkStatsProperty> NetworkStats
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<NetworkStatsProperty>().ToList();
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "updates", NamespaceUri = "")]
		public Updates Updates
		{
			get;
			set;
		}

		#endregion
	}
}