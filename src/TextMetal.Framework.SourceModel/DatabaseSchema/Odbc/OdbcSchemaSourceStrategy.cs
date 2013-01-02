/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

using TextMetal.Common.Core;
using TextMetal.Common.Data;

namespace TextMetal.Framework.SourceModel.DatabaseSchema.Odbc
{
	public class OdbcSchemaSourceStrategy : SchemaSourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the OdbcSchemaSourceStrategy class.
		/// </summary>
		public OdbcSchemaSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override int CoreCalculateColumnSize(string dataSourceTag, Column column)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)column == null)
				throw new ArgumentNullException("column");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return column.ColumnSqlType == "image" ||
				       column.ColumnSqlType == "text" ||
				       column.ColumnSqlType == "ntext" ? (int)0 :
					       (column.ColumnDbType == DbType.String &&
					        column.ColumnSqlType.SafeToString().StartsWith("n") &&
					        column.ColumnSize != 0 ?
						        (int)(column.ColumnSize / 2) :
						        column.ColumnSize);
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return 0;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override int CoreCalculateParameterSize(string dataSourceTag, Parameter parameter)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if ((object)parameter == null)
				throw new ArgumentNullException("parameter");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
			{
				return parameter.ParameterSqlType == "image" ||
				       parameter.ParameterSqlType == "text" ||
				       parameter.ParameterSqlType == "ntext" ? (int)0 :
					       (parameter.ParameterDbType == DbType.String &&
					        parameter.ParameterSqlType.SafeToString().StartsWith("n") &&
					        parameter.ParameterSize != 0 ?
						        (int)(parameter.ParameterSize / 2) :
						        parameter.ParameterSize);
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return 0;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
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

		protected override IEnumerable<IDataParameter> CoreGetDatabaseParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag)
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

		protected override bool CoreGetEmitImplicitReturnParameter(string dataSourceTag)
		{
			if ((object)dataSourceTag == null)
				throw new ArgumentNullException("dataSourceTag");

			if (dataSourceTag.SafeToString().ToLower() == "odbc.sqlserver")
				return true;
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
				return false;

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		protected override IEnumerable<IDataParameter> CoreGetForeignKeyColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, ForeignKey foreignKey)
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

		protected override IEnumerable<IDataParameter> CoreGetForeignKeyParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
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

		protected override IEnumerable<IDataParameter> CoreGetParameterParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Procedure procedure)
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

		protected override IEnumerable<IDataParameter> CoreGetProcedureParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema)
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

		protected override IEnumerable<IDataParameter> CoreGetSchemaParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database)
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

		protected override IEnumerable<IDataParameter> CoreGetTableParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema)
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

		protected override IEnumerable<IDataParameter> CoreGetUniqueKeyColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, UniqueKey uniqueKey)
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

		protected override IEnumerable<IDataParameter> CoreGetUniqueKeyParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table)
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
					case null:
					case "":
						return typeof(Object);
					case "BIGINT":
						return typeof(Int64);
					case "BINARY":
						return typeof(Byte[]);
					case "BIT":
						return typeof(Boolean);
					case "CHAR":
						return typeof(String);
					case "CURSOR":
						return typeof(Object);
					case "DATE":
						return typeof(DateTime);
					case "DATETIME":
						return typeof(DateTime);
					case "DATETIME2":
						return typeof(DateTime);
					case "DATETIMEOFFSET":
						return typeof(DateTimeOffset);
					case "DECIMAL":
						return typeof(Decimal);
					case "FLOAT":
						return typeof(Double);
					case "HIERARCHYID":
						return typeof(Object);
					case "IMAGE":
						return typeof(Byte[]);
					case "INT":
						return typeof(Int32);
					case "MONEY":
						return typeof(Decimal);
					case "NCHAR":
						return typeof(String);
					case "NTEXT":
						return typeof(String);
					case "NUMERIC":
						return typeof(Decimal);
					case "NVARCHAR":
						return typeof(String);
					case "REAL":
						return typeof(Double);
					case "SMALLDATETIME":
						return typeof(DateTime);
					case "SMALLINT":
						return typeof(Int16);
					case "SMALLMONEY":
						return typeof(Decimal);
					case "SQL_VARIANT":
						return typeof(Object);
					case "TABLE":
						return typeof(Object);
					case "TEXT":
						return typeof(Object);
					case "TIME":
						return typeof(DateTime);
					case "TIMESTAMP":
						return typeof(Byte[]);
					case "TINYINT":
						return typeof(Byte);
					case "UNIQUEIDENTIFIER":
						return typeof(Guid);
					case "VARBINARY":
						return typeof(Byte[]);
					case "VARCHAR":
						return typeof(String);
					case "XML":
						return typeof(XmlDocument);
					case "SYSNAME":
						return typeof(String);
					default:
						throw new ArgumentOutOfRangeException(string.Format("sqlType: '{0}'", sqlType));
				}
			}
			else if (dataSourceTag.SafeToString().ToLower() == "odbc.sybaseiq")
			{
				switch (sqlType = sqlType.SafeToString().ToUpper())
				{
					case null:
					case "":
						return typeof(Object);
					case "BIGINT":
						return typeof(Int64);
					case "BINARY":
						return typeof(Byte[]);
					case "BIT":
						return typeof(Boolean);
					case "BLOB":
						return typeof(Byte[]);
					case "CHAR":
						return typeof(String);
					case "DATE":
						return typeof(DateTime);
					case "DATETIME":
						return typeof(DateTime);
					case "DECIMAL":
						return typeof(Decimal);
					case "DOUBLE":
						return typeof(Double);
					case "FLOAT":
						return typeof(Single);
					case "INT":
						return typeof(Int32);
					case "INTEGER":
						return typeof(Int32);
					case "LONGSYSNAME":
						return typeof(String);
					case "MONEY":
						return typeof(Object);
					case "NUMERIC":
						return typeof(Decimal);
					case "NVARCHAR":
						return typeof(String);
					case "OLDBIT":
						return typeof(Object);
					case "REAL":
						return typeof(Object);
					case "SMALLDATETIME":
						return typeof(Object);
					case "SMALLINT":
						return typeof(Int16);
					case "SMALLMONEY":
						return typeof(Decimal);
					case "ST_GEOMETRY":
						return typeof(Byte[]);
					case "SYSNAME":
						return typeof(String);
					case "TIME":
						return typeof(DateTime);
					case "TIMESTAMP":
						return typeof(DateTime);
					case "TIMESTAMP WITH TIME ZONE":
						return typeof(DateTimeOffset);
					case "TINYINT":
						return typeof(Byte);
					case "UNIQUEIDENTIFIER":
						return typeof(Byte[]);
					case "UNIQUEIDENTIFIERSTR":
						return typeof(String);
					case "VARCHAR":
						return typeof(String);
					case "LONG BINARY":
						return typeof(Byte[]);
					case "VARBINARY":
						return typeof(Byte[]);
					case "LONG NVARCHAR":
						return typeof(String);
					case "LONG VARCHAR":
						return typeof(String);
					case "ANSISTRING":
						return typeof(String);
					case "LONG VARBIT":
						return typeof(Boolean[]);
					case "UNSIGNED BIGINT":
						return typeof(Int64);
					case "UNSIGNED INT":
						return typeof(Int32);
					case "UNSIGNED TINYINT":
						return typeof(Byte);
					case "UNSIGNED SMALLINT":
						return typeof(Int16);
					default:
						throw new ArgumentOutOfRangeException(string.Format("sqlType: '{0}'", sqlType));
				}
			}

			throw new ArgumentOutOfRangeException(string.Format("dataSourceTag: '{0}'", dataSourceTag));
		}

		#endregion
	}
}