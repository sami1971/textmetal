﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by:
// TextMetal 4.4.2.19231;
// 		Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
//		Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
//		Project URL: http://code.google.com/p/textmetal/
//
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------

/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;

using Expression = TextMetal.Core.ExpressionModel.IExpressionXmlObject;
using NullaryExpression = TextMetal.Core.ExpressionModel.NullaryExpressionConstruct;
using UnaryExpression = TextMetal.Core.ExpressionModel.UnaryExpressionConstruct;
using BinaryExpression = TextMetal.Core.ExpressionModel.BinaryExpressionConstruct;
using Surface = TextMetal.Core.ExpressionModel.SurfaceConstruct;
using Value = TextMetal.Core.ExpressionModel.ValueConstruct;
using Container = TextMetal.Core.ExpressionModel.ExpressionContainerConstruct;

namespace TextMetal.WebHostSample.Objects.Model
{
	public sealed class Query
	{		
		#region Constructors/Destructors
		
		public Query(Expression expression, Order[] orders, int skip, int take)
		{
			if ((object)expression == null)
				throw new ArgumentNullException("expression");

			if ((object)orders == null)
				throw new ArgumentNullException("orders");

			this.expression = expression;
			this.orders = orders;
			this.skip = skip;
			this.take = take;
		}
		
		#endregion
		
		#region Fields/Constants
		
		private readonly Expression expression;
		private readonly Order[] orders;
		private readonly int skip;
		private readonly int take;

		#endregion

		#region Properties/Indexers/Events

		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		public Order[] Orders
		{
			get
			{
				return this.orders;
			}
		}

		public int Skip
		{
			get
			{
				return this.skip;
			}
		}
		
		public int Take
		{
			get
			{
				return this.take;
			}
		}

		#endregion
	}
}