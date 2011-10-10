/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	[XmlElementMapping(LocalName = "Facet", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class FacetConstruct : SurfaceConstruct
	{
		#region Constructors/Destructors

		public FacetConstruct()
		{
		}

		#endregion

		#region Methods/Operators

		public override object EvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			object obj;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if (!dynamicWildcardTokenReplacementStrategy.GetByPath(this.Name, out obj))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message | facet not found on model");

			return obj;
		}

		#endregion
	}
}