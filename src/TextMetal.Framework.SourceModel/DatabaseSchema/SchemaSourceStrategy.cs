﻿/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

using TextMetal.Common.Core;
using TextMetal.Common.Data;

namespace TextMetal.Framework.SourceModel.DatabaseSchema
{
	public abstract class SchemaSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SchemaSourceStrategy class.
		/// </summary>
		protected SchemaSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static string FormatCSharpType(Type type)
		{
			if ((object)type == null)
				throw new ArgumentNullException("type");

			if (type.IsGenericType)
			{
				Type[] args;
				List<string> s;

				s = new List<string>();
				args = type.GetGenericArguments();

				if ((object)args != null)
				{
					foreach (Type arg in args)
						s.Add(FormatCSharpType(arg));
				}

				return string.Format("{0}<{1}>", Regex.Replace(type.Name, "([A-Za-z0-9_]+)(`[0-1]+)", "$1"), string.Join(", ", s.ToArray()));
			}

			return type.Name;
		}

		private static string GetAllAssemblyResourceFileText(Type type, string folder, string name)
		{
			string resourcePath;
			string sqlText;

			if ((object)type == null)
				throw new ArgumentNullException("type");

			if ((object)name == null)
				throw new ArgumentNullException("name");

			if (DataType.IsWhiteSpace(name))
				throw new ArgumentOutOfRangeException("name");

			resourcePath = string.Format("{0}.DML.{1}.{2}.sql", type.Namespace, folder, name);

			if (!Cerealization.TryGetStringFromAssemblyResource(type, resourcePath, out sqlText))
				throw new InvalidOperationException(string.Format("Failed to obtain assembly manifest (embedded) resource '{0}'.", resourcePath));

			return sqlText;
		}

		public static DbType InferDbTypeForClrType(Type clrType)
		{
			if ((object)clrType == null)
				throw new ArgumentNullException("clrType");

			if (clrType.IsByRef /* || type.IsPointer || type.IsArray */)
				return InferDbTypeForClrType(clrType.GetElementType());
			else if (clrType.IsGenericType &&
			         !clrType.IsGenericTypeDefinition &&
			         clrType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return InferDbTypeForClrType(Nullable.GetUnderlyingType(clrType));
			else if (clrType.IsEnum)
				return InferDbTypeForClrType(Enum.GetUnderlyingType(clrType));
			else if (clrType == typeof(Boolean))
				return DbType.Boolean;
			else if (clrType == typeof(Byte))
				return DbType.Byte;
			else if (clrType == typeof(DateTime))
				return DbType.DateTime;
			else if (clrType == typeof(DateTimeOffset))
				return DbType.DateTimeOffset;
			else if (clrType == typeof(Decimal))
				return DbType.Decimal;
			else if (clrType == typeof(Double))
				return DbType.Double;
			else if (clrType == typeof(Guid))
				return DbType.Guid;
			else if (clrType == typeof(Int16))
				return DbType.Int16;
			else if (clrType == typeof(Int32))
				return DbType.Int32;
			else if (clrType == typeof(Int64))
				return DbType.Int64;
			else if (clrType == typeof(SByte))
				return DbType.SByte;
			else if (clrType == typeof(Single))
				return DbType.Single;
			else if (clrType == typeof(TimeSpan))
				return DbType.Time;
			else if (clrType == typeof(UInt16))
				return DbType.UInt16;
			else if (clrType == typeof(UInt32))
				return DbType.UInt32;
			else if (clrType == typeof(UInt64))
				return DbType.UInt64;
			else if (clrType == typeof(Byte[]))
				return DbType.Binary;
			else if (clrType == typeof(Boolean[]))
				return DbType.Byte;
			else if (clrType == typeof(String))
				return DbType.String;
			else if (clrType == typeof(Object))
				return DbType.Object;
			else
				throw new InvalidOperationException(string.Format("Cannot infer parameter type from unsupported CLR type '{0}'.", clrType.FullName));
		}

		protected abstract int CoreCalculateColumnSize(string dataSourceTag, Column column);

		protected abstract int CoreCalculateParameterSize(string dataSourceTag, Parameter parameter);

		protected abstract IEnumerable<IDataParameter> CoreGetColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table);

		protected abstract IEnumerable<IDataParameter> CoreGetDatabaseParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag);

		protected abstract bool CoreGetEmitImplicitReturnParameter(string dataSourceTag);

		protected abstract IEnumerable<IDataParameter> CoreGetForeignKeyColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, ForeignKey foreignKey);

		protected abstract IEnumerable<IDataParameter> CoreGetForeignKeyParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table);

		protected abstract IEnumerable<IDataParameter> CoreGetParameterParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Procedure procedure);

		protected abstract string CoreGetParameterPrefix(string dataSourceTag);

		protected abstract IEnumerable<IDataParameter> CoreGetProcedureParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema);

		protected abstract IEnumerable<IDataParameter> CoreGetSchemaParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database);

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			const string PROP_TOKEN_CONNECTION_AQTN = "ConnectionType";
			const string PROP_TOKEN_CONNECTION_STRING = "ConnectionString";
			const string PROP_TOKEN_DATA_SOURCE_TAG = "DataSourceTag";
			const string PROP_TOKEN_SCHEMA_FILTER = "SchemaFilter";
			string connectionAqtn;
			Type connectionType = null;
			string connectionString = null;
			string dataSourceTag;
			string[] schemaFilter;
			IList<string> values;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			connectionAqtn = null;
			if (properties.TryGetValue(PROP_TOKEN_CONNECTION_AQTN, out values))
			{
				if ((object)values != null && values.Count == 1)
				{
					connectionAqtn = values[0];
					connectionType = Type.GetType(connectionAqtn, false);
				}
			}

			if ((object)connectionType == null)
				throw new InvalidOperationException(string.Format("Failed to load the connection type '{0}' via Type.GetType(..).", connectionAqtn));

			if (!typeof(IDbConnection).IsAssignableFrom(connectionType))
				throw new InvalidOperationException(string.Format("The connection type is not assignable to type '{0}'.", typeof(IDbConnection).FullName));

			if (properties.TryGetValue(PROP_TOKEN_CONNECTION_STRING, out values))
			{
				if ((object)values != null && values.Count == 1)
					connectionString = values[0];
			}

			if (DataType.IsNullOrWhiteSpace(connectionString))
				connectionString = sourceFilePath;

			if (DataType.IsWhiteSpace(connectionString))
				throw new InvalidOperationException(string.Format("The connection string cannot be null or whitespace."));

			dataSourceTag = null;
			if (properties.TryGetValue(PROP_TOKEN_DATA_SOURCE_TAG, out values))
			{
				if ((object)values != null && values.Count == 1)
					dataSourceTag = values[0];
			}

			schemaFilter = null;
			if (properties.TryGetValue(PROP_TOKEN_SCHEMA_FILTER, out values))
			{
				if ((object)values != null && values.Count > 0)
					schemaFilter = values.ToArray();
			}

			return this.GetSchemaModel(connectionString, connectionType, dataSourceTag, schemaFilter);
		}

		protected abstract IEnumerable<IDataParameter> CoreGetTableParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema);

		protected abstract IEnumerable<IDataParameter> CoreGetUniqueKeyColumnParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table, UniqueKey uniqueKey);

		protected abstract IEnumerable<IDataParameter> CoreGetUniqueKeyParameters(IUnitOfWorkContext unitOfWorkContext, string dataSourceTag, Database database, Schema schema, Table table);

		protected abstract Type CoreInferClrTypeForSqlType(string dataSourceTag, string sqlType);

		private object GetSchemaModel(string connectionString, Type connectionType, string dataSourceTag, string[] schemaFilter)
		{
			Database database;
			int recordsAffected;
			const string RETURN_VALUE = "ReturnValue";
			Type clrType;

			if ((object)connectionString == null)
				throw new ArgumentNullException("connectionString");

			if ((object)connectionType == null)
				throw new ArgumentNullException("connectionType");

			using (IUnitOfWorkContext unitOfWorkContext = UnitOfWorkContext.Create(connectionType, connectionString, false))
			{
				database = new Database();
				database.ConnectionString = connectionString;
				database.ConnectionType = connectionType.FullName;

				var dataReaderDatabase = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Database"), this.CoreGetDatabaseParameters(unitOfWorkContext, dataSourceTag), out recordsAffected);
				{
					if ((object)dataReaderDatabase != null &&
					    dataReaderDatabase.Count == 1)
					{
						database.InitialCatalogName = DataType.ChangeType<string>(dataReaderDatabase[0]["InitialCatalogName"]);
						database.InstanceName = DataType.ChangeType<string>(dataReaderDatabase[0]["InstanceName"]);
						database.MachineName = DataType.ChangeType<string>(dataReaderDatabase[0]["MachineName"]);
						database.ServerEdition = DataType.ChangeType<string>(dataReaderDatabase[0]["ServerEdition"]);
						database.ServerLevel = DataType.ChangeType<string>(dataReaderDatabase[0]["ServerLevel"]);
						database.ServerVersion = DataType.ChangeType<string>(dataReaderDatabase[0]["ServerVersion"]);
						database.InitialCatalogNamePascalCase = Name.GetPascalCase(database.InitialCatalogName);
						database.InitialCatalogNameCamelCase = Name.GetCamelCase(database.InitialCatalogName);
						database.InitialCatalogNameConstantCase = Name.GetConstantCase(database.InitialCatalogName);
						database.InitialCatalogNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(database.InitialCatalogName));
						database.InitialCatalogNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(database.InitialCatalogName));
						database.InitialCatalogNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(database.InitialCatalogName));
						database.InitialCatalogNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(database.InitialCatalogName));
						database.InitialCatalogNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(database.InitialCatalogName));
						database.InitialCatalogNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(database.InitialCatalogName));
					}
				}

				var dataReaderSchema = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Schemas"), this.CoreGetSchemaParameters(unitOfWorkContext, dataSourceTag, database), out recordsAffected);
				{
					if ((object)dataReaderSchema != null)
					{
						foreach (var drSchema in dataReaderSchema)
						{
							Schema schema;

							schema = new Schema();
							schema.SchemaName = DataType.ChangeType<string>(drSchema["SchemaName"]);
							schema.SchemaNamePascalCase = Name.GetPascalCase(schema.SchemaName);
							schema.SchemaNameCamelCase = Name.GetCamelCase(schema.SchemaName);
							schema.SchemaNameConstantCase = Name.GetConstantCase(schema.SchemaName);
							schema.SchemaNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(schema.SchemaName));
							schema.SchemaNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(schema.SchemaName));
							schema.SchemaNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(schema.SchemaName));
							schema.SchemaNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(schema.SchemaName));
							schema.SchemaNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(schema.SchemaName));
							schema.SchemaNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(schema.SchemaName));

							// filter unwanted schemas
							if ((object)schemaFilter != null)
							{
								if (!schemaFilter.Contains(schema.SchemaName))
									continue;
							}

							database.Schemas.Add(schema);

							var dataReaderTable = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Tables"), this.CoreGetTableParameters(unitOfWorkContext, dataSourceTag, database, schema), out recordsAffected);
							{
								foreach (var drTable in dataReaderTable)
								{
									Table table;

									table = new Table();
									table.IsView = DataType.ChangeType<bool>(drTable["IsView"]);
									table.TableName = DataType.ChangeType<string>(drTable["TableName"]);
									table.TableNamePascalCase = Name.GetPascalCase(table.TableName);
									table.TableNameCamelCase = Name.GetCamelCase(table.TableName);
									table.TableNameConstantCase = Name.GetConstantCase(table.TableName);
									table.TableNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(table.TableName));
									table.TableNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(table.TableName));
									table.TableNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(table.TableName));
									table.TableNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(table.TableName));
									table.TableNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(table.TableName));
									table.TableNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(table.TableName));

									schema.Tables.Add(table);

									var dataReaderColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Columns"), this.CoreGetColumnParameters(unitOfWorkContext, dataSourceTag, database, schema, table), out recordsAffected);
									{
										if ((object)dataReaderColumn != null)
										{
											foreach (var drColumn in dataReaderColumn)
											{
												Column column;

												column = new Column();

												column.ColumnName = DataType.ChangeType<string>(drColumn["ColumnName"]);
												column.ColumnOrdinal = DataType.ChangeType<int>(drColumn["ColumnOrdinal"]);
												column.ColumnNullable = DataType.ChangeType<bool>(drColumn["ColumnNullable"]);
												column.ColumnSize = DataType.ChangeType<int>(drColumn["ColumnSize"]);
												column.ColumnPrecision = DataType.ChangeType<int>(drColumn["ColumnPrecision"]);
												column.ColumnScale = DataType.ChangeType<int>(drColumn["ColumnScale"]);
												column.ColumnSqlType = DataType.ChangeType<string>(drColumn["ColumnSqlType"]);
												column.ColumnIsIdentity = DataType.ChangeType<bool>(drColumn["ColumnIsIdentity"]);
												column.ColumnIsComputed = DataType.ChangeType<bool>(drColumn["ColumnIsComputed"]);
												column.ColumnHasDefault = DataType.ChangeType<bool>(drColumn["ColumnHasDefault"]);
												column.ColumnHasCheck = DataType.ChangeType<bool>(drColumn["ColumnHasCheck"]);
												column.ColumnIsPrimaryKey = DataType.ChangeType<bool>(drColumn["ColumnIsPrimaryKey"]);
												column.PrimaryKeyName = DataType.ChangeType<string>(drColumn["PrimaryKeyName"]);
												column.PrimaryKeyColumnOrdinal = DataType.ChangeType<int>(drColumn["PrimaryKeyColumnOrdinal"]);
												column.ColumnNamePascalCase = Name.GetPascalCase(column.ColumnName);
												column.ColumnNameCamelCase = Name.GetCamelCase(column.ColumnName);
												column.ColumnNameConstantCase = Name.GetConstantCase(column.ColumnName);
												column.ColumnNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(column.ColumnName));
												column.ColumnNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(column.ColumnName));
												column.ColumnNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(column.ColumnName));

												clrType = this.CoreInferClrTypeForSqlType(dataSourceTag, column.ColumnSqlType);
												column.ColumnDbType = InferDbTypeForClrType(clrType);
												column.ColumnSize = this.CoreCalculateColumnSize(dataSourceTag, column); //recalculate

												column.ColumnClrType = clrType ?? typeof(object);
												column.ColumnClrNullableType = Reflexion.MakeNullableType(clrType);
												column.ColumnClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
												column.ColumnCSharpNullableLiteral = column.ColumnNullable.ToString().ToLower();
												column.ColumnCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, column.ColumnDbType);
												column.ColumnCSharpClrType = (object)column.ColumnClrType != null ? FormatCSharpType(column.ColumnClrType) : FormatCSharpType(typeof(object));
												column.ColumnCSharpClrNullableType = (object)column.ColumnClrNullableType != null ? FormatCSharpType(column.ColumnClrNullableType) : FormatCSharpType(typeof(object));
												column.ColumnCSharpClrNonNullableType = (object)column.ColumnClrNonNullableType != null ? FormatCSharpType(column.ColumnClrNonNullableType) : FormatCSharpType(typeof(object));

												table.Columns.Add(column);
											}
										}
									}

									if (table.Columns.Count(c => c.ColumnIsPrimaryKey) < 1)
									{
										table.HasNoDefinedPrimaryKeyColumns = true;
										table.Columns.ForEach(c => c.ColumnIsPrimaryKey = true);
									}

									var dataReaderForeignKey = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "ForeignKeys"), this.CoreGetForeignKeyParameters(unitOfWorkContext, dataSourceTag, database, schema, table), out recordsAffected);
									{
										if ((object)dataReaderForeignKey != null)
										{
											foreach (var drForeignKey in dataReaderForeignKey)
											{
												ForeignKey foreignKey;

												foreignKey = new ForeignKey();

												foreignKey.ForeignKeyName = DataType.ChangeType<string>(drForeignKey["ForeignKeyName"]);
												foreignKey.ForeignKeyIsDisabled = DataType.ChangeType<bool>(drForeignKey["ForeignKeyIsDisabled"]);
												foreignKey.ForeignKeyIsForReplication = DataType.ChangeType<bool>(drForeignKey["ForeignKeyIsForReplication"]);
												foreignKey.ForeignKeyOnDeleteRefIntAction = DataType.ChangeType<byte>(drForeignKey["ForeignKeyOnDeleteRefIntAction"]);
												foreignKey.ForeignKeyOnDeleteRefIntActionSqlName = DataType.ChangeType<string>(drForeignKey["ForeignKeyOnDeleteRefIntActionSqlName"]);
												foreignKey.ForeignKeyOnUpdateRefIntAction = DataType.ChangeType<byte>(drForeignKey["ForeignKeyOnUpdateRefIntAction"]);
												foreignKey.ForeignKeyOnUpdateRefIntActionSqlName = DataType.ChangeType<string>(drForeignKey["ForeignKeyOnUpdateRefIntActionSqlName"]);
												foreignKey.ForeignKeyNamePascalCase = Name.GetPascalCase(foreignKey.ForeignKeyName);
												foreignKey.ForeignKeyNameCamelCase = Name.GetCamelCase(foreignKey.ForeignKeyName);
												foreignKey.ForeignKeyNameConstantCase = Name.GetConstantCase(foreignKey.ForeignKeyName);
												foreignKey.ForeignKeyNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(foreignKey.ForeignKeyName));
												foreignKey.ForeignKeyNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(foreignKey.ForeignKeyName));
												foreignKey.ForeignKeyNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(foreignKey.ForeignKeyName));
												foreignKey.ForeignKeyNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(foreignKey.ForeignKeyName));
												foreignKey.ForeignKeyNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(foreignKey.ForeignKeyName));
												foreignKey.ForeignKeyNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(foreignKey.ForeignKeyName));

												table.ForeignKeys.Add(foreignKey);

												var dataReaderForeignKeyColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "ForeignKeyColumns"), this.CoreGetForeignKeyColumnParameters(unitOfWorkContext, dataSourceTag, database, schema, table, foreignKey), out recordsAffected);
												{
													if ((object)dataReaderForeignKeyColumn != null)
													{
														foreach (var drForeignKeyColumn in dataReaderForeignKeyColumn)
														{
															ForeignKeyColumnRef foreignKeyColumnRef;

															foreignKeyColumnRef = new ForeignKeyColumnRef();

															foreignKeyColumnRef.ForeignKeyOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["ForeignKeyOrdinal"]);
															foreignKeyColumnRef.ColumnOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["ColumnOrdinal"]);
															foreignKeyColumnRef.ColumnName = DataType.ChangeType<string>(drForeignKeyColumn["ColumnName"]);
															foreignKeyColumnRef.PrimarySchemaName = DataType.ChangeType<string>(drForeignKeyColumn["PrimarySchemaName"]);
															foreignKeyColumnRef.PrimaryTableName = DataType.ChangeType<string>(drForeignKeyColumn["PrimaryTableName"]);
															foreignKeyColumnRef.PrimaryKeyName = DataType.ChangeType<string>(drForeignKeyColumn["PrimaryKeyName"]);
															foreignKeyColumnRef.PrimaryKeyOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["PrimaryKeyOrdinal"]);
															foreignKeyColumnRef.PrimaryKeyColumnOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["PrimaryKeyColumnOrdinal"]);
															foreignKeyColumnRef.PrimaryKeyColumnName = DataType.ChangeType<string>(drForeignKeyColumn["PrimaryKeyColumnName"]);

															foreignKey.ForeignKeyColumnRefs.Add(foreignKeyColumnRef);
														}
													}
												}
											}
										}
									}

									var dataReaderUniqueKey = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "UniqueKeys"), this.CoreGetUniqueKeyParameters(unitOfWorkContext, dataSourceTag, database, schema, table), out recordsAffected);
									{
										if ((object)dataReaderUniqueKey != null)
										{
											foreach (var drUniqueKey in dataReaderUniqueKey)
											{
												UniqueKey uniqueKey;

												uniqueKey = new UniqueKey();

												uniqueKey.UniqueKeyName = DataType.ChangeType<string>(drUniqueKey["UniqueKeyName"]);
												uniqueKey.UniqueKeyIsDisabled = DataType.ChangeType<bool>(drUniqueKey["UniqueKeyIsDisabled"]);
												uniqueKey.UniqueKeyNamePascalCase = Name.GetPascalCase(uniqueKey.UniqueKeyName);
												uniqueKey.UniqueKeyNameCamelCase = Name.GetCamelCase(uniqueKey.UniqueKeyName);
												uniqueKey.UniqueKeyNameConstantCase = Name.GetConstantCase(uniqueKey.UniqueKeyName);
												uniqueKey.UniqueKeyNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(uniqueKey.UniqueKeyName));
												uniqueKey.UniqueKeyNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(uniqueKey.UniqueKeyName));
												uniqueKey.UniqueKeyNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(uniqueKey.UniqueKeyName));
												uniqueKey.UniqueKeyNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(uniqueKey.UniqueKeyName));
												uniqueKey.UniqueKeyNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(uniqueKey.UniqueKeyName));
												uniqueKey.UniqueKeyNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(uniqueKey.UniqueKeyName));

												table.UniqueKeys.Add(uniqueKey);

												var dataReaderUniqueKeyColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "UniqueKeyColumns"), this.CoreGetUniqueKeyColumnParameters(unitOfWorkContext, dataSourceTag, database, schema, table, uniqueKey), out recordsAffected);
												{
													if ((object)dataReaderUniqueKeyColumn != null)
													{
														foreach (var drUniqueKeyColumn in dataReaderUniqueKeyColumn)
														{
															UniqueKeyColumnRef uniqueKeyColumnRef;

															uniqueKeyColumnRef = new UniqueKeyColumnRef();

															uniqueKeyColumnRef.UniqueKeyOrdinal = DataType.ChangeType<int>(drUniqueKeyColumn["UniqueKeyOrdinal"]);
															uniqueKeyColumnRef.ColumnOrdinal = DataType.ChangeType<int>(drUniqueKeyColumn["ColumnOrdinal"]);
															uniqueKeyColumnRef.ColumnName = DataType.ChangeType<string>(drUniqueKeyColumn["ColumnName"]);
															uniqueKeyColumnRef.UniqueKeyColumnDescendingSort = DataType.ChangeType<bool>(drUniqueKeyColumn["UniqueKeyColumnDescendingSort"]);

															uniqueKey.UniqueKeyColumnRefs.Add(uniqueKeyColumnRef);
														}
													}
												}
											}
										}
									}
								}
							}

							var dataReaderProcedure = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Procedures"), this.CoreGetProcedureParameters(unitOfWorkContext, dataSourceTag, database, schema), out recordsAffected);
							{
								if ((object)dataReaderProcedure != null)
								{
									foreach (var drProcedure in dataReaderProcedure)
									{
										Procedure procedure;

										procedure = new Procedure();
										procedure.ProcedureName = DataType.ChangeType<string>(drProcedure["ProcedureName"]);
										procedure.ProcedureNamePascalCase = Name.GetPascalCase(procedure.ProcedureName);
										procedure.ProcedureNameCamelCase = Name.GetCamelCase(procedure.ProcedureName);
										procedure.ProcedureNameConstantCase = Name.GetConstantCase(procedure.ProcedureName);
										procedure.ProcedureNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(procedure.ProcedureName));
										procedure.ProcedureNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(procedure.ProcedureName));
										procedure.ProcedureNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(procedure.ProcedureName));
										procedure.ProcedureNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(procedure.ProcedureName));
										procedure.ProcedureNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(procedure.ProcedureName));
										procedure.ProcedureNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(procedure.ProcedureName));

										schema.Procedures.Add(procedure);

										var dataReaderParameter = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "Parameters"), this.CoreGetParameterParameters(unitOfWorkContext, dataSourceTag, database, schema, procedure), out recordsAffected);
										{
											if ((object)dataReaderParameter != null)
											{
												foreach (var drParameter in dataReaderParameter)
												{
													Parameter parameter;

													parameter = new Parameter();

													parameter.ParameterPrefix = DataType.ChangeType<string>(drParameter["ParameterName"]).Substring(0, 1);
													parameter.ParameterName = DataType.ChangeType<string>(drParameter["ParameterName"]).Substring(1);
													parameter.ParameterOrdinal = DataType.ChangeType<int>(drParameter["ParameterOrdinal"]);
													parameter.ParameterSize = DataType.ChangeType<int>(drParameter["ParameterSize"]);
													parameter.ParameterPrecision = DataType.ChangeType<int>(drParameter["ParameterPrecision"]);
													parameter.ParameterScale = DataType.ChangeType<int>(drParameter["ParameterScale"]);
													parameter.ParameterSqlType = DataType.ChangeType<string>(drParameter["ParameterSqlType"]);
													parameter.ParameterIsOutput = DataType.ChangeType<bool>(drParameter["ParameterIsOutput"]);
													parameter.ParameterIsReadOnly = DataType.ChangeType<bool>(drParameter["ParameterIsReadOnly"]);
													parameter.ParameterIsCursorRef = DataType.ChangeType<bool>(drParameter["ParameterIsCursorRef"]);
													parameter.ParameterIsReturnValue = DataType.ChangeType<bool>(drParameter["ParameterIsReturnValue"]);
													parameter.ParameterDefaultValue = DataType.ChangeType<string>(drParameter["ParameterDefaultValue"]);
													parameter.ParameterIsResultColumn = DataType.ChangeType<bool>(drParameter["ParameterIsResultColumn"]);
													parameter.ParameterNamePascalCase = Name.GetPascalCase(parameter.ParameterName);
													parameter.ParameterNameCamelCase = Name.GetCamelCase(parameter.ParameterName);
													parameter.ParameterNameConstantCase = Name.GetConstantCase(parameter.ParameterName);
													parameter.ParameterNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(parameter.ParameterName));
													parameter.ParameterNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(parameter.ParameterName));
													parameter.ParameterNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(parameter.ParameterName));
													parameter.ParameterNullable = true;
													parameter.ParameterDirection = (parameter.ParameterIsOutput || parameter.ParameterIsReadOnly) ? ParameterDirection.Output : ParameterDirection.Input;

													clrType = this.CoreInferClrTypeForSqlType(dataSourceTag, parameter.ParameterSqlType);
													parameter.ParameterDbType = InferDbTypeForClrType(clrType);
													parameter.ParameterSize = this.CoreCalculateParameterSize(dataSourceTag, parameter);

													parameter.ParameterClrType = clrType;
													parameter.ParameterClrNullableType = Reflexion.MakeNullableType(clrType);
													parameter.ParameterClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
													parameter.ParameterCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, parameter.ParameterDbType);
													parameter.ParameterCSharpClrType = (object)parameter.ParameterClrType != null ? FormatCSharpType(parameter.ParameterClrType) : FormatCSharpType(typeof(object));
													parameter.ParameterCSharpClrNullableType = (object)parameter.ParameterClrNullableType != null ? FormatCSharpType(parameter.ParameterClrNullableType) : FormatCSharpType(typeof(object));
													parameter.ParameterCSharpClrNonNullableType = (object)parameter.ParameterClrNonNullableType != null ? FormatCSharpType(parameter.ParameterClrNonNullableType) : FormatCSharpType(typeof(object));
													parameter.ParameterCSharpNullableLiteral = parameter.ParameterNullable.ToString().ToLower();

													procedure.Parameters.Add(parameter);
												}
											}

											// implicit return value parameter
											if (this.CoreGetEmitImplicitReturnParameter(dataSourceTag))
											{
												Parameter parameter;

												parameter = new Parameter();

												parameter.ParameterPrefix = this.CoreGetParameterPrefix(dataSourceTag);
												parameter.ParameterName = RETURN_VALUE;
												parameter.ParameterOrdinal = int.MaxValue;
												parameter.ParameterSize = 0;
												parameter.ParameterPrecision = 0;
												parameter.ParameterScale = 0;
												parameter.ParameterSqlType = "int";
												parameter.ParameterIsOutput = true;
												parameter.ParameterIsReadOnly = true;
												parameter.ParameterIsCursorRef = false;
												parameter.ParameterIsReturnValue = true;
												parameter.ParameterDefaultValue = null;
												parameter.ParameterIsResultColumn = false;
												parameter.ParameterNamePascalCase = Name.GetPascalCase(RETURN_VALUE);
												parameter.ParameterNameCamelCase = Name.GetCamelCase(RETURN_VALUE);
												parameter.ParameterNameConstantCase = Name.GetConstantCase(RETURN_VALUE);
												parameter.ParameterNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(RETURN_VALUE));
												parameter.ParameterNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(RETURN_VALUE));
												parameter.ParameterNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(RETURN_VALUE));
												parameter.ParameterNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(RETURN_VALUE));
												parameter.ParameterNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(RETURN_VALUE));
												parameter.ParameterNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(RETURN_VALUE));
												parameter.ParameterNullable = true;
												parameter.ParameterDirection = ParameterDirection.ReturnValue;

												clrType = this.CoreInferClrTypeForSqlType(dataSourceTag, parameter.ParameterSqlType);
												parameter.ParameterDbType = InferDbTypeForClrType(clrType);
												parameter.ParameterSize = this.CoreCalculateParameterSize(dataSourceTag, parameter);

												parameter.ParameterClrType = clrType;
												parameter.ParameterClrNullableType = Reflexion.MakeNullableType(clrType);
												parameter.ParameterClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
												parameter.ParameterCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, parameter.ParameterDbType);
												parameter.ParameterCSharpClrType = (object)parameter.ParameterClrType != null ? FormatCSharpType(parameter.ParameterClrType) : FormatCSharpType(typeof(object));
												parameter.ParameterCSharpClrNullableType = (object)parameter.ParameterClrNullableType != null ? FormatCSharpType(parameter.ParameterClrNullableType) : FormatCSharpType(typeof(object));
												parameter.ParameterCSharpClrNonNullableType = (object)parameter.ParameterClrNonNullableType != null ? FormatCSharpType(parameter.ParameterClrNonNullableType) : FormatCSharpType(typeof(object));
												parameter.ParameterCSharpNullableLiteral = parameter.ParameterNullable.ToString().ToLower();

												procedure.Parameters.Add(parameter);
											}
										}

										// re-map result column parameters into first class columns
										Parameter[] columnParameters;
										columnParameters = procedure.Parameters.Where(p => p.ParameterIsResultColumn).ToArray();

										if ((object)columnParameters != null && columnParameters.Length > 0)
										{
											foreach (Parameter columnParameter in columnParameters)
											{
												Column column;

												column = new Column();

												column.ColumnName = columnParameter.ParameterName;
												column.ColumnOrdinal = columnParameter.ParameterOrdinal;
												column.ColumnNullable = columnParameter.ParameterNullable;
												column.ColumnSize = columnParameter.ParameterSize;
												column.ColumnPrecision = columnParameter.ParameterPrecision;
												column.ColumnScale = columnParameter.ParameterScale;
												column.ColumnSqlType = columnParameter.ParameterSqlType;
												column.ColumnIsIdentity = false;
												column.ColumnIsComputed = false;
												column.ColumnHasDefault = !DataType.IsNullOrWhiteSpace(columnParameter.ParameterDefaultValue);
												column.ColumnHasCheck = false;
												column.ColumnIsPrimaryKey = false;
												column.ColumnNamePascalCase = Name.GetPascalCase(columnParameter.ParameterName);
												column.ColumnNameCamelCase = Name.GetCamelCase(columnParameter.ParameterName);
												column.ColumnNameConstantCase = Name.GetConstantCase(columnParameter.ParameterName);
												column.ColumnNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(columnParameter.ParameterName));
												column.ColumnNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(columnParameter.ParameterName));
												column.ColumnNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(columnParameter.ParameterName));
												column.ColumnNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(columnParameter.ParameterName));
												column.ColumnNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(columnParameter.ParameterName));
												column.ColumnNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(columnParameter.ParameterName));

												clrType = this.CoreInferClrTypeForSqlType(dataSourceTag, columnParameter.ParameterSqlType);
												column.ColumnDbType = InferDbTypeForClrType(clrType);
												column.ColumnSize = this.CoreCalculateColumnSize(dataSourceTag, column); //recalculate

												column.ColumnClrType = clrType ?? typeof(object);
												column.ColumnClrNullableType = Reflexion.MakeNullableType(clrType);
												column.ColumnClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
												column.ColumnCSharpNullableLiteral = column.ColumnNullable.ToString().ToLower();
												column.ColumnCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, column.ColumnDbType);
												column.ColumnCSharpClrType = (object)column.ColumnClrType != null ? FormatCSharpType(column.ColumnClrType) : FormatCSharpType(typeof(object));
												column.ColumnCSharpClrNullableType = (object)column.ColumnClrNullableType != null ? FormatCSharpType(column.ColumnClrNullableType) : FormatCSharpType(typeof(object));
												column.ColumnCSharpClrNonNullableType = (object)column.ColumnClrNonNullableType != null ? FormatCSharpType(column.ColumnClrNonNullableType) : FormatCSharpType(typeof(object));

												//procedure.Columns.Add(column);
												procedure.Parameters.Remove(columnParameter);
											}
										}

										// REFERENCE:
										// http://connect.microsoft.com/VisualStudio/feedback/details/314650/sqm1014-sqlmetal-ignores-stored-procedures-that-use-temp-tables
										IDataParameter[] parameters;
										parameters = procedure.Parameters.Where(p => !p.ParameterIsReturnValue && !p.ParameterIsResultColumn).Select(p => unitOfWorkContext.CreateParameter(p.ParameterIsOutput ? ParameterDirection.Output : ParameterDirection.Input, p.ParameterDbType, p.ParameterSize, (byte)p.ParameterPrecision, (byte)p.ParameterScale, p.ParameterNullable, p.ParameterName, null)).ToArray();

										try
										{
											var dataReaderMetadata = AdoNetHelper.ExecuteSchema(unitOfWorkContext, CommandType.StoredProcedure, string.Format(GetAllAssemblyResourceFileText(this.GetType(), dataSourceTag, "ProcedureSchema"), schema.SchemaName, procedure.ProcedureName), parameters);
											{
												if ((object)dataReaderMetadata != null)
												{
													foreach (var drMetadata in dataReaderMetadata)
													{
														Column column;

														column = new Column();

														column.ColumnName = DataType.ChangeType<string>(drMetadata["ColumnName"]);
														column.ColumnOrdinal = DataType.ChangeType<int>(drMetadata["ColumnOrdinal"]);
														column.ColumnNullable = DataType.ChangeType<bool>(drMetadata["AllowDBNull"]);
														column.ColumnSize = DataType.ChangeType<int>(drMetadata["ColumnSize"]);
														column.ColumnPrecision = DataType.ChangeType<int>(drMetadata["NumericPrecision"]);
														column.ColumnScale = DataType.ChangeType<int>(drMetadata["NumericScale"]);
														// TODO FIX
														//column.ColumnSqlType = DataType.ChangeType<string>(drMetadata["DataTypeName"]);
														//column.ColumnIsIdentity = DataType.ChangeType<bool>(drMetadata["IsIdentity"]);
														//column.ColumnIsComputed = DataType.ChangeType<bool>(drMetadata["IsReadOnly"]);
														//column.ColumnHasDefault = DataType.ChangeType<bool>(drMetadata["ColumnHasDefault"]);
														//column.ColumnHasCheck = DataType.ChangeType<bool>(drMetadata["ColumnHasCheck"]);
														//column.ColumnIsPrimaryKey = DataType.ChangeType<bool>(drMetadata["IsKey"]);
														column.ColumnNamePascalCase = Name.GetPascalCase(column.ColumnName);
														column.ColumnNameCamelCase = Name.GetCamelCase(column.ColumnName);
														column.ColumnNameConstantCase = Name.GetConstantCase(column.ColumnName);
														column.ColumnNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(column.ColumnName));
														column.ColumnNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(column.ColumnName));
														column.ColumnNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(column.ColumnName));

														clrType = this.CoreInferClrTypeForSqlType(dataSourceTag, column.ColumnSqlType);
														column.ColumnDbType = InferDbTypeForClrType(clrType);
														column.ColumnSize = this.CoreCalculateColumnSize(dataSourceTag, column); //recalculate

														column.ColumnClrType = clrType ?? typeof(object);
														column.ColumnClrNullableType = Reflexion.MakeNullableType(clrType);
														column.ColumnClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
														column.ColumnCSharpNullableLiteral = column.ColumnNullable.ToString().ToLower();
														column.ColumnCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, column.ColumnDbType);
														column.ColumnCSharpClrType = (object)column.ColumnClrType != null ? FormatCSharpType(column.ColumnClrType) : FormatCSharpType(typeof(object));
														column.ColumnCSharpClrNullableType = (object)column.ColumnClrNullableType != null ? FormatCSharpType(column.ColumnClrNullableType) : FormatCSharpType(typeof(object));
														column.ColumnCSharpClrNonNullableType = (object)column.ColumnClrNonNullableType != null ? FormatCSharpType(column.ColumnClrNonNullableType) : FormatCSharpType(typeof(object));

														procedure.Columns.Add(column);
													}
												}
											}
										}
										catch (Exception ex)
										{
											Console.Error.WriteLine(Reflexion.GetErrors(ex, 0));
										}
									}
								}
							}
						}
					}
				}
			}

			return database;
		}

		#endregion
	}
}