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
using System.Collections.Generic;
using System.Linq;
using System.Data;

using TextMetal.Plumbing.CommonFacilities;

using TextMetal.WebHostSample.Objects.Model.Procedures;

namespace TextMetal.WebHostSample.Objects.Model
{
	public partial class Repository
	{		
		#region Methods/Operators
		
		protected TResponse ExecuteReReRe<TRequest, TResult, TResponse>(UnitOfWorkContext unitOfWorkContext, TRequest request, CommandType commandType, string commandText, IList<IDataParameter> commandParameters, bool executeAsCud, int thisOrThatRecordsAffected, Action<IDictionary<string, object>, TResult> mapToCallback)
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

			OnProfileCommand(typeof(TResult), commandType, commandText, commandParameters, executeAsCud, thisOrThatRecordsAffected);
			
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
		
		public BlahBlahBlah_Response Execute_BlahBlahBlah(BlahBlahBlah_Request requestBlahBlahBlah)
		{
			BlahBlahBlah_Response retval;
			
			if ((object)UnitOfWorkContext.Current == null)
			{
				using (UnitOfWorkContext unitOfWorkContext = Repository.GetUnitOfWorkContext())
				{
					retval = this.Execute_BlahBlahBlah(unitOfWorkContext, requestBlahBlahBlah);
						
					unitOfWorkContext.Complete();
					
					return retval;
				}
			}			
			else
			{
				return this.Execute_BlahBlahBlah(UnitOfWorkContext.Current, requestBlahBlahBlah);
			}
		}
						
		public BlahBlahBlah_Response Execute_BlahBlahBlah(UnitOfWorkContext unitOfWorkContext, BlahBlahBlah_Request requestBlahBlahBlah)
		{	
			BlahBlahBlah_Response responseBlahBlahBlah;						
			IDataParameter commandParameter;
			List<IDataParameter> commandParameters;			
			string commandText;
			object value;
			bool executeAsCud;
			int thisOrThatRecordsAffected;
			
			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");
				
			if ((object)requestBlahBlahBlah == null)
				throw new ArgumentNullException("requestBlahBlahBlah");

			this.OnPreExecuteBlahBlahBlah(unitOfWorkContext, requestBlahBlahBlah);
			
			commandParameters = new List<IDataParameter>();

			commandParameter = unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.Int32, 4, 10, 0, true, ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_I), requestBlahBlahBlah.I);
			commandParameters.Add(commandParameter);
			ConnectionSpecificParameterMagic(unitOfWorkContext, commandParameter, "int");

			commandParameter = unitOfWorkContext.CreateParameter(ParameterDirection.Input, DbType.Binary, 0, 0, 0, true, ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_O), requestBlahBlahBlah.O);
			commandParameters.Add(commandParameter);
			ConnectionSpecificParameterMagic(unitOfWorkContext, commandParameter, "image");

			commandParameter = unitOfWorkContext.CreateParameter(ParameterDirection.Output, DbType.String, 100, 0, 0, true, ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_J), requestBlahBlahBlah.J);
			commandParameters.Add(commandParameter);
			ConnectionSpecificParameterMagic(unitOfWorkContext, commandParameter, "varchar");

			commandParameter = unitOfWorkContext.CreateParameter(ParameterDirection.ReturnValue, DbType.Int32, 0, 0, 0, true, ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_RETURN_VALUE), null);
			commandParameters.Add(commandParameter);
			ConnectionSpecificParameterMagic(unitOfWorkContext, commandParameter, "int");

			executeAsCud = false;

			commandText = ConnectionSpecificGetTableName(BlahBlahBlah_Request.SCHEMA_NAME_DBO, BlahBlahBlah_Request.PROCEDURE_NAME_BLAHBLAHBLAHS);
			ConnectionSpecificCommandMagic(unitOfWorkContext, executeAsCud, out thisOrThatRecordsAffected);

			responseBlahBlahBlah = this.ExecuteReReRe<BlahBlahBlah_Request, BlahBlahBlah_Result, BlahBlahBlah_Response>(unitOfWorkContext, requestBlahBlahBlah, CommandType.StoredProcedure, commandText, commandParameters, executeAsCud, thisOrThatRecordsAffected, MapToBlahBlahBlah_Result);	
			
			foreach (BlahBlahBlah_Result resultBlahBlahBlah2 in responseBlahBlahBlah.Results)
				this.OnExecuteResultBlahBlahBlah(unitOfWorkContext, resultBlahBlahBlah2);
			
			commandParameter = commandParameters.SingleOrDefault(p => p.ParameterName == ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_J));
			
			if ((object)commandParameter == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message"); 
				
			responseBlahBlahBlah.J = commandParameter.Value.ChangeType<String>();

			commandParameter = commandParameters.SingleOrDefault(p => p.ParameterName == ConnectionSpecificGetParameterName(BlahBlahBlah_Request.PARAMETER_NAME_RETURN_VALUE));
			
			if ((object)commandParameter == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message"); 
				
			responseBlahBlahBlah.ReturnValue = commandParameter.Value.ChangeType<Int32>();				
			
			this.OnPostExecuteBlahBlahBlah(unitOfWorkContext, responseBlahBlahBlah);
			
			return responseBlahBlahBlah;
		}
		
		private static void MapToBlahBlahBlah_Result(IDictionary<string, object> result, BlahBlahBlah_Result resultBlahBlahBlah)
		{
			object value;
			
			if ((object)result == null)
				throw new ArgumentNullException("result");
				
			if ((object)resultBlahBlahBlah == null)
				throw new ArgumentNullException("resultBlahBlahBlah");
			
			if (result.TryGetValue("A", out value))
				resultBlahBlahBlah.A = value.ChangeType<Nullable<Int32>>();

			if (result.TryGetValue("B", out value))
				resultBlahBlahBlah.B = value.ChangeType<Nullable<Int32>>();

			if (result.TryGetValue("Iii", out value))
				resultBlahBlahBlah.Iii = value.ChangeType<Nullable<Int32>>();

			if (result.TryGetValue("Ooo", out value))
				resultBlahBlahBlah.Ooo = value.ChangeType<Byte[]>();
		}
		
		partial void OnPreExecuteBlahBlahBlah(UnitOfWorkContext unitOfWorkContext, BlahBlahBlah_Request requestBlahBlahBlah);
		
		partial void OnExecuteResultBlahBlahBlah(UnitOfWorkContext unitOfWorkContext, BlahBlahBlah_Result resultBlahBlahBlah);
		
		partial void OnPostExecuteBlahBlahBlah(UnitOfWorkContext unitOfWorkContext, BlahBlahBlah_Response responseBlahBlahBlah);

		#endregion
	}
}
