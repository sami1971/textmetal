/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web.Mvc;

namespace TextMetal.WebHostSample.Controllers
{
	[HandleError]
	public class TestController : Controller
	{
		#region Constructors/Destructors

		public TestController()
		{
		}

		#endregion

		#region Methods/Operators

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionName("index")]
		public ActionResult IndexGet()
		{
			return this.View(new
			                 {
			                 	X = new Random().Next()
			                 });
		}

		#endregion
	}
}