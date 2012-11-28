/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Data;

namespace TextMetal.Common.Data.Advanced
{
	public interface IDataSourceTagSpecific
	{
		#region Properties/Indexers/Events

		string DataSourceTag
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void CommandMagic(IUnitOfWorkContext unitOfWorkContext, bool executeAsCud, out int thisOrThatRecordsAffected);

		string GetAliasedColumnName(string tableAlias, string columnName);

		string GetColumnName(string columnName);

		string GetIdentityCommand();

		string GetParameterName(string parameterName);

		string GetTableAlias(string tableAlias);

		string GetTableName(string schemaName, string tableName);

		void ParameterMagic(IUnitOfWorkContext unitOfWorkContext, IDataParameter commandParameter, string generatedFromColumnNativeType);

		#endregion
	}
}