//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

using Microsoft.Win32;

namespace TextMetal.ConnectionDialogApi
{
	public class SqlConnectionProperties : AdoDotNetConnectionProperties
	{
		#region Constructors/Destructors

		public SqlConnectionProperties()
			: base("System.Data.SqlClient")
		{
			this.LocalReset();
		}

		#endregion

		#region Fields/Constants

		private const int SqlError_CannotOpenDatabase = 4060;

		#endregion

		#region Properties/Indexers/Events

		protected override PropertyDescriptor DefaultProperty
		{
			get
			{
				return this.GetProperties(new Attribute[0])["DataSource"];
			}
		}

		public override bool IsComplete
		{
			get
			{
				if (!(this.ConnectionStringBuilder["Data Source"] is string) ||
				    (this.ConnectionStringBuilder["Data Source"] as string).Length == 0)
					return false;
				if (!(bool)this.ConnectionStringBuilder["Integrated Security"] &&
				    (!(this.ConnectionStringBuilder["User ID"] is string) ||
				     (this.ConnectionStringBuilder["User ID"] as string).Length == 0))
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void Inspect(DbConnection connection)
		{
			if (connection.ServerVersion.StartsWith("07", StringComparison.Ordinal) ||
			    connection.ServerVersion.StartsWith("08", StringComparison.Ordinal))
				throw new NotSupportedException(Strings.SqlConnectionProperties_UnsupportedSqlVersion);
		}

		private void LocalReset()
		{
			// We always start with integrated security turned on
			this["Integrated Security"] = true;
		}

		public override void Reset()
		{
			base.Reset();
			this.LocalReset();
		}

		public override void Test()
		{
			string dataSource = this.ConnectionStringBuilder["Data Source"] as string;
			if (dataSource == null || dataSource.Length == 0)
				throw new InvalidOperationException(Strings.SqlConnectionProperties_MustSpecifyDataSource);
			string database = this.ConnectionStringBuilder["Initial Catalog"] as string;
			try
			{
				base.Test();
			}
			catch (SqlException e)
			{
				if (e.Number == SqlError_CannotOpenDatabase && database != null && database.Length > 0)
					throw new InvalidOperationException(Strings.SqlConnectionProperties_CannotTestNonExistentDatabase);
				else
					throw;
			}
		}

		protected override string ToTestString()
		{
			bool savedPooling = (bool)this.ConnectionStringBuilder["Pooling"];
			bool wasDefault = !this.ConnectionStringBuilder.ShouldSerialize("Pooling");
			this.ConnectionStringBuilder["Pooling"] = false;
			string testString = this.ConnectionStringBuilder.ConnectionString;
			this.ConnectionStringBuilder["Pooling"] = savedPooling;
			if (wasDefault)
				this.ConnectionStringBuilder.Remove("Pooling");
			return testString;
		}

		#endregion
	}

	public class SqlFileConnectionProperties : SqlConnectionProperties
	{
		#region Constructors/Destructors

		public SqlFileConnectionProperties()
			: this(null)
		{
		}

		public SqlFileConnectionProperties(string defaultInstanceName)
		{
			this._defaultDataSource = ".";
			if (defaultInstanceName != null && defaultInstanceName.Length > 0)
				this._defaultDataSource += "\\" + defaultInstanceName;
			else
			{
				DataSourceConverter conv = new DataSourceConverter();
				TypeConverter.StandardValuesCollection coll = conv.GetStandardValues(null);
				if (coll.Count > 0)
					this._defaultDataSource = coll[0] as string;
			}
			this.LocalReset();
		}

		#endregion

		#region Fields/Constants

		private string _defaultDataSource;

		#endregion

		#region Properties/Indexers/Events

		public override bool IsComplete
		{
			get
			{
				if (!base.IsComplete)
					return false;
				if (!(this.ConnectionStringBuilder["AttachDbFilename"] is string) ||
				    (this.ConnectionStringBuilder["AttachDbFilename"] as string).Length == 0)
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		private bool CanResetDataSource(object component)
		{
			return !(this["Data Source"] is string) || !(this["Data Source"] as string).Equals(this._defaultDataSource, StringComparison.OrdinalIgnoreCase);
		}

		protected override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection descriptors = base.GetProperties(attributes);
			PropertyDescriptor dataSourceDescriptor = descriptors.Find("DataSource", true);
			if (dataSourceDescriptor != null)
			{
				int index = descriptors.IndexOf(dataSourceDescriptor);
				PropertyDescriptor[] descriptorArray = new PropertyDescriptor[descriptors.Count];
				descriptors.CopyTo(descriptorArray, 0);
				descriptorArray[index] = new DynamicPropertyDescriptor(dataSourceDescriptor, new TypeConverterAttribute(typeof(DataSourceConverter)));
				(descriptorArray[index] as DynamicPropertyDescriptor).CanResetValueHandler = new CanResetValueHandler(this.CanResetDataSource);
				(descriptorArray[index] as DynamicPropertyDescriptor).ResetValueHandler = new ResetValueHandler(this.ResetDataSource);
				descriptors = new PropertyDescriptorCollection(descriptorArray, true);
			}
			return descriptors;
		}

		private void LocalReset()
		{
			this["Data Source"] = this._defaultDataSource;
			this["User Instance"] = true;
			this["Connection Timeout"] = 30;
		}

		public override void Reset()
		{
			base.Reset();
			this.LocalReset();
		}

		private void ResetDataSource(object component)
		{
			this["Data Source"] = this._defaultDataSource;
		}

		public override void Test()
		{
			string attachDbFilename = this.ConnectionStringBuilder["AttachDbFilename"] as string;
			try
			{
				if (attachDbFilename == null || attachDbFilename.Length == 0)
					throw new InvalidOperationException(Strings.SqlFileConnectionProperties_NoFileSpecified);
				this.ConnectionStringBuilder["AttachDbFilename"] = Path.GetFullPath(attachDbFilename);
				if (!File.Exists(this.ConnectionStringBuilder["AttachDbFilename"] as string))
					throw new InvalidOperationException(Strings.SqlFileConnectionProperties_CannotTestNonExistentMdf);
				base.Test();
			}
			catch (SqlException e)
			{
				if (e.Number == -2) // timeout
					throw new ApplicationException(e.Errors[0].Message + Environment.NewLine + Strings.SqlFileConnectionProperties_TimeoutReasons);
				throw;
			}
			finally
			{
				if (attachDbFilename != null && attachDbFilename.Length > 0)
					this.ConnectionStringBuilder["AttachDbFilename"] = attachDbFilename;
			}
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private class DataSourceConverter : StringConverter
		{
			#region Constructors/Destructors

			public DataSourceConverter()
			{
			}

			#endregion

			#region Fields/Constants

			private StandardValuesCollection _standardValues;

			#endregion

			#region Methods/Operators

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (this._standardValues == null)
				{
					string[] dataSources = null;

					if (HelpUtils.IsWow64())
					{
						List<String> dataSourceList = new List<String>();
						// Read 64 registry key of SQL Server Instances Names. 
						dataSourceList.AddRange(HelpUtils.GetValueNamesWow64("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL", NativeMethods.KEY_WOW64_64KEY | NativeMethods.KEY_QUERY_VALUE));
						// Read 32 registry key of SQL Server Instances Names. 
						dataSourceList.AddRange(HelpUtils.GetValueNamesWow64("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL", NativeMethods.KEY_WOW64_32KEY | NativeMethods.KEY_QUERY_VALUE));
						dataSources = dataSourceList.ToArray();
					}
					else
					{
						// Look in the registry for all local SQL Server instances
						RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL");
						if (key != null)
						{
							using (key)
								dataSources = key.GetValueNames();
						}
					}

					if (dataSources != null)
					{
						for (int i = 0; i < dataSources.Length; i++)
						{
							if (String.Equals(dataSources[i], "MSSQLSERVER", StringComparison.OrdinalIgnoreCase))
								dataSources[i] = ".";
							else
								dataSources[i] = ".\\" + dataSources[i];
						}
						this._standardValues = new StandardValuesCollection(dataSources);
					}
					else
						this._standardValues = new StandardValuesCollection(new string[0]);
				}
				return this._standardValues;
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