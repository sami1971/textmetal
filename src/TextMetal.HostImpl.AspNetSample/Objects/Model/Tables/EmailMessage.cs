/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.HostImpl.Web.Email;

namespace TextMetal.HostImpl.AspNetSample.Objects.Model.Tables
{
	public partial class EmailMessage : IEmailMessage
	{
		#region Properties/Indexers/Events

		public IList<IEmailAttachment> EmailAttachments
		{
			get
			{
				return null;
			}
		}

		#endregion
	}
}