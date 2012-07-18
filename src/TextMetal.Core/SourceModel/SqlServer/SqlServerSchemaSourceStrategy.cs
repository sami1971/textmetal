/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.SourceModel.SqlServer
{
	public class SqlServerSchemaSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		public SqlServerSchemaSourceStrategy()
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

		private static string GetAllAssemblyResourceFileText(string name)
		{
			string resourcePath;
			Type targetType;
			string sqlText = null;

			if ((object)name == null)
				throw new ArgumentNullException("name");

			if (DataType.IsWhiteSpace(name))
				throw new ArgumentOutOfRangeException("name");

			targetType = typeof(SqlServerSchemaSourceStrategy);
			resourcePath = string.Format("{0}.DML.{1}.sql", targetType.Namespace, name);

			if (!Cerealization.TryGetStringFromAssemblyResource(targetType, resourcePath, out sqlText))
				throw new InvalidOperationException(string.Format("Failed to obtain assembly manifest (embedded) resource '{0}'.", resourcePath));

			return sqlText;
		}

		private static Type InferClrTypeForSqlType(string sqlType)
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
			else if (clrType == typeof(UInt16))
				return DbType.UInt16;
			else if (clrType == typeof(UInt32))
				return DbType.UInt32;
			else if (clrType == typeof(UInt64))
				return DbType.UInt64;
			else if (clrType == typeof(Byte[]))
				return DbType.Binary;
			else if (clrType == typeof(String))
				return DbType.String;
			else if (clrType == typeof(Object))
				return DbType.Object;
			else
				throw new InvalidOperationException(string.Format("Cannot infer parameter type from unsupported CLR type '{0}'.", clrType.FullName));
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			return this.GetSchemaModel(sourceFilePath);
		}

		private object GetSchemaModel(string connectionString)
		{
			Type connectionType = typeof(SqlConnection);
			Database database;
			int recordsAffected;

			using (UnitOfWorkContext unitOfWorkContext = UnitOfWorkContext.Create(connectionType, connectionString, false))
			{
				database = new Database();
				database.ConnectionString = connectionString;
				database.ConnectionType = connectionType.FullName;

				var dataReaderDatabase = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Database"), null, out recordsAffected);
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

				var dataReaderSchema = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Schemas"), null, out recordsAffected);
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

							database.Schemas.Add(schema);

							var dataReaderTable = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Tables"), new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName) }, out recordsAffected);
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

									var dataReaderColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Columns"), new IDataParameter[]
									                                                                                                                        {
									                                                                                                                        	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
									                                                                                                                        	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@TableOrViewName", table.TableName)
									                                                                                                                        }, out recordsAffected);
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
												column.ColumnSize = DataType.ChangeType<short>(drColumn["ColumnSize"]);
												column.ColumnPrecision = DataType.ChangeType<byte>(drColumn["ColumnPrecision"]);
												column.ColumnScale = DataType.ChangeType<byte>(drColumn["ColumnScale"]);
												column.ColumnSqlType = DataType.ChangeType<string>(drColumn["ColumnSqlType"]);
												column.ColumnIsIdentity = DataType.ChangeType<bool>(drColumn["ColumnIsIdentity"]);
												column.ColumnIsComputed = DataType.ChangeType<bool>(drColumn["ColumnIsComputed"]);
												column.ColumnHasDefault = DataType.ChangeType<bool>(drColumn["ColumnHasDefault"]);
												column.ColumnHasCheck = DataType.ChangeType<bool>(drColumn["ColumnHasCheck"]);
												column.ColumnIsPrimaryKey = DataType.ChangeType<bool>(drColumn["ColumnIsPrimaryKey"]);
												column.ColumnNamePascalCase = Name.GetPascalCase(column.ColumnName);
												column.ColumnNameCamelCase = Name.GetCamelCase(column.ColumnName);
												column.ColumnNameConstantCase = Name.GetConstantCase(column.ColumnName);
												column.ColumnNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(column.ColumnName));
												column.ColumnNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(column.ColumnName));
												column.ColumnNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(column.ColumnName));
												column.ColumnNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(column.ColumnName));

												table.Columns.Add(column);
											}
										}
									}

									var dataReaderMetadata = AdoNetHelper.ExecuteSchema(unitOfWorkContext, CommandType.Text, string.Format("SELECT * FROM [{0}].[{1}] WHERE -1 = 1", schema.SchemaName, table.TableName), null);
									{
										if ((object)dataReaderMetadata != null)
										{
											foreach (var drMetadata in dataReaderMetadata)
											{
												Column column;
												string columnName;
												Type clrType;

												columnName = DataType.ChangeType<string>(drMetadata["ColumnName"]);
												clrType = DataType.ChangeType<Type>(drMetadata["DataType"]);

												column = table.Columns.SingleOrDefault(x => x.ColumnName == columnName);

												if ((object)column == null)
													throw new InvalidOperationException("TODO (enhancement): add meaningful message");

												column.ColumnClrType = clrType;
												column.ColumnClrNullableType = Reflexion.MakeNullableType(clrType);
												column.ColumnClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
												column.ColumnDbType = InferDbTypeForClrType(clrType);

												column.ColumnSize =
													column.ColumnSqlType == "image" ||
													column.ColumnSqlType == "text" ||
													column.ColumnSqlType == "ntext" ? (short)0 :
													                                           	(column.ColumnDbType == DbType.String &&
													                                           	 column.ColumnSqlType.SafeToString().StartsWith("n") &&
													                                           	 column.ColumnSize != 0 ?
													                                           	                        	(short)(column.ColumnSize / 2) :
													                                           	                        	                               	column.ColumnSize);

												column.ColumnCSharpNullableLiteral = column.ColumnNullable.ToString().ToLower();
												column.ColumnCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, column.ColumnDbType);
												column.ColumnCSharpClrType = (object)column.ColumnClrType != null ? FormatCSharpType(column.ColumnClrType) : null;
												column.ColumnCSharpClrNullableType = (object)column.ColumnClrNullableType != null ? FormatCSharpType(column.ColumnClrNullableType) : null;
												column.ColumnCSharpClrNonNullableType = (object)column.ColumnClrNonNullableType != null ? FormatCSharpType(column.ColumnClrNonNullableType) : null;
											}
										}
									}

									if (table.Columns.Count(c => c.ColumnIsPrimaryKey) < 1)
										table.Columns.ForEach(c => c.ColumnIsPrimaryKey = true);

									var dataReaderForeignKey = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("ForeignKeys"), new IDataParameter[]
									                                                                                                                                {
									                                                                                                                                	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
									                                                                                                                                	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@TableName", table.TableName)
									                                                                                                                                }, out recordsAffected);
									{
										if ((object)dataReaderForeignKey != null)
										{
											foreach (var drForeignKey in dataReaderForeignKey)
											{
												ForeignKey foreignKey;

												foreignKey = new ForeignKey();

												foreignKey.ForeignKeyName = DataType.ChangeType<string>(drForeignKey["ForeignKeyName"]);
												foreignKey.ForeignKeyOnDeleteRefIntAction = DataType.ChangeType<byte>(drForeignKey["ForeignKeyOnDeleteRefIntAction"]);
												foreignKey.ForeignKeyOnDeleteRefIntActionSqlName = DataType.ChangeType<string>(drForeignKey["ForeignKeyOnDeleteRefIntActionSqlName"]);
												foreignKey.ForeignKeyOnUpdateRefIntAction = DataType.ChangeType<byte>(drForeignKey["ForeignKeyOnUpdateRefIntAction"]);
												foreignKey.ForeignKeyOnUpdateRefIntActionSqlName = DataType.ChangeType<string>(drForeignKey["ForeignKeyOnUpdateRefIntActionSqlName"]);
												foreignKey.ForeignKeyIsDisabled = DataType.ChangeType<bool>(drForeignKey["ForeignKeyIsDisabled"]);
												foreignKey.ForeignKeyIsForReplication = DataType.ChangeType<bool>(drForeignKey["ForeignKeyIsForReplication"]);
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

												var dataReaderForeignKeyColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("ForeignKeyColumns"), new IDataParameter[]
												                                                                                                                                            {
												                                                                                                                                            	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
												                                                                                                                                            	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@TableName", table.TableName),
												                                                                                                                                            	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@ForeignKeyName", foreignKey.ForeignKeyName)
												                                                                                                                                            }, out recordsAffected);
												{
													if ((object)dataReaderForeignKeyColumn != null)
													{
														foreach (var drForeignKeyColumn in dataReaderForeignKeyColumn)
														{
															ForeignKeyColumnRef foreignKeyColumnRef;

															foreignKeyColumnRef = new ForeignKeyColumnRef();

															foreignKeyColumnRef.ForeignKeyChildColumnOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["ForeignKeyChildColumnOrdinal"]);
															foreignKeyColumnRef.ForeignKeyChildTableName = DataType.ChangeType<string>(drForeignKeyColumn["ForeignKeyChildTableName"]);
															foreignKeyColumnRef.ForeignKeyColumnOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["ForeignKeyColumnOrdinal"]);
															foreignKeyColumnRef.ForeignKeyParentColumnOrdinal = DataType.ChangeType<int>(drForeignKeyColumn["ForeignKeyParentColumnOrdinal"]);

															foreignKey.ForeignKeyColumnRefs.Add(foreignKeyColumnRef);
														}
													}
												}
											}
										}
									}

									var dataReaderUniqueKey = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("UniqueKeys"), new IDataParameter[]
									                                                                                                                              {
									                                                                                                                              	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
									                                                                                                                              	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@TableName", table.TableName)
									                                                                                                                              }, out recordsAffected);
									{
										if ((object)dataReaderUniqueKey != null)
										{
											foreach (var drUniqueKey in dataReaderUniqueKey)
											{
												UniqueKey uniqueKey;

												uniqueKey = new UniqueKey();

												uniqueKey.UniqueKeyName = DataType.ChangeType<string>(drUniqueKey["UniqueKeyName"]);
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

												var dataReaderUniqueKeyColumn = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("UniqueKeyColumns"), new IDataParameter[]
												                                                                                                                                          {
												                                                                                                                                          	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
												                                                                                                                                          	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@TableName", table.TableName),
												                                                                                                                                          	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@UniqueKeyName", uniqueKey.UniqueKeyName)
												                                                                                                                                          }, out recordsAffected);
												{
													if ((object)dataReaderUniqueKeyColumn != null)
													{
														foreach (var drUniqueKeyColumn in dataReaderUniqueKeyColumn)
														{
															UniqueKeyColumnRef uniqueKeyColumnRef;

															uniqueKeyColumnRef = new UniqueKeyColumnRef();

															uniqueKeyColumnRef.UniqueKeyColumnDescendingSort = DataType.ChangeType<bool>(drUniqueKeyColumn["UniqueKeyColumnDescendingSort"]);
															uniqueKeyColumnRef.UniqueKeyColumnOrdinal = DataType.ChangeType<byte>(drUniqueKeyColumn["UniqueKeyColumnOrdinal"]);
															uniqueKeyColumnRef.UniqueKeyParentColumnOrdinal = DataType.ChangeType<int>(drUniqueKeyColumn["UniqueKeyParentColumnOrdinal"]);

															uniqueKey.UniqueKeyColumnRefs.Add(uniqueKeyColumnRef);
														}
													}
												}
											}
										}
									}
								}
							}

							var dataReaderProcedure = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Procedures"), new IDataParameter[] { unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName) }, out recordsAffected);
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

										var dataReaderParameter = unitOfWorkContext.ExecuteDictionary(CommandType.Text, GetAllAssemblyResourceFileText("Parameters"), new IDataParameter[]
										                                                                                                                              {
										                                                                                                                              	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@SchemaName", schema.SchemaName),
										                                                                                                                              	unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.String, 100, 0, 0, true, "@ProcedureName", procedure.ProcedureName)
										                                                                                                                              }, out recordsAffected);
										{
											if ((object)dataReaderParameter != null)
											{
												foreach (var drParameter in dataReaderParameter)
												{
													Parameter parameter;
													Type clrType;

													parameter = new Parameter();

													parameter.ParameterPrefix = DataType.ChangeType<string>(drParameter["ParameterName"]).Substring(0, 1);
													parameter.ParameterName = DataType.ChangeType<string>(drParameter["ParameterName"]).Substring(1);
													parameter.ParameterOrdinal = DataType.ChangeType<int>(drParameter["ParameterOrdinal"]);
													parameter.ParameterSize = DataType.ChangeType<short>(drParameter["ParameterSize"]);
													parameter.ParameterPrecision = DataType.ChangeType<byte>(drParameter["ParameterPrecision"]);
													parameter.ParameterScale = DataType.ChangeType<byte>(drParameter["ParameterScale"]);
													parameter.ParameterSqlType = DataType.ChangeType<string>(drParameter["ParameterSqlType"]);
													parameter.ParameterIsOutput = DataType.ChangeType<bool>(drParameter["ParameterIsOutput"]);
													parameter.ParameterIsReadOnly = DataType.ChangeType<bool>(drParameter["ParameterIsReadOnly"]);
													parameter.ParameterIsCursorRef = DataType.ChangeType<bool>(drParameter["ParameterIsCursorRef"]);
													parameter.ParameterNamePascalCase = Name.GetPascalCase(parameter.ParameterName);
													parameter.ParameterNameCamelCase = Name.GetCamelCase(parameter.ParameterName);
													parameter.ParameterNameConstantCase = Name.GetConstantCase(parameter.ParameterName);
													parameter.ParameterNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(parameter.ParameterName));
													parameter.ParameterNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(parameter.ParameterName));
													parameter.ParameterNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(parameter.ParameterName));
													parameter.ParameterNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(parameter.ParameterName));

													clrType = InferClrTypeForSqlType(parameter.ParameterSqlType);
													parameter.ParameterClrType = clrType;
													parameter.ParameterClrNullableType = Reflexion.MakeNullableType(clrType);
													parameter.ParameterClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
													parameter.ParameterDbType = InferDbTypeForClrType(clrType);
													parameter.ParameterNullable = true;

													parameter.ParameterSize =
														parameter.ParameterSqlType == "image" ||
														parameter.ParameterSqlType == "text" ||
														parameter.ParameterSqlType == "ntext" ? (short)0 :
														                                                 	(parameter.ParameterDbType == DbType.String &&
														                                                 	 parameter.ParameterSqlType.SafeToString().StartsWith("n") &&
														                                                 	 parameter.ParameterSize != 0 ?
														                                                 	                              	(short)(parameter.ParameterSize / 2) :
														                                                 	                              	                                     	parameter.ParameterSize);

													parameter.ParameterCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, parameter.ParameterDbType);
													parameter.ParameterCSharpClrType = (object)parameter.ParameterClrType != null ? FormatCSharpType(parameter.ParameterClrType) : null;
													parameter.ParameterCSharpClrNullableType = (object)parameter.ParameterClrNullableType != null ? FormatCSharpType(parameter.ParameterClrNullableType) : null;
													parameter.ParameterCSharpClrNonNullableType = (object)parameter.ParameterClrNonNullableType != null ? FormatCSharpType(parameter.ParameterClrNonNullableType) : null;
													parameter.ParameterCSharpNullableLiteral = parameter.ParameterNullable.ToString().ToLower();

													parameter.ParameterDirection = (parameter.ParameterIsOutput || parameter.ParameterIsReadOnly) ? ParameterDirection.Output : ParameterDirection.Input;

													procedure.Parameters.Add(parameter);
												}
											}

											// implicit return value parameter
											{
												Parameter parameter;
												Type clrType;

												parameter = new Parameter();

												parameter.ParameterPrefix = "@ReturnValue".Substring(0, 1);
												parameter.ParameterName = "@ReturnValue".Substring(1);
												parameter.ParameterOrdinal = int.MaxValue;
												parameter.ParameterSize = 0;
												parameter.ParameterPrecision = 0;
												parameter.ParameterScale = 0;
												parameter.ParameterSqlType = "int";
												parameter.ParameterIsOutput = true;
												parameter.ParameterIsReadOnly = true;
												parameter.ParameterIsCursorRef = false;
												parameter.ParameterNamePascalCase = Name.GetPascalCase("@ReturnValue".Substring(1));
												parameter.ParameterNameCamelCase = Name.GetCamelCase("@ReturnValue".Substring(1));
												parameter.ParameterNameConstantCase = Name.GetConstantCase("@ReturnValue".Substring(1));
												parameter.ParameterNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm("@ReturnValue".Substring(1)));
												parameter.ParameterNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm("@ReturnValue".Substring(1)));
												parameter.ParameterNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm("@ReturnValue".Substring(1)));
												parameter.ParameterNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm("@ReturnValue".Substring(1)));
												parameter.ParameterNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm("@ReturnValue".Substring(1)));
												parameter.ParameterNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm("@ReturnValue".Substring(1)));

												clrType = InferClrTypeForSqlType(parameter.ParameterSqlType);
												parameter.ParameterClrType = clrType;
												parameter.ParameterClrNullableType = Reflexion.MakeNullableType(clrType);
												parameter.ParameterClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
												parameter.ParameterDbType = InferDbTypeForClrType(clrType);
												parameter.ParameterNullable = true;

												parameter.ParameterSize =
													parameter.ParameterSqlType == "image" ||
													parameter.ParameterSqlType == "text" ||
													parameter.ParameterSqlType == "ntext" ? (short)0 :
													                                                 	(parameter.ParameterDbType == DbType.String &&
													                                                 	 parameter.ParameterSize != 0 ?
													                                                 	                              	(short)(parameter.ParameterSize / 2) :
													                                                 	                              	                                     	parameter.ParameterSize);

												parameter.ParameterCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, parameter.ParameterDbType);
												parameter.ParameterCSharpClrType = (object)parameter.ParameterClrType != null ? FormatCSharpType(parameter.ParameterClrType) : null;
												parameter.ParameterCSharpClrNullableType = (object)parameter.ParameterClrNullableType != null ? FormatCSharpType(parameter.ParameterClrNullableType) : null;
												parameter.ParameterCSharpClrNonNullableType = (object)parameter.ParameterClrNonNullableType != null ? FormatCSharpType(parameter.ParameterClrNonNullableType) : null;
												parameter.ParameterCSharpNullableLiteral = parameter.ParameterNullable.ToString().ToLower();

												parameter.ParameterDirection = ParameterDirection.ReturnValue;

												procedure.Parameters.Add(parameter);
											}
										}

										// REFERENCE:
										// http://connect.microsoft.com/VisualStudio/feedback/details/314650/sqm1014-sqlmetal-ignores-stored-procedures-that-use-temp-tables
										IDataParameter[] parameters;
										parameters = procedure.Parameters.Where(p => p.ParameterName != "@ReturnValue".Substring(1)).Select(p => unitOfWorkContext.CreateParameter(p.ParameterIsOutput ? ParameterDirection.Output : ParameterDirection.Input, p.ParameterDbType, p.ParameterSize, p.ParameterPrecision, p.ParameterScale, p.ParameterNullable, p.ParameterName, null)).ToArray();

										try
										{
											var dataReaderMetadata = AdoNetHelper.ExecuteSchema(unitOfWorkContext, CommandType.StoredProcedure, string.Format("[{0}].[{1}]", schema.SchemaName, procedure.ProcedureName), parameters);
											{
												if ((object)dataReaderMetadata != null)
												{
													foreach (var drMetadata in dataReaderMetadata)
													{
														Column column;
														Type clrType;

														column = new Column();

														column.ColumnName = DataType.ChangeType<string>(drMetadata["ColumnName"]);
														clrType = DataType.ChangeType<Type>(drMetadata["DataType"]);
														column.ColumnNamePascalCase = Name.GetPascalCase(column.ColumnName);
														column.ColumnNameCamelCase = Name.GetCamelCase(column.ColumnName);
														column.ColumnNameConstantCase = Name.GetConstantCase(column.ColumnName);
														column.ColumnNameSingularPascalCase = Name.GetPascalCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNameSingularCamelCase = Name.GetCamelCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNameSingularConstantCase = Name.GetConstantCase(Name.GetSingularForm(column.ColumnName));
														column.ColumnNamePluralPascalCase = Name.GetPascalCase(Name.GetPluralForm(column.ColumnName));
														column.ColumnNamePluralCamelCase = Name.GetCamelCase(Name.GetPluralForm(column.ColumnName));
														column.ColumnNamePluralConstantCase = Name.GetConstantCase(Name.GetPluralForm(column.ColumnName));

														column.ColumnClrType = clrType;
														column.ColumnClrNullableType = Reflexion.MakeNullableType(clrType);
														column.ColumnClrNonNullableType = Reflexion.MakeNonNullableType(clrType);
														column.ColumnDbType = InferDbTypeForClrType(clrType);

														column.ColumnSize =
															(column.ColumnDbType == DbType.String &&
															 (column.ColumnSize != 0) ? (short)(column.ColumnSize / 2) : column.ColumnSize);

														column.ColumnCSharpNullableLiteral = column.ColumnNullable.ToString().ToLower();
														column.ColumnCSharpDbType = string.Format("{0}.{1}", typeof(DbType).Name, column.ColumnDbType);
														column.ColumnCSharpClrType = (object)column.ColumnClrType != null ? FormatCSharpType(column.ColumnClrType) : null;
														column.ColumnCSharpClrNullableType = (object)column.ColumnClrNullableType != null ? FormatCSharpType(column.ColumnClrNullableType) : null;
														column.ColumnCSharpClrNonNullableType = (object)column.ColumnClrNonNullableType != null ? FormatCSharpType(column.ColumnClrNonNullableType) : null;

														procedure.Columns.Add(column);
													}
												}
											}
										}
										catch (SqlException sex)
										{
											Console.Error.WriteLine(sex.Message); // ;)
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