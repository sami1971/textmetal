/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(ChildElementModel = ChildElementModel.Sterile)]
	public sealed class TemplateXmlTextObject : TemplateXmlObject, IXmlTextObject
	{
		#region Constructors/Destructors

		public TemplateXmlTextObject()
		{
		}

		#endregion

		#region Fields/Constants

		private XmlName name;
		private string text;

		#endregion

		#region Properties/Indexers/Events

		public XmlName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string text;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			text = templatingContext.Tokenizer.ExpandTokens(this.Text, dynamicWildcardTokenReplacementStrategy);

			templatingContext.Output.CurrentTextWriter.Write(text);
		}

		#endregion
	}
}