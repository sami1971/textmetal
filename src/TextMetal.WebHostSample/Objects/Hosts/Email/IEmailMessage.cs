/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace TextMetal.WebHostSample.Objects.Hosts.Email
{
	public interface IEmailMessage
	{
		#region Properties/Indexers/Events

		string Bcc
		{
			get;
			set;
		}

		string Body
		{
			get;
			set;
		}

		string Cc
		{
			get;
			set;
		}

		IList<IEmailAttachment> EmailAttachments
		{
			get;
		}

		string From
		{
			get;
			set;
		}

		bool? IsBodyHtml
		{
			get;
			set;
		}

		bool? Processed
		{
			get;
			set;
		}

		string ReplyTo
		{
			get;
			set;
		}

		string Sender
		{
			get;
			set;
		}

		string Subject
		{
			get;
			set;
		}

		string To
		{
			get;
			set;
		}

		#endregion
	}
}