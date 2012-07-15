//-----------------------------------------------------------------------
// <copyright file="PagedCollection.cs" company="Beemway">
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
	public class PagedCollection<T> : SoXmlObject
	{
		#region Constructors/Destructors

		public PagedCollection()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "count", NamespaceUri = "")]
		public int NumberOfResults
		{
			get;
			set;
		}

		[XmlAttributeMapping(LocalName = "start", NamespaceUri = "")]
		public int Start
		{
			get;
			set;
		}

		[XmlAttributeMapping(LocalName = "total", NamespaceUri = "")]
		public int Total
		{
			get;
			set;
		}

		#endregion
	}
}