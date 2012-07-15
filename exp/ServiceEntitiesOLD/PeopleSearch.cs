//-----------------------------------------------------------------------
// <copyright file="PeopleSearch.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "people-search", NamespaceUri = "")]
	public class PeopleSearch : SoXmlObject
	{
		#region Constructors/Destructors

		public PeopleSearch()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "facets", NamespaceUri = "")]
		public Facets Facets
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "num-results", NamespaceUri = "")]
		public int NumberOfResults
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "people", NamespaceUri = "")]
		public People People
		{
			get;
			set;
		}

		#endregion
	}
}