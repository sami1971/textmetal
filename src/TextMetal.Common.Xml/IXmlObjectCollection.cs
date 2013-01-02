/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// Represents an XML object collection.
	/// </summary>
	public interface IXmlObjectCollection<TXmlObject> : IList<TXmlObject>
		where TXmlObject : IXmlObject
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the site XML object or null if this is unattached.
		/// </summary>
		IXmlObject Site
		{
			get;
		}

		#endregion
	}
}