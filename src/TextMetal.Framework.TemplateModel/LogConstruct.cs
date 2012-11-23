/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Log", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class LogConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the LogConstruct class.
		/// </summary>
		public LogConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string message;
		private bool newline;
		private Severity severity;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "message", NamespaceUri = "")]
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		[XmlAttributeMapping(LocalName = "newline", NamespaceUri = "")]
		public bool Newline
		{
			get
			{
				return this.newline;
			}
			set
			{
				this.newline = value;
			}
		}

		[XmlAttributeMapping(LocalName = "severity", NamespaceUri = "")]
		public Severity Severity
		{
			get
			{
				return this.severity;
			}
			set
			{
				this.severity = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string message;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			message = templatingContext.Tokenizer.ExpandTokens(this.Message, dynamicWildcardTokenReplacementStrategy);

			if (this.Newline)
				templatingContext.Output.LogTextWriter.WriteLine("{0}:\t{1}", this.Severity, message);
			else
				templatingContext.Output.LogTextWriter.Write("{0}:\t{1}", this.Severity, message);

			if (this.Severity == Severity.Error)
				throw new InvalidOperationException(string.Format("A user defined error occured: '{0}'", message));
		}

		#endregion
	}
}