//-----------------------------------------------------------------------
// <copyright file="Like.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "like", NamespaceUri = "")]
	public class Like : SoXmlObject
	{
		#region Constructors/Destructors

		public Like()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "person", NamespaceUri = "")]
		public Person Person
		{
			get;
			set;
		}

		#endregion
	}
}