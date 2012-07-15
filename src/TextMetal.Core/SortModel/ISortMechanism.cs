/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.SortModel
{
	/// <summary>
	/// 	Provides for sort mechanics.
	/// </summary>
	public interface ISortMechanism
	{
		#region Methods/Operators

		IEnumerable EvaluateSort(TemplatingContext templatingContext, IEnumerable values);

		#endregion
	}
}