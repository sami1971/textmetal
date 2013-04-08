/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Web.Routing;

namespace TextMetal.HostImpl.AspNetSample.Objects
{
	/// <summary>
	/// http://www.coderjournal.com/2008/03/force-mvc-route-url-lowercase/
	/// </summary>
	public class LowercaseRoute : Route
	{
		#region Constructors/Destructors

		public LowercaseRoute(string url, IRouteHandler routeHandler)
			: base(url, routeHandler)
		{
		}

		public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
			: base(url, defaults, routeHandler)
		{
		}

		public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
			: base(url, defaults, constraints, routeHandler)
		{
		}

		public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
			: base(url, defaults, constraints, dataTokens, routeHandler)
		{
		}

		#endregion

		#region Methods/Operators

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			VirtualPathData path = base.GetVirtualPath(requestContext, values);

			if ((object)path != null)
				path.VirtualPath = path.VirtualPath.ToLowerInvariant();

			return path;
		}

		#endregion
	}
}