/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.XmlModel
{
	/// <summary>
	/// 	Specifies the allowed child element types for an XML object properties (applicable only to those which are well-known via properties with mapping attributes).
	/// </summary>
	public enum ChildElementType
	{
		/// <summary>
		/// 	The element is a text element using it's local name and namespace. This is the default.
		/// </summary>
		TextValue = 0,

		/// <summary>
		/// 	The element will be rendered as a non-text element using it's local name and namespace.
		/// </summary>
		Unqualified = 1,

		/// <summary>
		/// 	The element will be rendered as a non-text element using it's local name dot prefixed with the local name of it's parent node, and namespace. This resembles XAML markup.
		/// </summary>
		ParentQualified = 2
	}
}