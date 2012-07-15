/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

using TextMetal.Core;
using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	public abstract class SoXmlObject : ISoXmlObject
	{
		#region Constructors/Destructors

		protected SoXmlObject()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IList<ISoXmlObject> children = new List<ISoXmlObject>();
		private SoXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		IList<IXmlObject> IXmlObject.AnonymousChildren
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return new ListAdapter<IXmlObject, ISoXmlObject>(this.Children);
			}
		}

		public virtual IList<ISoXmlObject> Children
		{
			get
			{
				return this.children;
			}
		}

		public SoXmlObject Parent
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

		IXmlObject IXmlObject.Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value as SoXmlObject;
			}
		}

		#endregion
	}
}