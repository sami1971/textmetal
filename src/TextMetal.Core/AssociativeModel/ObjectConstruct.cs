/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// 	Provides an XML construct for associative objects (not a base class however).
	/// </summary>
	[XmlElementMapping(LocalName = "Object", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public class ObjectConstruct : AssociativeXmlObject, IActualThing
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ObjectConstruct class.
		/// </summary>
		public ObjectConstruct()
		{
		}

		#endregion
	}
}