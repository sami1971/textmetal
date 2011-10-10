/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "AssModContainer", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AnonymousChildrenAllowedType = typeof(IAssociativeXmlObject))]
	public sealed class AssModContainerConstruct : AssociativeXmlObject<IXmlObject>
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