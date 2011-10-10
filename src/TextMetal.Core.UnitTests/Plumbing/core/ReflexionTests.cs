/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using TextMetal.Core.Plumbing;
using TextMetal.Core.UnitTests.TestingInfrastructure;

namespace TextMetal.Core.UnitTests.Plumbing.core
{
	[TestFixture]
	public class ReflexionTests
	{
		#region Constructors/Destructors

		public ReflexionTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldAssociativeOnlyGetLogicalPropertyTypeTest()
		{
			Dictionary<string, object> mockObject;
			string propertyName;
			Type propertyType;
			bool result;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, null
			mockObject = new Dictionary<string, object>();
			propertyName = null;

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, ""
			mockObject = new Dictionary<string, object>();
			propertyName = "";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, "PropName"
			mockObject = new Dictionary<string, object>();
			mockObject["FirstName"] = "john";
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyType);
			Assert.AreEqual(typeof(string), propertyType);
		}

		[Test]
		public void ShouldAssociativeOnlyGetLogicalPropertyValueTest()
		{
			Dictionary<string, object> mockObject;
			string propertyName;
			object propertyValue;
			bool result;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, null
			mockObject = new Dictionary<string, object>();
			propertyName = null;

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, ""
			mockObject = new Dictionary<string, object>();
			propertyName = "";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, "PropName"
			mockObject = new Dictionary<string, object>();
			mockObject["FirstName"] = "john";
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual("john", propertyValue);
		}

		[Test]
		public void ShouldAssociativeOnlySetLogicalPropertyValueTest()
		{
			Dictionary<string, object> mockObject;
			string propertyName;
			object propertyValue;
			bool result;

			propertyValue = null;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, null
			mockObject = new Dictionary<string, object>();
			propertyName = null;

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, ""
			mockObject = new Dictionary<string, object>();
			propertyName = "";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, "PropName"
			mockObject = new Dictionary<string, object>();
			mockObject["FirstName"] = null;
			propertyName = "FirstName";
			propertyValue = "john";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual("john", propertyValue);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnDefinedGetNoAttributesTest()
		{
			Reflexion.GetZeroAttributes<MockMultipleTestAttibute>(typeof(MockTestAttributedClass));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ShouldFailOnMultiDefinedGetAttributeTest()
		{
			Reflexion.GetOneAttribute<MockMultipleTestAttibute>(typeof(MockTestAttributedClass));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConversionTypeMakeNonNullableTypeTest()
		{
			Reflexion.MakeNonNullableType(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullConversionTypeMakeNullableTypeTest()
		{
			Reflexion.MakeNullableType(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTargetGetAttributeICustomAttributeProviderTest()
		{
			Reflexion.GetOneAttribute<MockMultipleTestAttibute>((ICustomAttributeProvider)null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTargetGetAttributesICustomAttributeProviderTest()
		{
			Reflexion.GetAllAttributes<MockMultipleTestAttibute>((ICustomAttributeProvider)null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTargetGetZeroAttributesICustomAttributeProviderTest()
		{
			Reflexion.GetZeroAttributes<MockMultipleTestAttibute>((ICustomAttributeProvider)null);
		}

		[Test]
		public void ShouldGetAttributeICustomAttributeProviderTest()
		{
			MockSingleTestAttibute sta;

			sta = Reflexion.GetOneAttribute<MockSingleTestAttibute>(typeof(MockTestAttributedClass));

			Assert.IsNotNull(sta);
			Assert.AreEqual(5, sta.Value);
		}

		[Test]
		public void ShouldGetAttributesICustomAttributeProviderTest()
		{
			MockMultipleTestAttibute[] tas;

			tas = Reflexion.GetAllAttributes<MockMultipleTestAttibute>(typeof(MockTestAttributedClass));

			Assert.IsNotNull(tas);
			Assert.AreEqual(2, tas.Length);
		}

		[Test]
		public void ShouldGetDefaultOnNullValueChangeTypeTest()
		{
			object value;

			value = DataType.ChangeType(null, typeof(int));

			Assert.AreEqual(default(int), value);
		}

		[Test]
		public void ShouldGetErrors()
		{
			MockException mockException;
			string message;

			try
			{
				try
				{
					throw new InvalidOperationException("ioe.collected.outer", new DivideByZeroException("dbze.collected.inner"));
				}
				catch (Exception ex)
				{
					mockException = new MockException("me.outer", new BadImageFormatException("bie.inner"));
					mockException.CollectedExceptions.Add(ex);

					throw mockException;
				}
			}
			catch (Exception ex)
			{
				message = Reflexion.GetErrors(ex, 0);

				Console.WriteLine(message);
				//Assert.AreEqual("[SwIsHw.Core.UnitTests.TestingInfrastructure.MockException]\r\nouter\r\n[System.Exception]\r\ncollected.outer\r\n[System.Exception]\r\ncollected.inner\r\n[System.Exception]\r\ninner", message);
			}
		}

		[Test]
		public void ShouldGetNoAttributesTest()
		{
			Reflexion.GetZeroAttributes<AssemblyDescriptionAttribute>(typeof(MockTestAttributedClass));
		}

		[Test]
		public void ShouldGetNonNullOnNonNullValueChangeTypeTest()
		{
			object value;

			value = DataType.ChangeType((byte)1, typeof(int));

			Assert.IsNotNull(value);
			Assert.IsInstanceOf<int>(value);
			Assert.AreEqual((int)1, value);
		}

		[Test]
		public void ShouldGetNonNullOnNonNullValueNullableChangeTypeTest()
		{
			object value;

			value = DataType.ChangeType((byte)1, typeof(int?));

			Assert.IsNotNull(value);
			Assert.IsInstanceOf<int?>(value);
			Assert.AreEqual((int?)1, value);
		}

		[Test]
		public void ShouldGetNullAttributeICustomAttributeProviderTest()
		{
			MockMultipleTestAttibute ta;

			ta = Reflexion.GetOneAttribute<MockMultipleTestAttibute>(typeof(Exception));

			Assert.IsNull(ta);
		}

		[Test]
		public void ShouldGetNullAttributesICustomAttributeProviderTest()
		{
			MockMultipleTestAttibute[] tas;

			tas = Reflexion.GetAllAttributes<MockMultipleTestAttibute>(typeof(Exception));

			Assert.IsNull(tas);
		}

		[Test]
		public void ShouldMakeNonNullableTypeTest()
		{
			Type conversionType;
			Type nonNullableType;

			conversionType = typeof(int);
			nonNullableType = Reflexion.MakeNonNullableType(conversionType);
			Assert.AreEqual(typeof(int), nonNullableType);

			conversionType = typeof(int?);
			nonNullableType = Reflexion.MakeNonNullableType(conversionType);
			Assert.AreEqual(typeof(int), nonNullableType);

			conversionType = typeof(IDisposable);
			nonNullableType = Reflexion.MakeNonNullableType(conversionType);
			Assert.IsNull(nonNullableType);
		}

		[Test]
		public void ShouldMakeNullableTypeTest()
		{
			Type conversionType;
			Type nullableType;

			conversionType = typeof(int);
			nullableType = Reflexion.MakeNullableType(conversionType);
			Assert.AreEqual(typeof(int?), nullableType);

			conversionType = typeof(int?);
			nullableType = Reflexion.MakeNullableType(conversionType);
			Assert.AreEqual(typeof(int?), nullableType);

			conversionType = typeof(IDisposable);
			nullableType = Reflexion.MakeNullableType(conversionType);
			Assert.AreEqual(typeof(IDisposable), nullableType);
		}

		[Test]
		public void ShouldReflectionOnlyGetLogicalPropertyTypeTest()
		{
			MockObject mockObject;
			string propertyName;
			Type propertyType;
			bool result;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, null
			mockObject = new MockObject();
			propertyName = null;

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, ""
			mockObject = new MockObject();
			propertyName = "";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsFalse(result);
			Assert.IsNull(propertyType);

			// !null, "PropName"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyType);
			Assert.AreEqual(typeof(string), propertyType);

			// !null, "PropName:PropName!!!getter"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "NoGetter";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyType);
			Assert.AreEqual(typeof(object), propertyType);

			// !null, "PropName:PropName!!!setter"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "NoSetter";

			result = Reflexion.GetLogicalPropertyType(mockObject, propertyName, out propertyType);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyType);
			Assert.AreEqual(typeof(object), propertyType);
		}

		[Test]
		public void ShouldReflectionOnlyGetLogicalPropertyValueTest()
		{
			MockObject mockObject;
			string propertyName;
			object propertyValue;
			bool result;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, null
			mockObject = new MockObject();
			propertyName = null;

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, ""
			mockObject = new MockObject();
			propertyName = "";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, "PropName"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "FirstName";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual("john", propertyValue);

			// !null, "PropName:PropName!!!getter"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "NoGetter";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, "PropName:PropName!!!setter"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "NoSetter";

			result = Reflexion.GetLogicalPropertyValue(mockObject, propertyName, out propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual(1, propertyValue);
		}

		[Test]
		public void ShouldReflectionOnlySetLogicalPropertyValueTest()
		{
			MockObject mockObject;
			string propertyName;
			object propertyValue;
			bool result;

			propertyValue = null;

			// null, null
			mockObject = null;
			propertyName = null;

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, ""
			mockObject = null;
			propertyName = "";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// null, "PropName"
			mockObject = null;
			propertyName = "FirstName";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, null
			mockObject = new MockObject();
			propertyName = null;

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, ""
			mockObject = new MockObject();
			propertyName = "";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
			Assert.IsNull(propertyValue);

			// !null, "PropName"
			mockObject = new MockObject();
			propertyName = "FirstName";
			propertyValue = "john";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual("john", propertyValue);

			// !null, "PropName:PropName!!!getter"
			mockObject = new MockObject();
			propertyName = "NoGetter";
			propertyValue = "john";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsTrue(result);
			Assert.IsNotNull(propertyValue);
			Assert.AreEqual("john", propertyValue);

			// !null, "PropName:PropName!!!setter"
			mockObject = new MockObject();
			mockObject.FirstName = "john";
			propertyName = "NoSetter";

			result = Reflexion.SetLogicalPropertyValue(mockObject, propertyName, propertyValue);

			Assert.IsFalse(result);
		}

		#endregion
	}
}