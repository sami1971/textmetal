/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

using TextMetal.Core.Plumbing;

using Expression = TextMetal.Core.ExpressionModel.IExpressionXmlObject;
using NullaryExpression = TextMetal.Core.ExpressionModel.NullaryExpressionConstruct;
using UnaryExpression = TextMetal.Core.ExpressionModel.UnaryExpressionConstruct;
using BinaryExpression = TextMetal.Core.ExpressionModel.BinaryExpressionConstruct;
using Surface = TextMetal.Core.ExpressionModel.SurfaceConstruct;
using Value = TextMetal.Core.ExpressionModel.ValueConstruct;
using Container = TextMetal.Core.ExpressionModel.ExpressionContainerConstruct;

namespace TextMetal.Core.ExpressionModel
{
	public sealed class PrintExpressionVisitor : ExpressionVisitor
	{
		#region Constructors/Destructors

		private PrintExpressionVisitor()
		{
		}

		#endregion

		#region Fields/Constants

		private const string CLOSE = ")";
		private const string NULL_VALUE = "<null>";
		private const string OPEN = "(";
		private const string SURFACE_NAME_FORMAT = "{0}";
		private const string VALUE_TEXT_FORMAT = "{0}";
		private readonly StringBuilder strings = new StringBuilder();

		#endregion

		#region Properties/Indexers/Events

		private StringBuilder Strings
		{
			get
			{
				return this.strings;
			}
		}

		#endregion

		#region Methods/Operators

		public static string GetExpressionText(Expression expression)
		{
			PrintExpressionVisitor expressionVisitor;
			string expressionText;

			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			expressionVisitor = new PrintExpressionVisitor();
			expressionVisitor.Visit(expression);
			expressionText = expressionVisitor.Strings.ToString();

			return expressionText;
		}

		protected override Expression VisitBinary(BinaryExpression binaryExpression)
		{
			DescriptionAttribute descriptionAttribute;
			FieldInfo fieldInfo;

			if ((object)binaryExpression == null)
				throw new ArgumentNullException("binaryExpression");

			this.Strings.Append(OPEN);

			this.Visit(binaryExpression.LeftExpression);

			fieldInfo = typeof(BinaryOperator).GetField(binaryExpression.BinaryOperator.ToString());

			if ((object)fieldInfo == null)
				throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported.", binaryExpression.BinaryOperator));

			descriptionAttribute = Reflexion.GetOneAttribute<DescriptionAttribute>(fieldInfo);

			if ((object)descriptionAttribute == null)
				throw new NotSupportedException(string.Format("The binary operator '{0}' is not described.", binaryExpression.BinaryOperator));

			this.Strings.Append(descriptionAttribute.Description);

			this.Visit(binaryExpression.RightExpression);

			this.Strings.Append(CLOSE);

			return binaryExpression;
		}

		protected override Expression VisitContainer(Container container)
		{
			if ((object)container == null)
				throw new ArgumentNullException("container");

			if ((object)container.Content != null)
				this.Visit(container.Content);

			return container;
		}

		protected override Expression VisitNullary(NullaryExpression nullaryExpression)
		{
			DescriptionAttribute descriptionAttribute;
			FieldInfo fieldInfo;

			if ((object)nullaryExpression == null)
				throw new ArgumentNullException("nullaryExpression");

			fieldInfo = typeof(NullaryOperator).GetField(nullaryExpression.NullaryOperator.ToString());

			if ((object)fieldInfo == null)
				throw new NotSupportedException(string.Format("The nullary operator '{0}' is not supported.", nullaryExpression.NullaryOperator));

			descriptionAttribute = Reflexion.GetOneAttribute<DescriptionAttribute>(fieldInfo);

			if ((object)descriptionAttribute == null)
				throw new NotSupportedException(string.Format("The nullary operator '{0}' is not described.", nullaryExpression.NullaryOperator));

			this.Strings.Append(descriptionAttribute.Description);

			return nullaryExpression;
		}

		protected override Expression VisitSurface(Surface surface)
		{
			string surfaceName;

			if ((object)surface == null)
				throw new ArgumentNullException("surface");

			surfaceName = surface.Name;
			surfaceName = string.Format(SURFACE_NAME_FORMAT, surfaceName);

			this.Strings.Append(surfaceName);

			return surface;
		}

		protected override Expression VisitUnary(UnaryExpression unaryExpression)
		{
			DescriptionAttribute descriptionAttribute;
			FieldInfo fieldInfo;

			if ((object)unaryExpression == null)
				throw new ArgumentNullException("unaryExpression");

			fieldInfo = typeof(UnaryOperator).GetField(unaryExpression.UnaryOperator.ToString());

			if ((object)fieldInfo == null)
				throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported.", unaryExpression.UnaryOperator));

			descriptionAttribute = Reflexion.GetOneAttribute<DescriptionAttribute>(fieldInfo);

			if ((object)descriptionAttribute == null)
				throw new NotSupportedException(string.Format("The unary operator '{0}' is not described.", unaryExpression.UnaryOperator));

			if (unaryExpression.UnaryOperator == UnaryOperator.Not)
				this.Visit(unaryExpression.TheExpression);

			this.Strings.Append(descriptionAttribute.Description);

			if (unaryExpression.UnaryOperator != UnaryOperator.Not)
				this.Visit(unaryExpression.TheExpression);

			return unaryExpression;
		}

		protected override Expression VisitUnknown(Expression expression)
		{
			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			return expression;
		}

		protected override Expression VisitValue(Value value)
		{
			string valueText;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			valueText = value.__.SafeToString(null, NULL_VALUE);
			valueText = string.Format(VALUE_TEXT_FORMAT, valueText);

			this.Strings.Append(valueText);

			return value;
		}

		#endregion
	}
}