/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;

namespace TextMetal.Core.Plumbing
{
	/// <summary>
	/// Provides static helper and/or extension methods for ADO.NET.
	/// </summary>
	public static class AdoNetHelper
	{
		#region Methods/Operators

		/// <summary>
		/// Request a new data parameter from the data source.
		/// </summary>
		/// <param name="unitOfWorkContext">The target unit of work context.</param>
		/// <param name="direction">Specifies the parameter direction.</param>
		/// <param name="type">Specifies the parameter provider-(in)dependent type.</param>
		/// <param name="size">Specifies the parameter size.</param>
		/// <param name="precision">Specifies the parameter precision.</param>
		/// <param name="scale">Specifies the parameter scale.</param>		
		/// <param name="nullable">Specifies the parameter nullable-ness.</param>
		/// <param name="name">Specifies the parameter name.</param>
		/// <param name="value">Specifies the parameter value.</param>
		/// <returns>The data parameter with the specified properties set.</returns>
		public static IDataParameter CreateParameter(this UnitOfWorkContext unitOfWorkContext, ParameterDirection direction, DbType type, int size, byte precision, byte scale, bool nullable, string name, object value)
		{
			IDbDataParameter dbDataParameter;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)unitOfWorkContext.Connection == null)
				throw new InvalidOperationException("There is not a valid connection associated with the current unit of work context.");

			using (IDbCommand dbCommand = unitOfWorkContext.Connection.CreateCommand())
				dbDataParameter = dbCommand.CreateParameter();

			dbDataParameter.ParameterName = name;
			dbDataParameter.Size = size;
			dbDataParameter.Value = value;
			dbDataParameter.Direction = direction;
			dbDataParameter.DbType = type;
			Reflexion.SetLogicalPropertyValue(dbDataParameter, "IsNullable", nullable, true, false);
			dbDataParameter.Precision = precision;
			dbDataParameter.Scale = scale;

			return dbDataParameter;
		}

		public static IList<IDictionary<string, object>> ExecuteDictionary(this UnitOfWorkContext unitOfWorkContext, CommandType commandType, string commandText, IEnumerable<IDataParameter> commandParameters, out int recordsAffected)
		{
			IList<IDictionary<string, object>> objs;
			IDictionary<string, object> obj;
			IDataReader dataReader;
			CommandBehavior commandBehavior;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)unitOfWorkContext.Connection == null)
				throw new InvalidOperationException("There is not a valid connection associated with the current unit of work context.");

			objs = new List<IDictionary<string, object>>();

			try
			{
				commandBehavior = false ? CommandBehavior.CloseConnection : CommandBehavior.Default;

				using (dataReader = ExecuteReader(unitOfWorkContext.Connection, unitOfWorkContext.Transaction, commandType, commandText, commandParameters, commandBehavior, null, false))
				{
					while (dataReader.Read())
					{
						obj = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

						for (int index = 0; index < dataReader.FieldCount; index++)
						{
							string key;
							object value;

							key = dataReader.GetName(index);
							value = dataReader.GetValue(index);

							obj.Add(key, value);
						}

						objs.Add(obj);
					}
				}

				recordsAffected = dataReader.RecordsAffected;
			}
			finally
			{
				// DO NOT DISPOSE OF UNIT_OF_WORK_CONTEXT - UP TO THE CALLER
				unitOfWorkContext.SafeToString();
			}

			return objs;
		}

		/// <summary>
		/// Executes a reader query operation against the database.
		/// </summary>
		/// <param name="dbConnection">The database connection.</param>
		/// <param name="dbTransaction">An optional local database transaction.</param>
		/// <param name="commandType">The type of the command.</param>
		/// <param name="commandText">The SQL text or stored procedure name.</param>
		/// <param name="commandParameters">The parameters to use during the operation.</param>
		/// <param name="commandBehavior">The reader behavior.</param>
		/// <param name="commandTimeout">The command timeout (use null for default).</param>
		/// <param name="commandPrepare">Whether to prepare the command at the data source.</param>
		/// <returns>The data reader result.</returns>
		public static IDataReader ExecuteReader(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDataParameter> commandParameters, CommandBehavior commandBehavior, int? commandTimeout, bool commandPrepare)
		{
			IDbCommand dbCommand = null;
			IDataReader dataReader = null;

			try
			{
				if ((object)dbConnection == null)
					throw new ArgumentNullException("dbConnection");

				// create a command
				dbCommand = dbConnection.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = commandType;
				dbCommand.CommandText = commandText;
				dbCommand.Transaction = dbTransaction;

				if ((object)commandTimeout != null)
					dbCommand.CommandTimeout = (int)commandTimeout;

				// add parameters
				if ((object)commandParameters != null)
				{
					foreach (IDataParameter parameter in commandParameters)
					{
						if ((object)parameter.Value == null)
							parameter.Value = DBNull.Value;

						dbCommand.Parameters.Add(parameter);
					}
				}

				if (commandPrepare)
					dbCommand.Prepare();

				// do the database work		
				dataReader = dbCommand.ExecuteReader(commandBehavior);

				return dataReader;
			}
			finally
			{
				// cleanup command
				if ((object)dbCommand != null)
				{
					dbCommand.Parameters.Clear();
					dbCommand.Dispose();
					dbCommand = null;
				}
			}
		}

		public static IList<IDictionary<string, object>> ExecuteSchema(this UnitOfWorkContext unitOfWorkContext, CommandType commandType, string commandText, IEnumerable<IDataParameter> commandParameters)
		{
			IList<IDictionary<string, object>> objs;
			IDictionary<string, object> obj;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)unitOfWorkContext.Connection == null)
				throw new InvalidOperationException("There is not a valid connection associated with the current unit of work context.");

			objs = new List<IDictionary<string, object>>();

			try
			{
				// 2011-09-07 (dpbullington@gmail.com / issue #12): found quirk if CommandBehavior == KeyInfo, hidden columns in views get returned; reverting to CommandBehavior == SchemaOnly
				using (IDataReader dataReader = ExecuteReader(unitOfWorkContext.Connection, unitOfWorkContext.Transaction, commandType, commandText, commandParameters, CommandBehavior.SchemaOnly, null, false))
				{
					using (DataTable dataTable = dataReader.GetSchemaTable())
					{
						if ((object)dataTable != null)
						{
							//dataTable.WriteXml(@"out.xml");
							foreach (DataRow dataRow in dataTable.Rows)
							{
								obj = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

								for (int index = 0; index < dataTable.Columns.Count; index++)
								{
									string key;
									object value;

									key = dataTable.Columns[index].ColumnName;
									value = dataRow[index];

									obj.Add(key, value);
								}

								objs.Add(obj);
							}
						}
					}
				}
			}
			finally
			{
				// DO NOT DISPOSE OF UNIT_OF_WORK_CONTEXT - UP TO THE CALLER
				unitOfWorkContext.SafeToString();
			}

			return objs;
		}

		#endregion
	}
}