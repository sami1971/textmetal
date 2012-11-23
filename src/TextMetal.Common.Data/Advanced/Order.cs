using System;

using TextMetal.Common.Expressions;

namespace TextMetal.Common.Data.Advanced
{
	public sealed class Order
	{
		#region Constructors/Destructors

		public Order(ISurface facet, bool ascending)
		{
			if ((object)facet == null)
				throw new ArgumentNullException("facet");

			this.facet = facet;
			this.ascending = ascending;
		}

		#endregion

		#region Fields/Constants

		private bool ascending;
		private ISurface facet;

		#endregion

		#region Properties/Indexers/Events

		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		public ISurface Facet
		{
			get
			{
				return this.facet;
			}
		}

		#endregion
	}
}