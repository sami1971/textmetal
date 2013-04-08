/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// Marks an assembly as containing one or more types containing dependency registration methods. Marks a class type as containing one or more dependency registration methods. Marks a public, static void(void) method as a dependency registration method. This attribute supports automatic application domain configuration of dependency resolution serivces.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class DependencyRegistrationAttribute : Attribute
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DependencyRegistrationAttribute class.
		/// </summary>
		public DependencyRegistrationAttribute()
		{
		}

		#endregion
	}
}