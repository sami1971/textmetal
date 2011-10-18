//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;

namespace TextMetal.ConnectionDialogApi
{
	public class AdoDotNetConnectionProperties : IDataConnectionProperties, ICustomTypeDescriptor
	{
		#region Constructors/Destructors

		public AdoDotNetConnectionProperties(string providerName)
		{
			Debug.Assert(providerName != null);
			this._providerName = providerName;

			// Create an underlying connection string builder object
			DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
			Debug.Assert(factory != null);
			this._connectionStringBuilder = factory.CreateConnectionStringBuilder();
			Debug.Assert(this._connectionStringBuilder != null);
			this._connectionStringBuilder.BrowsableConnectionString = false;
		}

		#endregion

		#region Fields/Constants

		private DbConnectionStringBuilder _connectionStringBuilder;
		private string _providerName;

		#endregion

		#region Properties/Indexers/Events

		public event EventHandler PropertyChanged;

		public virtual object this[string propertyName]
		{
			get
			{
				// Property name must not be null
				if (propertyName == null)
					throw new ArgumentNullException("propertyName");

				// If property doesn't exist, just return null
				object testValue = null;
				if (!this._connectionStringBuilder.TryGetValue(propertyName, out testValue))
					return null;

				// If property value has been set, return this value
				if (this._connectionStringBuilder.ShouldSerialize(propertyName))
					return this._connectionStringBuilder[propertyName];

				// Get the property's default value (if any)
				object val = this._connectionStringBuilder[propertyName];

				// If a default value exists, return it
				if (val != null)
					return val;

				// No value has been set and no default value exists, so return DBNull.Value
				return DBNull.Value;
			}
			set
			{
				// Property name must not be null
				if (propertyName == null)
					throw new ArgumentNullException("propertyName");

				// Remove the value
				this._connectionStringBuilder.Remove(propertyName);

				// Handle cases where value is DBNull.Value
				if (value == DBNull.Value)
				{
					// Leave the property in the reset state
					this.OnPropertyChanged(EventArgs.Empty);
					return;
				}

				// Get the property's default value (if any)
				object val = null;
				this._connectionStringBuilder.TryGetValue(propertyName, out val);

				// Set the value
				this._connectionStringBuilder[propertyName] = value;

				// If the value is equal to the default, remove it again
				if (Equals(val, value))
					this._connectionStringBuilder.Remove(propertyName);

				this.OnPropertyChanged(EventArgs.Empty);
			}
		}

		public DbConnectionStringBuilder ConnectionStringBuilder
		{
			get
			{
				return this._connectionStringBuilder;
			}
		}

		protected virtual PropertyDescriptor DefaultProperty
		{
			get
			{
				return TypeDescriptor.GetDefaultProperty(this._connectionStringBuilder, true);
			}
		}

		public virtual bool IsComplete
		{
			get
			{
				return true;
			}
		}

		public virtual bool IsExtensible
		{
			get
			{
				return !this._connectionStringBuilder.IsFixedSize;
			}
		}

		#endregion

		#region Methods/Operators

		public virtual void Add(string propertyName)
		{
			if (!this._connectionStringBuilder.ContainsKey(propertyName))
			{
				this._connectionStringBuilder.Add(propertyName, String.Empty);
				this.OnPropertyChanged(EventArgs.Empty);
			}
		}

		public virtual bool Contains(string propertyName)
		{
			return this._connectionStringBuilder.ContainsKey(propertyName);
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this._connectionStringBuilder, true);
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this._connectionStringBuilder, true);
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this._connectionStringBuilder, true);
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this._connectionStringBuilder, true);
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this._connectionStringBuilder, true);
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return this.DefaultProperty;
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this._connectionStringBuilder, editorBaseType, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this._connectionStringBuilder, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this._connectionStringBuilder, attributes, true);
		}

		protected virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(this._connectionStringBuilder, attributes);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.GetProperties(new Attribute[0]);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties(attributes);
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this._connectionStringBuilder;
		}

		protected virtual void Inspect(DbConnection connection)
		{
		}

		protected virtual void OnPropertyChanged(EventArgs e)
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, e);
		}

		public virtual void Parse(string s)
		{
			this._connectionStringBuilder.ConnectionString = s;
			this.OnPropertyChanged(EventArgs.Empty);
		}

		public virtual void Remove(string propertyName)
		{
			if (this._connectionStringBuilder.ContainsKey(propertyName))
			{
				this._connectionStringBuilder.Remove(propertyName);
				this.OnPropertyChanged(EventArgs.Empty);
			}
		}

		public virtual void Reset()
		{
			this._connectionStringBuilder.Clear();
			this.OnPropertyChanged(EventArgs.Empty);
		}

		public virtual void Reset(string propertyName)
		{
			if (this._connectionStringBuilder.ContainsKey(propertyName))
			{
				this._connectionStringBuilder.Remove(propertyName);
				this.OnPropertyChanged(EventArgs.Empty);
			}
		}

		public virtual void Test()
		{
			string testString = this.ToTestString();

			// If the connection string is empty, don't even bother testing
			if (testString == null || testString.Length == 0)
				throw new InvalidOperationException(Strings.AdoDotNetConnectionProperties_NoProperties);

			// Create a connection object
			DbConnection connection = null;
			DbProviderFactory factory = DbProviderFactories.GetFactory(this._providerName);
			Debug.Assert(factory != null);
			connection = factory.CreateConnection();
			Debug.Assert(connection != null);

			// Try to open it
			try
			{
				connection.ConnectionString = testString;
				connection.Open();
				this.Inspect(connection);
			}
			finally
			{
				connection.Dispose();
			}
		}

		public virtual string ToDisplayString()
		{
			PropertyDescriptorCollection sensitiveProperties = this.GetProperties(new Attribute[] { PasswordPropertyTextAttribute.Yes });
			List<KeyValuePair<string, object>> savedValues = new List<KeyValuePair<string, object>>();
			foreach (PropertyDescriptor sensitiveProperty in sensitiveProperties)
			{
				string propertyName = sensitiveProperty.DisplayName;
				if (this.ConnectionStringBuilder.ShouldSerialize(propertyName))
				{
					savedValues.Add(new KeyValuePair<string, object>(propertyName, this.ConnectionStringBuilder[propertyName]));
					this.ConnectionStringBuilder.Remove(propertyName);
				}
			}
			try
			{
				return this.ConnectionStringBuilder.ConnectionString;
			}
			finally
			{
				foreach (KeyValuePair<string, object> savedValue in savedValues)
				{
					if (savedValue.Value != null)
						this.ConnectionStringBuilder[savedValue.Key] = savedValue.Value;
				}
			}
		}

		public virtual string ToFullString()
		{
			return this._connectionStringBuilder.ConnectionString;
		}

		public override string ToString()
		{
			return this.ToFullString();
		}

		protected virtual string ToTestString()
		{
			return this._connectionStringBuilder.ConnectionString;
		}

		#endregion
	}
}