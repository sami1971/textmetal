/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	public abstract class XmlSterileObject<TParent> : XmlObject, IXmlSterileObject<TParent>
		where TParent : class, IXmlObject
	{
		#region Constructors/Destructors

		protected XmlSterileObject()
		{
		}

		#endregion

		#region Properties/Indexers/Events

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