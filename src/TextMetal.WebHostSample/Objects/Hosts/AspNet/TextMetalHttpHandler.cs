/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web;

using TextMetal.Core.Plumbing;

namespace TextMetal.WebHostSample.Objects.Hosts.AspNet
{
	public class TextMetalHttpHandler : IHttpHandler
	{
		#region Constructors/Destructors

		public TextMetalHttpHandler()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		public void ProcessRequest(HttpContext context)
		{
			string originalPath;
			string viewFilePath;

			if ((object)context == null)
				throw new ArgumentNullException("context");

			originalPath = context.Request.Path.SafeToString();
			viewFilePath = context.Server.MapPath(originalPath);

			WebHost.Host(viewFilePath, new object(), context.Response.Output);
		}

		#endregion
	}
}