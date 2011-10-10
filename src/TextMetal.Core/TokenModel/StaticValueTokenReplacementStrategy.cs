/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.TokenModel
{
	/// <summary>
	/// Provides a static token replacement strategy which returns
	/// the same static value each request.
	/// </summary>
	public class StaticValueTokenReplacementStrategy : ITokenReplacementStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the StaticValueTokenReplacementStrategy class.
		/// </summary>
		/// <param name="value">The static value to evaluate during token replacement.</param>
		public StaticValueTokenReplacementStrategy(object value)
		{
			this.value = value;
		}

		#endregion

		#region Fields/Constants

		private readonly object value;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the static value to evaluate during token replacement.
		/// </summary>
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Evaluate a token using any parameters specified.
		/// </summary>
		/// <param name="parameters">Should be null for value semantics; or a string object array for function semantics.</param>
		/// <returns>An approapriate token replacement value.</returns>
		public object Evaluate(string[] parameters)
		{
			return this.Value;
		}

		#endregion
	}
}