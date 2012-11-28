/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using TextMetal.Common.Core;

namespace TextMetal.Common.Data
{
	public static class ExtensionMethods
	{
		#region Methods/Operators

		public static TResponse ExecuteReReRe<TRequest, TResult, TResponse>(this IUnitOfWorkContext unitOfWorkContext, TRequest request, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, bool executeAsCud, int thisOrThatRecordsAffected, Action<IDictionary<string, object>, TResult> mapToCallback)
			where TRequest : class, new()
			where TResult : class, new()
			where TResponse : class, new()
		{
			TResponse response;
			TResult result;
			int recordsAffected;
			IList<IDictionary<string, object>> results;
			IList<TResult> list = null;
			object value;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)request == null)
				throw new ArgumentNullException("request");

			if ((object)commandText == null)
				throw new ArgumentNullException("commandText");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)mapToCallback == null)
				throw new ArgumentNullException("mapToCallback");

			if (DataType.IsNullOrWhiteSpace(commandText))
				throw new ArgumentOutOfRangeException("commandText");

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if (executeAsCud && recordsAffected <= thisOrThatRecordsAffected)
			{
				// concurrency failure
				unitOfWorkContext.Divergent();
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}
			else if (!executeAsCud && recordsAffected != thisOrThatRecordsAffected)
			{
				// idempotency failure
				unitOfWorkContext.Divergent();
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}

			if ((object)results == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			response = new TResponse();

			if (Reflexion.GetLogicalPropertyValue(response, "Results", out value))
				list = value as IList<TResult>;

			if ((object)list != null)
				list.Clear();

			foreach (IDictionary<string, object> _result in results)
			{
				result = new TResult();

				mapToCallback(_result, result);

				if ((object)list != null)
					list.Add(result);
			}

			return response;
		}

		public static TModel FetchModel<TModel>(this IUnitOfWorkContext unitOfWorkContext, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, int queryExpectedRecordsAffected, Action<IDictionary<string, object>, TModel> mapToCallback)
			where TModel : class, new()
		{
			TModel model;
			int recordsAffected;
			IList<IDictionary<string, object>> results;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandText == null)
				throw new ArgumentNullException("commandText");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)mapToCallback == null)
				throw new ArgumentNullException("mapToCallback");

			if (DataType.IsNullOrWhiteSpace(commandText))
				throw new ArgumentOutOfRangeException("commandText");

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if (recordsAffected != queryExpectedRecordsAffected)
			{
				// idempotency failure
				unitOfWorkContext.Divergent();
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}

			if ((object)results == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (results.Count != 1)
				return null;

			model = new TModel();

			mapToCallback(results[0], model);

			return model;
		}

		/// <summary>
		/// 	Allows for easy scalar query execution over a UnitOfWorkContext.
		/// </summary>
		/// <typeparam name="TValue"> The scalar type. </typeparam>
		/// <param name="unitOfWorkContext"> The target UnitOfWorkContext. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <returns> The scalar value (with type conversion) or null. </returns>
		public static TValue FetchScalar<TValue>(this IUnitOfWorkContext unitOfWorkContext, CommandType commandType, string commandText, IEnumerable<IDataParameter> commandParameters)
		{
			int recordsAffected;
			IList<IDictionary<string, object>> results;
			object dbValue;

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if ((object)results == null || results.Count != 1 || results[0].Count != 1)
				return default(TValue);

			dbValue = results[0][results[0].Keys.First()];

			return dbValue.ChangeType<TValue>();
		}

		public static void FillModel<TModel>(this IUnitOfWorkContext unitOfWorkContext, TModel model, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, int queryExpectedRecordsAffected, Action<IDictionary<string, object>, TModel> mapToCallback)
			where TModel : class, new()
		{
			int recordsAffected;
			IList<IDictionary<string, object>> results;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)model == null)
				throw new ArgumentNullException("model");

			if ((object)commandText == null)
				throw new ArgumentNullException("commandText");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)mapToCallback == null)
				throw new ArgumentNullException("mapToCallback");

			if (DataType.IsNullOrWhiteSpace(commandText))
				throw new ArgumentOutOfRangeException("commandText");

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if (recordsAffected != queryExpectedRecordsAffected)
			{
				// idempotency failure
				unitOfWorkContext.Divergent();
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}

			if ((object)results == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (results.Count != 1)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			mapToCallback(results[0], model);
		}

		public static bool PersistModel<TModel>(this IUnitOfWorkContext unitOfWorkContext, TModel model, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, int persistNotExpectedRecordsAffected, Action<IDictionary<string, object>, TModel> mapToCallback)
			where TModel : class, new()
		{
			int recordsAffected;
			IList<IDictionary<string, object>> results;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)model == null)
				throw new ArgumentNullException("model");

			if ((object)commandText == null)
				throw new ArgumentNullException("commandText");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)mapToCallback == null)
				throw new ArgumentNullException("mapToCallback");

			if (DataType.IsNullOrWhiteSpace(commandText))
				throw new ArgumentOutOfRangeException("commandText");

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if (recordsAffected <= persistNotExpectedRecordsAffected)
			{
				// concurrency failure
				unitOfWorkContext.Divergent();
				return false;
			}

			if ((object)results == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (results.Count == 1)
				mapToCallback(results[0], model);

			return true;
		}

		public static IList<TModel> QueryModel<TModel>(this IUnitOfWorkContext unitOfWorkContext, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, int queryExpectedRecordsAffected, Action<IDictionary<string, object>, TModel> mapToCallback)
			where TModel : class, new()
		{
			IList<TModel> models;
			TModel model;
			int recordsAffected;
			IList<IDictionary<string, object>> results;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandText == null)
				throw new ArgumentNullException("commandText");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)mapToCallback == null)
				throw new ArgumentNullException("mapToCallback");

			if (DataType.IsNullOrWhiteSpace(commandText))
				throw new ArgumentOutOfRangeException("commandText");

			results = unitOfWorkContext.ExecuteDictionary(commandType, commandText, commandParameters, out recordsAffected);

			if (recordsAffected != queryExpectedRecordsAffected)
			{
				// idempotency failure
				unitOfWorkContext.Divergent();
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}

			if ((object)results == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			models = new List<TModel>();

			foreach (IDictionary<string, object> result in results)
			{
				model = new TModel();
				mapToCallback(result, model);
				models.Add(model);
			}

			return models;
		}

		#endregion
	}
}