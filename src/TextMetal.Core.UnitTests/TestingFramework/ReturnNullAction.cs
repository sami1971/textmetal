/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

using NMock2;
using NMock2.Monitoring;

namespace TextMetal.Core.UnitTests.TestingFramework
{
	/// <summary>
	/// 	Circumvents nullable issue in NMock2.
	/// </summary>
	public class ReturnNullAction : IAction
	{
		#region Constructors/Destructors

		private ReturnNullAction()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly ReturnNullAction instance = new ReturnNullAction();

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the singleton instance.
		/// </summary>
		public static ReturnNullAction Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Not documented on purpose.
		/// </summary>
		/// <param name="writer"> Not documented on purpose. </param>
		public void DescribeTo(TextWriter writer)
		{
		}

		/// <summary>
		/// 	Not documented on purpose.
		/// </summary>
		/// <param name="invocation"> Not documented on purpose. </param>
		public void Invoke(Invocation invocation)
		{
		}

		#endregion
	}
}