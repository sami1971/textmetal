/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mail;
using System.Xml;
using System.Xml.Serialization;

using TextMetal.Core;
using TextMetal.Core.InputOutputModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.WebHostSample.Objects.Hosts.Email
{
	[Serializable]
	[XmlRoot(ElementName = "MessageTemplate", Namespace = "http://www.textmetal.com/api/v4.4.0")]
	public class MessageTemplate
	{
		#region Constructors/Destructors

		public MessageTemplate()
		{
		}

		#endregion

		#region Fields/Constants

		private XmlElement bccXml;
		private XmlElement bodyXml;
		private XmlElement ccXml;
		private XmlElement fromXml;
		private bool isBodyHtml;
		private XmlElement replyToXml;
		private XmlElement senderXml;
		private XmlElement subjectXml;
		private XmlElement toXml;

		#endregion

		#region Properties/Indexers/Events

		[XmlElement("BCC", Order = 5)]
		public XmlElement BccXml
		{
			get
			{
				return this.bccXml;
			}
			set
			{
				this.bccXml = value;
			}
		}

		[XmlElement("Body", Order = 8)]
		public XmlElement BodyXml
		{
			get
			{
				return this.bodyXml;
			}
			set
			{
				this.bodyXml = value;
			}
		}

		[XmlElement("CC", Order = 4)]
		public XmlElement CcXml
		{
			get
			{
				return this.ccXml;
			}
			set
			{
				this.ccXml = value;
			}
		}

		[XmlElement("From", Order = 0)]
		public XmlElement FromXml
		{
			get
			{
				return this.fromXml;
			}
			set
			{
				this.fromXml = value;
			}
		}

		[XmlElement("IsBodyHtml", Order = 7)]
		public bool IsBodyHtml
		{
			get
			{
				return this.isBodyHtml;
			}
			set
			{
				this.isBodyHtml = value;
			}
		}

		[XmlElement("ReplyTo", Order = 2)]
		public XmlElement ReplyToXml
		{
			get
			{
				return this.replyToXml;
			}
			set
			{
				this.replyToXml = value;
			}
		}

		[XmlElement("Sender", Order = 1)]
		public XmlElement SenderXml
		{
			get
			{
				return this.senderXml;
			}
			set
			{
				this.senderXml = value;
			}
		}

		[XmlElement("Subject", Order = 6)]
		public XmlElement SubjectXml
		{
			get
			{
				return this.subjectXml;
			}
			set
			{
				this.subjectXml = value;
			}
		}

		[XmlElement("To", Order = 3)]
		public XmlElement ToXml
		{
			get
			{
				return this.toXml;
			}
			set
			{
				this.toXml = value;
			}
		}

		#endregion

		#region Methods/Operators

		public static TEmailMessage SendEmailTemplate<TEmailMessage>(Type templateReosurceType, string templateReosurceName, object modelObject, Func<TEmailMessage, bool> saveMethod)
			where TEmailMessage : class, IEmailMessage, new()
		{
			MessageTemplate messageTemplate;
			TEmailMessage emailMessage;
			SmtpClient smtpClient;
			string[] addresses;

			if ((object)templateReosurceType == null)
				throw new ArgumentNullException("templateReosurceType");

			if ((object)templateReosurceName == null)
				throw new ArgumentNullException("templateReosurceName");

			if ((object)modelObject == null)
				throw new ArgumentNullException("modelObject");

			if ((object)saveMethod == null)
				throw new ArgumentNullException("saveMethod");

			if (!Cerealization.TryGetFromAssemblyResource<MessageTemplate>(templateReosurceType, templateReosurceName, out messageTemplate))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			emailMessage = messageTemplate.Resolve<TEmailMessage>(modelObject);

			if ((object)emailMessage == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!saveMethod(emailMessage))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			// yeah, its hardcoded...
			smtpClient = new SmtpClient("relay-hosting.secureserver.net", 25);
			smtpClient.EnableSsl = false;
			smtpClient.UseDefaultCredentials = true;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

			using (MailMessage mailMessage = new MailMessage())
			{
				if (!DataType.IsNullOrWhiteSpace(emailMessage.Subject))
					mailMessage.Subject = emailMessage.Subject;

				if (!DataType.IsNullOrWhiteSpace(emailMessage.Body))
					mailMessage.Body = emailMessage.Body;

				if (!DataType.IsNullOrWhiteSpace(emailMessage.To))
				{
					addresses = emailMessage.To.Split(';');

					if ((object)addresses != null)
					{
						foreach (string address in addresses)
						{
							if (!DataType.IsNullOrWhiteSpace(address))
								mailMessage.To.Add(address);
						}
					}
				}

				if (!DataType.IsNullOrWhiteSpace(emailMessage.From))
					mailMessage.From = new MailAddress(emailMessage.From);

				if (!DataType.IsNullOrWhiteSpace(emailMessage.Sender))
					mailMessage.Sender = new MailAddress(emailMessage.Sender);

				if (!DataType.IsNullOrWhiteSpace(emailMessage.ReplyTo))
					mailMessage.ReplyTo = new MailAddress(emailMessage.ReplyTo);

				if (!DataType.IsNullOrWhiteSpace(emailMessage.Bcc))
				{
					addresses = emailMessage.Bcc.Split(';');

					if ((object)addresses != null)
					{
						foreach (string address in addresses)
						{
							if (!DataType.IsNullOrWhiteSpace(address))
								mailMessage.Bcc.Add(address);
						}
					}
				}

				if (!DataType.IsNullOrWhiteSpace(emailMessage.Cc))
				{
					addresses = emailMessage.Cc.Split(';');

					if ((object)addresses != null)
					{
						foreach (string address in addresses)
						{
							if (!DataType.IsNullOrWhiteSpace(address))
								mailMessage.CC.Add(address);
						}
					}
				}

				mailMessage.IsBodyHtml = emailMessage.IsBodyHtml ?? false;

				try
				{
					smtpClient.Send(mailMessage);
					emailMessage.Processed = true;
				}
				finally
				{
					saveMethod(emailMessage);
				}
			}

			return emailMessage;
		}

		private static void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
		{
			object[] parameters;
			MailMessage mailMessage;
			IEmailMessage emailMessage;
			Action<IEmailMessage, bool> saveMethod;

			if ((object)sender == null)
				throw new ArgumentNullException("sender");

			if ((object)e == null)
				throw new ArgumentNullException("e");

			parameters = (object[])e.UserState;

			if ((object)parameters == null || parameters.Length != 3)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			mailMessage = (MailMessage)parameters[0];
			emailMessage = (IEmailMessage)parameters[1];
			saveMethod = (Action<IEmailMessage, bool>)parameters[2];

			if ((object)mailMessage == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			// dispose here instead
			mailMessage.Dispose();

			if ((object)emailMessage == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if ((object)saveMethod == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (e.Cancelled)
				return;

			if ((object)e.Error != null)
				return;

			saveMethod(emailMessage, true);
		}

		private TEmailMessage Resolve<TEmailMessage>(object source)
			where TEmailMessage : class, IEmailMessage, new()
		{
			XmlPersistEngine xpe;
			TemplateConstruct template;
			TemplatingContext templatingContext;
			NullInputMechanism nullInputMechanism;
			StringOutputMechanism stringOutputMechanism;
			XmlTextReader templateXmlTextReader;

			TEmailMessage emailMessage;

			if ((object)source == null)
				throw new ArgumentNullException("source");

			emailMessage = new TEmailMessage();

			xpe = new XmlPersistEngine();
			xpe.RegisterWellKnownConstructs();

			nullInputMechanism = new NullInputMechanism();
			stringOutputMechanism = new StringOutputMechanism();
			templatingContext = new TemplatingContext(xpe, new Tokenizer(true), nullInputMechanism, stringOutputMechanism);

			// FROM
			templateXmlTextReader = new XmlTextReader(new StringReader(this.FromXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.From = stringOutputMechanism.RecycleOutput();

			// SENDER
			templateXmlTextReader = new XmlTextReader(new StringReader(this.SenderXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.Sender = stringOutputMechanism.RecycleOutput();

			// REPLYTO
			templateXmlTextReader = new XmlTextReader(new StringReader(this.ReplyToXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.ReplyTo = stringOutputMechanism.RecycleOutput();

			// TO
			templateXmlTextReader = new XmlTextReader(new StringReader(this.ToXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.To = stringOutputMechanism.RecycleOutput();

			// CC
			templateXmlTextReader = new XmlTextReader(new StringReader(this.CcXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.Cc = stringOutputMechanism.RecycleOutput();

			// BCC
			templateXmlTextReader = new XmlTextReader(new StringReader(this.BccXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.Bcc = stringOutputMechanism.RecycleOutput();

			// SUBJECT
			templateXmlTextReader = new XmlTextReader(new StringReader(this.SubjectXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.Subject = stringOutputMechanism.RecycleOutput();

			// ISBODYHTML
			emailMessage.IsBodyHtml = this.IsBodyHtml;

			// BODY
			templateXmlTextReader = new XmlTextReader(new StringReader(this.BodyXml.OuterXml));
			template = (TemplateConstruct)xpe.DeserializeFromXml(templateXmlTextReader);

			templatingContext.IteratorModels.Push(source);
			template.ExpandTemplate(templatingContext);
			templatingContext.IteratorModels.Pop();

			emailMessage.Body = stringOutputMechanism.RecycleOutput();

			return emailMessage;
		}

		#endregion
	}
}