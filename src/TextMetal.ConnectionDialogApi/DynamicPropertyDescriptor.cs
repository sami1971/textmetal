//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TextMetal.ConnectionDialogApi
{
	internal class DynamicPropertyDescriptor : PropertyDescriptor
	{
		public DynamicPropertyDescriptor(string name)
			: base(name, null)
		{
		}

#if NOT_USED
		public DynamicPropertyDescriptor(string name, string displayName) : this(name, displayName, null, null, ReadOnlyAttribute.Default.IsReadOnly)
		{
		}

		public DynamicPropertyDescriptor(string name, string displayName, string category) : this(name, displayName, category, null, ReadOnlyAttribute.Default.IsReadOnly)
		{
		}

		public DynamicPropertyDescriptor(string name, string displayName, string category, string description) : this(name, displayName, category, description, ReadOnlyAttribute.Default.IsReadOnly)
		{
		}

		public DynamicPropertyDescriptor(string name, string displayName, string category, string description, bool isReadOnly) : base(name, BuildAttributes(displayName, category, description, isReadOnly))
		{
		}
#endif

		public DynamicPropertyDescriptor(string name, params Attribute[] attributes)
			: base(name, FilterAttributes(attributes))
		{
		}

		public DynamicPropertyDescriptor(PropertyDescriptor baseDescriptor)
			: this(baseDescriptor, null)
		{
		}

		public DynamicPropertyDescriptor(PropertyDescriptor baseDescriptor, params Attribute[] newAttributes)
			: base(baseDescriptor, newAttributes)
		{
			this.AttributeArray = FilterAttributes(this.AttributeArray);
			this._baseDescriptor = baseDescriptor;
		}

		public override string Name
		{
			get
			{
				if (this._name != null)
					return this._name;
				return base.Name;
			}
		}

		public override string Category
		{
			get
			{
				if (this._category != null)
					return this._category;
				return base.Category;
			}
		}

		public override string Description
		{
			get
			{
				if (this._description != null)
					return this._description;
				return base.Description;
			}
		}

		public override Type PropertyType
		{
			get
			{
				if (this._propertyType != null)
					return this._propertyType;
				if (this._baseDescriptor != null)
					return this._baseDescriptor.PropertyType;
				return null;
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return (ReadOnlyAttribute.Yes.Equals(this.Attributes[typeof(ReadOnlyAttribute)]));
			}
		}

		public override TypeConverter Converter
		{
			get
			{
				if (this._converterTypeName != null)
				{
					if (this._converter == null)
					{
						Type converterType = this.GetTypeFromName(this._converterTypeName);
						if (typeof(TypeConverter).IsAssignableFrom(converterType))
							this._converter = (TypeConverter)this.CreateInstance(converterType);
					}
					if (this._converter != null)
						return this._converter;
				}
				return base.Converter;
			}
		}

		public override AttributeCollection Attributes
		{
			get
			{
				if (this._attributes != null)
				{
					Dictionary<object, Attribute> attributes = new Dictionary<object, Attribute>();
					foreach (Attribute attr in this.AttributeArray)
						attributes[attr.TypeId] = attr;
					foreach (Attribute attr in this._attributes)
					{
						if (!attr.IsDefaultAttribute())
							attributes[attr.TypeId] = attr;
						else if (attributes.ContainsKey(attr.TypeId))
							attributes.Remove(attr.TypeId);
						CategoryAttribute categoryAttr = attr as CategoryAttribute;
						if (categoryAttr != null)
							this._category = categoryAttr.Category;
						DescriptionAttribute descriptionAttr = attr as DescriptionAttribute;
						if (descriptionAttr != null)
							this._description = descriptionAttr.Description;
						TypeConverterAttribute typeConverterAttr = attr as TypeConverterAttribute;
						if (typeConverterAttr != null)
						{
							this._converterTypeName = typeConverterAttr.ConverterTypeName;
							this._converter = null;
						}
					}
					Attribute[] newAttributes = new Attribute[attributes.Values.Count];
					attributes.Values.CopyTo(newAttributes, 0);
					this.AttributeArray = newAttributes;
					this._attributes = null;
				}
				return base.Attributes;
			}
		}

		public GetValueHandler GetValueHandler
		{
			get
			{
				return this._getValueHandler;
			}
			set
			{
				this._getValueHandler = value;
			}
		}

		public SetValueHandler SetValueHandler
		{
			get
			{
				return this._setValueHandler;
			}
			set
			{
				this._setValueHandler = value;
			}
		}

		public CanResetValueHandler CanResetValueHandler
		{
			get
			{
				return this._canResetValueHandler;
			}
			set
			{
				this._canResetValueHandler = value;
			}
		}

		public ResetValueHandler ResetValueHandler
		{
			get
			{
				return this._resetValueHandler;
			}
			set
			{
				this._resetValueHandler = value;
			}
		}

		public ShouldSerializeValueHandler ShouldSerializeValueHandler
		{
			get
			{
				return this._shouldSerializeValueHandler;
			}
			set
			{
				this._shouldSerializeValueHandler = value;
			}
		}

		public GetChildPropertiesHandler GetChildPropertiesHandler
		{
			get
			{
				return this._getChildPropertiesHandler;
			}
			set
			{
				this._getChildPropertiesHandler = value;
			}
		}

		public override Type ComponentType
		{
			get
			{
				if (this._componentType != null)
					return this._componentType;
				if (this._baseDescriptor != null)
					return this._baseDescriptor.ComponentType;
				return null;
			}
		}

		public void SetName(string value)
		{
			if (value == null)
				value = String.Empty;
			this._name = value;
		}

		public void SetDisplayName(string value)
		{
			if (value == null)
				value = DisplayNameAttribute.Default.DisplayName;
			this.SetAttribute(new DisplayNameAttribute(value));
		}

		public void SetCategory(string value)
		{
			if (value == null)
				value = CategoryAttribute.Default.Category;
			this._category = value;
			this.SetAttribute(new CategoryAttribute(value));
		}

		public void SetDescription(string value)
		{
			if (value == null)
				value = DescriptionAttribute.Default.Description;
			this._description = value;
			this.SetAttribute(new DescriptionAttribute(value));
		}

		public void SetPropertyType(Type value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			this._propertyType = value;
		}

		public void SetDesignTimeOnly(bool value)
		{
			this.SetAttribute(new DesignOnlyAttribute(value));
		}

		public void SetIsBrowsable(bool value)
		{
			this.SetAttribute(new BrowsableAttribute(value));
		}

		public void SetIsLocalizable(bool value)
		{
			this.SetAttribute(new LocalizableAttribute(value));
		}

		public void SetIsReadOnly(bool value)
		{
			this.SetAttribute(new ReadOnlyAttribute(value));
		}

		public void SetConverterType(Type value)
		{
			this._converterTypeName = (value != null) ? value.AssemblyQualifiedName : null;
			if (this._converterTypeName != null)
				this.SetAttribute(new TypeConverterAttribute(value));
			else
				this.SetAttribute(TypeConverterAttribute.Default);
			this._converter = null;
		}

		public void SetAttribute(Attribute value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			if (this._attributes == null)
				this._attributes = new List<Attribute>();
			this._attributes.Add(value);
		}

		public void SetAttributes(params Attribute[] values)
		{
			foreach (Attribute value in values)
				this.SetAttribute(value);
		}

		public void SetComponentType(Type value)
		{
			this._componentType = value;
		}

		public override object GetValue(object component)
		{
			if (this.GetValueHandler != null)
				return this.GetValueHandler(component);
			if (this._baseDescriptor != null)
				return this._baseDescriptor.GetValue(component);
			return null;
		}

		public override void SetValue(object component, object value)
		{
			if (this.SetValueHandler != null)
			{
				this.SetValueHandler(component, value);
				this.OnValueChanged(component, EventArgs.Empty);
			}
			else if (this._baseDescriptor != null)
			{
				this._baseDescriptor.SetValue(component, value);
				this.OnValueChanged(component, EventArgs.Empty);
			}
		}

		public override bool CanResetValue(object component)
		{
			if (this.CanResetValueHandler != null)
				return this.CanResetValueHandler(component);
			if (this._baseDescriptor != null)
				return this._baseDescriptor.CanResetValue(component);
			return (this.Attributes[typeof(DefaultValueAttribute)] != null);
		}

		public override void ResetValue(object component)
		{
			if (this.ResetValueHandler != null)
				this.ResetValueHandler(component);
			else if (this._baseDescriptor != null)
				this._baseDescriptor.ResetValue(component);
			else
			{
				DefaultValueAttribute attribute = this.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
				if (attribute != null)
					this.SetValue(component, attribute.Value);
			}
		}

		public override bool ShouldSerializeValue(object component)
		{
			if (this.ShouldSerializeValueHandler != null)
				return this.ShouldSerializeValueHandler(component);
			if (this._baseDescriptor != null)
				return this._baseDescriptor.ShouldSerializeValue(component);
			DefaultValueAttribute attribute = this.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
			return (attribute != null && !Equals(this.GetValue(component), attribute.Value));
		}

		public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (this.GetChildPropertiesHandler != null)
				return this.GetChildPropertiesHandler(instance, filter);
			if (this._baseDescriptor != null)
				return this._baseDescriptor.GetChildProperties(instance, filter);
			return base.GetChildProperties(instance, filter);
		}

		protected override int NameHashCode
		{
			get
			{
				if (this._name != null)
					return this._name.GetHashCode();
				return base.NameHashCode;
			}
		}

#if NOT_USED
		private static Attribute[] BuildAttributes(string displayName, string category, string description, bool isReadOnly)
		{
			List<Attribute> attributes = new List<Attribute>();
			if (displayName != null && displayName != DisplayNameAttribute.Default.DisplayName)
			{
				attributes.Add(new DisplayNameAttribute(displayName));
			}
			if (category != null && category != CategoryAttribute.Default.Category)
			{
				attributes.Add(new CategoryAttribute(category));
			}
			if (description != null && description != DescriptionAttribute.Default.Description)
			{
				attributes.Add(new DescriptionAttribute(description));
			}
			if (isReadOnly != ReadOnlyAttribute.Default.IsReadOnly)
			{
				attributes.Add(new ReadOnlyAttribute(isReadOnly));
			}
			return attributes.ToArray();
		}
#endif

		private static Attribute[] FilterAttributes(Attribute[] attributes)
		{
			Dictionary<object, Attribute> dictionary = new Dictionary<object, Attribute>();
			foreach (Attribute attribute in attributes)
			{
				if (!attribute.IsDefaultAttribute())
					dictionary.Add(attribute.TypeId, attribute);
			}
			Attribute[] newAttributes = new Attribute[dictionary.Values.Count];
			dictionary.Values.CopyTo(newAttributes, 0);
			return newAttributes;
		}

		private string _name;
		private string _category;
		private string _description;
		private Type _propertyType;
		private string _converterTypeName;
		private TypeConverter _converter;
		private List<Attribute> _attributes;
		private GetValueHandler _getValueHandler;
		private SetValueHandler _setValueHandler;
		private CanResetValueHandler _canResetValueHandler;
		private ResetValueHandler _resetValueHandler;
		private ShouldSerializeValueHandler _shouldSerializeValueHandler;
		private GetChildPropertiesHandler _getChildPropertiesHandler;
		private Type _componentType;
		private PropertyDescriptor _baseDescriptor;
	}

	internal delegate object GetValueHandler(object component);

	internal delegate void SetValueHandler(object component, object value);

	internal delegate bool CanResetValueHandler(object component);

	internal delegate void ResetValueHandler(object component);

	internal delegate bool ShouldSerializeValueHandler(object component);

	internal delegate PropertyDescriptorCollection GetChildPropertiesHandler(object instance, Attribute[] filter);
}