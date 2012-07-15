/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Web.Mvc;

using TextMetal.Core.Plumbing;

namespace TextMetal.WebHostSample.Objects.Hosts.AspNet
{
	public class TextMetalViewEngine : VirtualPathProviderViewEngine
	{
		#region Constructors/Destructors

		public TextMetalViewEngine()
		{
			base.MasterLocationFormats = new string[]
			                             {
			                             	"~/Views/{1}/{0}.master",
			                             	"~/Views/Shared/{0}.master"
			                             };

			base.AreaMasterLocationFormats = new string[]
			                                 {
			                                 	"~/Areas/{2}/Views/{1}/{0}.master",
			                                 	"~/Areas/{2}/Views/Shared/{0}.master"
			                                 };

			base.ViewLocationFormats = new string[]
			                           {
			                           	"~/Views/{1}/{0}.tm",
			                           	"~/Views/{1}/{0}.aspx",
			                           	"~/Views/{1}/{0}.ascx",
			                           	"~/Views/Shared/{0}.tm",
			                           	"~/Views/Shared/{0}.aspx",
			                           	"~/Views/Shared/{0}.ascx"
			                           };

			base.AreaViewLocationFormats = new string[]
			                               {
			                               	"~/Areas/{2}/Views/{1}/{0}.tm",
			                               	"~/Areas/{2}/Views/{1}/{0}.aspx",
			                               	"~/Areas/{2}/Views/{1}/{0}.ascx",
			                               	"~/Areas/{2}/Views/Shared/{0}.tm",
			                               	"~/Areas/{2}/Views/Shared/{0}.aspx",
			                               	"~/Areas/{2}/Views/Shared/{0}.ascx"
			                               };

			base.PartialViewLocationFormats = base.ViewLocationFormats;
			base.AreaPartialViewLocationFormats = base.AreaViewLocationFormats;
		}

		#endregion

		#region Methods/Operators

		public static void CallMeInGlobalAsax()
		{
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new TextMetalViewEngine());
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
		{
			if ((object)controllerContext == null)
				throw new ArgumentNullException("controllerContext");

			if ((object)partialPath == null)
				throw new ArgumentNullException("partialPath");

			if (DataType.IsWhiteSpace(partialPath))
				throw new ArgumentOutOfRangeException("partialPath");

			if (partialPath.EndsWith(".tm"))
				return new TextMetalView(partialPath);
			else
				return new WebFormView(partialPath, null);
		}

		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
		{
			if ((object)controllerContext == null)
				throw new ArgumentNullException("controllerContext");

			if ((object)viewPath == null)
				throw new ArgumentNullException("viewPath");

			if ((object)masterPath == null)
				throw new ArgumentNullException("masterPath");

			if (DataType.IsWhiteSpace(viewPath))
				throw new ArgumentOutOfRangeException("viewPath");

			if (viewPath.EndsWith(".tm") && DataType.IsNullOrWhiteSpace(masterPath))
				return new TextMetalView(viewPath);
			else if (viewPath.EndsWith(".tm") && !String.IsNullOrEmpty(masterPath))
				return new TextMetalView(viewPath, masterPath);
			else
				return new WebFormView(viewPath, masterPath);
		}

		protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
		{
			if ((object)controllerContext == null)
				throw new ArgumentNullException("controllerContext");

			if ((object)virtualPath == null)
				throw new ArgumentNullException("virtualPath");

			return base.FileExists(controllerContext, virtualPath);
		}

		#endregion
	}
}