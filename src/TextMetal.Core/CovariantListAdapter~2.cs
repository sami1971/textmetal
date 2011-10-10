/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace TextMetal.Core
{
	public class CovariantListAdapter<TToBase, TFromDerived> : IList<TToBase>
		where TFromDerived : class, TToBase
	{
		#region Constructors/Destructors

		public CovariantListAdapter(IList<TFromDerived> inner)
		{
			this.inner = inner;
		}

		#endregion

		#region Fields/Constants

		private readonly IList<TFromDerived> inner;

		#endregion

		#region Properties/Indexers/Events

		public TToBase this[int index]
		{
			get
			{
				return this.Inner[index];
			}
			set
			{
				this.Inner[index] = (TFromDerived)value;
			}
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public IList<TFromDerived> Inner
		{
			get
			{
				return this.inner;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return this.Inner.IsReadOnly;
			}
		}

		#endregion

		#region Methods/Operators

		public void Add(TToBase item)
		{
			this.Inner.Add((TFromDerived)item);
		}

		public void Clear()
		{
			this.Inner.Clear();
		}

		public bool Contains(TToBase item)
		{
			return this.Inner.Contains((TFromDerived)item);
		}

		public void CopyTo(TToBase[] array, int arrayIndex)
		{
			this.Inner.CopyTo((TFromDerived[])(object)array, arrayIndex);
		}

		public IEnumerator<TToBase> GetEnumerator()
		{
			if ((object)this.Inner != null)
			{
				foreach (TFromDerived obj in this.Inner)
					yield return obj;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.Inner).GetEnumerator();
		}

		public int IndexOf(TToBase item)
		{
			return this.Inner.IndexOf((TFromDerived)item);
		}

		public void Insert(int index, TToBase item)
		{
			this.Inner.Insert(index, (TFromDerived)item);
		}

		public bool Remove(TToBase item)
		{
			return this.Inner.Remove((TFromDerived)item);
		}

		public void RemoveAt(int index)
		{
			this.Inner.RemoveAt(index);
		}

		#endregion
	}
}