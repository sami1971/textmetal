/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "If", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class IfConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Condition", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "False", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "True", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
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
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

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