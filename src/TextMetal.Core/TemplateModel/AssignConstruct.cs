/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Assign", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class AssignConstruct : XmlSterileObject<ITemplateXmlObject>, ITemplateXmlObject
	{
		#region Constructors/Destructors

		public AssignConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private ExpressionContainerConstruct expression;
		private string token;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Expression", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
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

		public void ExpandTemplate(TemplatingContext templatingContext)
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
				throw new InvalidOperationException("TODO (enhancement): add meaningful message | not found on model");
		}

		#endregion
	}
}