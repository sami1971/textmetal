/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SortModel
{
	[XmlElementMapping(LocalName = "SortContainer", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Content)]
	public sealed class SortContainerConstruct : SortXmlObject
	{
		#region Constructors/Destructors

		public SortContainerConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string id;

		#endregion

		#region Properties/Indexers/Events

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

		protected override IEnumerable CoreEvaluateSort(TemplatingContext templatingContext, IEnumerable values)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			if ((object)values == null)
				throw new ArgumentNullException("values");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Items != null)
			{
				foreach (ISortXmlObject child in this.Items)
					values = child.EvaluateSort(templatingContext, values);
			}

			return values;
		}

		#endregion
	}
}