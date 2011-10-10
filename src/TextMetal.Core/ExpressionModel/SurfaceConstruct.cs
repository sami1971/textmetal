/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	public abstract class SurfaceConstruct : XmlSterileObject<ExpressionContainerConstruct>, IExpressionXmlObject
	{
		#region Constructors/Destructors

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

		#region Methods/Operators

		public abstract object EvaluateExpression(TemplatingContext templatingContext);

		#endregion
	}
}