//-----------------------------------------------------------------------
// <copyright file="Country.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "country", NamespaceUri = "")]
	public class Country : SoXmlObject
	{
		#region Constructors/Destructors

		public Country()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "code", NamespaceUri = "")]
		public string Code
		{
			get;
			set;
		}

		#endregion
	}
}