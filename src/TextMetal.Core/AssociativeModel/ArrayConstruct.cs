﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Linq;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "Array", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Items)]
	public sealed class ArrayConstruct : AssociativeXmlObject, IActualThing
	{
		#region Constructors/Destructors

		public ArrayConstruct()
		{
		}

		#endregion

		#region Methods/Operators

		protected override IEnumerator CoreGetAssociativeObjectEnumerator()
		{
			return this.Items.OfType<IActualThing>().GetEnumerator();
		}

		#endregion
	}
}