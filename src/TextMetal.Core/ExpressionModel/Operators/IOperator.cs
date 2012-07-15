/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators
{
	/// <summary>
	/// 	Defines the contract an operator must expose.
	/// </summary>
	public interface IOperator
	{
		#region Methods/Operators

		/// <summary>
		/// 	Gets the mneumonic for the current operator.
		/// </summary>
		/// <returns> An instance of the OperatorMneumonicAttribute. </returns>
		OperatorMneumonicAttribute GetMneumonic();

		#endregion
	}
}