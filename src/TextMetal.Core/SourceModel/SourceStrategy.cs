/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace TextMetal.Core.SourceModel
{
	public abstract class SourceStrategy : ISourceStrategy
	{
		#region Constructors/Destructors

		protected SourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties);

		public object GetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			try
			{
				return this.CoreGetSourceObject(sourceFilePath, properties);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The source strategy failed (see inner exception).", ex);
			}
		}

		#endregion
	}
}