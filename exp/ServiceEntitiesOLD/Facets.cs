//-----------------------------------------------------------------------
// <copyright file="Facets.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "facets", NamespaceUri = "")]
	public class Facets : PagedCollection<Facet>
	{
		#region Constructors/Destructors

		public Facets()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "facet", NamespaceUri = "")]
		public IList<Facet> Items
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<Facet>().ToList();
			}
		}

		#endregion
	}
}