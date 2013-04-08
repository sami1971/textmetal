/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// Provides a base for all XML objects.
	/// </summary>
	public abstract class XmlObject : IXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the XmlObject class.
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
		/// Gets an array of allowed child XML object types.
		/// </summary>
		public virtual Type[] AllowedChildTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		/// <summary>
		/// Gets an array of allowed parent XML object types.
		/// </summary>
		public virtual Type[] AllowedParentTypes
		{
			get
			{
				return new Type[] { typeof(IXmlObject) };
			}
		}

		/// <summary>
		/// Gets or sets the optional single XML object content.
		/// </summary>
		public IXmlObject Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.content, value);
				this.content = value;
			}
		}

		/// <summary>
		/// Gets a list of XML object items.
		/// </summary>
		public IXmlObjectCollection<IXmlObject> Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>
		/// Gets or sets the parent XML object or null if this is the document root.
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

		#region Methods/Operators

		/// <summary>
		/// Ensures that for any XML object property, the correct parent instance is set/unset.
		/// Should be called in the setter for all XML object properties, before assigning the value.
		/// Example:
		/// set { this.EnsureParentOnPropertySet(this.content, value); this.content = value; }
		/// </summary>
		/// <param name="oldValueObj"> The old XML object value (the backing field). </param>
		/// <param name="newValueObj"> The new XML object value (value). </param>
		protected void EnsureParentOnPropertySet(IXmlObject oldValueObj, IXmlObject newValueObj)
		{
			if ((object)oldValueObj != null)
				oldValueObj.Parent = null;

			if ((object)newValueObj != null)
				newValueObj.Parent = this;
		}

		#endregion
	}
}