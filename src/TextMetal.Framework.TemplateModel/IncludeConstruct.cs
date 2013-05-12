/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;
using TextMetal.Framework.ExpressionModel;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Include", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class IncludeConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the IncludeConstruct class.
		/// </summary>
		public IncludeConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "name", NamespaceUri = "")]
		public string Name
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

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(ITemplatingContext templatingContext)
		{
			string name;
			string content;
			ExpressionContainerConstruct expressionContainerConstruct;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			name = templatingContext.Tokenizer.ExpandTokens(this.Name, dynamicWildcardTokenReplacementStrategy);

			content = templatingContext.Input.LoadContent(name);

			expressionContainerConstruct = new ExpressionContainerConstruct();
			expressionContainerConstruct.Content = new ValueConstruct()
			                                       {
				                                       __ = content
			                                       };

			new WriteConstruct()
			{
				Text = expressionContainerConstruct
			}.ExpandTemplate(templatingContext);
		}

		#endregion
	}
}