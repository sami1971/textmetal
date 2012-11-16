/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

using TextMetal.Plumbing.CommonFacilities;

namespace TextMetal.Core.SourceModel.DatabaseSchema.Odbc
{
	public class OdbcSchemaSourceStrategy : SchemaSourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the OdbcSchemaSourceStrategy class.
		/// </summary>
		public OdbcSchemaSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override short CoreCalculateColumnSize(string dataSourceTag, Column column)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)column == null)
				throw new ArgumentNullException("column");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return column.ColumnSqlType == "image" ||
				       column.ColumnSqlType == "text" ||
				       column.ColumnSqlType == "ntext" ? (short)0 :
					                                                  (column.ColumnDbType == DbType.String &&
					                                                   column.ColumnSqlType.SafeToString().StartsWith("n") &&
					                                                   column.ColumnSize != 0 ?
						                                                                          (short)(column.ColumnSize / 2) :
							                                                                                                         column.ColumnSize);
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return 0;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override short CoreCalculateParameterSize(string dataSourceTag, Parameter parameter)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)parameter == null)
				throw new ArgumentNullException("parameter");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return parameter.ParameterSqlType == "image" ||
				       parameter.ParameterSqlType == "text" ||
				       parameter.ParameterSqlType == "ntext" ? (short)0 :
					                                                        (parameter.ParameterDbType == DbType.String &&
					                                                         parameter.ParameterSqlType.SafeToString().StartsWith("n") &&
					                                                         parameter.ParameterSize != 0 ?
						                                                                                      (short)(parameter.ParameterSize / 2) :
							                                                                                                                           parameter.ParameterSize);
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return 0;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetColumnParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)table == null)
				throw new ArgumentNullException("table");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P3", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P4", table.TableName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetDatabaseParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return null;
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return null;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetForeignKeyColumnParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, ForeignKey foreignKey)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)table == null)
				throw new ArgumentNullException("table");

			if ((object)foreignKey == null)
				throw new ArgumentNullException("foreignKey");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P3", foreignKey.ForeignKeyName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P3", foreignKey.ForeignKeyName)
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetForeignKeyParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)table == null)
				throw new ArgumentNullException("table");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName)
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetParameterParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Procedure procedure)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)procedure == null)
				throw new ArgumentNullException("procedure");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", procedure.ProcedureName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", procedure.ProcedureName)
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override string CoreGetParameterPrefix(string dataSourceTag)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return "@";
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return "?";

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetProcedureParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName) };
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName) };

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetSchemaParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return null;
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return null;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetTableParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema)
		{
			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName), unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", schema.SchemaName) };
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName) };

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetUniqueKeyColumnParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, UniqueKey uniqueKey)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)table == null)
				throw new ArgumentNullException("table");

			if ((object)uniqueKey == null)
				throw new ArgumentNullException("uniqueKey");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P3", uniqueKey.UniqueKeyName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P3", uniqueKey.UniqueKeyName)
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetUniqueKeyParameters(UnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)database == null)
				throw new ArgumentNullException("database");

			if ((object)schema == null)
				throw new ArgumentNullException("schema");

			if ((object)table == null)
				throw new ArgumentNullException("table");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName)
				       };
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				return new IDataParameter[]
				       {
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P1", schema.SchemaName),
					       unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@P2", table.TableName)
				       };
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override Type CoreInferClrTypeForSqlType(string dataSourceTag, string sqlType)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				switch (sqlType = sqlType.SafeToString().ToUpper())
				{
					case "BIT":
						return typeof(Boolean);
					case "TINYINT":
						return typeof(Int16);
					case "INT":
						return typeof(Int32);
					case "BIGINT":
						return typeof(Int64);
					case "SMALLMONEY":
					case "MONEY":
					case "DECIMAL":
					case "NUMERIC":
						return typeof(Decimal);
					case "REAL":
						//case "FLOAT(24)":
						//return typeof(Single);
					case "FLOAT":
						//case "FLOAT(53)":
						return typeof(Double);
					case "CHAR":
					case "NCHAR":
					case "VARCHAR":
					case "NVARCHAR":
					case "TEXT":
					case "NTEXT":
					case "SYSNAME":
						return typeof(String);
					case "XML":
						return typeof(XmlDocument);
					case "SMALLDATETIME":
					case "DATETIME":
					case "DATETIME2":
						return typeof(DateTime);
					case "DATETIMEOFFSET":
						return typeof(DateTimeOffset);
					case "DATE":
						return typeof(DateTime);
					case "TIME":
						return typeof(TimeSpan);
					case "BINARY":
					case "VARBINARY":
					case "IMAGE":
					case "TIMESTAMP":
						return typeof(Byte[]);
					case "UNIQUEIDENTIFIER":
						return typeof(Guid);
					case "SQL_VARIANT":
						return typeof(Object);
					default:
						throw new ArgumentOutOfRangeException(string.Format("sqlType: '{0}'", sqlType));
				}
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		#endregion
	}
}