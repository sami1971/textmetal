/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Common.Expressions
{
	/// <summary>
	/// Represents a surface.
	/// </summary>
	public interface ISurface : IExpression
	{
		#region Properties/Indexers/Events

		string Name
		{
			get;
		}

		#endregion
	}
}