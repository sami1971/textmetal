/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core.XmlModel
{
	public abstract class XmlObject : IXmlObject
	{
		#region Constructors/Destructors

		protected XmlObject()
		{
		}

		#endregion

		// TODO: make custom XmlCollection from Collection<T> and use Site pattern and callbacks

		#region Fields/Constants

		private readonly IList<IXmlObject> items = new List<IXmlObject>();
		private IXmlObject content;
		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		public virtual Type[] AllowedChildTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		public virtual Type[] AllowedParentTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		public IXmlObject Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		public IList<IXmlObject> Items
		{
			get
			{
				return this.items;
			}
		}

		public IXmlObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		#endregion
	}
}