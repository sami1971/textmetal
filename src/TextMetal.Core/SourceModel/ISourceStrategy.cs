/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core.SourceModel
{
	public interface ISourceStrategy
	{
		#region Methods/Operators

		object GetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties);

		#endregion
	}
}