/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;
using TextMetal.Framework.ExpressionModel;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Write", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class WriteConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the WriteConstruct class.
		/// </summary>
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "default", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Text", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
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