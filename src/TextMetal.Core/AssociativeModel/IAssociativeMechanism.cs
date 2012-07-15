/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// 	Provides for associative object (dynamic) mechanics.
	/// </summary>
	public interface IAssociativeMechanism
	{
		#region Methods/Operators

		IEnumerator GetAssociativeObjectEnumerator();

		IDictionaryEnumerator GetAssociativeObjectEnumeratorDict();

		IEnumerator<KeyValuePair<string, object>> GetAssociativeObjectEnumeratorTickOne();

		object GetAssociativeObjectValue();

		#endregion
	}
}