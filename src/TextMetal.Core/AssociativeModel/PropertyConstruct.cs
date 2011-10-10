/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "Property", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class PropertyConstruct : AssociativeXmlObject<IAssociativeXmlObject>
	{
		#region Constructors/Destructors

		public PropertyConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string type;
		private string value;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "type", NamespaceUri = "")]
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		[XmlAttributeMapping(LocalName = "value", NamespaceUri = "")]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
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
				return obj.SafeToString() == this.Value; // string comparison fallback			
		}

		public override object GetAssociativeObjectValue()
		{
			object value;
			Type valueType;

			if (DataType.IsNullOrWhiteSpace(this.Type))
				valueType = typeof(string);
			else
				valueType = System.Type.GetType(this.Type, false);

			if ((object)valueType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!DataType.TryParse(valueType, this.Value, out value))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return value;
		}

		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		public override string ToString()
		{
			return this.Value;
		}

		#endregion
	}
}