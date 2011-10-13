/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
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

		#region Fields/Constants

		private readonly IList<IXmlObject> items = new List<IXmlObject>();
		private IXmlObject content;
		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

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

		#region Methods/Operators

		Type IXmlObject.GetAllowedChildTypes()
		{
			return typeof(IXmlObject);
		}

		Type IXmlObject.GetAllowedParentTypes()
		{
			return typeof(IXmlObject);
		}

		#endregion
	}
}