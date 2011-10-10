/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using Expression = TextMetal.Core.ExpressionModel.IExpressionXmlObject;
using NullaryExpression = TextMetal.Core.ExpressionModel.NullaryExpressionConstruct;
using UnaryExpression = TextMetal.Core.ExpressionModel.UnaryExpressionConstruct;
using BinaryExpression = TextMetal.Core.ExpressionModel.BinaryExpressionConstruct;
using Surface = TextMetal.Core.ExpressionModel.SurfaceConstruct;
using Value = TextMetal.Core.ExpressionModel.ValueConstruct;
using Container = TextMetal.Core.ExpressionModel.ExpressionContainerConstruct;

namespace TextMetal.Core.ExpressionModel
{
	public abstract class ExpressionVisitor
	{
		#region Constructors/Destructors

		protected ExpressionVisitor()
		{
		}

		#endregion

		#region Methods/Operators

		protected Expression Visit(Expression expression)
		{
			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			if (expression is NullaryExpression)
				return this.VisitNullary((NullaryExpression)expression);
			else if (expression is UnaryExpression)
				return this.VisitUnary((UnaryExpression)expression);
			else if (expression is BinaryExpression)
				return this.VisitBinary((BinaryExpression)expression);
			else if (expression is Surface)
				return this.VisitSurface((Surface)expression);
			else if (expression is Value)
				return this.VisitValue((Value)expression);
			else if (expression is Container)
				return this.VisitContainer((Container)expression);
			else
				return this.VisitUnknown(expression);
		}

		protected abstract Expression VisitBinary(BinaryExpression binaryExpression);

		protected abstract Expression VisitContainer(Container container);

		protected abstract Expression VisitNullary(NullaryExpression nullaryExpression);

		protected abstract Expression VisitSurface(Surface surface);

		protected abstract Expression VisitUnary(UnaryExpression unaryExpression);

		protected abstract Expression VisitUnknown(Expression expression);

		protected abstract Expression VisitValue(Value value);

		#endregion
	}
}