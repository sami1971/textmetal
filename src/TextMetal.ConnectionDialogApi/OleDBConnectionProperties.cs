//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;

using Microsoft.Win32;

namespace TextMetal.ConnectionDialogApi
{
	public class OleDBConnectionProperties : AdoDotNetConnectionProperties
	{
		#region Constructors/Destructors

		public OleDBConnectionProperties()
			: base("System.Data.OleDb")
		{
		}

		#endregion

		#region Fields/Constants

		private bool _disableProviderSelection;

		#endregion

		#region Properties/Indexers/Events

		public bool DisableProviderSelection
		{
			get
			{
				return this._disableProviderSelection;
			}
			set
			{
				this._disableProviderSelection = value;
			}
		}

		public override bool IsComplete
		{
			get
			{
				if (!(this.ConnectionStringBuilder["Provider"] is string) ||
				    (this.ConnectionStringBuilder["Provider"] as string).Length == 0)
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Gets the registered OLE DB providers as an array of ProgIDs.
		/// </summary>
		public static List<string> GetRegisteredProviders()
		{
			// Get the sources rowset for the root OLE DB enumerator
			Type rootEnumeratorType = Type.GetTypeFromCLSID(NativeMethods.CLSID_OLEDB_ENUMERATOR);
			OleDbDataReader dr = OleDbEnumerator.GetEnumerator(rootEnumeratorType);

			// Read the CLSIDs of each data source (not binders or enumerators)
			Dictionary<string, string> sources = new Dictionary<string, string>(); // avoids duplicate entries
			using (dr)
			{
				while (dr.Read())
				{
					int type = (int)dr["SOURCES_TYPE"];
					if (type == NativeMethods.DBSOURCETYPE_DATASOURCE_TDP ||
					    type == NativeMethods.DBSOURCETYPE_DATASOURCE_MDP)
						sources[dr["SOURCES_CLSID"] as string] = null;
				}
			} // reader is disposed here

			// Get the ProgID for each data source
			List<string> sourceProgIds = new List<string>(sources.Count);
			RegistryKey key = Registry.ClassesRoot.OpenSubKey("CLSID");
			using (key)
			{
				foreach (KeyValuePair<string, string> source in sources)
				{
					RegistryKey subKey = key.OpenSubKey(source.Key + "\\ProgID");
					// if this subkey does not exist, ignore it.
					if (subKey != null)
					{
						using (subKey)
							sourceProgIds.Add(subKey.GetValue(null) as string);
					}
				}
			} // key is disposed here

			// Sort the prog ID array by name
			sourceProgIds.Sort();

			// The OLE DB provider for ODBC is not supported by the OLE DB .NET provider, so remove it
			while (sourceProgIds.Contains("MSDASQL.1"))
				sourceProgIds.Remove("MSDASQL.1");

			return sourceProgIds;
		}

		private bool CanResetProvider(object component)
		{
			return false;
		}

		protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection descriptors = base.GetProperties(attributes);
			if (this._disableProviderSelection)
			{
				PropertyDescriptor providerDescriptor = descriptors.Find("Provider", true);
				if (providerDescriptor != null)
				{
					int index = descriptors.IndexOf(providerDescriptor);
					PropertyDescriptor[] descriptorArray = new PropertyDescriptor[descriptors.Count];
					descriptors.CopyTo(descriptorArray, 0);
					descriptorArray[index] = new DynamicPropertyDescriptor(providerDescriptor, ReadOnlyAttribute.Yes);
					(descriptorArray[index] as DynamicPropertyDescriptor).CanResetValueHandler = new CanResetValueHandler(this.CanResetProvider);
					descriptors = new PropertyDescriptorCollection(descriptorArray, true);
				}
			}
			return descriptors;
		}

		#endregion
	}

	public class OleDBSpecializedConnectionProperties : OleDBConnectionProperties
	{
		#region Constructors/Destructors

		public OleDBSpecializedConnectionProperties(string provider)
		{
			Debug.Assert(provider != null);
			this._provider = provider;
			this.LocalReset();
		}

		#endregion

		#region Fields/Constants

		private string _provider;

		#endregion

		#region Methods/Operators

		protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			bool disableProviderSelection = this.DisableProviderSelection;
			try
			{
				this.DisableProviderSelection = true;
				return base.GetProperties(attributes);
			}
			finally
			{
				this.DisableProviderSelection = disableProviderSelection;
			}
		}

		private void LocalReset()
		{
			// Initialize with the selected provider
			this["Provider"] = this._provider;
		}

		public override void Reset()
		{
			base.Reset();
			this.LocalReset();
		}

		#endregion
	}

	public class OleDBSqlConnectionProperties : OleDBSpecializedConnectionProperties
	{
		#region Constructors/Destructors

		public OleDBSqlConnectionProperties()
			: base("SQLOLEDB")
		{
			this.LocalReset();
		}

		#endregion

		#region Fields/Constants

		private static bool _gotSqlNativeClientRegistered;
		private static List<string> _sqlNativeClientProviders = null;
		private static bool _sqlNativeClientRegistered;

		#endregion

		#region Properties/Indexers/Events

		public static List<string> SqlNativeClientProviders
		{
			get
			{
				if (_sqlNativeClientProviders == null)
				{
					_sqlNativeClientProviders = new List<string>();

					List<string> providers = GetRegisteredProviders();
					Debug.Assert(providers != null, "provider list is null");
					foreach (string provider in providers)
					{
						if (provider.StartsWith("SQLNCLI"))
						{
							int idx = provider.IndexOf(".");
							if (idx > 0)
								_sqlNativeClientProviders.Add(provider.Substring(0, idx).ToUpperInvariant());
						}
					}

					_sqlNativeClientProviders.Sort();
				}

				Debug.Assert(_sqlNativeClientProviders != null, "Native Client list is null");
				return _sqlNativeClientProviders;
			}
		}

		private static bool SqlNativeClientRegistered
		{
			get
			{
				if (!_gotSqlNativeClientRegistered)
				{
					RegistryKey key = null;
					try
					{
						_sqlNativeClientRegistered = SqlNativeClientProviders.Count > 0;
					}
					finally
					{
						if (key != null)
							key.Close();
					}
					_gotSqlNativeClientRegistered = true;
				}
				return _sqlNativeClientRegistered;
			}
		}

		public override bool IsComplete
		{
			get
			{
				if (!base.IsComplete)
					return false;
				if (!(this.ConnectionStringBuilder["Data Source"] is string) ||
				    (this.ConnectionStringBuilder["Data Source"] as string).Length == 0)
					return false;
				if ((this.ConnectionStringBuilder["Integrated Security"] == null ||
				     !this.ConnectionStringBuilder["Integrated Security"].ToString().Equals("SSPI", StringComparison.OrdinalIgnoreCase)) &&
				    (!(this.ConnectionStringBuilder["User ID"] is string) ||
				     (this.ConnectionStringBuilder["User ID"] as string).Length == 0))
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection descriptors = base.GetProperties(attributes);
			if (SqlNativeClientRegistered)
			{
				DynamicPropertyDescriptor providerDescriptor = descriptors.Find("Provider", true) as DynamicPropertyDescriptor;
				if (providerDescriptor != null)
				{
					if (!this.DisableProviderSelection)
						providerDescriptor.SetIsReadOnly(false);
					providerDescriptor.SetConverterType(typeof(SqlProviderConverter));
				}
			}
			return descriptors;
		}

		private void LocalReset()
		{
			// We always start with integrated security turned on
			this["Integrated Security"] = "SSPI";
		}

		public override void Reset()
		{
			base.Reset();
			this.LocalReset();
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class SqlProviderConverter : StringConverter
		{
			#region Constructors/Destructors

			public SqlProviderConverter()
			{
			}

			#endregion

			#region Methods/Operators

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				List<string> stdCollection = new List<string>();
				stdCollection.Add("SQLOLEDB");

				foreach (string provider in SqlNativeClientProviders)
					stdCollection.Add(provider);

				return new StandardValuesCollection(stdCollection);
			}

			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return true;
			}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			#endregion
		}

		#endregion
	}

	public class OleDBOracleConnectionProperties : OleDBSpecializedConnectionProperties
	{
		#region Constructors/Destructors

		public OleDBOracleConnectionProperties()
			: base("MSDAORA")
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public override bool IsComplete
		{
			get
			{
				if (!base.IsComplete)
					return false;
				if (!(this.ConnectionStringBuilder["Data Source"] is string) ||
				    (this.ConnectionStringBuilder["Data Source"] as string).Length == 0)
					return false;
				if ((!(this.ConnectionStringBuilder["User ID"] is string) ||
				     (this.ConnectionStringBuilder["User ID"] as string).Length == 0))
					return false;
				return true;
			}
		}

		#endregion
	}

	public class OleDBAccessConnectionProperties : OleDBSpecializedConnectionProperties
	{
		#region Constructors/Destructors

		public OleDBAccessConnectionProperties()
			: base("Microsoft.Jet.OLEDB.4.0")
		{
			this._userChangedProvider = false;
		}

		#endregion

		#region Fields/Constants

		private static bool _access12ProviderRegistered;
		private static bool _gotAccess12ProviderRegistered;
		private bool _userChangedProvider;

		#endregion

		#region Properties/Indexers/Events

		public override object this[string propertyName]
		{
			set
			{
				base[propertyName] = value;
				if (String.Equals(propertyName, "Provider", StringComparison.OrdinalIgnoreCase))
				{
					if (value != null && value != DBNull.Value)
						this.OnProviderChanged(this.ConnectionStringBuilder, EventArgs.Empty);
					else
						this._userChangedProvider = false;
				}
				if (String.Equals(propertyName, "Data Source", StringComparison.Ordinal))
					this.OnDataSourceChanged(this.ConnectionStringBuilder, EventArgs.Empty);
			}
		}

		private static bool Access12ProviderRegistered
		{
			get
			{
				if (!_gotAccess12ProviderRegistered)
				{
					RegistryKey key = null;
					try
					{
						key = Registry.ClassesRoot.OpenSubKey("Microsoft.ACE.OLEDB.12.0");
						_access12ProviderRegistered = (key != null);
					}
					finally
					{
						if (key != null)
							key.Close();
					}
					_gotAccess12ProviderRegistered = true;
				}
				return _access12ProviderRegistered;
			}
		}

		public override bool IsComplete
		{
			get
			{
				if (!base.IsComplete)
					return false;
				if (!(this.ConnectionStringBuilder["Data Source"] is string) ||
				    (this.ConnectionStringBuilder["Data Source"] as string).Length == 0)
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection descriptors = base.GetProperties(attributes);
			if (Access12ProviderRegistered)
			{
				DynamicPropertyDescriptor providerDescriptor = descriptors.Find("Provider", true) as DynamicPropertyDescriptor;
				if (providerDescriptor != null)
				{
					if (!this.DisableProviderSelection)
						providerDescriptor.SetIsReadOnly(false);
					providerDescriptor.SetConverterType(typeof(JetProviderConverter));
					providerDescriptor.AddValueChanged(this.ConnectionStringBuilder, new EventHandler(this.OnProviderChanged));
				}
				PropertyDescriptor dataSourceDescriptor = descriptors.Find("DataSource", true);
				if (dataSourceDescriptor != null)
				{
					int index = descriptors.IndexOf(dataSourceDescriptor);
					PropertyDescriptor[] descriptorArray = new PropertyDescriptor[descriptors.Count];
					descriptors.CopyTo(descriptorArray, 0);
					descriptorArray[index] = new DynamicPropertyDescriptor(dataSourceDescriptor);
					descriptorArray[index].AddValueChanged(this.ConnectionStringBuilder, new EventHandler(this.OnDataSourceChanged));
					descriptors = new PropertyDescriptorCollection(descriptorArray, true);
				}
			}
			PropertyDescriptor passwordDescriptor = descriptors.Find("Jet OLEDB:Database Password", true);
			if (passwordDescriptor != null)
			{
				int index = descriptors.IndexOf(passwordDescriptor);
				PropertyDescriptor[] descriptorArray = new PropertyDescriptor[descriptors.Count];
				descriptors.CopyTo(descriptorArray, 0);
				descriptorArray[index] = new DynamicPropertyDescriptor(passwordDescriptor, PasswordPropertyTextAttribute.Yes);
				descriptors = new PropertyDescriptorCollection(descriptorArray, true);
			}
			return descriptors;
		}

		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			if (Access12ProviderRegistered && !this._userChangedProvider)
			{
				string dataSource = this["Data Source"] as string;
				if (dataSource != null)
				{
					dataSource = dataSource.Trim().ToUpperInvariant();
					if (dataSource.EndsWith(".ACCDB", StringComparison.Ordinal))
						base["Provider"] = "Microsoft.ACE.OLEDB.12.0";
					else
						base["Provider"] = "Microsoft.Jet.OLEDB.4.0";
				}
			}
		}

		private void OnProviderChanged(object sender, EventArgs e)
		{
			if (Access12ProviderRegistered)
				this._userChangedProvider = true;
		}

		public override void Remove(string propertyName)
		{
			base.Remove(propertyName);
			if (String.Equals(propertyName, "Provider", StringComparison.OrdinalIgnoreCase))
				this._userChangedProvider = false;
			if (String.Equals(propertyName, "Data Source", StringComparison.Ordinal))
				this.OnDataSourceChanged(this.ConnectionStringBuilder, EventArgs.Empty);
		}

		public override void Reset()
		{
			base.Reset();
			this._userChangedProvider = false;
		}

		public override void Reset(string propertyName)
		{
			base.Reset(propertyName);
			if (String.Equals(propertyName, "Provider", StringComparison.OrdinalIgnoreCase))
				this._userChangedProvider = false;
			if (String.Equals(propertyName, "Data Source", StringComparison.Ordinal))
				this.OnDataSourceChanged(this.ConnectionStringBuilder, EventArgs.Empty);
		}

		public override void Test()
		{
			string dataSource = this.ConnectionStringBuilder["Data Source"] as string;
			if (dataSource == null || dataSource.Length == 0)
				throw new InvalidOperationException(Strings.OleDBAccessConnectionProperties_MustSpecifyDataSource);
			base.Test();
		}

		public override string ToDisplayString()
		{
			string savedPassword = null;
			if (this.ConnectionStringBuilder.ContainsKey("Jet OLEDB:Database Password") &&
			    this.ConnectionStringBuilder.ShouldSerialize("Jet OLEDB:Database Password"))
			{
				savedPassword = this.ConnectionStringBuilder["Jet OLEDB:Database Password"] as string;
				this.ConnectionStringBuilder.Remove("Jet OLEDB:Database Password");
			}
			string displayString = base.ToDisplayString();
			if (savedPassword != null)
				this.ConnectionStringBuilder["Jet OLEDB:Database Password"] = savedPassword;
			return displayString;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class JetProviderConverter : StringConverter
		{
			#region Constructors/Destructors

			public JetProviderConverter()
			{
			}

			#endregion

			#region Methods/Operators

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return new StandardValuesCollection(new string[]
				                                    {
				                                    	"Microsoft.Jet.OLEDB.4.0", "Microsoft.ACE.OLEDB.12.0"
				                                    });
			}

			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return true;
			}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			#endregion
		}

		#endregion
	}
}