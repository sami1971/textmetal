/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace TextMetal.Framework.Core
{
	/// <summary>
	/// Provides for associative object (dynamic) mechanics.
	/// </summary>
	public interface IAssociativeMechanism
	{
		#region Methods/Operators

		/// <summary>
		/// Gets the enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator or null. </returns>
		IEnumerator GetAssociativeObjectEnumerator();

		/// <summary>
		/// Gets the dictionary enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IDictionaryEnumerator or null. </returns>
		IDictionaryEnumerator GetAssociativeObjectEnumeratorDict();

		/// <summary>
		/// Gets the enumerator (tick one) for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator`1 or null. </returns>
		IEnumerator<KeyValuePair<string, object>> GetAssociativeObjectEnumeratorTickOne();

		/// <summary>
		/// Gets the value of the current associative object instance.
		/// </summary>
		/// <returns> A value or null. </returns>
		object GetAssociativeObjectValue();

		#endregion
	}
}