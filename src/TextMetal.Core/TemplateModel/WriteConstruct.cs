﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Write", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class WriteConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public WriteConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private ExpressionContainerConstruct @default;
		private bool dofvisnow;
		private string format;
		private bool newline;
		private ExpressionContainerConstruct text;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "default", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct Default
		{
			get
			{
				return this.@default;
			}
			set
			{
				this.@default = value;
			}
		}

		[XmlAttributeMapping(LocalName = "dofvisnow", NamespaceUri = "")]
		public bool DoFvIsNoW
		{
			get
			{
				return this.dofvisnow;
			}
			set
			{
				this.dofvisnow = value;
			}
		}

		[XmlAttributeMapping(LocalName = "format", NamespaceUri = "")]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Text", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct Text
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
			object valueObj = null, defaultObj = null;
			string output;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Text != null)
				valueObj = this.Text.EvaluateExpression(templatingContext);

			if ((object)this.Default != null)
				defaultObj = this.Default.EvaluateExpression(templatingContext);

			output = valueObj.SafeToString(this.Format, defaultObj.SafeToString("", null, true), this.DoFvIsNoW);

			output = templatingContext.Tokenizer.ExpandTokens(output, dynamicWildcardTokenReplacementStrategy);

			if (this.Newline)
				templatingContext.Output.CurrentTextWriter.WriteLine(output);
			else
				templatingContext.Output.CurrentTextWriter.Write(output);
		}

		#endregion
	}
}