/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core.UnitTests.TestingFramework
{
	public static class ValueGenerator
	{
		#region Fields/Constants

		private static readonly Random random = new Random();
		private static readonly Dictionary<Type, object> valueHistory = new Dictionary<Type, object>();

		#endregion

		#region Methods/Operators

		public static TValue GetNextValue<TValue>()
		{
			Type valueType;
			object oresult;
			TValue zresult;

			valueType = typeof(TValue);
			oresult = GetNextValue(valueType);

			if ((object)oresult == null)
				zresult = default(TValue);
			else
				zresult = (TValue)oresult;

			if (valueHistory.ContainsKey(valueType))
				valueHistory.Remove(valueType);

			valueHistory.Add(valueType, zresult);

			return zresult;
		}

		private static object GetNextValue(Type valueType)
		{
			Type openNullableType;

			if ((object)valueType == null)
				throw new ArgumentNullException("valueType");

			openNullableType = typeof(Nullable<>);

			if (valueType.IsArray)
				return Array.CreateInstance(valueType.GetElementType(), random.Next(0, 99));
			else if (valueType.IsGenericType &&
			         !valueType.IsGenericTypeDefinition &&
			         valueType.GetGenericTypeDefinition().Equals(openNullableType))
				return GetNextValue(valueType.GetGenericArguments()[0]);
			else if (valueType == typeof(String))
				return new string((Char)random.Next(33, 127), random.Next(1, 100));
			else if (valueType == typeof(Boolean))
				return (random.Next(0, 999) % 2) == 0;
			else if (valueType == typeof(Byte))
				return (Byte)random.Next(Byte.MinValue, Byte.MaxValue);
			else if (valueType == typeof(Char))
				return (Char)random.Next(Char.MinValue, Char.MaxValue);
			else if (valueType == typeof(DateTime))
				return new DateTime(random.Next(1900, 2099), random.Next(1, 12), random.Next(1, 28), random.Next(0, 23), random.Next(0, 59), random.Next(0, 59), 0);
			else if (valueType == typeof(DateTimeOffset))
				return new DateTimeOffset(new DateTime(random.Next(1900, 2099), random.Next(1, 12), random.Next(1, 28), random.Next(0, 23), random.Next(0, 59), random.Next(0, 59), 0));
			else if (valueType == typeof(Decimal))
				return (Decimal)random.NextDouble();
			else if (valueType == typeof(Double))
				return random.NextDouble();
			else if (valueType == typeof(Guid))
			{
				byte[] buffer;
				buffer = new byte[16];
				random.NextBytes(buffer);
				return new Guid(buffer);
			}
			else if (valueType == typeof(Int16))
				return (Int16)random.Next(Int16.MinValue, Int16.MaxValue);
			else if (valueType == typeof(Int32))
				return random.Next(Int32.MinValue, Int32.MaxValue);
			else if (valueType == typeof(Int64))
				return (Int64)random.Next(Int32.MinValue, Int32.MaxValue);
			else if (valueType == typeof(SByte))
				return (SByte)random.Next(SByte.MinValue, SByte.MaxValue);
			else if (valueType == typeof(Single))
				return (Single)random.NextDouble();
			else if (valueType == typeof(TimeSpan))
				return new TimeSpan(random.Next(0, 99), random.Next(0, 23), random.Next(0, 59), random.Next(0, 59), 0);
			else if (valueType == typeof(UInt16))
				return (UInt16)random.Next(UInt16.MinValue, UInt16.MaxValue);
			else if (valueType == typeof(UInt32))
				return (UInt32)random.Next(Int32.MinValue, Int32.MaxValue);
			else if (valueType == typeof(UInt64))
				return (UInt64)random.Next(Int32.MinValue, Int32.MaxValue);
			else if (valueType.IsEnum) // special case
				return GetNextValue(Enum.GetUnderlyingType(valueType));
			else
				throw new ArgumentOutOfRangeException("valueType", string.Format("The value type '{0}' is not supported.", valueType.FullName));
		}

		public static TValue GetPreviousValue<TValue>()
		{
			Type valueType;
			object oresult;
			TValue zresult;

			valueType = typeof(TValue);

			if (valueHistory.ContainsKey(valueType))
				oresult = valueHistory[valueType];
			else
				oresult = null;

			if ((object)oresult == null)
				zresult = default(TValue);
			else
				zresult = (TValue)oresult;

			return zresult;
		}

		#endregion
	}
}