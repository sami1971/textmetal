/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Expressions;

namespace TextMetal.Common.Data.Advanced
{
	public sealed class Query
	{
		#region Constructors/Destructors

		public Query(IExpression expression, Order[] orders, int skip, int take)
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

		private readonly IExpression expression;
		private readonly Order[] orders;
		private readonly int skip;
		private readonly int take;

		#endregion

		#region Properties/Indexers/Events

		public IExpression Expression
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