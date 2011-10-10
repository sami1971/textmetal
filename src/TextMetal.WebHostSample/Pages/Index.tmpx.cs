/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using TextMetal.Core.SourceModel;
using TextMetal.WebHostSample.Objects.Model;

namespace TextMetal.WebHostSample.Pages
{
	public class Index : ISourceStrategy
	{
		#region Methods/Operators

		public object GetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			Repository repository;

			repository = new Repository();

			repository.TryWriteEventLogEntry(Guid.NewGuid().ToString());

			repository.FindEventLogs(null).ToList().ForEach(el => repository.DiscardEventLog(el));

			return new { Y = "deez nizzles" };
		}

		#endregion
	}
}