/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.AssociativeModel;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "Expando", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ExpandoConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		///     Initializes a new instance of the ExpandoConstruct class.
		/// </summary>
		public ExpandoConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private AssociativeContainerConstruct dynamic;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Dynamic", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public AssociativeContainerConstruct Expression
		{
			get
			{
				return this.dynamic;
			}
			set
			{
				this.dynamic = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");
		}

		#endregion
	}
}