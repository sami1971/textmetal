//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TextMetal.ConnectionDialogApi
{
	public class DataSource
	{
		#region Constructors/Destructors

		private DataSource()
		{
			this._displayName = Strings.DataSource_UnspecifiedDisplayName;
			this._providers = new DataProviderCollection(this);
		}

		public DataSource(string name, string displayName)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			this._name = name;
			this._displayName = displayName;
			this._providers = new DataProviderCollection(this);
		}

		#endregion

		#region Fields/Constants

		public const string MicrosoftSqlServerFileName = "MicrosoftSqlServerFile";
		public const string MicrosoftSqlCeFileName = "MicrosoftSqlServerFile";
		private static DataSource _accessDataSource;
		private static DataSource _odbcDataSource;
		private static DataSource _oracleDataSource;
		private static DataSource _sqlDataSource;
		private static DataSource _sqlCeDataSource;
		private static DataSource _sqlFileDataSource;
		private DataProvider _defaultProvider;
		private string _displayName;
		private string _name;
		private ICollection<DataProvider> _providers;

		#endregion

		#region Properties/Indexers/Events

		public static DataSource AccessDataSource
		{
			get
			{
				if (_accessDataSource == null)
				{
					_accessDataSource = new DataSource("MicrosoftAccess", Strings.DataSource_MicrosoftAccess);
					_accessDataSource.Providers.Add(DataProvider.OleDBDataProvider);
				}
				return _accessDataSource;
			}
		}

		public static DataSource OdbcDataSource
		{
			get
			{
				if (_odbcDataSource == null)
				{
					_odbcDataSource = new DataSource("OdbcDsn", Strings.DataSource_MicrosoftOdbcDsn);
					_odbcDataSource.Providers.Add(DataProvider.OdbcDataProvider);
				}
				return _odbcDataSource;
			}
		}

		public static DataSource OracleDataSource
		{
			get
			{
				if (_oracleDataSource == null)
				{
					_oracleDataSource = new DataSource("Oracle", Strings.DataSource_Oracle);
					_oracleDataSource.Providers.Add(DataProvider.OracleDataProvider);
					_oracleDataSource.Providers.Add(DataProvider.OleDBDataProvider);
					_oracleDataSource.DefaultProvider = DataProvider.OracleDataProvider;
				}
				return _oracleDataSource;
			}
		}

		public static DataSource SqlDataSource
		{
			get
			{
				if (_sqlDataSource == null)
				{
					_sqlDataSource = new DataSource("MicrosoftSqlServer", Strings.DataSource_MicrosoftSqlServer);
					_sqlDataSource.Providers.Add(DataProvider.SqlDataProvider);
					_sqlDataSource.Providers.Add(DataProvider.OleDBDataProvider);
					_sqlDataSource.DefaultProvider = DataProvider.SqlDataProvider;
				}
				return _sqlDataSource;
			}
		}

		public static DataSource SqlFileDataSource
		{
			get
			{
				if (_sqlFileDataSource == null)
				{
					_sqlFileDataSource = new DataSource("MicrosoftSqlServerFile", Strings.DataSource_MicrosoftSqlServerFile);
					_sqlFileDataSource.Providers.Add(DataProvider.SqlDataProvider);
				}
				return _sqlFileDataSource;
			}
		}

		public static DataSource SqlCeDataSource
		{
			get
			{
				if (_sqlCeDataSource == null)
				{
					_sqlCeDataSource = new DataSource("MicrosoftSqlCe", Strings.DataSource_MicrosoftSqlCe);
					_sqlCeDataSource.Providers.Add(DataProvider.SqlCeDataProvider);
					//_sqlCeDataSource.DefaultProvider = DataProvider.SqlCeDataProvider;
				}
				return _sqlCeDataSource;
			}
		}

		public DataProvider DefaultProvider
		{
			get
			{
				switch (this._providers.Count)
				{
					case 0:
						Debug.Assert(this._defaultProvider == null);
						return null;
					case 1:
						// If there is only one data provider, it must be the default
						IEnumerator<DataProvider> e = this._providers.GetEnumerator();
						e.MoveNext();
						return e.Current;
					default:
						return (this._name != null) ? this._defaultProvider : null;
				}
			}
			set
			{
				if (this._providers.Count == 1 && this._defaultProvider != value)
					throw new InvalidOperationException(Strings.DataSource_CannotChangeSingleDataProvider);
				if (value != null && !this._providers.Contains(value))
					throw new InvalidOperationException(Strings.DataSource_DataProviderNotFound);
				this._defaultProvider = value;
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

		public ICollection<DataProvider> Providers
		{
			get
			{
				return this._providers;
			}
		}

		#endregion

		#region Methods/Operators

		public static void AddStandardDataSources(DataConnectionDialog dialog)
		{
			dialog.DataSources.Add(SqlDataSource);
			dialog.DataSources.Add(SqlFileDataSource);
			dialog.DataSources.Add(OracleDataSource);
			dialog.DataSources.Add(AccessDataSource);
			dialog.DataSources.Add(OdbcDataSource);
			dialog.DataSources.Add(SqlCeDataSource);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.SqlDataProvider);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OracleDataProvider);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OleDBDataProvider);
			dialog.UnspecifiedDataSource.Providers.Add(DataProvider.OdbcDataProvider);
			dialog.DataSources.Add(dialog.UnspecifiedDataSource);
		}

		internal static DataSource CreateUnspecified()
		{
			return new DataSource();
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class DataProviderCollection : ICollection<DataProvider>
		{
			#region Constructors/Destructors

			public DataProviderCollection(DataSource source)
			{
				Debug.Assert(source != null);

				this._list = new List<DataProvider>();
				this._source = source;
			}

			#endregion

			#region Fields/Constants

			private ICollection<DataProvider> _list;
			private DataSource _source;

			#endregion

			#region Properties/Indexers/Events

			public int Count
			{
				get
				{
					return this._list.Count;
				}
			}

			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			#endregion

			#region Methods/Operators

			public void Add(DataProvider item)
			{
				if (item == null)
					throw new ArgumentNullException("item");
				if (!this._list.Contains(item))
					this._list.Add(item);
			}

			public void Clear()
			{
				this._list.Clear();
				this._source._defaultProvider = null;
			}

			public bool Contains(DataProvider item)
			{
				return this._list.Contains(item);
			}

			public void CopyTo(DataProvider[] array, int arrayIndex)
			{
				this._list.CopyTo(array, arrayIndex);
			}

			public IEnumerator<DataProvider> GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			public bool Remove(DataProvider item)
			{
				bool result = this._list.Remove(item);
				if (item == this._source._defaultProvider)
					this._source._defaultProvider = null;
				return result;
			}

			#endregion
		}

		#endregion
	}
}