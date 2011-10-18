//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace TextMetal.ConnectionDialogApi
{
	public class DataProvider
	{
		#region Constructors/Destructors

		public DataProvider(string name, string displayName, string shortDisplayName)
			: this(name, displayName, shortDisplayName, null, null)
		{
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description)
			: this(name, displayName, shortDisplayName, description, null)
		{
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			this._name = name;
			this._displayName = displayName;
			this._shortDisplayName = shortDisplayName;
			this._description = description;
			this._targetConnectionType = targetConnectionType;
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType, Type connectionPropertiesType)
			: this(name, displayName, shortDisplayName, description, targetConnectionType)
		{
			if (connectionPropertiesType == null)
				throw new ArgumentNullException("connectionPropertiesType");

			this._connectionPropertiesTypes = new Dictionary<string, Type>();
			this._connectionPropertiesTypes.Add(String.Empty, connectionPropertiesType);
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType, Type connectionUIControlType, Type connectionPropertiesType)
			: this(name, displayName, shortDisplayName, description, targetConnectionType, connectionPropertiesType)
		{
			if (connectionUIControlType == null)
				throw new ArgumentNullException("connectionUIControlType");

			this._connectionUIControlTypes = new Dictionary<string, Type>();
			this._connectionUIControlTypes.Add(String.Empty, connectionUIControlType);
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType, IDictionary<string, Type> connectionUIControlTypes, Type connectionPropertiesType)
			: this(name, displayName, shortDisplayName, description, targetConnectionType, connectionPropertiesType)
		{
			this._connectionUIControlTypes = connectionUIControlTypes;
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType, IDictionary<string, string> dataSourceDescriptions, IDictionary<string, Type> connectionUIControlTypes, Type connectionPropertiesType)
			: this(name, displayName, shortDisplayName, description, targetConnectionType, connectionUIControlTypes, connectionPropertiesType)
		{
			this._dataSourceDescriptions = dataSourceDescriptions;
		}

		public DataProvider(string name, string displayName, string shortDisplayName, string description, Type targetConnectionType, IDictionary<string, string> dataSourceDescriptions, IDictionary<string, Type> connectionUIControlTypes, IDictionary<string, Type> connectionPropertiesTypes)
			: this(name, displayName, shortDisplayName, description, targetConnectionType)
		{
			this._dataSourceDescriptions = dataSourceDescriptions;
			this._connectionUIControlTypes = connectionUIControlTypes;
			this._connectionPropertiesTypes = connectionPropertiesTypes;
		}

		#endregion

		#region Fields/Constants

		private static DataProvider _odbcDataProvider;
		private static DataProvider _oleDBDataProvider;
		private static DataProvider _oracleDataProvider;
		private static DataProvider _sqlDataProvider;
		private static DataProvider _sqlCeDataProvider;
		private IDictionary<string, Type> _connectionPropertiesTypes;
		private IDictionary<string, Type> _connectionUIControlTypes;
		private IDictionary<string, string> _dataSourceDescriptions;
		private string _description;
		private string _displayName;
		private string _name;
		private string _shortDisplayName;
		private Type _targetConnectionType;

		#endregion

		#region Properties/Indexers/Events

		public static DataProvider OdbcDataProvider
		{
			get
			{
				if (_odbcDataProvider == null)
				{
					Dictionary<string, string> descriptions = new Dictionary<string, string>();
					descriptions.Add(DataSource.OdbcDataSource.Name, Strings.DataProvider_Odbc_DataSource_Description);

					Dictionary<string, Type> uiControls = new Dictionary<string, Type>();
					uiControls.Add(String.Empty, typeof(OdbcConnectionUIControl));

					_odbcDataProvider = new DataProvider(
						"System.Data.Odbc",
						Strings.DataProvider_Odbc,
						Strings.DataProvider_Odbc_Short,
						Strings.DataProvider_Odbc_Description,
						typeof(OdbcConnection),
						descriptions,
						uiControls,
						typeof(OdbcConnectionProperties));
				}
				return _odbcDataProvider;
			}
		}

		public static DataProvider OleDBDataProvider
		{
			get
			{
				if (_oleDBDataProvider == null)
				{
					Dictionary<string, string> descriptions = new Dictionary<string, string>();
					descriptions.Add(DataSource.SqlDataSource.Name, Strings.DataProvider_OleDB_SqlDataSource_Description);
					descriptions.Add(DataSource.OracleDataSource.Name, Strings.DataProvider_OleDB_OracleDataSource_Description);
					descriptions.Add(DataSource.AccessDataSource.Name, Strings.DataProvider_OleDB_AccessDataSource_Description);

					Dictionary<string, Type> uiControls = new Dictionary<string, Type>();
					uiControls.Add(DataSource.SqlDataSource.Name, typeof(SqlConnectionUIControl));
					uiControls.Add(DataSource.OracleDataSource.Name, typeof(OracleConnectionUIControl));
					uiControls.Add(DataSource.AccessDataSource.Name, typeof(AccessConnectionUIControl));
					uiControls.Add(String.Empty, typeof(OleDBConnectionUIControl));

					Dictionary<string, Type> properties = new Dictionary<string, Type>();
					properties.Add(DataSource.SqlDataSource.Name, typeof(OleDBSqlConnectionProperties));
					properties.Add(DataSource.OracleDataSource.Name, typeof(OleDBOracleConnectionProperties));
					properties.Add(DataSource.AccessDataSource.Name, typeof(OleDBAccessConnectionProperties));
					properties.Add(String.Empty, typeof(OleDBConnectionProperties));

					_oleDBDataProvider = new DataProvider(
						"System.Data.OleDb",
						Strings.DataProvider_OleDB,
						Strings.DataProvider_OleDB_Short,
						Strings.DataProvider_OleDB_Description,
						typeof(OleDbConnection),
						descriptions,
						uiControls,
						properties);
				}
				return _oleDBDataProvider;
			}
		}

		public static DataProvider OracleDataProvider
		{
			get
			{
				if (_oracleDataProvider == null)
				{
					Dictionary<string, string> descriptions = new Dictionary<string, string>();
					descriptions.Add(DataSource.OracleDataSource.Name, Strings.DataProvider_Oracle_DataSource_Description);

					Dictionary<string, Type> uiControls = new Dictionary<string, Type>();
					uiControls.Add(String.Empty, typeof(OracleConnectionUIControl));

					_oracleDataProvider = new DataProvider(
						"System.Data.OracleClient",
						Strings.DataProvider_Oracle,
						Strings.DataProvider_Oracle_Short,
						Strings.DataProvider_Oracle_Description,
						// Disable OracleClient deprecation warnings
#pragma warning disable 618
						typeof(OracleConnection),
#pragma warning restore 618
						descriptions,
						uiControls,
						typeof(OracleConnectionProperties));
				}
				return _oracleDataProvider;
			}
		}

		public static DataProvider SqlDataProvider
		{
			get
			{
				if (_sqlDataProvider == null)
				{
					Dictionary<string, string> descriptions = new Dictionary<string, string>();
					descriptions.Add(DataSource.SqlDataSource.Name, Strings.DataProvider_Sql_DataSource_Description);
					descriptions.Add(DataSource.MicrosoftSqlServerFileName, Strings.DataProvider_Sql_FileDataSource_Description);

					Dictionary<string, Type> uiControls = new Dictionary<string, Type>();
					uiControls.Add(DataSource.SqlDataSource.Name, typeof(SqlConnectionUIControl));
					uiControls.Add(DataSource.MicrosoftSqlServerFileName, typeof(SqlFileConnectionUIControl));
					uiControls.Add(String.Empty, typeof(SqlConnectionUIControl));

					Dictionary<string, Type> properties = new Dictionary<string, Type>();
					properties.Add(DataSource.MicrosoftSqlServerFileName, typeof(SqlFileConnectionProperties));
					properties.Add(String.Empty, typeof(SqlConnectionProperties));

					_sqlDataProvider = new DataProvider(
						"System.Data.SqlClient",
						Strings.DataProvider_Sql,
						Strings.DataProvider_Sql_Short,
						Strings.DataProvider_Sql_Description,
						typeof(SqlConnection),
						descriptions,
						uiControls,
						properties);
				}
				return _sqlDataProvider;
			}
		}

		public static DataProvider SqlCeDataProvider
		{
			get
			{
				if (_sqlCeDataProvider == null)
				{
					Dictionary<string, string> descriptions = new Dictionary<string, string>();
					descriptions.Add(DataSource.SqlCeDataSource.Name, Strings.DataProvider_SqlCe_DataSource_Description);
					descriptions.Add(DataSource.MicrosoftSqlCeFileName, Strings.DataProvider_SqlCe_FileDataSource_Description);

					Dictionary<string, Type> uiControls = new Dictionary<string, Type>();
					uiControls.Add(DataSource.SqlCeDataSource.Name, typeof(SqlCeConnectionUIControl));
					uiControls.Add(DataSource.MicrosoftSqlCeFileName, typeof(SqlCeConnectionUIControl));
					uiControls.Add(String.Empty, typeof(SqlConnectionUIControl));

					Dictionary<string, Type> properties = new Dictionary<string, Type>();
					properties.Add(DataSource.MicrosoftSqlCeFileName, typeof(SqlCeConnectionProperties));
					properties.Add(String.Empty, typeof(SqlCeConnectionProperties));

					_sqlCeDataProvider = new DataProvider(
						"Microsoft.SqlServerCe.Client",
						Strings.DataProvider_SqlCe,
						Strings.DataProvider_SqlCe_Short,
						Strings.DataProvider_SqlCe_Description,
						typeof(SqlCeConnection),
						descriptions,
						uiControls,
						properties);
				}
				return _sqlCeDataProvider;
			}
		}

		public string Description
		{
			get
			{
				return this.GetDescription(null);
			}
		}

		public string DisplayName
		{
			get
			{
				return (this._displayName != null) ? this._displayName : this._name;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
		}

		public string ShortDisplayName
		{
			get
			{
				return this._shortDisplayName;
			}
		}

		public Type TargetConnectionType
		{
			get
			{
				return this._targetConnectionType;
			}
		}

		#endregion

		#region Methods/Operators

		public IDataConnectionProperties CreateConnectionProperties()
		{
			return this.CreateConnectionProperties(null);
		}

		public virtual IDataConnectionProperties CreateConnectionProperties(DataSource dataSource)
		{
			string key = null;
			if (this._connectionPropertiesTypes != null &&
			    ((dataSource != null && this._connectionPropertiesTypes.ContainsKey(key = dataSource.Name)) ||
			     this._connectionPropertiesTypes.ContainsKey(key = String.Empty)))
				return Activator.CreateInstance(this._connectionPropertiesTypes[key]) as IDataConnectionProperties;
			else
				return null;
		}

		public IDataConnectionUIControl CreateConnectionUIControl()
		{
			return this.CreateConnectionUIControl(null);
		}

		public virtual IDataConnectionUIControl CreateConnectionUIControl(DataSource dataSource)
		{
			string key = null;
			if (this._connectionUIControlTypes != null &&
			    (dataSource != null && this._connectionUIControlTypes.ContainsKey(key = dataSource.Name)) ||
			    this._connectionUIControlTypes.ContainsKey(key = String.Empty))
				return Activator.CreateInstance(this._connectionUIControlTypes[key]) as IDataConnectionUIControl;
			else
				return null;
		}

		public virtual string GetDescription(DataSource dataSource)
		{
			if (this._dataSourceDescriptions != null && dataSource != null &&
			    this._dataSourceDescriptions.ContainsKey(dataSource.Name))
				return this._dataSourceDescriptions[dataSource.Name];
			else
				return this._description;
		}

		#endregion
	}
}