/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
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
	[XmlElementMapping(LocalName = "OutputScope", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class OutputScopeConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the OutputScopeConstruct class.
		/// </summary>
		public OutputScopeConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;

		#endregion

		#region Properties/Indexers/Events

		protected override bool IsScopeBlock
		{
			get
			{
				return true;
			}
		}

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

			templatingContext.Output.LogTextWriter.WriteLine("['{0:O}' (UTC)]\tEntering output scope '{1}'.", DateTime.UtcNow, name);
			templatingContext.Output.EnterScope(name);

			if ((object)this.Items != null)
			{
				foreach (ITemplateMechanism templateMechanism in this.Items)
					templateMechanism.ExpandTemplate(templatingContext);
			}

			templatingContext.Output.LeaveScope(name);
			templatingContext.Output.LogTextWriter.WriteLine("['{0:O}' (UTC)]\tLeaving output scope '{1}'.", DateTime.UtcNow, name);

			if (!DataType.IsNullOrWhiteSpace(name))
			{
				new FreeConstruct()
				{
					Token = "#OutputScopeName"
				}.ExpandTemplate(templatingContext);
			}
		}

		#endregion
	}
}