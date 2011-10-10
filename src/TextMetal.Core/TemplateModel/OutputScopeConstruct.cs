/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "OutputScope", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AnonymousChildrenAllowedType = typeof(ITemplateXmlObject))]
	public sealed class OutputScopeConstruct : XmlItemsObject<ITemplateXmlObject, ITemplateXmlObject>, ITemplateXmlObject
	{
		#region Constructors/Destructors

		public OutputScopeConstruct()
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

		public void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string name;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			name = templatingContext.Tokenizer.ExpandTokens(this.Name, dynamicWildcardTokenReplacementStrategy);

			if (!DataType.IsNullOrWhiteSpace(name))
			{
				new AllocConstruct()
				{
					Token = "#OutputScopeName"
				}.ExpandTemplate(templatingContext);
			}

			if (!DataType.IsNullOrWhiteSpace(name))
			{
				ExpressionContainerConstruct expressionContainerConstruct;
				ValueConstruct valueConstruct;

				expressionContainerConstruct = new ExpressionContainerConstruct();

				valueConstruct = new ValueConstruct()
				                 {
				                 	Type = typeof(string).FullName,
				                 	__ = name
				                 };

				expressionContainerConstruct.Content = valueConstruct;

				new AssignConstruct()
				{
					Token = "#OutputScopeName",
					Expression = expressionContainerConstruct
				}.ExpandTemplate(templatingContext);
			}

			templatingContext.Output.EnterScope(name);

			if ((object)this.Items != null)
			{
				foreach (ITemplateMechanism templateMechanism in this.Items)
					templateMechanism.ExpandTemplate(templatingContext);
			}

			templatingContext.Output.LeaveScope(name);

			if (!DataType.IsNullOrWhiteSpace(name))
			{
				new FreeConstruct()
				{
					Token = "#OutputScopeName"
				}.ExpandTemplate(templatingContext);
			}
		}

		public void ExpandTemplate(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			templatingContext.VariableTables.Push(new Dictionary<string, object>());

			this.CoreExpandTemplate(templatingContext);

			templatingContext.VariableTables.Pop();
		}

		#endregion
	}
}