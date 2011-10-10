/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "Object", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AnonymousChildrenAllowedType = typeof(IAssociativeXmlObject))]
	public class ObjectConstruct : AssociativeXmlObject<IAssociativeXmlObject>, IActualThing
	{
		#region Constructors/Destructors

		public ObjectConstruct()
		{
		}

		#endregion
	}
}