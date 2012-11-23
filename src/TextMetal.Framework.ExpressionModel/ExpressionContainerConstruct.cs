/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Expressions;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "ExpressionContainer", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Content)]
	public sealed class ExpressionContainerConstruct : ExpressionXmlObject, IContainer
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ExpressionContainerConstruct class.
		/// </summary>
		public ExpressionContainerConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string id;

		#endregion

		#region Properties/Indexers/Events

		public new ExpressionXmlObject Content
		{
			get
			{
				return (ExpressionXmlObject)base.Content;
			}
			set
			{
				base.Content = value;
			}
		}

		IExpression IContainer.Content
		{
			get
			{
				return this.Content;
			}
		}

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

		protected override object CoreEvaluateExpression(TemplatingContext templatingContext)
		{
			object ovalue;
			string svalue;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Content != null)
				ovalue = ((IExpressionXmlObject)this.Content).EvaluateExpression(templatingContext);
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