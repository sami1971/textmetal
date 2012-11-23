/*
	Copyright �2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using NMock2;

using NUnit.Framework;

using TextMetal.Common.Solder.DependencyManagement;

[assembly: DependencyRegistration]

namespace TextMetal.Common.UnitTests.Solder.DependencyManagement
{
	[TestFixture]
	[DependencyRegistration]
	public class DependencyManagerTests
	{
		#region Constructors/Destructors

		public DependencyManagerTests()
		{
		}

		#endregion

		#region Methods/Operators

		public static void NoDependencyRegistration()
		{
		}

		[DependencyRegistration]
		public static void ShouldNotMatchDelegateSignature(int unused)
		{
			throw new Exception();
		}

		[DependencyRegistration]
		private static void ShouldNotMatchPrivateMethod()
		{
			throw new Exception();
		}

		[DependencyRegistration]
		public static void YesDependencyRegistration()
		{
			DependencyManager.AppDomainInstance.AddResolution<IFormattable>("bob", DelegateDependencyResolution.FromFunc<double>(() => (double)1234.56789));
		}

		[Test]
		public void ShouldAddResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCheckIfHasResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			IDisposable mockDisposable;
			string selectorKey;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();
			mockDisposable = mockery.NewMock<IDisposable>();

			dependencyManager = new DependencyManager();

			result = dependencyManager.HasTypeResolution<IDisposable>(null);
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("x");
			Assert.IsFalse(result);

			selectorKey = "x";
			dependencyManager.AddResolution<IDisposable>(selectorKey, mockDependencyResolution);

			result = dependencyManager.HasTypeResolution<IDisposable>(null);
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("x");
			Assert.IsTrue(result);

			dependencyManager.ClearAllResolutions();

			result = dependencyManager.HasTypeResolution<IDisposable>(null);
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("x");
			Assert.IsFalse(result);

			selectorKey = "";
			dependencyManager.AddResolution<IDisposable>(selectorKey, mockDependencyResolution);

			result = dependencyManager.HasTypeResolution<IDisposable>(null);
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("");
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution<IDisposable>("x");
			Assert.IsFalse(result);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCheckIfHasResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			IDisposable mockDisposable;
			Type targetType;
			string selectorKey;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();
			mockDisposable = mockery.NewMock<IDisposable>();

			targetType = typeof(IDisposable);
			dependencyManager = new DependencyManager();

			result = dependencyManager.HasTypeResolution(targetType, null);
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution(targetType, "");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution(targetType, "x");
			Assert.IsFalse(result);

			selectorKey = "x";
			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);

			result = dependencyManager.HasTypeResolution(targetType, null);
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution(targetType, "");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution(targetType, "x");
			Assert.IsTrue(result);

			dependencyManager.ClearAllResolutions();

			result = dependencyManager.HasTypeResolution(targetType, null);
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution(targetType, "");
			Assert.IsFalse(result);

			result = dependencyManager.HasTypeResolution(targetType, "x");
			Assert.IsFalse(result);

			selectorKey = "";
			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);

			result = dependencyManager.HasTypeResolution(targetType, null);
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution(targetType, "");
			Assert.IsTrue(result);

			result = dependencyManager.HasTypeResolution(targetType, "x");
			Assert.IsFalse(result);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldClearAllResolutionsTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "ddd";

			result = dependencyManager.ClearAllResolutions();

			Assert.IsFalse(result);

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);

			result = dependencyManager.ClearAllResolutions();

			Assert.IsTrue(result);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldClearTypeResolutions1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = "ddd";

			result = dependencyManager.ClearTypeResolutions<object>();

			Assert.IsFalse(result);

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);

			result = dependencyManager.ClearTypeResolutions<object>();

			Assert.IsTrue(result);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldClearTypeResolutionsTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "ddd";

			result = dependencyManager.ClearTypeResolutions(targetType);

			Assert.IsFalse(result);

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);

			result = dependencyManager.ClearTypeResolutions(targetType);

			Assert.IsTrue(result);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldCreateTest()
		{
			DependencyManager dependencyManager;

			dependencyManager = new DependencyManager();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedAddResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.Dispose();
			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.Dispose();
			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedCheckIfHasResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();

			dependencyManager.Dispose();
			dependencyManager.HasTypeResolution<object>(null);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedCheckIfHasResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);

			dependencyManager.Dispose();
			dependencyManager.HasTypeResolution(targetType, null);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedClearAllResolutionsTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();

			dependencyManager.Dispose();
			result = dependencyManager.ClearAllResolutions();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedClearTypeResolutions1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();

			dependencyManager.Dispose();
			result = dependencyManager.ClearTypeResolutions<object>();
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedClearTypeResolutionsTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			bool result;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);

			dependencyManager.Dispose();
			result = dependencyManager.ClearTypeResolutions(targetType);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedRemoveResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.Dispose();
			dependencyManager.RemoveResolution<object>(selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedRemoveResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.Dispose();
			dependencyManager.RemoveResolution(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedResolveDependency1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.Dispose();
			value = dependencyManager.ResolveDependency<object>(selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ShouldFailOnDisposedResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.Dispose();
			value = dependencyManager.ResolveDependency(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(DependencyException))]
		public void ShouldFailOnKeyExistsAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(DependencyException))]
		public void ShouldFailOnKeyNotExistsRemoveResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "zzz";

			dependencyManager.RemoveResolution(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(DependencyException))]
		public void ShouldFailOnKeyNotExistsResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "xxx";

			value = dependencyManager.ResolveDependency(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(DependencyException))]
		public void ShouldFailOnNotAssignableResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(1));

			dependencyManager = new DependencyManager();
			targetType = typeof(IDisposable);
			selectorKey = "yyy";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
			value = dependencyManager.ResolveDependency<IDisposable>(selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullDependencyResolutionAddResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = null;

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullDependencyResolutionAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = null;

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyAddResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			selectorKey = null;

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = null;

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyRemoveResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = null;

			dependencyManager.RemoveResolution<object>(selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyRemoveResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = null;

			dependencyManager.RemoveResolution(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyResolveDependency1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = null;

			value = dependencyManager.ResolveDependency<object>(selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = null;

			value = dependencyManager.ResolveDependency(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeAddResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(null));

			dependencyManager = new DependencyManager();
			targetType = null;
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeCheckIfHasResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = null;

			dependencyManager.HasTypeResolution(targetType, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeClearTypeResolutionsTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = null;

			dependencyManager.ClearTypeResolutions(targetType);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeRemoveResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = null;
			selectorKey = "x";

			dependencyManager.RemoveResolution(targetType, selectorKey);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = null;
			selectorKey = "x";

			value = dependencyManager.ResolveDependency(targetType, selectorKey);
		}

		[Test]
		public void ShouldNotFailOnDoubleDisposedAddResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();

			Assert.IsFalse(dependencyManager.Disposed);

			dependencyManager.Dispose();

			Assert.IsTrue(dependencyManager.Disposed);

			dependencyManager.Dispose();

			Assert.IsTrue(dependencyManager.Disposed);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldRemoveResolution1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);
			dependencyManager.RemoveResolution<object>(selectorKey);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldRemoveResolutionTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
			dependencyManager.RemoveResolution(targetType, selectorKey);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldResolveDependency1Test()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(1));

			dependencyManager = new DependencyManager();
			selectorKey = "x";

			dependencyManager.AddResolution<object>(selectorKey, mockDependencyResolution);
			value = dependencyManager.ResolveDependency<object>(selectorKey);

			Assert.IsNotNull(value);
			Assert.AreEqual(1, value);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldResolveDependencyTest()
		{
			DependencyManager dependencyManager;
			Mockery mockery;
			IDependencyResolution mockDependencyResolution;
			Type targetType;
			string selectorKey;
			object value;

			mockery = new Mockery();
			mockDependencyResolution = mockery.NewMock<IDependencyResolution>();

			Expect.Once.On(mockDependencyResolution).Method("Resolve").WithNoArguments().Will(Return.Value(1));

			dependencyManager = new DependencyManager();
			targetType = typeof(object);
			selectorKey = "x";

			dependencyManager.AddResolution(targetType, selectorKey, mockDependencyResolution);
			value = dependencyManager.ResolveDependency(targetType, selectorKey);

			Assert.IsNotNull(value);
			Assert.AreEqual(1, value);

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldVerifyAutoWiringTest()
		{
			IFormattable formattable;

			formattable = DependencyManager.AppDomainInstance.ResolveDependency<IFormattable>("bob");

			Assert.IsNotNull(formattable);
			Assert.IsInstanceOf<double>(formattable);
			Assert.AreEqual(1234.56789, (double)formattable);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		[DependencyRegistration]
		private static class Inner
		{
			#region Methods/Operators

			[DependencyRegistration]
			public static void ShouldNotMatchPrivateType()
			{
				throw new Exception();
			}

			#endregion
		}

		#endregion
	}
}