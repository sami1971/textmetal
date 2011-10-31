/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextMetal.Core
{
	internal static class Name
	{
		#region Fields/Constants

		private const string CSHARP_ID_REGEX = @"[a-zA-Z_\@][a-zA-Z_0-9]{0,1023}";

		#endregion

		#region Properties/Indexers/Events

		public static string CSharpIdentifierRegEx
		{
			get
			{
				return CSHARP_ID_REGEX;
			}
		}

		#endregion

		#region Methods/Operators

		public static string GetCamelCase(string value)
		{
			StringBuilder sb;
			char prev;
			int i = 0;
			bool toupper = false;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			value = GetValidCSharpIdentifier(value);

			if (value.Length < 1)
				return value;

			sb = new StringBuilder();
			prev = value[0];

			foreach (char curr in value)
			{
				if (curr == '_')
				{
					toupper = true;
					continue; // ignore setting prev=curr
				}
				
				toupper = toupper || char.IsDigit(prev) || (char.IsLower(prev) && char.IsUpper(curr));

				if (toupper)
					sb.Append(char.ToUpper(curr));
				else
					sb.Append(char.ToLower(curr));

				prev = curr;
				i++;
				toupper = false;
			}

			return sb.ToString();
		}

		public static string GetConstantCase(string value)
		{
			StringBuilder sb;
			char prev;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			value = GetValidCSharpIdentifier(value);

			if (value.Length < 1)
				return value;

			sb = new StringBuilder();
			prev = value[0];

			foreach (char curr in value)
			{
				if (curr == '_')
					continue;

				if (char.IsLower(prev) && char.IsUpper(curr))
					sb.Append('_');

				sb.Append(char.ToUpper(curr));
				prev = curr;
			}

			return sb.ToString();
		}

		public static string GetPascalCase(string value)
		{
			StringBuilder sb;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			value = GetValidCSharpIdentifier(value);

			if (value.Length < 1)
				return value;

			value = GetCamelCase(value);
			sb = new StringBuilder(value);

			sb[0] = char.ToUpper(sb[0]);

			return sb.ToString();
		}

		public static string GetPluralForm(string value)
		{
			if ((object)value == null)
				throw new ArgumentNullException("value");

			value = GetValidCSharpIdentifier(value);

			if (value.Length < 1)
				return value;

			if ((value.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
			     value.EndsWith("ch", StringComparison.OrdinalIgnoreCase)) ||
			    (value.EndsWith("ss", StringComparison.OrdinalIgnoreCase) ||
			     value.EndsWith("sh", StringComparison.OrdinalIgnoreCase)))
			{
				value = value + "es";
				return value;
			}

			if ((value.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
			     (value.Length > 1)) &&
			    !IsVowel(value[value.Length - 2]))
			{
				value = value.Remove(value.Length - 1, 1);
				value = value + "ies";
				return value;
			}

			if (!value.EndsWith("s", StringComparison.OrdinalIgnoreCase))
				value = value + "s";

			return value;
		}

		public static string GetSingularForm(string value)
		{
			if ((object)value == null)
				throw new ArgumentNullException("value");

			value = GetValidCSharpIdentifier(value);

			if (value.Length < 1)
				return value;

			if (string.Compare(value, "series", StringComparison.OrdinalIgnoreCase) != 0)
			{
				if (string.Compare(value, "wines", StringComparison.OrdinalIgnoreCase) == 0)
					return value.Remove(value.Length - 1, 1);

				if ((value.Length > 3) && value.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
				{
					if (!IsVowel(value[value.Length - 4]))
					{
						value = value.Remove(value.Length - 3, 3);
						value = value + 'y';
					}
					return value;
				}

				if (value.EndsWith("ees", StringComparison.OrdinalIgnoreCase))
				{
					value = value.Remove(value.Length - 1, 1);
					return value;
				}

				if ((value.EndsWith("ches", StringComparison.OrdinalIgnoreCase) ||
				     value.EndsWith("xes", StringComparison.OrdinalIgnoreCase)) ||
				    value.EndsWith("sses", StringComparison.OrdinalIgnoreCase))
				{
					value = value.Remove(value.Length - 2, 2);
					return value;
				}

				if (string.Compare(value, "gas", StringComparison.OrdinalIgnoreCase) == 0)
					return value;

				if (((value.Length > 1) && value.EndsWith("s", StringComparison.OrdinalIgnoreCase)) &&
				    (!value.EndsWith("ss", StringComparison.OrdinalIgnoreCase) &&
				     !value.EndsWith("us", StringComparison.OrdinalIgnoreCase)))
					value = value.Remove(value.Length - 1, 1);
			}
			return value;
		}

		private static string GetValidCSharpIdentifier(string value)
		{
			StringBuilder sb;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			sb = new StringBuilder();

			foreach (char curr in value)
			{
				if (char.IsLetterOrDigit(curr) || curr == '_')
					sb.Append(curr);
				else if (curr == ' ')
					sb.Append('_');
			}

			return sb.ToString();
		}

		public static bool IsValidCSharpIdentifier(string value)
		{
			return Regex.IsMatch(value, CSharpIdentifierRegEx, RegexOptions.IgnorePatternWhitespace);
		}

		private static bool IsVowel(char ch)
		{
			switch (ch)
			{
				case 'A':
				case 'E':
				case 'I':
				case 'O':
				case 'U':
				case 'Y':
					return true;
				case 'a':
				case 'e':
				case 'i':
				case 'o':
				case 'u':
				case 'y':
					return true;
				default:
					return false;
			}
		}

		#endregion
	}
}