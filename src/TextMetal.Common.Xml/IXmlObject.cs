/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// 	Represents an XML object and it's "schema".
	/// </summary>
	public interface IXmlObject
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets an array of allowed child XML object types.
		/// </summary>
		Type[] AllowedChildTypes
		{
			get;
		}

		/// <summary>
		/// 	Gets an array of allowed parent XML object types.
		/// </summary>
		Type[] AllowedParentTypes
		{
			get;
		}

		/// <summary>
		/// 	Gets or sets the optional single XML object content.
		/// </summary>
		IXmlObject Content
		{
			get;
			set;
		}

		/// <summary>
		/// 	Gets a list of XML object items.
		/// </summary>
		IXmlObjectCollection<IXmlObject> Items
		{
			get;
		}

		/// <summary>
		/// 	Gets or sets the parent XML object or null if this is the document root.
		/// </summary>
		IXmlObject Parent
		{
			get;
			set;
		}

		#endregion
	}
}