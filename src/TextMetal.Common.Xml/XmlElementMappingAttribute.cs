/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// 	Marks a class as an XML object which is mapped to/from an XML element.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class XmlElementMappingAttribute : Attribute
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the XmlElementMappingAttribute class.
		/// </summary>
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

		/// <summary>
		/// 	Gets or sets the child element model (applicable only to those child elements which are not well-known via properties with mapping attributes).
		/// </summary>
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

		/// <summary>
		/// 	Gets or sets the local name of the XML element.
		/// </summary>
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

		/// <summary>
		/// 	Gets or sets the namespace URI of the XML element.
		/// </summary>
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