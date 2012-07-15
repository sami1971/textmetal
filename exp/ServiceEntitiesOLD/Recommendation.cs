//-----------------------------------------------------------------------
// <copyright file="Recommendation.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "recommendation", NamespaceUri = "")]
	public class Recommendation : SoXmlObject
	{
		#region Constructors/Destructors

		public Recommendation()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "id", NamespaceUri = "")]
		public int Id
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "recommendation-type", NamespaceUri = "")]
		public RecommendationType RecommendationType
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "recommender", NamespaceUri = "")]
		public Person Recommender
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "recommendation-snippet", NamespaceUri = "")]
		public string Snippet
		{
			get;
			set;
		}

		#endregion
	}
}