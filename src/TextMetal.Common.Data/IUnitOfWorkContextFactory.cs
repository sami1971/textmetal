/*
	Copyright �2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Data
{
	public interface IUnitOfWorkContextFactory
	{
		#region Methods/Operators

		IUnitOfWorkContext GetUnitOfWorkContext();

		#endregion
	}
}