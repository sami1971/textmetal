/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

using NMock22;

using NUnit.Framework;

using SwIsHw.Data.Transactions;

namespace SwIsHw.Data.UnitTests.Transactions
{
	[TestFixture]
	public class DataSourceTransactionTests
	{
		#region Constructors/Destructors

		public DataSourceTransactionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldAbortTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCommitTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;
			IDataSourceTransactionContext mockDataSourceTransactionContext;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataSourceTransactionContext = mockery.NewMock<IDataSourceTransactionContext>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDataSourceTransactionContext).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, mockDataSourceTransactionContext);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNotNull(transaction.Context);

			transaction.Commit();
			transaction.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCreateTest()
		{
			DataSourceTransaction transaction;

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);
			Assert.AreEqual(IsolationLevel.Unspecified, transaction.IsolationLevel);

			transaction.Dispose();

			Assert.IsTrue(transaction.Adjudicated);
			Assert.IsTrue(transaction.Disposed);

			transaction = new DataSourceTransaction(IsolationLevel.Serializable);

			Assert.IsNotNull(transaction);
			Assert.AreEqual(IsolationLevel.Serializable, transaction.IsolationLevel);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnAlreadyAdjudicatedBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Commit();
			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnAlreadyAdjudicatedTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Rollback").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Rollback();
			transaction.Commit();
			transaction.Dispose();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnAlreadyBoundBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnAlreadyContextSetBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			IDataSourceTransactionContext mockDataSourceTransactionContext;

			mockery = new Mockery();
			mockDataSourceTransactionContext = mockery.NewMock<IDataSourceTransactionContext>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Context = mockDataSourceTransactionContext;

			Assert.IsNotNull(transaction.Context);

			transaction.Context = null; // ok

			transaction.Context = mockDataSourceTransactionContext;

			mockDataSourceTransactionContext = mockery.NewMock<IDataSourceTransactionContext>();
			transaction.Context = mockDataSourceTransactionContext; // not ok
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnAlreadyDisposedAdjudicateTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Rollback").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Dispose();
			transaction.Rollback();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnAlreadyDisposedBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Dispose();
			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = null;
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionStringBindTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = null;
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);
		}

		[Test]
		public void ShouldNotFailOnDoubleDisposeTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Dispose();
			transaction.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldRollbackTest()
		{
			Mockery mockery;
			DataSourceTransaction transaction;
			string mockConnectionString;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			mockery = new Mockery();
			mockConnectionString = "db=local";
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Rollback").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsNotNull(transaction);

			Assert.IsNull(transaction.ConnectionString);
			Assert.IsNull(transaction.Connection);
			Assert.IsNull(transaction.Transaction);
			Assert.IsFalse(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Bind(mockConnectionString, mockDbConnection, mockDbTransaction, null);

			Assert.IsNotNull(transaction.ConnectionString);
			Assert.IsNotNull(transaction.Connection);
			Assert.IsNotNull(transaction.Transaction);
			Assert.IsTrue(transaction.Bound);
			Assert.IsNull(transaction.Context);

			transaction.Rollback();
			transaction.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldSetAndGetAmbientTransactionTest()
		{
			DataSourceTransaction transaction;

			Assert.IsNull(DataSourceTransaction.Current);

			transaction = new DataSourceTransaction();
			DataSourceTransaction.FrameTransaction(transaction);

			Assert.IsNotNull(DataSourceTransaction.Current);
		}

		[SetUp]
		public void TestSetUp()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		[TearDown]
		public void TestTearDown()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		#endregion
	}
}

/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;
using System.Transactions;

using NMock22;

using NUnit.Framework;

using SwIsHw.Data.AdoNet;
using SwIsHw.Data.Transactions;
using SwIsHw.Data.UnitTests.TestingInfrastructure;

using IsolationLevel = System.Data.IsolationLevel;

namespace SwIsHw.Data.UnitTests.AdoNet
{
	[TestFixture]
	public class AdoNetDataSourceTests
	{
		#region Constructors/Destructors

		public AdoNetDataSourceTests()
		{
		}

		#endregion

		#region Fields/Constants

		private const string MOCK_CONNECTION_STRING = "myConnectionString";

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateParameterTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;

			IDataParameter p;
			IDbDataParameter mockDbParameter;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDbParameter = mockery.NewMock<IDbDataParameter>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockDbConnection).Method("CreateCommand").Will(Return.Value(mockDbCommand));
			Expect.Once.On(mockDbConnection).Method("Dispose");
			Expect.Once.On(mockDbCommand).Method("CreateParameter").Will(Return.Value(mockDbParameter));
			Expect.Once.On(mockDbCommand).Method("Dispose");

			Expect.Once.On(mockDbParameter).SetProperty("ParameterName").To("@bob");
			Expect.Once.On(mockDbParameter).SetProperty("Size").To(1);
			Expect.Once.On(mockDbParameter).SetProperty("Value").To("test");
			Expect.Once.On(mockDbParameter).SetProperty("Direction").To(ParameterDirection.Input);
			Expect.Once.On(mockDbParameter).SetProperty("DbType").To(DbType.String);
			Expect.Once.On(mockDbParameter).SetProperty("Precision").To((byte)2);
			Expect.Once.On(mockDbParameter).SetProperty("Scale").To((byte)3);
			//Expect.Once.On(mockDbParameter).SetProperty("IsNullable").To(true);

			Expect.Once.On(mockDbParameter).GetProperty("ParameterName").Will(Return.Value("@bob"));
			Expect.Once.On(mockDbParameter).GetProperty("Size").Will(Return.Value(1));
			Expect.Once.On(mockDbParameter).GetProperty("Value").Will(Return.Value("test"));
			Expect.Once.On(mockDbParameter).GetProperty("Direction").Will(Return.Value(ParameterDirection.Input));
			Expect.Once.On(mockDbParameter).GetProperty("DbType").Will(Return.Value(DbType.String));
			Expect.Once.On(mockDbParameter).GetProperty("Precision").Will(Return.Value((byte)2));
			Expect.Once.On(mockDbParameter).GetProperty("Scale").Will(Return.Value((byte)3));
			//Expect.Once.On(mockDbParameter).GetProperty("IsNullable").Will(Return.Value(true));

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			p = dataSource.CreateParameter(null, ParameterDirection.Input, DbType.String, 1, 2, 3, true, "@bob", "test");

			Assert.IsNotNull(p);

			Assert.AreEqual(ParameterDirection.Input, p.Direction);
			Assert.AreEqual("@bob", p.ParameterName);
			Assert.AreEqual("test", p.Value);
			Assert.AreEqual(1, ((IDbDataParameter)p).Size);
			Assert.AreEqual(DbType.String, p.DbType);
			Assert.AreEqual((byte)2, ((IDbDataParameter)p).Precision);
			Assert.AreEqual((byte)3, ((IDbDataParameter)p).Scale);
			//Assert.AreEqual(true, ((IDbDataParameter)p).IsNullable);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCreateTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteNonQuerySprocWithParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteNonQueryTextNoParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.Text);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandTimeout").To(15);
			Expect.AtLeastOnce.On(mockDbCommand).Method("Prepare").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			recordsAffected = dataSource.ExecuteNonQuery(CommandType.Text, "blah blah blah", null, 15, true);

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteReaderCloseConnectionSprocWithParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDataReader mockDataReader;

			IDataReader dataReader;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };
			mockDataReader = mockery.NewMock<IDataReader>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			//Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteReader").With(CommandBehavior.CloseConnection).Will(Return.Value(mockDataReader));
			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataReader).Method("Dispose").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataReader = dataSource.ExecuteReader(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, CommandBehavior.CloseConnection, null, false);

			Assert.IsNotNull(dataReader);

			dataReader.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteReaderCloseConnectionTextNoParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataReader mockDataReader;

			IDataReader dataReader;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataReader = mockery.NewMock<IDataReader>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			//Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.Text);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteReader").With(CommandBehavior.CloseConnection).Will(Return.Value(mockDataReader));
			Expect.AtLeastOnce.On(mockDataReader).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandTimeout").To(15);
			Expect.AtLeastOnce.On(mockDbCommand).Method("Prepare").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataReader = dataSource.ExecuteReader(CommandType.Text, "blah blah blah", null, CommandBehavior.CloseConnection, 15, true);

			Assert.IsNotNull(dataReader);

			dataReader.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteReaderNoCloseConnectionSprocWithParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDataReader mockDataReader;

			IDataReader dataReader;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };
			mockDataReader = mockery.NewMock<IDataReader>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteReader").With(CommandBehavior.SingleRow).Will(Return.Value(mockDataReader));
			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataReader).Method("Dispose").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataReader = dataSource.ExecuteReader(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, CommandBehavior.SingleRow, null, false);

			Assert.IsNotNull(dataReader);

			dataReader.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteReaderNoCloseConnectionTextNoParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataReader mockDataReader;

			IDataReader dataReader;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataReader = mockery.NewMock<IDataReader>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.Text);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteReader").With(CommandBehavior.SingleRow).Will(Return.Value(mockDataReader));
			Expect.AtLeastOnce.On(mockDataReader).Method("Dispose").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataReader = dataSource.ExecuteReader(CommandType.Text, "blah blah blah", null, CommandBehavior.SingleRow, null, false);

			Assert.IsNotNull(dataReader);

			dataReader.Dispose();

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteScalarSprocWithParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;

			object returnValue;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteScalar").WithNoArguments().Will(Return.Value(1));

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			returnValue = dataSource.ExecuteScalar(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);

			Assert.AreEqual(1, returnValue);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteScalarTextNoParametersTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;

			object returnValue;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.Text);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteScalar").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandTimeout").To(15);
			Expect.AtLeastOnce.On(mockDbCommand).Method("Prepare").WithNoArguments();

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			returnValue = dataSource.ExecuteScalar(CommandType.Text, "blah blah blah", null, 15, true);

			Assert.AreEqual(1, returnValue);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteUnderAmbientDataSourceTransactionAbortTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDbTransaction mockDbTransaction;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").With(IsolationLevel.Unspecified).Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			using (DataSourceTransaction transaction = new DataSourceTransaction())
			{
				Assert.IsNull(DataSourceTransaction.Current);
				DataSourceTransaction.FrameTransaction(transaction);
				Assert.IsNotNull(DataSourceTransaction.Current);

				dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

				recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);
			}

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteUnderAmbientDataSourceTransactionCompleteTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDbTransaction mockDbTransaction;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").With(IsolationLevel.Unspecified).Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			using (DataSourceTransactionScope dsts = new DataSourceTransactionScope())
			{
				dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

				recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);

				dsts.Complete();
			}

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteUnderAmbientDataSourceTransactionNotCompleteTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDbTransaction mockDbTransaction;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.AtLeastOnce.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").With(IsolationLevel.Unspecified).Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Rollback").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			using (DataSourceTransactionScope dsts = new DataSourceTransactionScope())
			{
				dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

				recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);

				recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);
			}

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExecuteUnderAmbientDistributedTransactionAndDataSourceTransactionCompleteTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			using (TransactionScope ts = new TransactionScope())
			{
				using (DataSourceTransactionScope dsts = new DataSourceTransactionScope())
				{
					dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

					recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);

					dsts.Complete();
					ts.Complete();
				}
			}

			Assert.AreEqual(1, recordsAffected);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnConnectionStringMismatchUnderAmbientDataSourceTransactionTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDbTransaction mockDbTransaction;
			DataSourceTransaction transaction;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").WithNoArguments().Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			transaction = new DataSourceTransaction();

			Assert.IsFalse(transaction.Bound);

			Assert.IsNull(DataSourceTransaction.Current);
			DataSourceTransaction.FrameTransaction(transaction);
			Assert.IsNotNull(DataSourceTransaction.Current);

			transaction.Bind("xxx", mockDbConnection, mockDbTransaction, null);

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnConnectionTypeMismatchUnderAmbientDataSourceTransactionTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbCommand mockDbCommand;
			IDataParameterCollection mockDataParameterCollection;
			IDataParameter[] mockDataParameters;
			IDataParameter mockDataParameter0;
			IDataParameter mockDataParameter1;
			IDbTransaction mockDbTransaction;
			DataSourceTransaction transaction;

			int recordsAffected;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbCommand = mockery.NewMock<IDbCommand>();
			mockDataParameterCollection = mockery.NewMock<IDataParameterCollection>();
			mockDataParameter0 = mockery.NewMock<IDataParameter>();
			mockDataParameter1 = mockery.NewMock<IDataParameter>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();
			mockDataParameters = new IDataParameter[] { mockDataParameter0, mockDataParameter1 };

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("CreateCommand").WithNoArguments().Will(Return.Value(mockDbCommand));
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").WithNoArguments().Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbCommand).GetProperty("Parameters").Will(Return.Value(mockDataParameterCollection));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Clear").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Connection").To(mockDbConnection);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandType").To(CommandType.StoredProcedure);
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("CommandText").To("blah blah blah");
			Expect.AtLeastOnce.On(mockDbCommand).SetProperty("Transaction");
			Expect.AtLeastOnce.On(mockDbCommand).Method("ExecuteNonQuery").WithNoArguments().Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			Expect.AtLeastOnce.On(mockDataParameter0).GetProperty("Value").Will(Return.Value(1));
			Expect.AtLeastOnce.On(mockDataParameter1).GetProperty("Value").Will(Return.Value(null));
			Expect.AtLeastOnce.On(mockDataParameter1).SetProperty("Value").To(DBNull.Value);
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter0).Will(Return.Value(0));
			Expect.AtLeastOnce.On(mockDataParameterCollection).Method("Add").With(mockDataParameter1).Will(Return.Value(0));

			transaction = new DataSourceTransaction();

			Assert.IsFalse(transaction.Bound);

			Assert.IsNull(DataSourceTransaction.Current);
			DataSourceTransaction.FrameTransaction(transaction);
			Assert.IsNotNull(DataSourceTransaction.Current);

			transaction.Bind(MOCK_CONNECTION_STRING, new MockConnection(), mockDbTransaction, null);

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			recordsAffected = dataSource.ExecuteNonQuery(CommandType.StoredProcedure, "blah blah blah", mockDataParameters, null, false);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionFactoryCreateTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = null;

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnNullConnectionFromFactoryGetOpenConnectionTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(null));

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataSource.ExecuteNonQuery(CommandType.Text, "blah blah blah", null, null, false);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnNullConnectionGetConnectionFromFactoryTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(null));

			dataSource = new AdoNetDataSource(MOCK_CONNECTION_STRING, mockConnectionFactory);

			dataSource.CreateParameter(null, ParameterDirection.Input, DbType.String, 1, 2, 3, true, "@bob", "test");
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionStringCreateTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			dataSource = new AdoNetDataSource(null, mockConnectionFactory);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionStringGetForTest()
		{
			AdoNetDataSource dataSource;

			dataSource = AdoNetDataSource.GetFor<MockConnection>(null);
		}

		[Test]
		public void ShouldGetForTest()
		{
			Mockery mockery;
			AdoNetDataSource dataSource;

			mockery = new Mockery();

			dataSource = AdoNetDataSource.GetFor<MockConnection>(MOCK_CONNECTION_STRING);

			Assert.IsNotNull(dataSource);
		}

		[SetUp]
		public void TestSetUp()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		[TearDown]
		public void TestTearDown()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		#endregion
	}
}

/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;
using System.Transactions;

using NMock22;

using NUnit.Framework;

using SwIsHw.Data.AdoNet;
using SwIsHw.Data.Transactions;
using SwIsHw.Data.UnitTests.TestingInfrastructure;

using IsolationLevel = System.Data.IsolationLevel;

namespace SwIsHw.Data.UnitTests.AdoNet
{
	[TestFixture]
	public class AdoNetAmbientAwareTests
	{
		#region Constructors/Destructors

		public AdoNetAmbientAwareTests()
		{
		}

		#endregion

		#region Fields/Constants

		private const string MOCK_CONNECTION_STRING = "myConnectionString";

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			Assert.AreEqual(MOCK_CONNECTION_STRING, ambientAware.BypassConnectionString);
			Assert.AreEqual(mockConnectionFactory, ambientAware.BypassConnectionFactory);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnConnectionStringMismatchUnderAmbientDataSourceTransactionTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;
			DataSourceTransaction transaction;

			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").WithNoArguments().Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsFalse(transaction.Bound);

			Assert.IsNull(DataSourceTransaction.Current);
			DataSourceTransaction.FrameTransaction(transaction);
			Assert.IsNotNull(DataSourceTransaction.Current);

			transaction.Bind("xxx", mockDbConnection, mockDbTransaction, null);

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnConnectionTypeMismatchUnderAmbientDataSourceTransactionTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;
			DataSourceTransaction transaction;

			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").WithNoArguments().Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Commit").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			transaction = new DataSourceTransaction();

			Assert.IsFalse(transaction.Bound);

			Assert.IsNull(DataSourceTransaction.Current);
			DataSourceTransaction.FrameTransaction(transaction);
			Assert.IsNotNull(DataSourceTransaction.Current);

			transaction.Bind(MOCK_CONNECTION_STRING, new MockConnection(), mockDbTransaction, null);

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionFactoryCreateTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = null;

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnNullConnectionFromFactoryGetOpenConnectionTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(null));

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnNullConnectionGetConnectionFromFactoryTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(null));

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnection(false);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConnectionStringCreateTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();

			ambientAware = new MockAdoNetAmbientAware(null, mockConnectionFactory);
		}

		[Test]
		public void ShouldGetConnectionAndTransactionNotAmbientTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;

			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);

			Assert.IsNotNull(dbConnection);
			Assert.IsNull(dbTransaction);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldGetConnectionAndTransactionUnderAmbientDataSourceTransactionTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;
			IDbTransaction mockDbTransaction;

			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();
			mockDbTransaction = mockery.NewMock<IDbTransaction>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.AtLeastOnce.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).Method("BeginTransaction").With(IsolationLevel.Unspecified).Will(Return.Value(mockDbTransaction));
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Rollback").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbTransaction).Method("Dispose").WithNoArguments();

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			Assert.True(ambientAware.IsShouldDisposeResources);

			using (DataSourceTransactionScope dsts = new DataSourceTransactionScope())
			{
				ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);

				Assert.IsFalse(ambientAware.IsShouldDisposeResources);

				Assert.IsNotNull(dbConnection);
				Assert.IsNotNull(dbTransaction);
			}
			Assert.True(ambientAware.IsShouldDisposeResources);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldGetConnectionAndTransactionUnderAmbientDistributedTransactionAndDataSourceTransactionTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;

			IDbConnection dbConnection;
			IDbTransaction dbTransaction;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.Once.On(mockConnectionFactory).GetProperty("ConnectionType").Will(Return.Value(mockDbConnection.GetType()));
			//Expect.AtLeastOnce.On(mockDbConnection).GetProperty("State").Will(Return.Value(ConnectionState.Open));
			Expect.AtLeastOnce.On(mockDbConnection).Method("Dispose").WithNoArguments();
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			Assert.True(ambientAware.IsShouldDisposeResources);
			using (TransactionScope ts = new TransactionScope())
			{
				Assert.IsTrue(ambientAware.IsShouldDisposeResources);
				using (DataSourceTransactionScope dsts = new DataSourceTransactionScope())
				{
					Assert.IsFalse(ambientAware.IsShouldDisposeResources);
					ambientAware.BypassGetConnectionAndTransaction(out dbConnection, out dbTransaction);

					Assert.IsNotNull(dbConnection);
					Assert.IsNull(dbTransaction);
				}
				Assert.True(ambientAware.IsShouldDisposeResources);
			}
			Assert.True(ambientAware.IsShouldDisposeResources);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldGetConnectionNoOpenTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnection(false);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldGetConnectionWithOpenTest()
		{
			Mockery mockery;
			MockAdoNetAmbientAware ambientAware;
			IConnectionFactory mockConnectionFactory;
			IDbConnection mockDbConnection;

			mockery = new Mockery();
			mockConnectionFactory = mockery.NewMock<IConnectionFactory>();
			mockDbConnection = mockery.NewMock<IDbConnection>();

			Expect.Once.On(mockConnectionFactory).Method("GetConnection").Will(Return.Value(mockDbConnection));
			Expect.AtLeastOnce.On(mockDbConnection).SetProperty("ConnectionString").To("myConnectionString");
			Expect.AtLeastOnce.On(mockDbConnection).Method("Open").WithNoArguments();

			ambientAware = new MockAdoNetAmbientAware(MOCK_CONNECTION_STRING, mockConnectionFactory);

			ambientAware.BypassGetConnection(true);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[SetUp]
		public void TestSetUp()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		[TearDown]
		public void TestTearDown()
		{
			while (!DataSourceTransaction.UnwindTransaction())
				; // do nothing
		}

		#endregion
	}
}