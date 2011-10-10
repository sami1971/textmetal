/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace TextMetal.Core.XmlModel
{
	public abstract class XmlItemsObject<TParent, TItem> : XmlObject, IXmlItemsObject<TParent, TItem>
		where TParent : class, IXmlObject
		where TItem : class, IXmlObject
	{
		#region Constructors/Destructors

		protected XmlItemsObject()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IList<TItem> Items
		{
			get
			{
				if ((object)((IXmlItemsObject)this).Items == null)
					return null;

				return new ContravariantListAdapter<TItem, IXmlObject>(((IXmlItemsObject)this).Items);
			}
		}

		IList<IXmlObject> IXmlItemsObject.Items
		{
			get
			{
				if ((object)((IXmlObject)this).AnonymousChildren == null)
					return null;

				return ((IXmlObject)this).AnonymousChildren;
			}
		}

		public TParent Parent
		{
			get
			{
				return (TParent)((IXmlObject)this).Parent;
			}
			set
			{
				((IXmlObject)this).Parent = value;
			}
		}

		#endregion
	}
}