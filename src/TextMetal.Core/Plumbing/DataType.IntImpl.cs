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

#if NET_FX_40
	// DO NOTHING
#else
		private static bool IsValidEnum(Type enumType, string value)
		{
			try
			{
				Enum.Parse(enumType, value, true);
			}
			catch (ArgumentNullException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}

			return true;
		}

		private static bool IsValidGuid(string value)
		{
			try
			{
				new Guid(value);
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (FormatException)
			{
				return false;
			}

			return true;
		}

		private static object ParseEnum(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, true);
		}

		private static Guid ParseGuid(string value)
		{
			return new Guid(value);
		}
#endif

		#endregion
	}
}