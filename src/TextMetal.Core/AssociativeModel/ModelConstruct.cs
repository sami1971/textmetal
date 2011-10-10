/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "Model", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AnonymousChildrenAllowedType = typeof(IAssociativeXmlObject))]
	public sealed class ModelConstruct : ObjectConstruct
	{
		#region Constructors/Destructors

		public ModelConstruct()
		{
		}

		#endregion
	}
}