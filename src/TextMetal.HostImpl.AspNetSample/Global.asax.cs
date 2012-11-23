/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using TextMetal.HostImpl.AspNetSample.App_Start;
using TextMetal.HostImpl.AspNetSample.Objects;
using TextMetal.HostImpl.AspNetSample.Objects.Model;

namespace TextMetal.HostImpl.AspNetSample
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

			AreaRegistration.RegisterAllAreas();

			//WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		#endregion
	}
}