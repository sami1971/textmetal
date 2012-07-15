/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using TextMetal.Core.Plumbing;
using TextMetal.WebHostSample.Objects;
using TextMetal.WebHostSample.Objects.Hosts.AspNet;
using TextMetal.WebHostSample.Objects.Model;

namespace TextMetal.WebHostSample
{
	public class MvcApplication : HttpApplication
	{
		#region Fields/Constants

		private static readonly IRepository repository = new Repository();

		#endregion

		#region Properties/Indexers/Events

		public static IRepository Repository
		{
			get
			{
				return repository;
			}
		}

		#endregion

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

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception ex;
			HttpException httpEx;

			ex = this.Server.GetLastError();

			if ((object)ex == null)
				return;

			httpEx = ex as HttpException;

			if ((object)httpEx != null)
			{
				if (httpEx.GetHttpCode() == 404)
					return; // exclude Page Not Founds...
			}

			if (ex is HttpUnhandledException)
				ex = ex.InnerException ?? ex;

			Repository.TryWriteEventLogEntry(Reflexion.GetErrors(ex, 0));
			Repository.TrySendEmailTemplate(EmailTemplateResourceNames.EVENT_LOG, new
			                                                                      {
			                                                                      	Error = Reflexion.GetErrors(ex, 0)
			                                                                      });
		}

		protected void Application_Start()
		{
			TextMetalViewEngine.CallMeInGlobalAsax();

			RegisterRoutes(RouteTable.Routes);
		}

		#endregion
	}
}