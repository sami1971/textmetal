/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class XmlElementMappingAttribute : Attribute
	{
		#region Constructors/Destructors

		public XmlElementMappingAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private ChildElementModel childElementModel;
		private string localName;
		private string namespaceUri;

		#endregion

		#region Properties/Indexers/Events

		public ChildElementModel ChildElementModel
		{
			get
			{
				return this.childElementModel;
			}
			set
			{
				this.childElementModel = value;
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

		#endregion
	}
}