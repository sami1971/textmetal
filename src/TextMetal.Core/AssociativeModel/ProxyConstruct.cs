/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// DO NOT SERIALIZE (e.g. no XmlElementMapping)
	/// </summary>
	[XmlElementMapping(LocalName = "Proxy", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class ProxyConstruct : AssociativeXmlObject<IAssociativeXmlObject>, IActualThing
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

		public override bool Equals(object obj)
		{
			object thisValue, thatValue;

			thisValue = this.GetAssociativeObjectValue();

			if ((object)obj == null)
				return (object)thisValue == null;
			else if (obj is PropertyConstruct)
			{
				thatValue = ((PropertyConstruct)obj).GetAssociativeObjectValue();
				return DataType.ObjectsEqualValueSemantics(thisValue, thatValue);
			}
			else
				return obj.SafeToString() == this.Value.SafeToString(); // string comparison fallback			
		}

		public override object GetAssociativeObjectValue()
		{
			return this.Value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public override string ToString()
		{
			return this.Value.SafeToString();
		}

		#endregion
	}
}