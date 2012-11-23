/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Common.Core.StringTokens
{
	/// <summary>
	/// 	Provides a dynamic token replacement strategy which executes an on-demand callback method to obtain a replacement value.
	/// </summary>
	public class DynamicValueTokenReplacementStrategy : ITokenReplacementStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the DynamicValueTokenReplacementStrategy class.
		/// </summary>
		/// <param name="method"> The callback method to evaluate during token replacement. </param>
		public DynamicValueTokenReplacementStrategy(Func<string[], object> method)
		{
			if ((object)method == null)
				throw new ArgumentNullException("method");

			this.method = method;
		}

		#endregion

		#region Fields/Constants

		private readonly Func<string[], object> method;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the callback method to evaluate during token replacement.
		/// </summary>
		public Func<string[], object> Method
		{
			get
			{
				return this.method;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Used by the token model to execute public, static methods with zero parameters in a dynamic manner.
		/// </summary>
		/// <param name="parameters"> An array of parameters in the form: assembly-qualified-type-name, method-name, [parameters, ...] </param>
		/// <returns> The return value of the executed method. </returns>
		public static object StaticMethodResolver(string[] parameters)
		{
			int index = 0;
			Type targetType = null;
			MethodInfo methodInfo;
			object methodValue = null;

			if ((object)parameters == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			foreach (string parameter in parameters)
			{
				if (DataType.IsNullOrWhiteSpace(parameter))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				if (index == 0)
				{
					targetType = Type.GetType(parameter, false);
					index++;
				}
				else if (index == 1)
				{
					if ((object)targetType == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					methodInfo = targetType.GetMethod(parameter, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null);

					if ((object)methodInfo == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					methodValue = methodInfo.Invoke(null, null);
					index++;
				}
				else
				{
					if ((object)methodValue == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					targetType = methodValue.GetType();
					methodInfo = targetType.GetMethod(parameter, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null);

					if ((object)methodInfo == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					// TODO: Add support for method parameters and type coersion and edit documentation.
					methodValue = methodInfo.Invoke(null, null);
					index++;
				}
			}

			return methodValue;
		}

		/// <summary>
		/// 	Used by the token model to get the value of public, static properties with zero parameters in a dynamic manner.
		/// </summary>
		/// <param name="parameters"> An array of parameters in the form: assembly-qualified-type-name, property-name </param>
		/// <returns> The return value of the property getter. </returns>
		public static object StaticPropertyResolver(string[] parameters)
		{
			int index = 0;
			Type targetType = null;
			PropertyInfo propertyInfo;
			object propertyValue = null;

			if ((object)parameters == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			foreach (string parameter in parameters)
			{
				if (DataType.IsNullOrWhiteSpace(parameter))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				if (index == 0)
				{
					targetType = Type.GetType(parameter, false);
					index++;
				}
				else if (index == 1)
				{
					if ((object)targetType == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					propertyInfo = targetType.GetProperty(parameter, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, null, null, new Type[] { }, null);

					if ((object)propertyInfo == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (!propertyInfo.CanRead)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					propertyValue = propertyInfo.GetValue(null, null);
					index++;
				}
				else
				{
					if ((object)propertyValue == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					targetType = propertyValue.GetType();
					propertyInfo = targetType.GetProperty(parameter, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static, null, null, new Type[] { }, null);

					if ((object)propertyInfo == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					// TODO: Add support for method parameters and type coersion and edit documentation.
					propertyValue = propertyInfo.GetValue(null, null);
					index++;
				}
			}

			return propertyValue;
		}

		/// <summary>
		/// 	Evaluate a token using any parameters specified.
		/// </summary>
		/// <param name="parameters"> Should be null for value semantics; or a valid string array for function semantics. </param>
		/// <returns> An approapriate token replacement value. </returns>
		public object Evaluate(string[] parameters)
		{
			return this.Method(parameters);
		}

		#endregion
	}
}