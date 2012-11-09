﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by:
// TextMetal 4.4.5.39286;
// 		Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
//		Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
//		Project URL: https://github.com/dpbullington/textmetal
//
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------

/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

using TextMetal.Plumbing.CommonFacilities;

namespace TextMetal.WebHostSample.Objects.Model
{
	public partial class Repository : IRepository
	{		
		#region Fields/Constants
		
		private const int SQLITE_PERSIST_NOT_EXPECTED_RECORDS_AFFECTED = 0;
		private const int SQLITE_QUERY_EXPECTED_RECORDS_AFFECTED = 0;
		private const string SQLITE_IDENTITY_COMMAND = "LAST_INSERT_ROWID()";

		private const string SQLITE_COLUMN_ALIASED_FORMAT = "{0}.{1}";
		private const string SQLITE_COLUMN_NAME_FORMAT = "{0}";
		private const string SQLITE_PARAMETER_NAME_FORMAT = "@{0}";
		private const string SQLITE_SCHEMA_TABLE_NAME_FORMAT = "{1}";
		private const string SQLITE_TABLE_ALIAS_FORMAT = "{0}";
		private const string SQLITE_TABLE_NAME_FORMAT = "{0}";

		#endregion

		#region Methods/Operators
		
		private static string SqliteSpecificGetIdentityCommand()
		{
			string retVal;

			retVal = SQLITE_IDENTITY_COMMAND;

			return retVal;
		}

		private static string SqliteSpecificGetTableName(string schemaName, string tableName)
		{
			string retVal;

			retVal = !DataType.IsNullOrWhiteSpace(schemaName) ?
				string.Format(SQLITE_SCHEMA_TABLE_NAME_FORMAT, schemaName, tableName) :
				string.Format(SQLITE_TABLE_NAME_FORMAT, tableName);

			return retVal;
		}

		private static string SqliteSpecificGetTableAlias(string tableAlias)
		{
			string retVal;

			retVal = string.Format(SQLITE_TABLE_ALIAS_FORMAT, tableAlias);

			return retVal;
		}

		private static string SqliteSpecificGetParameterName(string parameterName)
		{
			string retVal;

			retVal = string.Format(SQLITE_PARAMETER_NAME_FORMAT, parameterName);

			return retVal;
		}

		private static string SqliteSpecificGetColumnName(string columnName)
		{
			string retVal;

			retVal = string.Format(SQLITE_COLUMN_NAME_FORMAT, columnName);

			return retVal;
		}

		private static string SqliteSpecificGetAliasedColumnName(string tableAlias, string columnName)
		{
			string retVal;

			retVal = string.Format(SQLITE_COLUMN_ALIASED_FORMAT, SqliteSpecificGetTableAlias(tableAlias), columnName);

			return retVal;
		}

		private static void SqliteSpecificParameterMagic(UnitOfWorkContext unitOfWorkContext, IDataParameter commandParameter, string generatedFromColumnNativeType)
		{
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandParameter == null)
				throw new ArgumentNullException("commandParameter");
		}

		#endregion
	}
}
