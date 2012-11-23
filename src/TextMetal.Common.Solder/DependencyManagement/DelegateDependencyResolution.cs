/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// 	Provides the Factory Method pattern used to resolve dependencies.
	/// </summary>
	public sealed class DelegateDependencyResolution : IDependencyResolution
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the DelegateDependencyResolution class.
		/// </summary>
		/// <param name="method"> The callback method to execute during resolution. </param>
		public DelegateDependencyResolution(Delegate method)
		{
			if ((object)method == null)
				throw new ArgumentNullException("method");

			this.method = method;
		}

		#endregion

		#region Fields/Constants

		private readonly Delegate method;

		#endregion

		#region Properties/Indexers/Events

		private Delegate Method
		{
			get
			{
				return this.method;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Gets an instance of DelegateDependencyResolution from the specified Func`1 delegate.
		/// </summary>
		/// <typeparam name="TObject"> The target type of resolution. </typeparam>
		/// <param name="func"> The callback method to execute during resolution. </param>
		/// <returns> A DelegateDependencyResolution instance. </returns>
		public static DelegateDependencyResolution FromFunc<TObject>(Func<TObject> func)
		{
			if ((object)func == null)
				throw new ArgumentNullException("func");

			return new DelegateDependencyResolution(func);
		}

		/// <summary>
		/// 	Resolves a dependency.
		/// </summary>
		/// <returns> An instance of an object or null. </returns>
		public object Resolve()
		{
			return this.Method.DynamicInvoke(null);
		}

		#endregion
	}
}