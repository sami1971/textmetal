/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Expressions;
using TextMetal.Common.Xml;

namespace TextMetal.Framework.ExpressionModel
{
	public abstract class SurfaceConstruct : ExpressionXmlObject, ISurface
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SurfaceConstruct class.
		/// </summary>
		protected SurfaceConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		#endregion
	}
}