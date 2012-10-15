using System;
using System.Web.Mvc;
using System.Web.Routing;

using TextMetal.WebHostSample.Objects;

namespace TextMetal.WebHostSample
{
	public class RouteConfig
	{
		#region Methods/Operators

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.Add("Default", new LowercaseRoute("{controller}/{action}/{id}", new MvcRouteHandler())
			                      {
				                      Defaults = new RouteValueDictionary()
				                                 {
					                                 { "controller", "Test" },
					                                 { "action", "Index" },
					                                 { "id", "" }
				                                 }
			                      });
		}

		#endregion
	}
}