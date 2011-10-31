/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace TextMetal.Core.Plumbing.LinqToSql
{
	public static class LinqToSqlHelper
	{
		#region Methods/Operators

		public static ContextWrapper<TContext> GetContext<TContext>(this UnitOfWorkContext unitOfWorkContext)
			where TContext : class, IDisposable
		{
			Type contextType, dataContextType;
			TContext dataContext;
			ContextWrapper<TContext> contextWrapper;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			// assumption: LINQ to SQL DataContext derived context types are only supported
			// will support Entity Framework *Context types later, if possible.
			dataContextType = typeof(DataContext);
			contextType = typeof(TContext);

			if (!dataContextType.IsAssignableFrom(contextType))
				throw new NotSupportedException(string.Format("The (data) context type '{0}' is not supported.", contextType.FullName));

			dataContext = (TContext)(object)GetDataContext(unitOfWorkContext, contextType);
			contextWrapper = new ContextWrapper<TContext>(dataContext);

			return contextWrapper;
		}

		private static DataContext GetDataContext(UnitOfWorkContext unitOfWorkContext, Type dataContextType)
		{
			DataContext dataContext;
			MulticastContext<DataContext> multicastContext;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)dataContextType == null)
				throw new ArgumentNullException("dataContextType");

			if ((object)unitOfWorkContext.Context != null)
			{
				multicastContext = unitOfWorkContext.Context as MulticastContext<DataContext>;

				// will fail if not correct type (e.g. DataContext, ObjectContext, etc.)
				if ((object)multicastContext == null)
					throw new InvalidOperationException("Multicast context type obtained from the current data source transaction context does not match the current multicast context type.");

				if (!multicastContext.HasContext(dataContextType))
				{
					// create DC and add to existing MCC
					dataContext = GetDataContext(dataContextType, unitOfWorkContext.Connection, unitOfWorkContext.Transaction);
					multicastContext.SetContext(dataContextType, dataContext);
				}
				else
				{
					// grab existing DC from existing MCC
					dataContext = multicastContext.GetContext(dataContextType);
				}
			}
			else
			{
				// create DC and add to new MCC
				multicastContext = new MulticastContext<DataContext>();
				dataContext = GetDataContext(dataContextType, unitOfWorkContext.Connection, unitOfWorkContext.Transaction);
				multicastContext.SetContext(dataContextType, dataContext);
				unitOfWorkContext.Context = multicastContext;
			}

			return dataContext;
		}

		private static DataContext GetDataContext(Type dataContextType, IDbConnection dbConnection, IDbTransaction dbTransaction)
		{
			DataContext dataContext;
			MappingSource mappingSource;
			ConstructorInfo constructorInfo;

			if ((object)dataContextType == null)
				throw new ArgumentNullException("dataContextType");

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			mappingSource = new AttributeMappingSource();
			constructorInfo = dataContextType.GetConstructor(new Type[] { typeof(IDbConnection), typeof(MappingSource) });

			// assumption: reflection constructor contract/attribute-based mapping source
			dataContext = (DataContext)constructorInfo.Invoke(new object[] { dbConnection, mappingSource });

			if ((object)dbTransaction != null)
				dataContext.Transaction = (DbTransaction)dbTransaction;

			return dataContext;
		}

		#endregion
	}
}