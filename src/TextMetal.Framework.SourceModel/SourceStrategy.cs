/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Framework.Core;

namespace TextMetal.Framework.SourceModel
{
	public abstract class SourceStrategy : ISourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SourceStrategy class.
		/// </summary>
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
			finally
			{
			}
			/*catch (Exception ex)
			{
				throw new InvalidOperationException("The source strategy failed (see inner exception).", ex);
			}*/
		}

		#endregion
	}
}