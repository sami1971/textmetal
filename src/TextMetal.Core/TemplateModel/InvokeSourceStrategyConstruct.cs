/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core.SourceModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "InvokeSourceStrategy", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class InvokeSourceStrategyConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public InvokeSourceStrategyConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string assemblyQualifiedTypeName;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "aqt-name", NamespaceUri = "")]
		public string AssemblyQualifiedTypeName
		{
			get
			{
				return this.assemblyQualifiedTypeName;
			}
			set
			{
				this.assemblyQualifiedTypeName = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string aqtn;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			ISourceStrategy sourceStrategy;
			Type sourceStrategyType;
			object source;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			aqtn = templatingContext.Tokenizer.ExpandTokens(this.AssemblyQualifiedTypeName, dynamicWildcardTokenReplacementStrategy);

			sourceStrategyType = Type.GetType(aqtn, false);

			if ((object)sourceStrategyType == null)
				throw new InvalidOperationException(string.Format("Failed to load the source strategy type '{0}' via Type.GetType(..).", aqtn));

			if (!typeof(ISourceStrategy).IsAssignableFrom(sourceStrategyType))
				throw new InvalidOperationException(string.Format("The source strategy type is not assignable to type '{0}'.", typeof(ISourceStrategy).FullName));

			sourceStrategy = (ISourceStrategy)Activator.CreateInstance(sourceStrategyType);

			source = sourceStrategy.GetSourceObject("", new Dictionary<string, IList<string>>());

			templatingContext.IteratorModels.Push(source);

			if ((object)this.Items != null)
			{
				foreach (ITemplateMechanism templateMechanism in this.Items)
					templateMechanism.ExpandTemplate(templatingContext);
			}

			templatingContext.IteratorModels.Pop();
		}

		#endregion
	}
}