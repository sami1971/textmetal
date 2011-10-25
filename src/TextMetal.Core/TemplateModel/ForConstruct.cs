﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "For", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ForConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public ForConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private TemplateContainerConstruct body;
		private ExpressionContainerConstruct condition;
		private ExpressionContainerConstruct initializer;
		private ExpressionContainerConstruct iterator;
		private string varIx;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Body", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public TemplateContainerConstruct Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

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

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Intializer", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		protected override bool IsScopeBlock
		{
			get
			{
				return true;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "Iterator", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct Iterator
		{
			get
			{
				return this.iterator;
			}
			set
			{
				this.iterator = value;
			}
		}

		[XmlAttributeMapping(LocalName = "var-ix", NamespaceUri = "")]
		public string VarIx
		{
			get
			{
				return this.varIx;
			}
			set
			{
				this.varIx = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			const uint MAX_ITERATIONS_INFINITE_LOOP_CHECK = 999999;
			string varIx;
			uint index = 1; // one-based
			object value;
			bool shouldLoop;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			varIx = templatingContext.Tokenizer.ExpandTokens(this.VarIx, dynamicWildcardTokenReplacementStrategy);

			if (!DataType.IsNullOrWhiteSpace(varIx))
			{
				new AllocConstruct()
				{
					Token = varIx
				}.ExpandTemplate(templatingContext);
			}

			if ((object)this.Initializer != null)
				value = this.Initializer.EvaluateExpression(templatingContext);

			while (true)
			{
				if (index > MAX_ITERATIONS_INFINITE_LOOP_CHECK)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message | MAX_ITERATIONS_INFINITE_LOOP_CHECK");

				if ((object)this.Condition != null)
					value = this.Condition.EvaluateExpression(templatingContext);
				else
					value = false;

				if ((object)value != null && !(value is bool) && !(value is bool?))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				shouldLoop = ((bool)(value ?? false));

				if (!shouldLoop)
					break;

				if (!DataType.IsNullOrWhiteSpace(varIx))
				{
					ExpressionContainerConstruct expressionContainerConstruct;
					ValueConstruct valueConstruct;

					expressionContainerConstruct = new ExpressionContainerConstruct();

					valueConstruct = new ValueConstruct()
					                 {
					                 	Type = typeof(int).FullName,
					                 	__ = index
					                 };

					expressionContainerConstruct.Content = valueConstruct;

					new AssignConstruct()
					{
						Token = varIx,
						Expression = expressionContainerConstruct
					}.ExpandTemplate(templatingContext);
				}

				if ((object)this.Body != null)
					this.Body.ExpandTemplate(templatingContext);

				if ((object)this.Iterator != null)
					value = this.Iterator.EvaluateExpression(templatingContext);

				index++;
			}

			if (!DataType.IsNullOrWhiteSpace(varIx))
			{
				new FreeConstruct()
				{
					Token = varIx
				}.ExpandTemplate(templatingContext);
			}
		}

		#endregion
	}
}