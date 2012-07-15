/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// 	DO NOT SERIALIZE (e.g. no XmlElementMapping)
	/// </summary>
	[XmlElementMapping(LocalName = "Proxy", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ProxyConstruct : AssociativeXmlObject, IActualThing
	{
		#region Constructors/Destructors

		public ProxyConstruct(object value)
		{
			if ((object)value == null)
				throw new ArgumentNullException("value");

			this.value = value;
		}

		#endregion

		#region Fields/Constants

		private readonly object value;

		#endregion

		#region Properties/Indexers/Events

		public object Value
		{
			get
			{
				return this.value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override IEnumerator CoreGetAssociativeObjectEnumerator()
		{
			return null;
		}

		protected override object CoreGetAssociativeObjectValue()
		{
			return this.Value;
		}

		public override bool Equals(object obj)
		{
			return PropertyConstruct.CommonEquals(this, obj);
		}

		public override int GetHashCode()
		{
			return PropertyConstruct.CommonGetHashCode(this);
		}

		public override string ToString()
		{
			return PropertyConstruct.CommonToString(this);
		}

		#endregion
	}
}