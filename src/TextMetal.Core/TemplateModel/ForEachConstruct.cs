/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.SortModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "ForEach", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ForEachConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public ForEachConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private TemplateContainerConstruct body;
		private ExpressionContainerConstruct filter;
		private string @in;
		private SortContainerConstruct sort;
		private string varCt;
		private string varItem;
		private string varIx;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Body", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Filter", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public ExpressionContainerConstruct Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		[XmlAttributeMapping(LocalName = "in", NamespaceUri = "")]
		public string In
		{
			get
			{
				return this.@in;
			}
			set
			{
				this.@in = value;
			}
		}

		protected override bool IsScopeBlock
		{
			get
			{
				return true;
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.ParentQualified, LocalName = "Sort", NamespaceUri = "http://www.textmetal.com/api/v4.4.0")]
		public SortContainerConstruct Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				this.sort = value;
			}
		}

		[XmlAttributeMapping(LocalName = "var-ct", NamespaceUri = "")]
		public string VarCt
		{
			get
			{
				return this.varCt;
			}
			set
			{
				this.varCt = value;
			}
		}

		[XmlAttributeMapping(LocalName = "var-item", NamespaceUri = "")]
		public string VarItem
		{
			get
			{
				return this.varItem;
			}
			set
			{
				this.varItem = value;
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
			string @in, varCt, varItem, varIx;
			uint count = 0, index = 1; // one-based
			IEnumerable values;
			object obj;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			@in = templatingContext.Tokenizer.ExpandTokens(this.In, dynamicWildcardTokenReplacementStrategy);
			varItem = templatingContext.Tokenizer.ExpandTokens(this.VarItem, dynamicWildcardTokenReplacementStrategy);
			varCt = templatingContext.Tokenizer.ExpandTokens(this.VarCt, dynamicWildcardTokenReplacementStrategy);
			varIx = templatingContext.Tokenizer.ExpandTokens(this.VarIx, dynamicWildcardTokenReplacementStrategy);

			if (!DataType.IsNullOrWhiteSpace(varItem))
			{
				new AllocConstruct()
				{
					Token = varItem
				}.ExpandTemplate(templatingContext);
			}

			if (!DataType.IsNullOrWhiteSpace(varCt))
			{
				new AllocConstruct()
				{
					Token = varCt
				}.ExpandTemplate(templatingContext);
			}

			if (!DataType.IsNullOrWhiteSpace(varIx))
			{
				new AllocConstruct()
				{
					Token = varIx
				}.ExpandTemplate(templatingContext);
			}

			if (!dynamicWildcardTokenReplacementStrategy.GetByPath(@in, out obj))
				throw new InvalidOperationException(string.Format("The facet name '{0}' was not found on the target model.", @in));

			if ((object)obj == null)
				return;

			if (!(obj is IEnumerable))
				throw new InvalidOperationException(string.Format("The in expression the for-each construct is not assignable to type '{0}'.", typeof(IEnumerable).FullName));

			values = (IEnumerable)obj;
			obj = null; // not needed

			if ((object)this.Filter != null)
			{
				ArrayList temp;
				bool shouldFilter;

				temp = new ArrayList();

				if ((object)values != null)
				{
					foreach (object value in values)
					{
						templatingContext.IteratorModels.Push(value);

						obj = this.Filter.EvaluateExpression(templatingContext);

						templatingContext.IteratorModels.Pop();

						if ((object)obj != null && !(obj is bool) && !(obj is bool?))
							throw new InvalidOperationException(string.Format("The for-each construct filter expression has evaluated to a non-null value with an unsupported type '{0}'; only '{1}' and '{2}' types are supported.", value.GetType().FullName, typeof(bool).FullName, typeof(bool?).FullName));

						shouldFilter = !((bool)(obj ?? true));

						if (!shouldFilter)
						{
							count++;
							temp.Add(value);
						}
					}
				}

				values = temp;
			}
			else
			{
				if ((object)values != null)
				{
					foreach (object value in values)
						count++;
				}
			}

			if ((object)this.Sort != null)
				values = this.Sort.EvaluateSort(templatingContext, values);

			if (!DataType.IsNullOrWhiteSpace(varCt))
			{
				ExpressionContainerConstruct expressionContainerConstruct;
				ValueConstruct valueConstruct;

				expressionContainerConstruct = new ExpressionContainerConstruct();

				valueConstruct = new ValueConstruct()
				                 {
				                 	Type = typeof(int).FullName,
				                 	__ = count
				                 };

				expressionContainerConstruct.Content = valueConstruct;

				new AssignConstruct()
				{
					Token = varCt,
					Expression = expressionContainerConstruct
				}.ExpandTemplate(templatingContext);
			}

			if ((object)values != null)
			{
				foreach (object value in values)
				{
					if (!DataType.IsNullOrWhiteSpace(varItem))
					{
						ExpressionContainerConstruct expressionContainerConstruct;
						ValueConstruct valueConstruct;

						expressionContainerConstruct = new ExpressionContainerConstruct();

						valueConstruct = new ValueConstruct()
						                 {
						                 	__ = value
						                 };

						expressionContainerConstruct.Content = valueConstruct;

						new AssignConstruct()
						{
							Token = varItem,
							Expression = expressionContainerConstruct
						}.ExpandTemplate(templatingContext);
					}

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

					templatingContext.IteratorModels.Push(value);

					if ((object)this.Body != null)
						this.Body.ExpandTemplate(templatingContext);

					templatingContext.IteratorModels.Pop();

					index++;
				}

				if (!DataType.IsNullOrWhiteSpace(varIx))
				{
					new FreeConstruct()
					{
						Token = varIx
					}.ExpandTemplate(templatingContext);
				}

				if (!DataType.IsNullOrWhiteSpace(varCt))
				{
					new FreeConstruct()
					{
						Token = varCt
					}.ExpandTemplate(templatingContext);
				}

				if (!DataType.IsNullOrWhiteSpace(varItem))
				{
					new FreeConstruct()
					{
						Token = varItem
					}.ExpandTemplate(templatingContext);
				}
			}
		}

		#endregion
	}
}