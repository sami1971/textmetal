﻿/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.AssociativeModel
{
	/// <summary>
	/// Provides an XML construct for associative objects (not a base class however).
	/// </summary>
	[XmlElementMapping(LocalName = "Object", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Items)]
	public class ObjectConstruct : AssociativeXmlObject, IActualThing
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ObjectConstruct class.
		/// </summary>
		public ObjectConstruct()
		{
		}

		#endregion
	}
}