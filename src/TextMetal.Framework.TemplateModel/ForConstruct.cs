/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;
using TextMetal.Framework.ExpressionModel;

namespace TextMetal.Framework.TemplateModel
{
	[XmlElementMapping(LocalName = "For", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ForConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ForConstruct class.
		/// </summary>
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Body", NamespaceUri = "http://www.textmetal.com/api/v5.0.0")]
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Condition", NamespaceUri = "http://www.textmetal.com/api/v5.0.0")]
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Intializer", NamespaceUri = "http://www.textmetal.com/api/v5.0.0")]
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Iterator", NamespaceUri = "http://www.textmetal.com/api/v5.0.0")]
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
					throw new InvalidOperationException(string.Format("The for construct has exceeded the maximun number of iterations '{0}'; this is an infinite loop prevention mechansim.", MAX_ITERATIONS_INFINITE_LOOP_CHECK));

				if ((object)this.Condition != null)
					value = this.Condition.EvaluateExpression(templatingContext);
				else
					value = false;

				if ((object)value != null && !(value is bool) && !(value is bool?))
					throw new InvalidOperationException(string.Format("The for construct condition expression has evaluated to a non-null value with an unsupported type '{0}'; only '{1}' and '{2}' types are supported.", value.GetType().FullName, typeof(bool).FullName, typeof(bool?).FullName));

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