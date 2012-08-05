/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TextMetal.Core.Plumbing
{
	/// <summary>
	/// 	Provides static helper and/or extension methods for reflection.
	/// </summary>
	public static class Reflexion
	{
		#region Methods/Operators

		/// <summary>
		/// 	Gets all custom attributes of the specified type. If no custom attributes of the specified type are defined, then null is returned.
		/// </summary>
		/// <typeparam name="TAttribute"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </typeparam>
		/// <param name="target"> The target object. </param>
		/// <returns> The custom attributes array or null. </returns>
		public static TAttribute[] GetAllAttributes<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute
		{
			object[] attributes;

			if ((object)target == null)
				throw new ArgumentNullException("target");

			attributes = target.GetCustomAttributes(typeof(TAttribute), true);

			if ((object)attributes != null && attributes.Length != 0 && attributes is TAttribute[])
				return attributes as TAttribute[];
			else
				return null;
		}

		/// <summary>
		/// 	Returns the concatenation of error messages from an exception object. All inner exceptions and collected exceptions (public properties implementing IEnumerable&lt;Exception&gt;) are returned.
		/// </summary>
		/// <param name="exception"> The root exception to get errors. </param>
		/// <param name="indent"> The indent level count. </param>
		/// <returns> A string concatenation of error messages delimited by newlines. </returns>
		public static string GetErrors(Exception exception, int indent)
		{
			PropertyInfo[] propertyInfos;
			Type exceptionType, exceptionEnumerableType;
			string message = "";
			object propertyValue;
			bool first = true;

			exceptionEnumerableType = typeof(IEnumerable<Exception>);

			while ((object)exception != null)
			{
				if (first)
				{
					message +=
						new string('\t', indent) + "+++ BEGIN ROOT EXECPTION +++" + "\r\n" +
						new string('\t', indent) + "Type: " + exception.GetType().FullName + "\r\n" +
						new string('\t', indent) + "Message: " + exception.Message + "\r\n" +
						new string('\t', indent) + "Stack: \r\n" + new string('\t', indent) + exception.StackTrace + "\r\n\r\n";
				}
				else
				{
					message += "\r\n" +
					           new string('\t', indent) + "+++ BEGIN INNER EXECPTION +++" + "\r\n" +
					           new string('\t', indent) + "Type: " + exception.GetType().FullName + "\r\n" +
					           new string('\t', indent) + "Message: " + exception.Message + "\r\n" +
					           new string('\t', indent) + "Stack: \r\n" + new string('\t', indent) + exception.StackTrace + "\r\n\r\n";
				}

				exceptionType = exception.GetType();
				propertyInfos = exceptionType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

				if ((object)propertyInfos != null)
				{
					foreach (PropertyInfo propertyInfo in propertyInfos)
					{
						if (propertyInfo.CanRead &&
						    exceptionEnumerableType.IsAssignableFrom(propertyInfo.PropertyType))
						{
							propertyValue = propertyInfo.GetValue(exception, null);

							message += new string('\t', indent + 1) + "+++ BEGIN ENUMERABLE EXECPTIONS +++" + "\r\n";

							if ((object)propertyValue != null)
							{
								foreach (Exception subException in (IEnumerable<Exception>)propertyValue)
									message += "\r\n" + GetErrors(subException, indent + 1);
							}

							message += "\r\n" + new string('\t', indent + 1) + "+++ END ENUMERABLE EXECPTIONS +++" + "\r\n\r\n";
						}
					}
				}

				if (first)
				{
					message +=
						new string('\t', indent) + "+++ END ROOT EXECPTION +++" + "\r\n";
				}
				else
				{
					message +=
						new string('\t', indent) + "+++ END INNER EXECPTION +++" + "\r\n";
				}

				first = false;
				exception = exception.InnerException;
			}

			return message;
		}

		/// <summary>
		/// 	Attempts to get the property type for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to get the type for. </param>
		/// <param name="propertyType"> An output run-time type of the logical property or null if the logical property lookup failed. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		public static bool GetLogicalPropertyType(object targetInstance, string propertyName, out Type propertyType)
		{
			bool trigger = false;
			Type targetType;
			PropertyInfo propertyInfo;
			IDictionary targetDictionary;
			object propertyValue;

			propertyType = null;

			if ((object)targetInstance == null)
				return false;

			if (DataType.IsNullOrWhiteSpace(propertyName))
				return false;

			// STRATEGY: attempt reflection
			if (!trigger)
			{
				targetType = targetInstance.GetType();

				propertyInfo = targetType.GetLowestProperty(propertyName);

				if ((object)propertyInfo != null)
				{
					propertyType = propertyInfo.PropertyType;
					trigger = true;
				}
			}

			// STRATEGY: attempt association
			if (!trigger)
			{
				targetDictionary = targetInstance as IDictionary;

				if ((object)targetDictionary != null &&
				    targetDictionary.Contains(propertyName))
				{
					propertyValue = targetDictionary[propertyName];

					if ((object)propertyValue != null)
					{
						propertyType = propertyValue.GetType();
						trigger = true;
					}
				}
			}

			return trigger;
		}

		/// <summary>
		/// 	Attempts to get the property value for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to get the value for. </param>
		/// <param name="propertyValue"> An output run-time value of the logical property or null if the logical property lookup failed. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		public static bool GetLogicalPropertyValue(object targetInstance, string propertyName, out object propertyValue)
		{
			bool trigger = false;
			Type targetType;
			PropertyInfo propertyInfo;
			IDictionary targetDictionary;

			propertyValue = null;

			if ((object)targetInstance == null)
				return false;

			if (DataType.IsNullOrWhiteSpace(propertyName))
				return false;

			// STRATEGY: attempt reflection
			if (!trigger)
			{
				targetType = targetInstance.GetType();

				propertyInfo = targetType.GetLowestProperty(propertyName);

				if ((object)propertyInfo != null &&
				    propertyInfo.CanRead)
				{
					propertyValue = propertyInfo.GetValue(targetInstance, null);
					trigger = true;
				}
			}

			// STRATEGY: attempt association
			if (!trigger)
			{
				targetDictionary = targetInstance as IDictionary;

				if ((object)targetDictionary != null &&
				    targetDictionary.Contains(propertyName))
				{
					propertyValue = targetDictionary[propertyName];
					trigger = true;
				}
			}

			return trigger;
		}

		/// <summary>
		/// 	A private extension method used to obtain the least-derived public, instance property of a given name.
		/// </summary>
		/// <param name="propertyType"> The property type to interogate. </param>
		/// <param name="propertyName"> The property name to lookup. </param>
		/// <returns> A PropertyInfo for the least-derived public, instance property by the given name or null if none were found. </returns>
		private static PropertyInfo GetLowestProperty(this Type propertyType, string propertyName)
		{
			PropertyInfo property;

			while ((object)propertyType != null)
			{
				property = propertyType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, null, null, new Type[] { }, null);

				if ((object)property != null)
					return property;

				propertyType = propertyType.BaseType;
			}

			return null;
		}

		/// <summary>
		/// 	Get the single custom attribute of the attribute specified type. If more than one custom attribute exists for the requested type, an InvalidOperationException is thrown. If no custom attributes of the specified type are defined, then null is returned.
		/// </summary>
		/// <typeparam name="TAttribute"> The custom attribute type. </typeparam>
		/// <param name="target"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </param>
		/// <returns> The single custom attribute or null if none are defined. </returns>
		public static TAttribute GetOneAttribute<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute
		{
			TAttribute[] attributes;
			Type attributeType, targetType;

			if ((object)target == null)
				throw new ArgumentNullException("target");

			attributeType = typeof(TAttribute);
			targetType = target.GetType();

			attributes = GetAllAttributes<TAttribute>(target);

			if ((object)attributes == null || attributes.Length == 0)
				return null;
			else if (attributes.Length > 1)
				throw new InvalidOperationException(string.Format("Multiple custom attributes of type '{0}' are defined on type '{1}'.", attributeType.FullName, targetType.FullName));
			else
				return attributes[0];
		}

		/// <summary>
		/// 	Asserts that the custom attribute is not defined on the target. If more than zero custom attributes exist for the requested type, an InvalidOperationException is thrown.
		/// </summary>
		/// <typeparam name="TAttribute"> The custom attribute type. </typeparam>
		/// <param name="target"> The target ICustomAttributeProvider (Assembly, Type, MemberInfo, etc.) </param>
		public static void GetZeroAttributes<TAttribute>(ICustomAttributeProvider target)
			where TAttribute : Attribute
		{
			TAttribute[] attributes;
			Type attributeType, targetType;

			if ((object)target == null)
				throw new ArgumentNullException("target");

			attributeType = typeof(TAttribute);
			targetType = target.GetType();

			attributes = GetAllAttributes<TAttribute>(target);

			if ((object)attributes == null || attributes.Length == 0)
				return;
			else
				throw new InvalidOperationException(string.Format("Some custom attributes of type '{0}' are defined on type '{1}'.", attributeType.FullName, targetType.FullName));
		}

		/// <summary>
		/// 	Transforms a nullable type to its underlying non-nullable equivalent.
		/// </summary>
		/// <param name="conversionType"> The nullable run-time type to transform. </param>
		/// <returns> Teh non-nullbale run-time type. </returns>
		public static Type MakeNonNullableType(Type conversionType)
		{
			Type openNullableType;

			if ((object)conversionType == null)
				throw new ArgumentNullException("conversionType");

			openNullableType = typeof(Nullable<>);

			if (conversionType.IsGenericType &&
			    !conversionType.IsGenericTypeDefinition &&
			    conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return conversionType.GetGenericArguments()[0];

			if (conversionType.IsValueType)
				return conversionType;

			return null; // reference types cannot be non-nullable
		}

		/// <summary>
		/// 	Transforms a nullable type to its underlying non-nullable equivalent.
		/// </summary>
		/// <param name="conversionType"> The nullable run-time type to transform. </param>
		/// <returns> Teh non-nullbale run-time type. </returns>
		public static Type MakeNullableType(Type conversionType)
		{
			Type openNullableType, closedNullableType;

			if ((object)conversionType == null)
				throw new ArgumentNullException("conversionType");

			openNullableType = typeof(Nullable<>);

			if (!conversionType.IsValueType)
				return conversionType;

			if (conversionType.IsGenericType &&
			    !conversionType.IsGenericTypeDefinition &&
			    conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return conversionType;

			closedNullableType = openNullableType.MakeGenericType(conversionType);

			return closedNullableType;
		}

		/// <summary>
		/// 	Attempts to set the property value for a logical property (CLR, associative, etc.). This overload assume stayHard=false and makeSoft=true semantics.
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to set the value for. </param>
		/// <param name="propertyValue"> The value of the logical property to set or null. </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not. </returns>
		public static bool SetLogicalPropertyValue(object targetInstance, string propertyName, object propertyValue)
		{
			return SetLogicalPropertyValue(targetInstance, propertyName, propertyValue, false, true);
		}

		/// <summary>
		/// 	Attempts to set the property value for a logical property (CLR, associative, etc.).
		/// </summary>
		/// <param name="targetInstance"> The target instance to search for a logical property. </param>
		/// <param name="propertyName"> The logical property name to set the value for. </param>
		/// <param name="propertyValue"> The value of the logical property to set or null. </param>
		/// <param name="stayHard"> Force only 'hard' object semantics and not use associative lookup (i.e. the target instance must be a real CLR object). </param>
		/// <param name="makeSoft"> Allow making 'soft' object semantics (i.e. the target instance could be an associative object). </param>
		/// <returns> A value indicating whether the logical property name lookup was successful or not; lookup respects the 'stayHard' and 'makeSoft' flags. </returns>
		public static bool SetLogicalPropertyValue(object targetInstance, string propertyName, object propertyValue, bool stayHard, bool makeSoft)
		{
			bool trigger = false;
			Type targetType;
			PropertyInfo propertyInfo;
			IDictionary targetDictionary;

			if ((object)targetInstance == null)
				return false;

			if (DataType.IsNullOrWhiteSpace(propertyName))
				return false;

			// STRATEGY: attempt reflection
			if (!trigger)
			{
				targetType = targetInstance.GetType();

				propertyInfo = targetType.GetLowestProperty(propertyName);

				if ((object)propertyInfo != null &&
				    propertyInfo.CanWrite)
				{
					propertyValue = DataType.ChangeType(propertyValue, propertyInfo.PropertyType);
					propertyInfo.SetValue(targetInstance, propertyValue, null);
					trigger = true;
				}
			}

			// STRATEGY: attempt association
			if (!trigger && !stayHard)
			{
				targetDictionary = targetInstance as IDictionary;

				if ((object)targetDictionary != null)
				{
					if (!makeSoft && !targetDictionary.Contains(propertyName))
						return false;

					if (targetDictionary.Contains(propertyName))
						targetDictionary.Remove(propertyName);

					targetDictionary.Add(propertyName, propertyValue);
					trigger = true;
				}
			}

			return trigger;
		}

		#endregion
	}
}