/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.Plumbing
{
	/// <summary>
	/// 	Provides static helper and/or extension methods for core data type functionality such as validation and parsing.
	/// </summary>
	public static partial class DataType
	{
		#region Methods/Operators

		/// <summary>
		/// 	Performs a run-time type change on a given value.
		/// </summary>
		/// <typeparam name="T"> The type to change value to. </typeparam>
		/// <param name="value"> The value to change type. </param>
		/// <returns> A value changed to the given type. </returns>
		public static T ChangeType<T>(this object value)
		{
			return (T)ChangeType(value, typeof(T));
		}

		/// <summary>
		/// 	Performs a run-time type change on a given value.
		/// </summary>
		/// <param name="value"> The value to change type. </param>
		/// <param name="conversionType"> The type to change value to. </param>
		/// <returns> A value changed to the given type. </returns>
		public static object ChangeType(this object value, Type conversionType)
		{
			if ((object)conversionType == null)
				throw new ArgumentNullException("conversionType");

			if ((object)value == null || value == DBNull.Value)
				return DefaultValue(conversionType);

			if (conversionType.IsAssignableFrom(value.GetType()))
				return value;

			if (conversionType.IsGenericType &&
			    !conversionType.IsGenericTypeDefinition &&
			    conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
				conversionType = Nullable.GetUnderlyingType(conversionType);

			return Convert.ChangeType(value, conversionType);
		}

		/// <summary>
		/// 	Obtains the default value for a given type using reflection.
		/// </summary>
		/// <param name="targetType"> The target type. </param>
		/// <returns> The default value for the target type. </returns>
		public static object DefaultValue(Type targetType)
		{
			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
		}

		/// <summary>
		/// 	Determines if a string value is null, zero length, or only contains white space.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is null, zero length, or only contains white space. </returns>
		public static bool IsNullOrWhiteSpace(string value)
		{
#if NET_FX_40
			return String.IsNullOrWhiteSpace(value);
#else
			return (object)value == null || IsWhiteSpace(value);
#endif
		}

		/// <summary>
		/// 	Determines if a string value is zero length or only contains white space.
		/// </summary>
		/// <param name="value"> The string value to check. </param>
		/// <returns> A boolean value indicating whether the value is zero length or only contains white space. </returns>
		public static bool IsWhiteSpace(string value)
		{
			if ((object)value == null)
				return false;

			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
					return false;
			}

			return true;
		}

		/// <summary>
		/// 	Checks whether two object instances are equal using the Object.Equals() method. Value coercion is performed.
		/// </summary>
		/// <param name="objA"> An object instance or null. </param>
		/// <param name="objB"> Another object instance or null. </param>
		/// <returns> A value indicating whether the two object instances are equal. </returns>
		public static bool ObjectsEqualValueSemantics(object objA, object objB)
		{
			Type typeOfA = null, typeOfB = null;

			if ((object)objA != null)
				typeOfA = objA.GetType();

			if ((object)objB != null)
				typeOfB = objB.GetType();

			return ((object)objA != null ? objA.Equals(DataType.ChangeType(objB, typeOfA)) : ((object)objB != null ? objB.Equals(DataType.ChangeType(objA, typeOfB)) : true /* both null */));
		}

		/// <summary>
		/// 	Returns a string that represents the specified type with the format specification. If the value is null, then the default value of a zero length string is returned.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		public static string SafeToString<TValue>(this TValue value)
		{
			return SafeToString<TValue>(value, null, "");
		}

		/// <summary>
		/// 	Returns a string that represents the specified type with the format specification. If the value is null, then the default value of a zero length string is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		public static string SafeToString<TValue>(this TValue value, string format)
		{
			return SafeToString<TValue>(value, format, "");
		}

		/// <summary>
		/// 	Returns a string that represents the specified type with the format specification. If the value is null, then the default value is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <param name="default"> The default value to return if the value is null. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		public static string SafeToString<TValue>(this TValue value, string format, string @default)
		{
			return SafeToString<TValue>(value, format, @default, false);
		}

		/// <summary>
		/// 	Returns a string that represents the specified type with the format specification. If the value is null, then the default value is returned. No trimming is performed.
		/// </summary>
		/// <typeparam name="TValue"> The type of the value to obtain a string representation. </typeparam>
		/// <param name="value"> The target value. </param>
		/// <param name="format"> The string specifying the format to use or null to use the default format defined for the type of the IFormattable implementation. </param>
		/// <param name="default"> The default value to return if the value is null. </param>
		/// <param name="dofvisnow"> Use default value if the formatted value is null or whotespace. </param>
		/// <returns> A formatted string value if the value is not null; otherwise the default value specified. </returns>
		public static string SafeToString<TValue>(this TValue value, string format, string @default, bool dofvisnow)
		{
			string retval;

			//@default = (@default ?? "");

			if ((object)value == null)
				return @default;

			if (value is IFormattable)
				retval = ((IFormattable)value).ToString(format, null);
			else
				retval = value.ToString();

			if (IsNullOrWhiteSpace(retval) && dofvisnow)
				retval = @default;

			return retval;
		}

		/// <summary>
		/// 	Converts the specified string representation to its valueType equivalent and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="valueType"> The type to convert the string value. </param>
		/// <param name="value"> A string containing a valueType to convert. </param>
		/// <param name="result"> When this method returns, contains the valueType value equivalent contained in value, if the conversion succeeded, or null if the conversion failed. The conversion fails if the value parameter is null, is an empty string, or does not contain a valid string representation of an valueType. This parameter is passed uninitialized. </param>
		/// <returns> A boolean value of true if the value was converted successfully; otherwise, false. </returns>
		public static bool TryParse(Type valueType, string value, out object result)
		{
			bool retval;
			Type openNullableType;

			if ((object)valueType == null)
				throw new ArgumentNullException("valueType");

			openNullableType = typeof(Nullable<>);
			result = null;

			if (valueType.IsGenericType &&
			    !valueType.IsGenericTypeDefinition &&
			    valueType.GetGenericTypeDefinition().Equals(openNullableType))
			{
				if ((object)value == null)
					return true;
				else
					return TryParse(valueType.GetGenericArguments()[0], value, out result);
			}
			else if (valueType == typeof(String))
			{
				retval = true;
				result = value;
			}
			else if (valueType == typeof(Boolean))
			{
				Boolean zresult;
				retval = Boolean.TryParse(value, out zresult);
				result = zresult;
				result = zresult;
			}
			else if (valueType == typeof(Byte))
			{
				Byte zresult;
				retval = Byte.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Char))
			{
				Char zresult;
				retval = Char.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(DateTime))
			{
				DateTime zresult;
				retval = DateTime.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(DateTimeOffset))
			{
				DateTimeOffset zresult;
				retval = DateTimeOffset.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Decimal))
			{
				Decimal zresult;
				retval = Decimal.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Double))
			{
				Double zresult;
				retval = Double.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Guid))
			{
				Guid zresult;
#if NET_FX_40
				retval = Guid.TryParse(value, out zresult);
				result = zresult;
#else
				if (retval = IsValidGuid(value))
				{
					zresult = ParseGuid(value);
					result = zresult;
				}
#endif
			}
			else if (valueType == typeof(Int16))
			{
				Int16 zresult;
				retval = Int16.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Int32))
			{
				Int32 zresult;
				retval = Int32.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Int64))
			{
				Int64 zresult;
				retval = Int64.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(SByte))
			{
				SByte zresult;
				retval = SByte.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(Single))
			{
				Single zresult;
				retval = Single.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(TimeSpan))
			{
				TimeSpan zresult;
				retval = TimeSpan.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(UInt16))
			{
				UInt16 zresult;
				retval = UInt16.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(UInt32))
			{
				UInt32 zresult;
				retval = UInt32.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType == typeof(UInt64))
			{
				UInt64 zresult;
				retval = UInt64.TryParse(value, out zresult);
				result = zresult;
			}
			else if (valueType.IsEnum) // special case
			{
				object zresult;
#if NET_FX_40
				retval = Enum.TryParse(value, out zresult);
				result = zresult;
#else
				// Enum.GetUnderlyingType() not used here
				if (retval = IsValidEnum(valueType, value))
				{
					zresult = ParseEnum(valueType, value);
					result = zresult;
				}
#endif
			}
			else
				throw new ArgumentOutOfRangeException("valueType", string.Format("The value type '{0}' is not supported.", valueType.FullName));

			return retval;
		}

		/// <summary>
		/// 	Converts the specified string representation to its TValue equivalent and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <typeparam name="TValue"> The type to parse the string value. </typeparam>
		/// <param name="value"> A string containing a TValue to convert. </param>
		/// <param name="result"> When this method returns, contains the TValue value equivalent contained in value, if the conversion succeeded, or default(TValue) if the conversion failed. The conversion fails if the value parameter is null, is an empty string, or does not contain a valid string representation of a TValue. This parameter is passed uninitialized. </param>
		/// <returns> A boolean value of true if the value was converted successfully; otherwise, false. </returns>
		public static bool TryParse<TValue>(string value, out TValue result)
		{
			Type valueType;
			object oresult;
			bool retval;

			valueType = typeof(TValue);

			// no null check is intentional
			retval = TryParse(valueType, value, out oresult);

			if ((object)oresult == null)
				result = default(TValue);
			else
				result = (TValue)oresult;

			return retval;
		}

		#endregion
	}
}