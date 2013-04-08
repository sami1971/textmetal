#if !__MonoCS__ && DEFINE_ASP_NET_MVC_VERSION_40
using System;
using System.Web.Http;

namespace TextMetal.HostImpl.AspNetSample.App_Start
{
	public static class WebApiConfig
	{
		#region Methods/Operators

		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new
				          {
					          id = RouteParameter.Optional
				          }
				);
		}

		#endregion
	}
}

#endif