using System.Web.Mvc;

namespace TextMetal.WebHostSample
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