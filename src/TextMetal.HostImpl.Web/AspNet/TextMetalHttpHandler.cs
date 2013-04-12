/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web;

using TextMetal.Common.Core;

namespace TextMetal.HostImpl.Web.AspNet
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

			new WebHost().Host(viewFilePath, new object(), context.Response.Output);
		}

		#endregion
	}
}