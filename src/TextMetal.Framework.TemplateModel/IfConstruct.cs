/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;
using TextMetal.Framework.ExpressionModel;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "If", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class IfConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the IfConstruct class.
		/// </summary>
		public IfConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private ExpressionContainerConstruct condition;
		private TemplateContainerConstruct @false;
		private TemplateContainerConstruct @true;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Condition", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ExpressionContainerConstruct Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "False", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public TemplateContainerConstruct False
		{
			get
			{
				return this.@false;
			}
			set
			{
				this.@false = value;
			}
		}

		protected override bool IsScopeBlock
		{
			get
			{
				return true;
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "True", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public TemplateContainerConstruct True
		{
			get
			{
				return this.@true;
			}
			set
			{
				this.@true = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			object obj;
			bool conditional;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			if ((object)this.Condition == null)
				return;

			obj = this.Condition.EvaluateExpression(templatingContext);

			if ((object)obj != null && !(obj is bool) && !(obj is bool?))
				throw new InvalidOperationException(string.Format("The for condition expression has evaluated to a non-null value with an unsupported type '{0}'; only '{1}' and '{2}' types are supported.", obj.GetType().FullName, typeof(bool).FullName, typeof(bool?).FullName));

			conditional = ((bool)(obj ?? false));

			if (conditional)
			{
				if ((object)this.True != null)
					this.True.ExpandTemplate(templatingContext);
			}
			else
			{
				if ((object)this.False != null)
					this.False.ExpandTemplate(templatingContext);
			}
		}

		#endregion
	}
}