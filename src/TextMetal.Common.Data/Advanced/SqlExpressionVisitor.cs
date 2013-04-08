/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using TextMetal.Common.Expressions;

namespace TextMetal.Common.Data.Advanced
{
	public sealed class SqlExpressionVisitor : ExpressionVisitor
	{
		#region Constructors/Destructors

		public SqlExpressionVisitor(IDataSourceTagSpecific dataSourceTagSpecific, IUnitOfWorkContext unitOfWorkContext, IList<IDataParameter> commandParameters)
		{
			if ((object)dataSourceTagSpecific == null)
				throw new ArgumentNullException("dataSourceTagSpecific");

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			this.dataSourceTagSpecific = dataSourceTagSpecific;
			this.unitOfWorkContext = unitOfWorkContext;
			this.commandParameters = commandParameters;
		}

		#endregion

		#region Fields/Constants

		private readonly IList<IDataParameter> commandParameters;
		private readonly IDataSourceTagSpecific dataSourceTagSpecific;
		private readonly StringBuilder strings = new StringBuilder();
		private readonly IUnitOfWorkContext unitOfWorkContext;

		#endregion

		#region Properties/Indexers/Events

		private IList<IDataParameter> CommandParameters
		{
			get
			{
				return this.commandParameters;
			}
		}

		private IDataSourceTagSpecific DataSourceTagSpecific
		{
			get
			{
				return this.dataSourceTagSpecific;
			}
		}

		private StringBuilder Strings
		{
			get
			{
				return this.strings;
			}
		}

		private IUnitOfWorkContext UnitOfWorkContext
		{
			get
			{
				return this.unitOfWorkContext;
			}
		}

		#endregion

		#region Methods/Operators

		public static string GetFilterText(IDataSourceTagSpecific dataSourceTagSpecific, IUnitOfWorkContext unitOfWorkContext, IList<IDataParameter> commandParameters, IExpression expression)
		{
			SqlExpressionVisitor expressionVisitor;
			string expressionText;

			if ((object)dataSourceTagSpecific == null)
				throw new ArgumentNullException("dataSourceTagSpecific");

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			expressionVisitor = new SqlExpressionVisitor(dataSourceTagSpecific, unitOfWorkContext, commandParameters);
			expressionVisitor.Visit(expression);
			expressionText = expressionVisitor.Strings.ToString();

			return expressionText;
		}

		public static string GetSortText(IDataSourceTagSpecific dataSourceTagSpecific, IUnitOfWorkContext unitOfWorkContext, IList<IDataParameter> commandParameters, Order[] orders)
		{
			string expressionText;
			List<string> sortNames;

			if ((object)dataSourceTagSpecific == null)
				throw new ArgumentNullException("dataSourceTagSpecific");

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if ((object)commandParameters == null)
				throw new ArgumentNullException("commandParameters");

			if ((object)orders == null)
				throw new ArgumentNullException("orders");

			sortNames = new List<string>();

			if ((object)orders != null)
			{
				foreach (Order order in orders)
				{
					if ((object)order.Facet == null)
						continue;

					sortNames.Add(string.Format("{0} {1}", dataSourceTagSpecific.GetAliasedColumnName("t0", order.Facet.Name), order.Ascending ? "ASC" : "DESC"));
				}
			}

			if (sortNames.Count <= 0)
				sortNames.Add("1");

			expressionText = string.Join(", ", sortNames.ToArray());

			return expressionText;
		}

		private static DbType InferDbTypeForClrType(Type clrType)
		{
			if ((object)clrType == null)
				throw new ArgumentNullException("clrType");

			if (clrType.IsByRef /* || type.IsPointer || type.IsArray */)
				return InferDbTypeForClrType(clrType.GetElementType());
			else if (clrType.IsGenericType &&
			         clrType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return InferDbTypeForClrType(Nullable.GetUnderlyingType(clrType));
			else if (clrType.IsEnum)
				return InferDbTypeForClrType(Enum.GetUnderlyingType(clrType));
			else if (clrType == typeof(Boolean))
				return DbType.Boolean;
			else if (clrType == typeof(Byte))
				return DbType.Byte;
			else if (clrType == typeof(DateTime))
				return DbType.DateTime;
			else if (clrType == typeof(DateTimeOffset))
				return DbType.DateTimeOffset;
			else if (clrType == typeof(Decimal))
				return DbType.Decimal;
			else if (clrType == typeof(Double))
				return DbType.Double;
			else if (clrType == typeof(Guid))
				return DbType.Guid;
			else if (clrType == typeof(Int16))
				return DbType.Int16;
			else if (clrType == typeof(Int32))
				return DbType.Int32;
			else if (clrType == typeof(Int64))
				return DbType.Int64;
			else if (clrType == typeof(SByte))
				return DbType.SByte;
			else if (clrType == typeof(Single))
				return DbType.Single;
			else if (clrType == typeof(UInt16))
				return DbType.UInt16;
			else if (clrType == typeof(UInt32))
				return DbType.UInt32;
			else if (clrType == typeof(UInt64))
				return DbType.UInt64;
			else if (clrType == typeof(Byte[]))
				return DbType.Binary;
			else if (clrType == typeof(String))
				return DbType.String;
			else if (clrType == typeof(Object))
				return DbType.Object;
			else
				throw new InvalidOperationException(string.Format("Cannot infer parameter type from unsupported CLR type '{0}'.", clrType.FullName));
		}

		protected override IExpression VisitBinary(IBinaryExpression binaryExpression)
		{
			if ((object)binaryExpression == null)
				throw new ArgumentNullException("binaryExpression");

			this.Strings.Append("(");

			this.Visit(binaryExpression.LeftExpression);

			switch (binaryExpression.BinaryOperator)
			{
				case BinaryOperator.Add:
					this.Strings.Append(" + ");
					break;
				case BinaryOperator.Sub:
					this.Strings.Append(" - ");
					break;
				case BinaryOperator.Div:
					this.Strings.Append(" / ");
					break;
				case BinaryOperator.Mul:
					this.Strings.Append(" * ");
					break;
				case BinaryOperator.Mod:
					this.Strings.Append(" % ");
					break;
				case BinaryOperator.And:
					this.Strings.Append(" AND ");
					break;
				case BinaryOperator.Or:
					this.Strings.Append(" OR ");
					break;
				case BinaryOperator.Eq:
					this.Strings.Append(" = ");
					break;
				case BinaryOperator.Ne:
					this.Strings.Append(" <> ");
					break;
				case BinaryOperator.Gt:
					this.Strings.Append(" > ");
					break;
				case BinaryOperator.Ge:
					this.Strings.Append(" >= ");
					break;
				case BinaryOperator.Lt:
					this.Strings.Append(" < ");
					break;
				case BinaryOperator.Le:
					this.Strings.Append(" <= ");
					break;
				case BinaryOperator.StrLk:
					this.Strings.Append(" LIKE ");
					break;
				case BinaryOperator.Band:
					this.Strings.Append(" & ");
					break;
				case BinaryOperator.Bor:
					this.Strings.Append(" | ");
					break;
				case BinaryOperator.Bxor:
					this.Strings.Append(" ^ ");
					break;
				default:
					throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported.", binaryExpression.BinaryOperator));
			}

			this.Visit(binaryExpression.RightExpression);

			this.Strings.Append(")");

			return binaryExpression;
		}

		protected override IExpression VisitContainer(IContainer container)
		{
			if ((object)container == null)
				throw new ArgumentNullException("container");

			if ((object)container.Content != null)
				this.Visit(container.Content);

			return container;
		}

		protected override IExpression VisitNullary(INullaryExpression nullaryExpression)
		{
			if ((object)nullaryExpression == null)
				throw new ArgumentNullException("nullaryExpression");

			this.Strings.Append(" (1 = 1) ");

			return nullaryExpression;
		}

		protected override IExpression VisitSurface(ISurface surface)
		{
			string columnName;

			if ((object)surface == null)
				throw new ArgumentNullException("surface");

			columnName = this.DataSourceTagSpecific.GetAliasedColumnName("t0", surface.Name);

			this.Strings.Append(columnName);

			return surface;
		}

		protected override IExpression VisitUnary(IUnaryExpression unaryExpression)
		{
			if ((object)unaryExpression == null)
				throw new ArgumentNullException("unaryExpression");

			switch (unaryExpression.UnaryOperator)
			{
				case UnaryOperator.Not:
					this.Strings.Append(" NOT ");
					this.Visit(unaryExpression.TheExpression);
					break;
				case UnaryOperator.IsNull:
					this.Visit(unaryExpression.TheExpression);
					this.Strings.Append(" IS NULL ");
					break;
				case UnaryOperator.IsNotNull:
					this.Visit(unaryExpression.TheExpression);
					this.Strings.Append(" IS NOT NULL ");
					break;
				case UnaryOperator.Neg:
					this.Strings.Append(" - ");
					this.Visit(unaryExpression.TheExpression);
					break;
				case UnaryOperator.Pos:
					this.Strings.Append(" + ");
					this.Visit(unaryExpression.TheExpression);
					break;
				case UnaryOperator.Incr:
					this.Strings.Append(" (");
					this.Visit(unaryExpression.TheExpression);
					this.Strings.Append(" + 1) ");
					break;
				case UnaryOperator.Decr:
					this.Strings.Append(" (");
					this.Visit(unaryExpression.TheExpression);
					this.Strings.Append(" - 1) ");
					break;
				case UnaryOperator.BComp:
					this.Strings.Append(" ~ ");
					this.Visit(unaryExpression.TheExpression);
					break;
				default:
					throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported.", unaryExpression.UnaryOperator));
			}

			return unaryExpression;
		}

		protected override IExpression VisitUnknown(IExpression expression)
		{
			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			throw new NotSupportedException(string.Format("The unknown expression of type '{0}' is not supported.", expression.GetType()));
		}

		protected override IExpression VisitValue(IValue value)
		{
			IDataParameter commandParameter;
			string parameterName;
			Type valueType;

			if ((object)value == null)
				throw new ArgumentNullException("value");

			if ((object)value.__ == null)
				throw new InvalidOperationException("Cannot use the constant value NULL as a value operand; use UnaryExpression(..., UnaryOperator.IsNull) instead.");

			parameterName = this.DataSourceTagSpecific.GetParameterName(string.Format("expr_{0}", Guid.NewGuid().ToString("N")));
			valueType = value.__.GetType();

			commandParameter = this.UnitOfWorkContext.CreateParameter(ParameterDirection.Input, InferDbTypeForClrType(valueType), 0, 0, 0, true, parameterName, value.__);
			this.commandParameters.Add(commandParameter);

			this.Strings.Append(parameterName);

			return value;
		}

		#endregion
	}
}