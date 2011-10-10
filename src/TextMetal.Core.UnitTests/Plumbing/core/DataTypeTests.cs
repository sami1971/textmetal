/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using NUnit.Framework;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.UnitTests.Plumbing.core
{
	/// <summary>
	/// Unit tests.
	/// </summary>
	[TestFixture]
	public class DataTypeTests
	{
		#region Constructors/Destructors

		public DataTypeTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCheckIsNullOrWhiteSpaceTest()
		{
			Assert.IsTrue(DataType.IsNullOrWhiteSpace(null));
			Assert.IsTrue(DataType.IsNullOrWhiteSpace(""));
			Assert.IsTrue(DataType.IsNullOrWhiteSpace("   "));
			Assert.IsFalse(DataType.IsNullOrWhiteSpace("daniel"));
			Assert.IsFalse(DataType.IsNullOrWhiteSpace("   daniel     "));
		}

		[Test]
		public void ShouldCheckIsWhiteSpaceTest()
		{
			Assert.IsFalse(DataType.IsWhiteSpace(null));
			Assert.IsTrue(DataType.IsWhiteSpace("   "));
			Assert.IsFalse(DataType.IsWhiteSpace("daniel"));
			Assert.IsFalse(DataType.IsWhiteSpace("   daniel     "));
		}

		//[Test]
		//public void ShouldCheckObjectsCompareValueSemanticsTest()
		//{
		//    int? result;
		//    object objA, objB;

		//    // both null
		//    objA = null;
		//    objB = null;
		//    result = DataType.ObjectsCompareValueSemantics(objA, objB);
		//    Assert.IsNull(result);

		//    // objA null, objB not null
		//    objA = null;
		//    objB = 10;
		//    result = DataType.ObjectsCompareValueSemantics(objA, objB);
		//    Assert.Less(result, 0);

		//    // objA not null, objB null
		//    objA = 10;
		//    objB = null;
		//    result = DataType.ObjectsCompareValueSemantics(objA, objB);
		//    Assert.Greater(result, 0);

		//    // objA == objB
		//    objA = 100;
		//    objB = 100;
		//    result = DataType.ObjectsCompareValueSemantics(objA, objB);
		//    Assert.AreEqual(0, result);

		//    // objA != objB
		//    objA = 100;
		//    objB = -100;
		//    result = DataType.ObjectsCompareValueSemantics(objA, objB);
		//    Assert.Greater(result, 0);
		//}

		[Test]
		public void ShouldCheckObjectsEqualValueSemanticsTest()
		{
			bool result;
			object objA, objB;

			// both null
			objA = null;
			objB = null;
			result = DataType.ObjectsEqualValueSemantics(objA, objB);
			Assert.IsTrue(result);

			// objA null, objB not null
			objA = null;
			objB = "not null string";
			result = DataType.ObjectsEqualValueSemantics(objA, objB);
			Assert.IsFalse(result);

			// objA not null, objB null
			objA = "not null string";
			objB = null;
			result = DataType.ObjectsEqualValueSemantics(objA, objB);
			Assert.IsFalse(result);

			// objA == objB
			objA = 100;
			objB = 100;
			result = DataType.ObjectsEqualValueSemantics(objA, objB);
			Assert.IsTrue(result);

			// objA != objB
			objA = 100;
			objB = -100;
			result = DataType.ObjectsEqualValueSemantics(objA, objB);
			Assert.IsFalse(result);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldFailOnInvalidGenericTypeTryParseTest()
		{
			KeyValuePair<int, int> ovalue;
			bool result;

			result = DataType.TryParse<KeyValuePair<int, int>>(DBNull.Value.ToString(), out ovalue);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ShouldFailOnInvalidTypeTryParseTest()
		{
			object ovalue;
			bool result;

			result = DataType.TryParse(typeof(KeyValuePair<int, int>), DBNull.Value.ToString(), out ovalue);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeChangeTypeTest()
		{
			DataType.ChangeType(1, null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeDefaultValueTest()
		{
			DataType.DefaultValue(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTypeTryParseTest()
		{
			object ovalue;
			bool result;

			result = DataType.TryParse(null, "", out ovalue);
		}

		[Test]
		public void ShouldGetBooleanTest()
		{
			Boolean ovalue;
			bool result;

			result = DataType.TryParse<Boolean>("true", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(true, ovalue);

			result = DataType.TryParse<Boolean>("false", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(false, ovalue);
		}

		[Test]
		public void ShouldGetByteTest()
		{
			Byte ovalue;
			bool result;

			result = DataType.TryParse<Byte>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetCharTest()
		{
			Char ovalue;
			bool result;

			result = DataType.TryParse<Char>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual('0', ovalue);
		}

		[Test]
		public void ShouldGetDateTimeOffsetTest()
		{
			DateTimeOffset ovalue;
			bool result;

			result = DataType.TryParse<DateTimeOffset>("6/22/2003", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(new DateTimeOffset(new DateTime(2003, 6, 22)), ovalue);
		}

		[Test]
		public void ShouldGetDateTimeTest()
		{
			DateTime ovalue;
			bool result;

			result = DataType.TryParse<DateTime>("6/22/2003", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(new DateTime(2003, 6, 22), ovalue);
		}

		[Test]
		public void ShouldGetDecimalTest()
		{
			Decimal ovalue;
			bool result;

			result = DataType.TryParse<Decimal>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetDefaultValueTest()
		{
			object defaultValue;

			defaultValue = DataType.DefaultValue(typeof(int));

			Assert.AreEqual(0, defaultValue);

			defaultValue = DataType.DefaultValue(typeof(int?));

			Assert.IsNull(defaultValue);
		}

		[Test]
		public void ShouldGetDoubleTest()
		{
			Double ovalue;
			bool result;

			result = DataType.TryParse<Double>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetEnumTest()
		{
			CharSet ovalue;
			bool result;

			result = DataType.TryParse<CharSet>("Unicode", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(CharSet.Unicode, ovalue);
		}

		[Test]
		public void ShouldGetGuidTest()
		{
			Guid ovalue;
			bool result;

			result = DataType.TryParse<Guid>("{00000000-0000-0000-0000-000000000000}", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(Guid.Empty, ovalue);
		}

		[Test]
		public void ShouldGetInt16Test()
		{
			Int16 ovalue;
			bool result;

			result = DataType.TryParse<Int16>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetInt32Test()
		{
			Int32 ovalue;
			bool result;

			result = DataType.TryParse<Int32>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetInt64Test()
		{
			Int64 ovalue;
			bool result;

			result = DataType.TryParse<Int64>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetSByteTest()
		{
			SByte ovalue;
			bool result;

			result = DataType.TryParse<SByte>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetSingleTest()
		{
			Single ovalue;
			bool result;

			result = DataType.TryParse<Single>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetStringTest()
		{
			String ovalue;
			bool result;

			result = DataType.TryParse<String>("0-8-8-8-8-8-8-8-c", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual("0-8-8-8-8-8-8-8-c", ovalue);
		}

		[Test]
		public void ShouldGetTimeSpanTest()
		{
			TimeSpan ovalue;
			bool result;

			result = DataType.TryParse<TimeSpan>("0:0:0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(TimeSpan.Zero, ovalue);
		}

		[Test]
		public void ShouldGetUInt16Test()
		{
			UInt16 ovalue;
			bool result;

			result = DataType.TryParse<UInt16>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetUInt32Test()
		{
			UInt32 ovalue;
			bool result;

			result = DataType.TryParse<UInt32>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldGetUInt64Test()
		{
			UInt64 ovalue;
			bool result;

			result = DataType.TryParse<UInt64>("0", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual(0, ovalue);
		}

		[Test]
		public void ShouldNotGetBooleanTest()
		{
			Boolean ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Boolean>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetByteTest()
		{
			Byte ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Byte>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Byte>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetCharTest()
		{
			Char ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Char>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetDateTimeOffsetTest()
		{
			DateTimeOffset ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<DateTimeOffset>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetDateTimeTest()
		{
			DateTime ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<DateTime>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetDecimalTest()
		{
			Decimal ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Decimal>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Decimal>("11111111111111111111111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetDoubleTest()
		{
			Double ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Double>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Double>("999,769,313,486,232,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000.00", out ovalue));
		}

		[Test]
		public void ShouldNotGetEnumTest()
		{
			CharSet ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<CharSet>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetGuidTest()
		{
			Guid ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Guid>("gibberish", out ovalue));
		}

		[Test]
		public void ShouldNotGetInt16Test()
		{
			Int16 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int16>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Int16>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetInt32Test()
		{
			Int32 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int32>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Int32>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetInt64Test()
		{
			Int64 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int64>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Int64>("9999999999999999999", out ovalue));
		}

		[Test]
		public void ShouldNotGetSByteTest()
		{
			SByte ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<SByte>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<SByte>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetSingleTest()
		{
			Single ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Single>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<Single>("999,282,300,000,000,000,000,000,000,000,000,000,000.00", out ovalue));
		}

		[Test]
		public void ShouldNotGetTimeSpanTest()
		{
			TimeSpan ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<TimeSpan>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<TimeSpan>("99999999.02:48:05.4775807", out ovalue));
		}

		[Test]
		public void ShouldNotGetUInt16Test()
		{
			UInt16 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt16>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<UInt16>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetUInt32Test()
		{
			UInt32 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt32>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<UInt32>("1111111111111111111", out ovalue));
		}

		[Test]
		public void ShouldNotGetUInt64Test()
		{
			UInt64 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt64>("gibberish", out ovalue));
			Assert.IsFalse(result = DataType.TryParse<UInt64>("99999999999999999999", out ovalue));
		}

		[Test]
		public void ShouldSafeToStringTest()
		{
			Assert.AreEqual("123.456", DataType.SafeToString(123.456));
			Assert.AreEqual("123.46", DataType.SafeToString(123.456, "n"));
			Assert.AreEqual("urn:foo", DataType.SafeToString(new Uri("urn:foo"), "n"));

			Assert.AreEqual("", DataType.SafeToString((object)null, null));
			Assert.AreEqual(null, DataType.SafeToString((object)null, null, null));
			Assert.AreEqual("1", DataType.SafeToString((object)"", null, "1", true));
		}

		[Test]
		public void ShouldSpecialGetValueOnNonNullNullableGenericTryParseTest()
		{
			int? ovalue;
			bool result;

			result = DataType.TryParse<int?>("100", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual((int?)100, ovalue);
		}

		[Test]
		public void ShouldSpecialGetValueOnNonNullNullableTryParseTest()
		{
			object ovalue;
			bool result;

			result = DataType.TryParse(typeof(int?), "100", out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual((int?)100, ovalue);
		}

		[Test]
		public void ShouldSpecialGetValueOnNullNullableGenericTryParseTest()
		{
			int? ovalue;
			bool result;

			result = DataType.TryParse<int?>(null, out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual((int?)null, ovalue);
		}

		[Test]
		public void ShouldSpecialGetValueOnNullNullableTryParseTest()
		{
			object ovalue;
			bool result;

			result = DataType.TryParse(typeof(int?), null, out ovalue);
			Assert.IsTrue(result);
			Assert.AreEqual((int?)null, ovalue);
		}

		[Test]
		public void ShouldWithNullNotGetBooleanTest()
		{
			Boolean ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Boolean>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetByteTest()
		{
			Byte ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Byte>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetCharTest()
		{
			Char ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Char>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetDateTimeOffsetTest()
		{
			DateTimeOffset ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<DateTimeOffset>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetDateTimeTest()
		{
			DateTime ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<DateTime>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetDecimalTest()
		{
			Decimal ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Decimal>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetDoubleTest()
		{
			Double ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Double>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetEnumTest()
		{
			CharSet ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<CharSet>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetGuidTest()
		{
			Guid ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Guid>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetInt16Test()
		{
			Int16 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int16>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetInt32Test()
		{
			Int32 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int32>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetInt64Test()
		{
			Int64 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Int64>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetSByteTest()
		{
			SByte ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<SByte>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetSingleTest()
		{
			Single ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<Single>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetTimeSpanTest()
		{
			TimeSpan ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<TimeSpan>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetUInt16Test()
		{
			UInt16 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt16>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetUInt32Test()
		{
			UInt32 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt32>(null, out ovalue));
		}

		[Test]
		public void ShouldWithNullNotGetUInt64Test()
		{
			UInt64 ovalue;
			bool result;

			Assert.IsFalse(result = DataType.TryParse<UInt64>(null, out ovalue));
		}

		#endregion
	}
}