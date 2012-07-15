/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	[XmlElementMapping(LocalName = "Property", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class PropertyConstruct : AssociativeXmlObject
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

		public static bool CommonEquals(IAssociativeXmlObject lhs, object rhs)
		{
			object thisValue, thatValue;
			PropertyConstruct propertyConstruct;

			if ((object)lhs == null)
				throw new ArgumentNullException("rhs");

			thisValue = lhs.GetAssociativeObjectValue();

			if ((object)rhs == null)
				return (object)thisValue == null;

			if ((propertyConstruct = rhs as PropertyConstruct) != null)
				thatValue = propertyConstruct.GetAssociativeObjectValue();
			else
				thatValue = rhs;

			return DataType.ObjectsEqualValueSemantics(thisValue, thatValue);
		}

		public static int CommonGetHashCode(IAssociativeXmlObject lhs)
		{
			object value;

			if ((object)lhs == null)
				throw new ArgumentNullException("rhs");

			value = lhs.GetAssociativeObjectValue();

			if ((object)value == null)
				return 0;

			return value.GetHashCode();
		}

		public static string CommonToString(IAssociativeXmlObject lhs)
		{
			object value;

			if ((object)lhs == null)
				throw new ArgumentNullException("rhs");

			value = lhs.GetAssociativeObjectValue();

			if ((object)value == null)
				return null;

			return value.SafeToString();
		}

		protected override IEnumerator CoreGetAssociativeObjectEnumerator()
		{
			return null;
		}

		protected override object CoreGetAssociativeObjectValue()
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

		public override bool Equals(object obj)
		{
			return CommonEquals(this, obj);
		}

		public override int GetHashCode()
		{
			return CommonGetHashCode(this);
		}

		public override string ToString()
		{
			return CommonToString(this);
		}

		#endregion
	}
}