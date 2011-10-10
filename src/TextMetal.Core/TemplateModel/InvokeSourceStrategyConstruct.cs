/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core.SourceModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "InvokeSourceStrategy", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class InvokeSourceStrategyConstruct : XmlSterileObject<ITemplateXmlObject>, ITemplateXmlObject
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

		public void ExpandTemplate(TemplatingContext templatingContext)
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
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!typeof(ISourceStrategy).IsAssignableFrom(sourceStrategyType))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			sourceStrategy = (ISourceStrategy)Activator.CreateInstance(sourceStrategyType);

			source = sourceStrategy.GetSourceObject("", new Dictionary<string, IList<string>>());

			templatingContext.IteratorModels.Push(source);
		}

		#endregion
	}
}