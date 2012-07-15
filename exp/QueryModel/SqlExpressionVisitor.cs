/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using TextMetal.Core.ExpressionModel;
using TextMetal.Core.Plumbing;
using TextMetal.Core.SourceModel.SqlServer;

using Expression = TextMetal.Core.ExpressionModel.IExpressionXmlObject;
using NullaryExpression = TextMetal.Core.ExpressionModel.NullaryExpressionConstruct;
using UnaryExpression = TextMetal.Core.ExpressionModel.UnaryExpressionConstruct;
using BinaryExpression = TextMetal.Core.ExpressionModel.BinaryExpressionConstruct;
using Surface = TextMetal.Core.ExpressionModel.SurfaceConstruct;
using Value = TextMetal.Core.ExpressionModel.ValueConstruct;
using Container = TextMetal.Core.ExpressionModel.ExpressionContainerConstruct;

namespace TextMetal.Core.QueryModel
{
	public sealed class SqlExpressionVisitor : ExpressionVisitor
	{
		#region Constructors/Destructors

		private SqlExpressionVisitor(int parameterIndex)
		{
			this.parameterIndex = parameterIndex;
		}

		#endregion

		#region Fields/Constants

		private const string CLOSE = ")";
		private const string COLUMN_ALIASED_FORMAT = "{0}.{1}";
		private const string COLUMN_NAME_FORMAT = "[{0}]";
		private const string NOP = " (1 = 1) ";
		private const string OPEN = "(";
		internal const string PARAMETER_NAME_FORMAT = "@p{0:000}";
		private const string SCHEMA_TABLE_NAME_FORMAT = "[{0}].[{1}]";
		internal const string TABLE_ALIAS_FORMAT = "t{0:000}";
		private const string TABLE_NAME_FORMAT = "[{0}]";
		private readonly IDictionary<Parameter, object> parameters = new Dictionary<Parameter, object>();
		private readonly StringBuilder strings = new StringBuilder();
		private int parameterIndex;

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

		public static string GetExpressionText(IExpressionXmlObject expressionXmlObject, int parameterIndex)
		{
			SqlExpressionVisitor expressionVisitor;
			string expressionText;

			if ((object)expressionXmlObject == null)
				throw new ArgumentNullException("expressionXmlObject");

			expressionVisitor = new SqlExpressionVisitor(parameterIndex);
			expressionVisitor.Visit(expressionXmlObject);
			expressionText = expressionVisitor.Strings.ToString();

			return expressionText;
		}

		protected override Expression VisitBinary(BinaryExpression binaryExpression)
		{
			if ((object)binaryExpression == null)
				throw new ArgumentNullException("binaryExpression");

			this.strings.Append(OPEN);

			this.Visit(binaryExpression.LeftExpression);

			switch (binaryExpression.BinaryOperator)
			{
				case BinaryOperator.Add:
					this.strings.Append(" + ");
					break;
				case BinaryOperator.Sub:
					this.strings.Append(" - ");
					break;
				case BinaryOperator.Div:
					this.strings.Append(" / ");
					break;
				case BinaryOperator.Mul:
					this.strings.Append(" * ");
					break;
				case BinaryOperator.Mod:
					this.strings.Append(" % ");
					break;
				case BinaryOperator.And:
					this.strings.Append(" AND ");
					break;
				case BinaryOperator.Or:
					this.strings.Append(" OR ");
					break;
				case BinaryOperator.Xor:
					this.strings.Append(" ^ ");
					break;
				case BinaryOperator.Eq:
					this.strings.Append(" = ");
					break;
				case BinaryOperator.Ne:
					this.strings.Append(" <> ");
					break;
				case BinaryOperator.Lt:
					this.strings.Append(" < ");
					break;
				case BinaryOperator.Le:
					this.strings.Append(" <= ");
					break;
				case BinaryOperator.Gt:
					this.strings.Append(" > ");
					break;
				case BinaryOperator.Ge:
					this.strings.Append(" >= ");
					break;
				case BinaryOperator.StrLk:
					this.strings.Append(" LIKE ");
					break;
				default:
					throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported.", binaryExpression.BinaryOperator));
			}

			this.Visit(binaryExpression.RightExpression);

			this.strings.Append(CLOSE);

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
			if ((object)nullaryExpression == null)
				throw new ArgumentNullException("nullaryExpression");

			this.strings.Append(NOP);

			return nullaryExpression;
		}

		protected override Expression VisitSurface(Surface surface)
		{
			string tableAlias, columnName, columnAliasedName;

			if ((object)surface == null)
				throw new ArgumentNullException("surface");

			tableAlias = "________GetFromMap________";
			columnName = "________GetFromMap________";

			columnName = string.Format(COLUMN_NAME_FORMAT, columnName);

			if (!DataType.IsNullOrWhiteSpace(tableAlias))
				columnAliasedName = string.Format(COLUMN_ALIASED_FORMAT, tableAlias, columnName);
			else
				columnAliasedName = columnName;

			this.strings.Append(columnAliasedName);

			return surface;
		}

		protected override Expression VisitUnary(UnaryExpression unaryExpression)
		{
			if ((object)unaryExpression == null)
				throw new ArgumentNullException("unaryExpression");

			switch (unaryExpression.UnaryOperator)
			{
				case UnaryOperator.Not:
					this.strings.Append(" NOT ");
					this.Visit(unaryExpression.TheExpression);
					break;
				case UnaryOperator.IsNull:
					this.Visit(unaryExpression.TheExpression);
					this.strings.Append(" IS NULL ");
					break;
				case UnaryOperator.IsNotNull:
					this.Visit(unaryExpression.TheExpression);
					this.strings.Append(" IS NOT NULL ");
					break;
				case UnaryOperator.Neg:
					this.strings.Append(" - ");
					this.Visit(unaryExpression.TheExpression);
					break;
				case UnaryOperator.Pos:
					this.strings.Append(" + ");
					this.Visit(unaryExpression.TheExpression);
					break;
				default:
					throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported.", unaryExpression.UnaryOperator));
			}

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
			string parameterName;
			Type valueType;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			if ((object)value.__ == null)
				throw new InvalidOperationException("Cannot use the constant value NULL as a value operand; use UnaryExpression(..., UnaryOperator.IsNull) instead.");

			valueType = value.__.GetType();
			parameterName = string.Format(PARAMETER_NAME_FORMAT, this.parameterIndex);
			this.parameterIndex++;

			this.strings.Append(parameterName);

			this.parameters.Add(new Parameter()
			                    {
			                    	ParameterDirection = ParameterDirection.Input,
			                    	ParameterDbType = SqlServerSchemaSourceStrategy.InferDbTypeForClrType(valueType),
			                    	ParameterSize = 0,
			                    	ParameterPrecision = 0,
			                    	ParameterScale = 0,
			                    	ParameterNullable = false,
			                    	ParameterName = parameterName
			                    }, value.__);

			return value;
		}

		#endregion
	}
}