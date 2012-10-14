using System.Web;
using System.Web.Mvc;

namespace TextMetal.WebHostSample
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}