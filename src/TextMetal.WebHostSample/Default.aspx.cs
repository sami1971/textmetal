/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace TextMetal.WebHostSample
{
	public partial class _Default : Page
	{
		#region Methods/Operators

		public void Page_Load(object sender, EventArgs e)
		{
			// Change the current path so that the Routing handler can correctly interpret
			// the request, then restore the original path so that the OutputCache module
			// can correctly process the response (if caching is enabled).

			string originalPath = this.Request.Path;
			HttpContext.Current.RewritePath(this.Request.ApplicationPath, false);
			IHttpHandler httpHandler = new MvcHttpHandler();
			httpHandler.ProcessRequest(HttpContext.Current);
			HttpContext.Current.RewritePath(originalPath, false);
		}

		#endregion
	}
}