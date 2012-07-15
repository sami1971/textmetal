//-----------------------------------------------------------------------
// <copyright file="ApiRequest.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "api-standard-profile-request", NamespaceUri = "")]
	public class ApiRequest : SoXmlObject
	{
		#region Constructors/Destructors

		public ApiRequest()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "headers", NamespaceUri = "")]
		public Headers Headers
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "url", NamespaceUri = "")]
		public string Url
		{
			get;
			set;
		}

		public IList<HttpHeader> _Headers
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				if (this.Children.Count != 1)
					return null;

				if ((object)this.Children[0].AnonymousChildren == null)
					return null;

				return this.Children[0].AnonymousChildren.Cast<HttpHeader>().ToList();
			}
		}

		#endregion
	}
}