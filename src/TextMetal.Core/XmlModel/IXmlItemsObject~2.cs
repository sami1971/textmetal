/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace TextMetal.Core.XmlModel
{
	public interface IXmlItemsObject<TParent, TItem> : IXmlItemsObject
		where TParent : class, IXmlObject
		where TItem : class, IXmlObject
	{
		#region Properties/Indexers/Events

		new IList<TItem> Items
		{
			get;
		}

		new TParent Parent
		{
			get;
			set;
		}

		#endregion
	}
}