//-----------------------------------------------------------------------
// <copyright file="Facet.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "facet", NamespaceUri = "")]
	public class Facet : SoXmlObject
	{
		#region Constructors/Destructors

		public Facet()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IList<Bucket> Buckets
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<Bucket>().ToList();
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "code", NamespaceUri = "")]
		public string Code
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