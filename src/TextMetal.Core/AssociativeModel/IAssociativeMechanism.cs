/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

namespace TextMetal.Core.AssociativeModel
{
	public interface IAssociativeMechanism
	{
		#region Methods/Operators

		IEnumerator GetAssociativeObjectEnumerator();

		object GetAssociativeObjectValue();

		#endregion
	}
}