/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Import", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ImportConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public ImportConstruct()
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

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string name;
			ITemplateXmlObject fragment;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			name = templatingContext.Tokenizer.ExpandTokens(this.Name, dynamicWildcardTokenReplacementStrategy);

			fragment = templatingContext.Input.LoadFragment(name);

			if ((object)fragment == null)
				throw new InvalidOperationException(string.Format("Failed to import the fragment '{0}'.", name));

			fragment.ExpandTemplate(templatingContext);
		}

		#endregion
	}
}