/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	/// <summary>
	/// 	Marks a property of an XML object as being mapped to/from an XML attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class XmlAttributeMappingAttribute : Attribute
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the XmlAttributeMappingAttribute class.
		/// </summary>
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

		/// <summary>
		/// 	Gets or sets the local name of the XML attribute.
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
		/// 	Gets or sets the namespace URI of the XML attribute.
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

		/// <summary>
		/// 	Gets or sets the order of rendering of the attribute to the XML stream. Order is only applicable to XML output and is ignored during XML input.
		/// </summary>
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