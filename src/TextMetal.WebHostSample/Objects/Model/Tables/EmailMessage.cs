/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core;
using TextMetal.WebHostSample.Objects.Hosts.Email;

namespace TextMetal.WebHostSample.Objects.Model.Tables
{
	public partial class EmailMessage : IEmailMessage
	{
		#region Fields/Constants

		private readonly IList<IEmailAttachment> emailAttachments = new List<IEmailAttachment>();

		#endregion

		#region Properties/Indexers/Events

		public IList<IEmailAttachment> EmailAttachments
		{
			get
			{
				return this.emailAttachments;
			}
		}

		#endregion

		#region Methods/Operators

		public void Mark()
		{
			DateTime now;

			now = DateTime.Now;

			this.CreationTimestamp = this.CreationTimestamp ?? now;
			this.ModificationTimestamp = !this.IsNew ? now : this.CreationTimestamp;
			this.LogicalDelete = this.LogicalDelete ?? false;

			this.Processed = this.Processed ?? false;
		}

		public virtual Message[] Validate()
		{
			return new Message[] { };
		}

		#endregion
	}
}