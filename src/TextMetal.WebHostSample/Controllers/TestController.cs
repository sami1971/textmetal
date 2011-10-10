/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml;

using TextMetal.Core;
using TextMetal.Core.ExpressionModel;
using TextMetal.Core.SortModel;
using TextMetal.Core.XmlModel;

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
			//throw new InvalidOperationException("blah");

			return this.View(new
			                 {
			                 	X = new Random().Next()
			                 });
		}

		#endregion
	}
}