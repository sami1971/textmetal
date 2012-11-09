/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;

using NUnit.Framework;

namespace TextMetal.Plumbing.CommonFacilities.UnitTests.Plumbing.core
{
	[TestFixture]
	public static class ExecutionPathStorageTests
	{
		#region Classes/Structs/Interfaces/Enums/Delegates

		[TestFixture]
		public class CallContextExecutionPathStorageTests
		{
			#region Constructors/Destructors

			public CallContextExecutionPathStorageTests()
			{
			}

			#endregion

			#region Methods/Operators

			[Test]
			public void ShouldCreateAddGetRemoveCallContextExecutionPathStorageTest()
			{
				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 1);

				Assert.IsNotNull(ExecutionPathStorage.GetValue("x"));

				Assert.AreEqual(1, ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", null);

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.RemoveValue("x");

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));
			}

			[Test]
			public void ShouldThreadSafeRunCallContextExecutionPathStorageTest()
			{
				Thread t;

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 1);

				t = new Thread(this.TlsOtherThreadCallContext);
				t.Start();
				t.Join();

				Assert.IsNotNull(ExecutionPathStorage.GetValue("x"));
				Assert.AreEqual(1, ExecutionPathStorage.GetValue("x"));
			}

			[SetUp]
			public void TestSetUp()
			{
				HttpContext.Current = null;
			}

			[TearDown]
			public void TestTearDown()
			{
				HttpContext.Current = null;
			}

			private void TlsOtherThreadCallContext()
			{
				Thread.Sleep(1000);

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 2);
			}

			#endregion
		}

		[TestFixture]
		public class HttpContextExecutionPathStorageTests
		{
			#region Constructors/Destructors

			public HttpContextExecutionPathStorageTests()
			{
			}

			#endregion

			#region Methods/Operators

			[Test]
			public void ShouldCreateAddGetRemoveExecutionPathStorageTest()
			{
				Assert.IsTrue(ExecutionPathStorage.IsInHttpContext);

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 1);

				Assert.IsNotNull(ExecutionPathStorage.GetValue("x"));

				Assert.AreEqual(1, ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", null);

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.RemoveValue("x");

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));
			}

			[Test]
			public void ShouldThreadSafeRunExecutionPathStorageTest()
			{
				Thread t;

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 1);

				t = new Thread(this.TlsOtherThreadHttpContext);
				t.Start();
				t.Join();

				Assert.IsNotNull(ExecutionPathStorage.GetValue("x"));
				Assert.AreEqual(1, ExecutionPathStorage.GetValue("x"));
			}

			[SetUp]
			public void TestSetUp()
			{
				TextWriter tw;
				HttpWorkerRequest wr;

				// fake up HttpContext
				tw = new StringWriter();
				wr = new SimpleWorkerRequest("/webapp", "c:\\inetpub\\wwwroot\\webapp\\", "default.aspx", "", tw);

				HttpContext.Current = new HttpContext(wr);
			}

			[TearDown]
			public void TestTearDown()
			{
				HttpContext.Current = null;
			}

			private void TlsOtherThreadHttpContext()
			{
				TextWriter tw;
				HttpWorkerRequest wr;

				// fake up HttpContext
				tw = new StringWriter();
				wr = new SimpleWorkerRequest("/webapp", "c:\\inetpub\\wwwroot\\webapp\\", "default.aspx", "", tw);
				HttpContext.Current = new HttpContext(wr);

				Thread.Sleep(1000);

				Assert.IsNull(ExecutionPathStorage.GetValue("x"));

				ExecutionPathStorage.SetValue("x", 2);

				HttpContext.Current = null;
			}

			#endregion
		}

		#endregion
	}
}