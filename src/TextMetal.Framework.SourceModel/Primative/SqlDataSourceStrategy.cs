/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Data;
using TextMetal.Framework.AssociativeModel;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.SourceModel.Primative
{
	public class SqlDataSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SqlDataSourceStrategy class.
		/// </summary>
		public SqlDataSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static void WriteSqlQuery(IEnumerable<SqlQuery> sqlQueries, IAssociativeXmlObject parentAssociativeXmlObject, Type connectionType, string connectionString, bool getSchemaOnly)
		{
			ArrayConstruct arrayConstruct;
			ObjectConstruct objectConstruct;
			PropertyConstruct propertyConstruct;
			Tokenizer tokenizer;

			IList<IDictionary<string, object>> objs;
			string commandText;
			int rcecordsAffected;

			if ((object)sqlQueries == null)
				throw new ArgumentNullException("sqlQueries");

			if ((object)parentAssociativeXmlObject == null)
				throw new ArgumentNullException("parentAssociativeXmlObject");

			if ((object)connectionType == null)
				throw new ArgumentNullException("connectionType");

			if ((object)connectionString == null)
				throw new ArgumentNullException("connectionString");

			if (DataType.IsWhiteSpace(connectionString))
				throw new ArgumentOutOfRangeException("connectionString");

			tokenizer = new Tokenizer(true);

			foreach (SqlQuery sqlQuery in sqlQueries.OrderBy(c => c.Key).ThenBy(c => c.Order))
			{
				arrayConstruct = new ArrayConstruct();
				arrayConstruct.Name = sqlQuery.Key;
				parentAssociativeXmlObject.Items.Add(arrayConstruct);

				commandText = tokenizer.ExpandTokens(sqlQuery.Text, new DynamicWildcardTokenReplacementStrategy(new object[] { parentAssociativeXmlObject }));

				using (IUnitOfWorkContext unitOfWorkContext = UnitOfWorkContext.Create(connectionType, connectionString, false))
				{
					if (!getSchemaOnly)
						objs = unitOfWorkContext.ExecuteDictionary(sqlQuery.Type, commandText, null, out rcecordsAffected);
					else
						objs = unitOfWorkContext.ExecuteSchema(sqlQuery.Type, commandText, null);
				}

				if ((object)objs != null)
				{
					propertyConstruct = new PropertyConstruct();
					propertyConstruct.Name = "RowCount";
					propertyConstruct.Value = objs.Count.ToString();
					arrayConstruct.Items.Add(propertyConstruct);

					foreach (IDictionary<string, object> obj in objs)
					{
						objectConstruct = new ObjectConstruct();
						arrayConstruct.Items.Add(objectConstruct);

						if ((object)obj != null)
						{
							foreach (KeyValuePair<string, object> keyValuePair in obj)
							{
								propertyConstruct = new PropertyConstruct();
								propertyConstruct.Name = keyValuePair.Key;
								propertyConstruct.Value = keyValuePair.Value.SafeToString();

								if ((object)keyValuePair.Value != null)
									propertyConstruct.Type = keyValuePair.Value.GetType().FullName;

								objectConstruct.Items.Add(propertyConstruct);
							}
						}

						// correlated
						WriteSqlQuery(sqlQuery.SubQueries, objectConstruct, connectionType, connectionString, getSchemaOnly);
					}
				}
			}
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			const string CMDLN_TOKEN_CONNECTION_AQTN = "ConnectionType";
			const string CMDLN_TOKEN_CONNECTION_STRING = "ConnectionString";
			const string CMDLN_TOKEN_GET_SCHEMA_ONLY = "GetSchemaOnly";
			string connectionAqtn;
			Type connectionType = null;
			string connectionString = null;
			bool getSchemaOnly = false;
			IList<string> values;

			ModelConstruct modelConstruct;
			SqlQuery sqlQuery;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			connectionAqtn = null;
			if (properties.TryGetValue(CMDLN_TOKEN_CONNECTION_AQTN, out values))
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

			if (properties.TryGetValue(CMDLN_TOKEN_CONNECTION_STRING, out values))
			{
				if ((object)values != null && values.Count == 1)
					connectionString = values[0];
			}

			if (DataType.IsWhiteSpace(connectionString))
				throw new InvalidOperationException(string.Format("The connection string cannot be null or whitespace."));

			if (properties.TryGetValue(CMDLN_TOKEN_GET_SCHEMA_ONLY, out values))
			{
				if ((object)values != null && values.Count == 1)
					DataType.TryParse<bool>(values[0], out getSchemaOnly);
			}

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			sqlQuery = Cerealization.GetObjectFromFile<SqlQuery>(sourceFilePath);

			modelConstruct = new ModelConstruct();

			WriteSqlQuery(new SqlQuery[] { sqlQuery }, modelConstruct, connectionType, connectionString, getSchemaOnly);

			return modelConstruct;
		}

		#endregion
	}
}