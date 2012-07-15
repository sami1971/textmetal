/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "AssModContainer", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Content)]
	public sealed class AssModContainerConstruct : AssociativeXmlObject
	{
		#region Constructors/Destructors

		public AssModContainerConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string id;

		#endregion

		#region Properties/Indexers/Events

		public new AssociativeXmlObject Content
		{
			get
			{
				return (AssociativeXmlObject)base.Content;
			}
			set
			{
				base.Content = value;
			}
		}

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
	}
}