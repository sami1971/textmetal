/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	[XmlElementMapping(LocalName = "ExpressionContainer", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AnonymousChildrenAllowedType = typeof(IExpressionXmlObject))]
	public sealed class ExpressionContainerConstruct : XmlContentObject<IXmlObject, IExpressionXmlObject>, IExpressionXmlObject
	{
		#region Constructors/Destructors

		public ExpressionContainerConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string id;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "id", NamespaceUri = "")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		#endregion

		#region Methods/Operators

		public object EvaluateExpression(TemplatingContext templatingContext)
		{
			object ovalue;
			string svalue;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Content != null)
				ovalue = this.Content.EvaluateExpression(templatingContext);
			else
				ovalue = null;

			svalue = ovalue as string;

			if ((object)svalue != null)
				ovalue = templatingContext.Tokenizer.ExpandTokens(svalue, dynamicWildcardTokenReplacementStrategy);

			return ovalue;
		}

		#endregion
	}
}