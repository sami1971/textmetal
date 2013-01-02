/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Runtime.Remoting.Messaging;
using System.Web;

namespace TextMetal.Common.Core
{
	/// <summary>
	/// Manages execution path storage of objects in a manner which is safe in standard executables and libraries and ASP.NET code.
	/// </summary>
	public static class ExecutionPathStorage
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets a value indicating if the current application domain is running under ASP.NET.
		/// </summary>
		public static bool IsInHttpContext
		{
			get
			{
				return (object)HttpContext.Current != null;
			}
		}

		#endregion

		#region Methods/Operators

		private static object AspNetGetValue(string key)
		{
			return HttpContext.Current.Items[key];
		}

		private static void AspNetRemoveValue(string key)
		{
			HttpContext.Current.Items.Remove(key);
		}

		private static void AspNetSetValue(string key, object value)
		{
			HttpContext.Current.Items[key] = value;
		}

		private static object CallCtxGetValue(string key)
		{
			return CallContext.GetData(key);
		}

		private static void CallCtxRemoveValue(string key)
		{
			CallContext.FreeNamedDataSlot(key);
		}

		private static void CallCtxSetValue(string key, object value)
		{
			CallContext.SetData(key, value);
		}

		/// <summary>
		/// Gets a named value from the current execution context storage mechanism.
		/// </summary>
		/// <param name="key"> The key to lookup in execution path storage. </param>
		/// <returns> An object value or null. </returns>
		public static object GetValue(string key)
		{
			if (IsInHttpContext)
				return AspNetGetValue(key);
			else
				return CallCtxGetValue(key);
		}

		/// <summary>
		/// Remove a named value from the current execution context storage mechanism.
		/// </summary>
		/// <param name="key"> The key to remove in execution path storage. </param>
		public static void RemoveValue(string key)
		{
			if (IsInHttpContext)
				AspNetRemoveValue(key);
			else
				CallCtxRemoveValue(key);
		}

		/// <summary>
		/// Set a named value in the current execution context storage mechanism.
		/// </summary>
		/// <param name="key"> The key to store in execution path storage. </param>
		/// <param name="value"> An object instance or null. </param>
		public static void SetValue(string key, object value)
		{
			if (IsInHttpContext)
				AspNetSetValue(key, value);
			else
				CallCtxSetValue(key, value);
		}

		#endregion
	}
}