/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.ExpressionModel.Operators
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class OperatorMneumonicAttribute : Attribute
	{
		#region Constructors/Destructors

		public OperatorMneumonicAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private string description;
		private string symbol;

		#endregion

		#region Properties/Indexers/Events

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public string Symbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				this.symbol = value;
			}
		}

		#endregion
	}
}