/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Log", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class LogConstruct : XmlSterileObject<ITemplateXmlObject>, ITemplateXmlObject
	{
		#region Constructors/Destructors

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

		public void ExpandTemplate(TemplatingContext templatingContext)
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