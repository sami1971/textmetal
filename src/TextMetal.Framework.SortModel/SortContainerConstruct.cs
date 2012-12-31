/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.SortModel
{
	[XmlElementMapping(LocalName = "SortContainer", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class SortContainerConstruct : SortXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SortContainerConstruct class.
		/// </summary>
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

		public new IList<SortXmlObject> Items
		{
			get
			{
				return new ContravariantListAdapter<SortXmlObject, IXmlObject>(base.Items);
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