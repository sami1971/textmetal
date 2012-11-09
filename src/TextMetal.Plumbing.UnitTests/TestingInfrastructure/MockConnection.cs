/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

using NMock2;

namespace TextMetal.Plumbing.UnitTests.TestingInfrastructure
{
	public class MockConnection : IDbConnection
	{
		#region Constructors/Destructors

		public MockConnection()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public string ConnectionString
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
			}
		}

		public int ConnectionTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Database
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ConnectionState State
		{
			get
			{
				return ConnectionState.Open;
			}
		}

		#endregion

		#region Methods/Operators

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			return this.BeginTransaction();
		}

		public IDbTransaction BeginTransaction()
		{
			Mockery mocks;
			IDbTransaction mockDbTransaction;

			mocks = new Mockery();
			mockDbTransaction = mocks.NewMock<IDbTransaction>();
			Stub.On(mockDbTransaction);
			return mockDbTransaction;
		}

		public void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public IDbCommand CreateCommand()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}

		public void Open()
		{
		}

		#endregion
	}
}