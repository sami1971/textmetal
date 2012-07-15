//-----------------------------------------------------------------------
// <copyright file="Connections.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "connections", NamespaceUri = "")]
	public class Connections : PagedCollection<Person>
	{
		#region Constructors/Destructors

		public Connections()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IList<Person> Items
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<Person>().ToList();
			}
		}

		#endregion
	}
}