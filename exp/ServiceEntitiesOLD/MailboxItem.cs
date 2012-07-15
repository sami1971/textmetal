//-----------------------------------------------------------------------
// <copyright file="MailboxItem.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "mailbox-item", NamespaceUri = "")]
	public class MailboxItem : SoXmlObject
	{
		#region Constructors/Destructors

		public MailboxItem()
		{
			this.Recipients = new List<Recipient>();
		}

		public MailboxItem(List<Recipient> recipients)
		{
			if (recipients == null)
				throw new ArgumentNullException("recipients");

			this.Recipients = recipients;
		}

		#endregion

		#region Properties/Indexers/Events

		public string Body
		{
			get;
			set;
		}

		public Invitation ItemContent
		{
			get;
			set;
		}

		public List<Recipient> Recipients
		{
			get;
			set;
		}

		public string Subject
		{
			get;
			set;
		}

		#endregion
	}
}