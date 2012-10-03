/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	/// <summary>
	/// 	Provides a base for all XML objects.
	/// </summary>
	public abstract class XmlObject : IXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the XmlObject class.
		/// </summary>
		protected XmlObject()
		{
			this.items = new XmlObjectCollection<IXmlObject>(this);
		}

		#endregion

		#region Fields/Constants

		private readonly IXmlObjectCollection<IXmlObject> items;
		private IXmlObject content;
		private IXmlObject parent;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets an array of allowed child XML object types.
		/// </summary>
		public virtual Type[] AllowedChildTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		/// <summary>
		/// 	Gets an array of allowed parent XML object types.
		/// </summary>
		public virtual Type[] AllowedParentTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		/// <summary>
		/// 	Gets or sets the optional single XML object content.
		/// </summary>
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

		/// <summary>
		/// 	Gets a list of XML object items.
		/// </summary>
		public IXmlObjectCollection<IXmlObject> Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>
		/// 	Gets or sets the parent XML object or null if this is the document root.
		/// </summary>
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
	}
}