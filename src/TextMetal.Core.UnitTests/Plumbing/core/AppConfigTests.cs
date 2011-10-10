/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;

using NUnit.Framework;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.UnitTests.Plumbing.core
{
	[TestFixture]
	public class AppConfigTests
	{
		#region Constructors/Destructors

		public AppConfigTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetBooleanTest()
		{
			AppConfig.GetAppSetting<Boolean>("BadAppConfigValueBoolean");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetByteTest()
		{
			AppConfig.GetAppSetting<Byte>("BadAppConfigValueByte");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetCharTest()
		{
			AppConfig.GetAppSetting<Char>("BadAppConfigValueChar");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetConnectionProviderTest()
		{
			AppConfig.GetConnectionProvider("BadMyProvider");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetConnectionStringTest()
		{
			AppConfig.GetConnectionString("BadMyProvider");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetDateTimeTest()
		{
			AppConfig.GetAppSetting<DateTime>("BadAppConfigValueDateTime");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetDecimalTest()
		{
			AppConfig.GetAppSetting<Decimal>("BadAppConfigValueDecimal");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetDoubleTest()
		{
			AppConfig.GetAppSetting<Double>("BadAppConfigValueDouble");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetEnumTest()
		{
			AppConfig.GetAppSetting<CharSet>("BadAppConfigValueEnum");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetGuidTest()
		{
			AppConfig.GetAppSetting<Guid>("BadAppConfigValueGuid");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetInt16Test()
		{
			AppConfig.GetAppSetting<Int16>("BadAppConfigValueInt16");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetInt32Test()
		{
			AppConfig.GetAppSetting<Int32>("BadAppConfigValueInt32");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetInt64Test()
		{
			AppConfig.GetAppSetting<Int64>("BadAppConfigValueInt64");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetSByteTest()
		{
			AppConfig.GetAppSetting<SByte>("BadAppConfigValueSByte");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetSingleTest()
		{
			AppConfig.GetAppSetting<Single>("BadAppConfigValueSingle");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetTimeSpanTest()
		{
			AppConfig.GetAppSetting<TimeSpan>("BadAppConfigValueTimeSpan");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetUInt16Test()
		{
			AppConfig.GetAppSetting<UInt16>("BadAppConfigValueUInt16");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetUInt32Test()
		{
			AppConfig.GetAppSetting<UInt32>("BadAppConfigValueUInt32");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnInvalidValueGetUInt64Test()
		{
			AppConfig.GetAppSetting<UInt64>("BadAppConfigValueUInt64");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetBooleanTest()
		{
			AppConfig.GetAppSetting<Boolean>("NotThereAppConfigValueBoolean");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetByteTest()
		{
			AppConfig.GetAppSetting<Byte>("NotThereAppConfigValueByte");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetCharTest()
		{
			AppConfig.GetAppSetting<Char>("NotThereAppConfigValueChar");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetConnectionProviderTest()
		{
			AppConfig.GetConnectionProvider("NotThereMyProvider");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetConnectionStringTest()
		{
			AppConfig.GetConnectionString("NotThereMyProvider");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetDateTimeTest()
		{
			AppConfig.GetAppSetting<DateTime>("NotThereAppConfigValueDateTime");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetDecimalTest()
		{
			AppConfig.GetAppSetting<Decimal>("NotThereAppConfigValueDecimal");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetDoubleTest()
		{
			AppConfig.GetAppSetting<Double>("NotThereAppConfigValueDouble");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetEnumTest()
		{
			AppConfig.GetAppSetting<CharSet>("NotThereAppConfigValueEnum");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetGuidTest()
		{
			AppConfig.GetAppSetting<Guid>("NotThereAppConfigValueGuid");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetInt16Test()
		{
			AppConfig.GetAppSetting<Int16>("NotThereAppConfigValueInt16");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetInt32Test()
		{
			AppConfig.GetAppSetting<Int32>("NotThereAppConfigValueInt32");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetInt64Test()
		{
			AppConfig.GetAppSetting<Int64>("NotThereAppConfigValueInt64");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetSByteTest()
		{
			AppConfig.GetAppSetting<SByte>("NotThereAppConfigValueSByte");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetSingleTest()
		{
			AppConfig.GetAppSetting<Single>("NotThereAppConfigValueSingle");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetTimeSpanTest()
		{
			AppConfig.GetAppSetting<TimeSpan>("NotThereAppConfigValueTimeSpan");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetUInt16Test()
		{
			AppConfig.GetAppSetting<UInt16>("NotThereAppConfigValueUInt16");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetUInt32Test()
		{
			AppConfig.GetAppSetting<UInt32>("NotThereAppConfigValueUInt32");
		}

		[Test]
		[ExpectedException(typeof(ConfigurationErrorsException))]
		public void ShouldFailOnNonExistKeyGetUInt64Test()
		{
			AppConfig.GetAppSetting<UInt64>("NotThereAppConfigValueUInt64");
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullArgsParseCommandLineArgumentsTest()
		{
			AppConfig.ParseCommandLineArguments(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetBooleanTest()
		{
			AppConfig.GetAppSetting<Boolean>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetByteTest()
		{
			AppConfig.GetAppSetting<Byte>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetCharTest()
		{
			AppConfig.GetAppSetting<Char>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetDateTimeTest()
		{
			AppConfig.GetAppSetting<DateTime>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetDecimalTest()
		{
			AppConfig.GetAppSetting<Decimal>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetDoubleTest()
		{
			AppConfig.GetAppSetting<Double>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetEnumTest()
		{
			AppConfig.GetAppSetting<CharSet>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetGuidTest()
		{
			AppConfig.GetAppSetting<Guid>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetInt16Test()
		{
			AppConfig.GetAppSetting<Int16>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetInt32Test()
		{
			AppConfig.GetAppSetting<Int32>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetInt64Test()
		{
			AppConfig.GetAppSetting<Int64>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetSByteTest()
		{
			AppConfig.GetAppSetting<SByte>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetSingleTest()
		{
			AppConfig.GetAppSetting<Single>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetTimeSpanTest()
		{
			AppConfig.GetAppSetting<TimeSpan>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetUInt16Test()
		{
			AppConfig.GetAppSetting<UInt16>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetUInt32Test()
		{
			AppConfig.GetAppSetting<UInt32>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyGetUInt64Test()
		{
			AppConfig.GetAppSetting<UInt64>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullKeyHasAppSettingTest()
		{
			AppConfig.HasAppSetting(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullNameGetAppSettingTest()
		{
			AppConfig.GetAppSetting<string>(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullNameGetConnectionProviderTest()
		{
			AppConfig.GetConnectionProvider(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullNameGetConnectionStringTest()
		{
			AppConfig.GetConnectionString(null);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullNameHasConnectionStringTest()
		{
			AppConfig.HasConnectionString(null);
		}

		[Test]
		public void ShouldGetArgsParseCommandLineArgumentsTest()
		{
			List<string> args;
			IDictionary<string, IList<string>> cmdlnargs;

			args = new List<string>();
			args.Add("arg0");
			args.Add("");
			args.Add("-");
			args.Add("-arg1");
			args.Add("-arg2:");
			args.Add("-arg3:");
			args.Add("-arg4:value4");
			args.Add("-arg5:value5");
			args.Add("-arg4:value4");
			args.Add("arg6:value6");
			args.Add("-:value7");
			args.Add("-arg8:value8a");
			args.Add("-arg8:value8b");
			args.Add("-arg8:value8c");
			args.Add("-arg8:value8a");

			cmdlnargs = AppConfig.ParseCommandLineArguments(args.ToArray());

			Assert.IsNotNull(cmdlnargs);
			Assert.AreEqual(3, cmdlnargs.Count);

			Assert.AreEqual(1, cmdlnargs["arg4"].Count);
			Assert.AreEqual("value4", cmdlnargs["arg4"][0]);

			Assert.AreEqual(1, cmdlnargs["arg5"].Count);
			Assert.AreEqual("value5", cmdlnargs["arg5"][0]);

			Assert.AreEqual(3, cmdlnargs["arg8"].Count);
			Assert.AreEqual("value8a", cmdlnargs["arg8"][0]);
			Assert.AreEqual("value8b", cmdlnargs["arg8"][1]);
			Assert.AreEqual("value8c", cmdlnargs["arg8"][2]);
		}

		[Test]
		public void ShouldGetBooleanTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Boolean>("AppConfigValueBoolean"), true);
		}

		[Test]
		public void ShouldGetByteTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Byte>("AppConfigValueByte"), 0);
		}

		[Test]
		public void ShouldGetCharTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Char>("AppConfigValueChar"), '0');
		}

		[Test]
		public void ShouldGetConnectionProviderTest()
		{
			Assert.AreEqual(AppConfig.GetConnectionProvider("MyProvider"), "sqlkiller");
		}

		[Test]
		public void ShouldGetConnectionStringTest()
		{
			Assert.AreEqual(AppConfig.GetConnectionString("MyProvider"), "sql=con");
		}

		[Test]
		public void ShouldGetDateTimeTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<DateTime>("AppConfigValueDateTime"), new DateTime(2003, 6, 22));
		}

		[Test]
		public void ShouldGetDecimalTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Decimal>("AppConfigValueDecimal"), 0);
		}

		[Test]
		public void ShouldGetDoubleTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Double>("AppConfigValueDouble"), 0);
		}

		[Test]
		public void ShouldGetEnumTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<CharSet>("AppConfigValueEnum"), CharSet.Unicode);
		}

		[Test]
		public void ShouldGetGuidTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Guid>("AppConfigValueGuid"), Guid.Empty);
		}

		[Test]
		public void ShouldGetInt16Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Int16>("AppConfigValueInt16"), 0);
		}

		[Test]
		public void ShouldGetInt32Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Int32>("AppConfigValueInt32"), 0);
		}

		[Test]
		public void ShouldGetInt64Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Int64>("AppConfigValueInt64"), 0);
		}

		[Test]
		public void ShouldGetSByteTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<SByte>("AppConfigValueSByte"), 0);
		}

		[Test]
		public void ShouldGetSingleTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<Single>("AppConfigValueSingle"), 0);
		}

		[Test]
		public void ShouldGetStringTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<string>("AppConfigValueString"), "foobar");
		}

		[Test]
		public void ShouldGetTimeSpanTest()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<TimeSpan>("AppConfigValueTimeSpan"), TimeSpan.Zero);
		}

		[Test]
		public void ShouldGetUInt16Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<UInt16>("AppConfigValueUInt16"), 0);
		}

		[Test]
		public void ShouldGetUInt32Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<UInt32>("AppConfigValueUInt32"), 0);
		}

		[Test]
		public void ShouldGetUInt64Test()
		{
			Assert.AreEqual(AppConfig.GetAppSetting<UInt64>("AppConfigValueUInt64"), 0L);
		}

		[Test]
		public void ShouldHaveFalseHasAppSettingTest()
		{
			Assert.IsFalse(AppConfig.HasAppSetting("AppConfigValueBooleanFalse"));
		}

		[Test]
		public void ShouldHaveFalseHasConnectionStringTest()
		{
			Assert.IsFalse(AppConfig.HasConnectionString("MyProviderFalse"));
		}

		[Test]
		public void ShouldHaveTrueHasAppSettingTest()
		{
			Assert.IsTrue(AppConfig.HasAppSetting("AppConfigValueBoolean"));
		}

		[Test]
		public void ShouldHaveTrueHasConnectionStringTest()
		{
			Assert.IsTrue(AppConfig.HasConnectionString("MyProvider"));
		}

		#endregion
	}
}