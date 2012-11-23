/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;
using TextMetal.Framework.ExpressionModel;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Assign", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class AssignConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the AssignConstruct class.
		/// </summary>
		public AssignConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private ExpressionContainerConstruct expression;
		private string token;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Expression", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ExpressionContainerConstruct Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		[XmlAttributeMapping(LocalName = "token", NamespaceUri = "")]
		public string Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			object obj = null;
			string token;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			token = templatingContext.Tokenizer.ExpandTokens(this.Token, dynamicWildcardTokenReplacementStrategy);

			if ((object)this.Expression != null)
				obj = this.Expression.EvaluateExpression(templatingContext);

			if (!dynamicWildcardTokenReplacementStrategy.SetByPath(token, obj))
				throw new InvalidOperationException(string.Format("The facet name '{0}' was not found on the target model.", token));
		}

		#endregion
	}
}