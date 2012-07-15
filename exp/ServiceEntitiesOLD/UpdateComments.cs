//-----------------------------------------------------------------------
// <copyright file="UpdateComments.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "update-comments", NamespaceUri = "")]
	public class UpdateComments : PagedCollection<UpdateComment>
	{
		#region Constructors/Destructors

		public UpdateComments()
			: base()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "update-comment", NamespaceUri = "")]
		public IList<UpdateComment> Items
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<UpdateComment>().ToList();
			}
		}

		#endregion
	}
}