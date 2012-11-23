/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Expressions
{
	public abstract class ExpressionVisitor
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ExpressionVisitor class.
		/// </summary>
		protected ExpressionVisitor()
		{
		}

		#endregion

		#region Methods/Operators

		protected IExpression Visit(IExpression expression)
		{
			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			if (expression is INullaryExpression)
				return this.VisitNullary((INullaryExpression)expression);
			else if (expression is IUnaryExpression)
				return this.VisitUnary((IUnaryExpression)expression);
			else if (expression is IBinaryExpression)
				return this.VisitBinary((IBinaryExpression)expression);
			else if (expression is ISurface)
				return this.VisitSurface((ISurface)expression);
			else if (expression is IValue)
				return this.VisitValue((IValue)expression);
			else if (expression is IContainer)
				return this.VisitContainer((IContainer)expression);
			else
				return this.VisitUnknown(expression);
		}

		protected abstract IExpression VisitBinary(IBinaryExpression binaryExpression);

		protected abstract IExpression VisitContainer(IContainer container);

		protected abstract IExpression VisitNullary(INullaryExpression nullaryExpression);

		protected abstract IExpression VisitSurface(ISurface surface);

		protected abstract IExpression VisitUnary(IUnaryExpression unaryExpression);

		protected abstract IExpression VisitUnknown(IExpression expression);

		protected abstract IExpression VisitValue(IValue value);

		#endregion
	}
}