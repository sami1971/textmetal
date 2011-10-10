/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;
using TextMetal.WebHostSample.Objects.Hosts.Email;
using TextMetal.WebHostSample.Objects.Model.Tables;

namespace TextMetal.WebHostSample.Objects.Model
{
	public partial class Repository
	{
		#region Methods/Operators

		partial void OnPreInsertEmailAttachment(UnitOfWorkContext unitOfWorkContext, EmailAttachment emailAttachment)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailAttachment == null)
				throw new ArgumentNullException("emailAttachment");

			emailAttachment.Mark();
		}

		partial void OnPreInsertEmailMessage(UnitOfWorkContext unitOfWorkContext, EmailMessage emailMessage)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailMessage == null)
				throw new ArgumentNullException("emailMessage");

			emailMessage.Mark();
		}

		partial void OnPreInsertEventLog(UnitOfWorkContext unitOfWorkContext, EventLog eventLog)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)eventLog == null)
				throw new ArgumentNullException("eventLog");

			eventLog.Mark();
		}

		partial void OnPreUpdateEmailAttachment(UnitOfWorkContext unitOfWorkContext, EmailAttachment emailAttachment)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailAttachment == null)
				throw new ArgumentNullException("emailAttachment");

			emailAttachment.Mark();
		}

		partial void OnPreUpdateEmailMessage(UnitOfWorkContext unitOfWorkContext, EmailMessage emailMessage)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)emailMessage == null)
				throw new ArgumentNullException("emailMessage");

			emailMessage.Mark();
		}

		partial void OnPreUpdateEventLog(UnitOfWorkContext unitOfWorkContext, EventLog eventLog)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)eventLog == null)
				throw new ArgumentNullException("eventLog");

			eventLog.Mark();
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