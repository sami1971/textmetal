/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Data;
using TextMetal.HostImpl.AspNetSample.Objects.Model.Tables;
using TextMetal.HostImpl.Web.Email;

namespace TextMetal.HostImpl.AspNetSample.Objects.Model
{
	public partial class Repository
	{
		#region Methods/Operators

		partial void OnPreInsertEmailAttachment(IUnitOfWorkContext unitOfWorkContext, EmailAttachment emailAttachment)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailAttachment == null)
				throw new ArgumentNullException("emailAttachment");

			//emailAttachment.Mark();
		}

		partial void OnPreInsertEmailMessage(IUnitOfWorkContext unitOfWorkContext, EmailMessage emailMessage)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailMessage == null)
				throw new ArgumentNullException("emailMessage");

			//emailMessage.Mark();
		}

		partial void OnPreInsertEventLog(IUnitOfWorkContext unitOfWorkContext, EventLog eventLog)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)eventLog == null)
				throw new ArgumentNullException("eventLog");

			//eventLog.Mark();
		}

		partial void OnPreUpdateEmailAttachment(IUnitOfWorkContext unitOfWorkContext, EmailAttachment emailAttachment)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailAttachment == null)
				throw new ArgumentNullException("emailAttachment");

			//emailAttachment.Mark();
		}

		partial void OnPreUpdateEmailMessage(IUnitOfWorkContext unitOfWorkContext, EmailMessage emailMessage)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailMessage == null)
				throw new ArgumentNullException("emailMessage");

			//emailMessage.Mark();
		}

		partial void OnPreUpdateEventLog(IUnitOfWorkContext unitOfWorkContext, EventLog eventLog)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)eventLog == null)
				throw new ArgumentNullException("eventLog");

			//eventLog.Mark();
		}

		public bool TrySendEmailTemplate(string templateReosurceName, object modelObject)
		{
			EmailMessage emailMessage;

			try
			{
				emailMessage = MessageTemplate.SendEmailTemplate<EmailMessage>(typeof(Repository), templateReosurceName, modelObject, (em) => this.SaveEmailMessage(em));

				if ((object)emailMessage == null)
					throw new InvalidOperationException("bad stuff happended");

				return true;
			}
			catch (Exception ex)
			{
				// swallow intentionally
				this.TryWriteEventLogEntry(Reflexion.GetErrors(ex, 0));
				return false;
			}
		}

		public bool TryWriteEventLogEntry(string eventText)
		{
			EventLog eventLog;
			bool result;

			try
			{
				eventLog = new EventLog();
				eventLog.EventText = eventText;

				result = this.SaveEventLog(eventLog);
				return result;
			}
			catch (Exception ex)
			{
				// swallow intentionally
				return false;
			}
		}

		#endregion
	}
}