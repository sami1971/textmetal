/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core.XmlModel
{
	public interface IXmlObject
	{
		#region Properties/Indexers/Events

		IXmlObject Content
		{
			get;
			set;
		}

		IList<IXmlObject> Items
		{
			get;
		}

		IXmlObject Parent
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		Type GetAllowedChildTypes();

		Type GetAllowedParentTypes();

		#endregion
	}
}