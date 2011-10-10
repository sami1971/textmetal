/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class XmlAttributeMappingAttribute : Attribute
	{
		#region Constructors/Destructors

		public XmlAttributeMappingAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private string localName;
		private string namespaceUri;
		private int order;

		#endregion

		#region Properties/Indexers/Events

		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		#endregion
	}
}