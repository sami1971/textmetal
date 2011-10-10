/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class XmlChildElementMappingAttribute : Attribute
	{
		#region Constructors/Destructors

		public XmlChildElementMappingAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private ChildElementType childElementType;
		private string localName;
		private string namespaceUri;
		private int order;

		#endregion

		#region Properties/Indexers/Events

		public ChildElementType ChildElementType
		{
			get
			{
				return this.childElementType;
			}
			set
			{
				this.childElementType = value;
			}
		}

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