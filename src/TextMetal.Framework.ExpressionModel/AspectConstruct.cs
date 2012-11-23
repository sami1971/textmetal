/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "Aspect", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class AspectConstruct : SurfaceConstruct
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the AspectConstruct class.
		/// </summary>
		public AspectConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string alias;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "alias", NamespaceUri = "")]
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
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