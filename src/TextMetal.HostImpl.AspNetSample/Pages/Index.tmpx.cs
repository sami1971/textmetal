/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using TextMetal.HostImpl.AspNetSample.Objects.Model;

namespace TextMetal.HostImpl.AspNetSample.Pages
{
	public class Index : ISourceStrategy
	{
		#region Methods/Operators

		public object GetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			Repository repository;

			repository = new Repository();

			repository.TryWriteEventLogEntry(Guid.NewGuid().ToString());

			var list = repository.FindEventLogs(new Query(new NullaryExpressionConstruct(), new Order[] { }, 0, 0)).ToList();
			var ct = list.Count();

			list.ForEach(el => repository.DiscardEventLog(el));

			return new { Y = "deez nizzles", CT = ct };
		}

		#endregion
	}
}