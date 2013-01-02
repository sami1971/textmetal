/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Xml;

namespace TextMetal.Framework.AssociativeModel
{
	/// <summary>
	/// Provides an XML construct for associative models (root object of object graph).
	/// </summary>
	[XmlElementMapping(LocalName = "Model", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class ModelConstruct : ObjectConstruct
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ModelConstruct class.
		/// </summary>
		public ModelConstruct()
		{
		}

		#endregion
	}
}