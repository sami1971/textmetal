/*
	Copyright ©2002-2010 D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Plumbing.RuntimeInterception
{
	/// <summary>
	/// 	Represents a dynamic invocation using reflection.
	/// </summary>
	public interface IDynamicInvocation : IDisposable
	{
		#region Methods/Operators

		/// <summary>
		/// 	Represnts a dynamic invocation of a proxied type member.
		/// </summary>
		/// <param name="proxiedType"> The run-time type of the proxied type (may differ from MethodInfo.DeclaringType). </param>
		/// <param name="invokedMethodInfo"> The MethodInfo of the invoked member. </param>
		/// <param name="proxyInstance"> The proxy object instance. </param>
		/// <param name="invocationParameters"> The parameters passed to the invoked member, if appliable. </param>
		/// <returns> The return value from the invoked member, if appliable. </returns>
		object Invoke(Type proxiedType, MethodInfo invokedMethodInfo, object proxyInstance, object[] invocationParameters);

		#endregion
	}
}