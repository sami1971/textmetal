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
	[XmlElementMapping(LocalName = "Aspect", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class AspectConstruct : SurfaceConstruct
	{
		#region Constructors/Destructors

		public AspectConstruct()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreEvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			return this;
		}

		#endregion
	}
}