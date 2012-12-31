/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// Represents an XML text object and it's text value.
	/// </summary>
	public interface IXmlTextObject : IXmlObject
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets or sets the XML name (local name and namespace URI).
		/// </summary>
		XmlName Name
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the text value.
		/// </summary>
		string Text
		{
			get;
			set;
		}

		#endregion
	}
}