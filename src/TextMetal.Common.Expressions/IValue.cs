/*
	Copyright �2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Common.Expressions
{
	/// <summary>
	/// Represents a value.
	/// </summary>
	public interface IValue : IExpression
	{
		#region Properties/Indexers/Events

		object __
		{
			get;
		}

		#endregion
	}
}