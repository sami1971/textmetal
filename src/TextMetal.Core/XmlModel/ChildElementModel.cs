/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.XmlModel
{
	/// <summary>
	/// 	Specifies the child element model of an XML object (applicable only to those which are not well-known via properties with mapping attributes).
	/// </summary>
	public enum ChildElementModel
	{
		/// <summary>
		/// 	This XML object is not allowed to have any child elements. This is the default.
		/// </summary>
		Sterile = 0,

		/// <summary>
		/// 	This XML object can have ONE non-well-known child element. Use the Content property to access the possibly null value.
		/// </summary>
		Content = 1,

		/// <summary>
		/// 	This XML object can have MANY non-well-known child element. Use the Items property to access the possibly empty list of values.
		/// </summary>
		Items = 2
	}
}