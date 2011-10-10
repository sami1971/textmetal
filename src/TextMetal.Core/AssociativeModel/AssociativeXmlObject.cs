/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	public abstract class AssociativeXmlObject<TParent> : XmlItemsObject<TParent, IAssociativeXmlObject>, IAssociativeXmlObject, IDictionary, IDictionary<string, object>
		where TParent : class, IXmlObject
	{
		#region Constructors/Destructors

		protected AssociativeXmlObject()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;

		#endregion

		#region Properties/Indexers/Events

		object IDictionary.this[object key]
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary)[key];
			}
			set
			{
				PropertyConstruct propertyConstruct;

				propertyConstruct = ((IDictionary)this.InnerAsDictionary)[key] as PropertyConstruct;

				if ((object)propertyConstruct != null)
					propertyConstruct.Value = value.SafeToString();
			}
		}

		object IDictionary<string, object>.this[string key]
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary)[key];
			}
			set
			{
				PropertyConstruct propertyConstruct;

				propertyConstruct = ((IDictionary<string, object>)this.InnerAsDictionary)[key] as PropertyConstruct;

				if ((object)propertyConstruct != null)
					propertyConstruct.Value = value.SafeToString();
			}
		}

		int ICollection.Count
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Count;
			}
		}

		int ICollection<KeyValuePair<string, object>>.Count
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Count;
			}
		}

		private Dictionary<string, object> InnerAsDictionary
		{
			get
			{
				Dictionary<string, object> dictionary;

				dictionary = new Dictionary<string, object>();

				if ((object)this.Items != null)
				{
					foreach (IAssociativeXmlObject item in this.Items)
					{
						if (!DataType.IsNullOrWhiteSpace(item.Name) &&
						    !dictionary.ContainsKey(item.Name))
							dictionary.Add(item.Name, item.GetAssociativeObjectValue());
					}
				}

				return dictionary;
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsFixedSize;
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsReadOnly;
			}
		}

		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).IsReadOnly;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsSynchronized;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Keys;
			}
		}

		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Keys;
			}
		}

		[XmlAttributeMapping(LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).SyncRoot;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Values;
			}
		}

		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Values;
			}
		}

		#endregion

		#region Methods/Operators

		void IDictionary.Add(object key, object value)
		{
			((IDictionary)this.InnerAsDictionary).Add(key, value);
		}

		void IDictionary<string, object>.Add(string key, object value)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Add(key, value);
		}

		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Add(item);
		}

		void IDictionary.Clear()
		{
			((IDictionary)this.InnerAsDictionary).Clear();
		}

		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Clear();
		}

		bool IDictionary.Contains(object key)
		{
			return ((IDictionary)this.InnerAsDictionary).Contains(key);
		}

		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Contains(item);
		}

		bool IDictionary<string, object>.ContainsKey(string key)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).ContainsKey(key);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((IDictionary)this.InnerAsDictionary).CopyTo(array, index);
		}

		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).CopyTo(array, arrayIndex);
		}

		public virtual object GetAssociativeObjectValue()
		{
			// default (except for Property)
			return this;
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IDictionary)this.InnerAsDictionary).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.InnerAsDictionary).GetEnumerator();
		}

		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).GetEnumerator();
		}

		void IDictionary.Remove(object key)
		{
			((IDictionary)this.InnerAsDictionary).Remove(key);
		}

		bool IDictionary<string, object>.Remove(string key)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Remove(key);
		}

		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Remove(item);
		}

		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).TryGetValue(key, out value);
		}

		#endregion
	}
}