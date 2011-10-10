/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Core.Plumbing;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Reference", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class ReferenceConstruct : XmlSterileObject<ITemplateXmlObject>, ITemplateXmlObject
	{
		#region Constructors/Destructors

		public ReferenceConstruct()
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

		public void ExpandTemplate(TemplatingContext templatingContext)
		{
			string name;
			Assembly assembly;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			Type[] exportedTypes;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			name = templatingContext.Tokenizer.ExpandTokens(this.Name, dynamicWildcardTokenReplacementStrategy);

			if (DataType.IsNullOrWhiteSpace(name))
			{
				templatingContext.ClearReferences();
				return;
			}

			assembly = templatingContext.Input.LoadAssembly(name);

			if ((object)assembly == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			exportedTypes = assembly.GetExportedTypes();

			if ((object)exportedTypes != null)
			{
				foreach (Type exportedType in exportedTypes)
				{
					if (!exportedType.IsAbstract &&
					    typeof(IXmlObject).IsAssignableFrom(exportedType))
					{
						if (typeof(IXmlTextObject).IsAssignableFrom(exportedType))
							templatingContext.SetReference(exportedType);
						else
							templatingContext.AddReference(exportedType);
					}
				}
			}
		}

		#endregion
	}
}