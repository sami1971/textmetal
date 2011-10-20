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
using TextMetal.Core.QueryModel;
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
		
		[AcceptVerbs(HttpVerbs.Get)]
		[ActionName("query")]
		public ActionResult QueryGet()
		{
			IXmlPersistEngine xpe;

			xpe = new XmlPersistEngine();
			xpe.RegisterWellKnownConstructs();

			var select = new SelectConstruct();

			var expression = new NullaryExpressionConstruct();

			var projection = new ProjectionConstruct()
			                 {
			                 	Alias = ""
			                 };
			projection.TheExpression = new ExpressionContainerConstruct();
			projection.TheExpression.Content = expression;

			select.Projections = new ProjectionContainerConstruct();
			select.Projections.Distinct = false;
			select.Projections.Type = "Table1";
			select.Projections.Items.Add(projection);

			select.JoinSource = new JoinSourceContainerConstruct();

			var table = new TableConstruct()
			            {
			            	Name = "",
			            	Alias = "",
			            	Join = JoinType.Undefined
			            };

			select.JoinSource.Items.Add(table);

			table = new TableConstruct()
			        {
			        	Name = "",
			        	Alias = "",
			        	Join = JoinType.LeftOuter
			        };

			table.OnExpression = new ExpressionContainerConstruct();
			table.OnExpression.Content = expression;

			select.JoinSource.Items.Add(table);

			select.WhereExpression = new ExpressionContainerConstruct();
			select.WhereExpression.Content = expression;

			select.OrderBySort = new SortContainerConstruct();
			select.OrderBySort.Items.Add(new AscendingConstruct());

			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xpe.SerializeToXml(select, xmlTextWriter);
					xmlTextWriter.Flush();
				}

				return this.Content(stringWriter.ToString(), "text/plain", Encoding.UTF8);
			}
		}

		#endregion
	}
}