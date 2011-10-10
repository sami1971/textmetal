/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;

using NMock2;

namespace TextMetal.Core.UnitTests.TestingFramework
{
	/// <summary>
	/// Captures the value matched forlater examination.
	/// </summary>
	/// <typeparam name="TCaptureValue">The type of the value to capture.</typeparam>
	public class CaptureValueMatcher<TCaptureValue> : Matcher
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the CaptureValueMatcher`1 class.
		/// </summary>
		public CaptureValueMatcher()
		{
		}

		#endregion

		#region Fields/Constants

		private TCaptureValue capturedValue;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Event raised when the value is captured on this instance.
		/// </summary>
		public event EventHandler ValueCaptured;

		/// <summary>
		/// Gets the captured value.
		/// </summary>
		public TCaptureValue CapturedValue
		{
			get
			{
				return this.capturedValue;
			}
			private set
			{
				this.capturedValue = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Not documented on purpose.
		/// </summary>
		/// <param name="writer">Not documented on purpose.</param>
		public override void DescribeTo(TextWriter writer)
		{
		}

		/// <summary>
		/// Not documented on purpose.
		/// </summary>
		/// <param name="o">Not documented on purpose.</param>
		/// <returns>Not documented on purpose.</returns>
		public override bool Matches(object o)
		{
			this.CapturedValue = (TCaptureValue)o;

			if ((object)this.ValueCaptured != null)
				this.ValueCaptured(this, EventArgs.Empty);

			return true;
		}

		#endregion
	}
}