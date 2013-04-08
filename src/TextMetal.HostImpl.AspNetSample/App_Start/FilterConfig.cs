/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Web.Mvc;

namespace TextMetal.HostImpl.AspNetSample.App_Start
{
	public class FilterConfig
	{
		#region Methods/Operators

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		#endregion
	}
}