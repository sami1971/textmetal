//-----------------------------------------------------------------------
// <copyright file="Relation.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "relation", NamespaceUri = "")]
	public class Relation : SoXmlObject
	{
		#region Constructors/Destructors

		public Relation()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "distance", NamespaceUri = "")]
		public int Distance
		{
			get;
			set;
		}

		#endregion
	}
}