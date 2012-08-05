/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Linq;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// 	Provides an XML construct for associative arrays.
	/// </summary>
	[XmlElementMapping(LocalName = "Array", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Items)]
	public sealed class ArrayConstruct : AssociativeXmlObject, IActualThing
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ArrayConstruct class.
		/// </summary>
		public ArrayConstruct()
		{
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Gets the enumerator for the current associative object instance. Overrides the default behavior by returning an enumerator from a list of only IActualThing implementing child objects.
		/// </summary>
		/// <returns> An instance of IEnumerator or null. </returns>
		protected override IEnumerator CoreGetAssociativeObjectEnumerator()
		{
			return this.Items.OfType<IActualThing>().GetEnumerator();
		}

		#endregion
	}
}