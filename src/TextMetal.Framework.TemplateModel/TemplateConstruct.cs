/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Template", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class TemplateConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the TemplateConstruct class.
		/// </summary>
		public TemplateConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private bool debug;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "debug", NamespaceUri = "")]
		public bool Debug
		{
			get
			{
				return this.debug;
			}
			set
			{
				this.debug = value;
			}
		}

		protected override bool IsScopeBlock
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			if (this.Debug && !Debugger.IsAttached)
				Debugger.Break();

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Items != null)
			{
				foreach (ITemplateMechanism templateMechanism in this.Items)
					templateMechanism.ExpandTemplate(templatingContext);
			}
		}

		#endregion
	}
}