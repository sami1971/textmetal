/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace TextMetal.Core
{
	public class ContravariantListAdapter<TToDerived, TFromBase> : IList<TToDerived>
		where TToDerived : class, TFromBase
	{
		#region Constructors/Destructors

		public ContravariantListAdapter(IList<TFromBase> inner)
		{
			this.inner = inner;
		}

		#endregion

		#region Fields/Constants

		private readonly IList<TFromBase> inner;

		#endregion

		#region Properties/Indexers/Events

		public TToDerived this[int index]
		{
			get
			{
				return (TToDerived)this.Inner[index];
			}
			set
			{
				this.Inner[index] = (TFromBase)value;
			}
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public IList<TFromBase> Inner
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

		public void Add(TToDerived item)
		{
			this.Inner.Add((TFromBase)item);
		}

		public void Clear()
		{
			this.Inner.Clear();
		}

		public bool Contains(TToDerived item)
		{
			return this.Inner.Contains((TFromBase)item);
		}

		public void CopyTo(TToDerived[] array, int arrayIndex)
		{
			this.Inner.CopyTo((TFromBase[])(object)array, arrayIndex);
		}

		public IEnumerator<TToDerived> GetEnumerator()
		{
			if ((object)this.Inner != null)
			{
				foreach (TFromBase obj in this.Inner)
					yield return (TToDerived)obj;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.Inner).GetEnumerator();
		}

		public int IndexOf(TToDerived item)
		{
			return this.Inner.IndexOf((TFromBase)item);
		}

		public void Insert(int index, TToDerived item)
		{
			this.Inner.Insert(index, (TFromBase)item);
		}

		public bool Remove(TToDerived item)
		{
			return this.Inner.Remove((TFromBase)item);
		}

		public void RemoveAt(int index)
		{
			this.Inner.RemoveAt(index);
		}

		#endregion
	}
}