/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;

namespace TextMetal.Core.XmlModel
{
	public abstract class XmlContentObject<TParent, TContent> : XmlObject, IXmlContentObject<TParent, TContent>
		where TParent : class, IXmlObject
		where TContent : class, IXmlObject
	{
		#region Constructors/Destructors

		protected XmlContentObject()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public TContent Content
		{
			get
			{
				if ((object)((IXmlObject)this).AnonymousChildren == null)
					return null;

				return (TContent)((IXmlContentObject)this).Content;
			}
			set
			{
				((IXmlContentObject)this).Content = value;
			}
		}

		IXmlObject IXmlContentObject.Content
		{
			get
			{
				if ((object)((IXmlObject)this).AnonymousChildren == null)
					return null;

				return ((IXmlObject)this).AnonymousChildren.FirstOrDefault();
			}
			set
			{
				if ((object)((IXmlObject)this).AnonymousChildren != null)
				{
					((IXmlObject)this).AnonymousChildren.Clear();

					if ((object)value != null)
						((IXmlObject)this).AnonymousChildren.Add(value);
				}
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