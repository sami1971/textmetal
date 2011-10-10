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

		private readonly IList<IXmlObject> anonymousChildren = new List<IXmlObject>();
		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		IList<IXmlObject> IXmlObject.AnonymousChildren
		{
			get
			{
				return this.anonymousChildren;
			}
		}

		IXmlObject IXmlObject.Parent
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

		Type IXmlObject.GetAllowedAnonymousChildrenTypes()
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