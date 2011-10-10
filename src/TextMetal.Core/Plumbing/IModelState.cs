/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.Plumbing
{
	public interface IModelState
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets or sets a tri-state value indicating whether the current model instance is 
		/// TRUE: new (never been persisted) or FALSE: old (has been persisted).
		/// </summary>
		bool IsNew
		{
			get;
			set;
		}

		#endregion
	}
}