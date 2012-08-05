/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;

using TextMetal.Core.Plumbing;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	/// <summary>
	/// 	Serves as the base of all associative XML objects.
	/// </summary>
	public abstract class AssociativeXmlObject : XmlObject, IAssociativeXmlObject, IDictionary, IDictionary<string, object>
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the AssociativeXmlObject class.
		/// </summary>
		protected AssociativeXmlObject()
		{
		}

		#endregion

		#region Fields/Constants

		private string name;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets or sets the element with the specified key.
		/// </summary>
		/// <returns> The element with the specified key. </returns>
		/// <param name="key"> The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	object is read-only.-or- The property is set,
		/// 	<paramref name="key" />
		/// 	does not exist in the collection, and the
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	has a fixed size.</exception>
		/// <filterpriority>2</filterpriority>
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

		/// <summary>
		/// 	Gets or sets the element with the specified key.
		/// </summary>
		/// <returns> The element with the specified key. </returns>
		/// <param name="key"> The key of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and
		/// 	<paramref name="key" />
		/// 	is not found.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the
		/// 	<see cref="T:System.Collections.Generic.IDictionary`2" />
		/// 	is read-only.</exception>
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

		/// <summary>
		/// 	Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" /> .
		/// </summary>
		/// <returns> The number of elements contained in the <see cref="T:System.Collections.ICollection" /> . </returns>
		int ICollection.Count
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Count;
			}
		}

		/// <summary>
		/// 	Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> .
		/// </summary>
		/// <returns> The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
		int ICollection<KeyValuePair<string, object>>.Count
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Count;
			}
		}

		/// <summary>
		/// 	Returns a new dictionary each time this getter is accessed that represents any child associative XML objects by name.
		/// </summary>
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

		/// <summary>
		/// 	Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.
		/// </summary>
		/// <returns> true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false. </returns>
		bool IDictionary.IsFixedSize
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsFixedSize;
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.
		/// </summary>
		/// <returns> true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false. </returns>
		bool IDictionary.IsReadOnly
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsReadOnly;
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		/// <returns> true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false. </returns>
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).IsReadOnly;
			}
		}

		/// <summary>
		/// 	Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
		/// </summary>
		/// <returns> true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false. </returns>
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).IsSynchronized;
			}
		}

		/// <summary>
		/// 	Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see
		///  	cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see
		///  	cref="T:System.Collections.IDictionary" /> object. </returns>
		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Keys;
			}
		}

		/// <summary>
		/// 	Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see
		///  	cref="T:System.Collections.Generic.IDictionary`2" /> .
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see
		///  	cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Keys;
			}
		}

		/// <summary>
		/// 	Gets the associative name of the current associative XML object.
		/// </summary>
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

		/// <summary>
		/// 	Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" /> .
		/// </summary>
		/// <returns> An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" /> . </returns>
		object ICollection.SyncRoot
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).SyncRoot;
			}
		}

		/// <summary>
		/// 	Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see
		///  	cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see
		///  	cref="T:System.Collections.IDictionary" /> object. </returns>
		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)this.InnerAsDictionary).Values;
			}
		}

		/// <summary>
		/// 	Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see
		///  	cref="T:System.Collections.Generic.IDictionary`2" /> .
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see
		///  	cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return ((IDictionary<string, object>)this.InnerAsDictionary).Values;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// 	Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		/// <param name="key"> The <see cref="T:System.Object" /> to use as the key of the element to add. </param>
		/// <param name="value"> The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	object.</exception>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	is read-only.-or- The
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	has a fixed size.</exception>
		/// <filterpriority>2</filterpriority>
		void IDictionary.Add(object key, object value)
		{
			((IDictionary)this.InnerAsDictionary).Add(key, value);
		}

		/// <summary>
		/// 	Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" /> .
		/// </summary>
		/// <param name="key"> The object to use as the key of the element to add. </param>
		/// <param name="value"> The object to use as the value of the element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the
		/// 	<see cref="T:System.Collections.Generic.IDictionary`2" />
		/// 	.</exception>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.Generic.IDictionary`2" />
		/// 	is read-only.</exception>
		void IDictionary<string, object>.Add(string key, object value)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Add(key, value);
		}

		/// <summary>
		/// 	Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" /> .
		/// </summary>
		/// <param name="item"> The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.Generic.ICollection`1" />
		/// 	is read-only.</exception>
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Add(item);
		}

		/// <summary>
		/// 	Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		void IDictionary.Clear()
		{
			((IDictionary)this.InnerAsDictionary).Clear();
		}

		/// <summary>
		/// 	Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
		/// </summary>
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			((IDictionary<string, object>)this.InnerAsDictionary).Clear();
		}

		/// <summary>
		/// 	Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.
		/// </summary>
		/// <returns> true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false. </returns>
		/// <param name="key"> The key to locate in the <see cref="T:System.Collections.IDictionary" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <filterpriority>2</filterpriority>
		bool IDictionary.Contains(object key)
		{
			return ((IDictionary)this.InnerAsDictionary).Contains(key);
		}

		/// <summary>
		/// 	Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <returns> true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. </returns>
		/// <param name="item"> The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Contains(item);
		}

		/// <summary>
		/// 	Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
		/// </summary>
		/// <returns> true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false. </returns>
		/// <param name="key"> The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" /> . </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).ContainsKey(key);
		}

		/// <summary>
		/// 	Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" /> , starting at a particular <see
		///  	cref="T:System.Array" /> index.
		/// </summary>
		/// <param name="array"> The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see
		///  	cref="T:System.Collections.ICollection" /> . The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index"> The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="array" />
		/// 	is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="index" />
		/// 	is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		/// 	<paramref name="array" />
		/// 	is multidimensional.-or- The number of elements in the source
		/// 	<see cref="T:System.Collections.ICollection" />
		/// 	is greater than the available space from
		/// 	<paramref name="index" />
		/// 	to the end of the destination
		/// 	<paramref name="array" />
		/// 	.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the source
		/// 	<see cref="T:System.Collections.ICollection" />
		/// 	cannot be cast automatically to the type of the destination
		/// 	<paramref name="array" />
		/// 	.</exception>
		/// <filterpriority>2</filterpriority>
		void ICollection.CopyTo(Array array, int index)
		{
			((IDictionary)this.InnerAsDictionary).CopyTo(array, index);
		}

		/// <summary>
		/// 	Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" /> , starting at a particular <see
		///  	cref="T:System.Array" /> index.
		/// </summary>
		/// <param name="array"> The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see
		///  	cref="T:System.Collections.Generic.ICollection`1" /> . The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="arrayIndex"> The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="array" />
		/// 	is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="arrayIndex" />
		/// 	is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		/// 	<paramref name="array" />
		/// 	is multidimensional.-or-The number of elements in the source
		/// 	<see cref="T:System.Collections.Generic.ICollection`1" />
		/// 	is greater than the available space from
		/// 	<paramref name="arrayIndex" />
		/// 	to the end of the destination
		/// 	<paramref name="array" />
		/// 	.-or-Type
		/// 	<paramref name="T" />
		/// 	cannot be cast automatically to the type of the destination
		/// 	<paramref name="array" />
		/// 	.</exception>
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			((IDictionary<string, object>)this.InnerAsDictionary).CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// 	Gets the enumerator for the current associative object instance. Derived types can override the default behavior of returning the enumerator from the result of InnerAsDictionary (arrays should overrride this behavior for example).
		/// </summary>
		/// <returns> An instance of IEnumerator or null. </returns>
		protected virtual IEnumerator CoreGetAssociativeObjectEnumerator()
		{
			// default (except for Array)
			return ((IDictionary)this.InnerAsDictionary).GetEnumerator();
		}

		/// <summary>
		/// 	Gets the dictionary enumerator for the current associative object instance. Derived types can override the default behavior of returning the enumerator from the result of InnerAsDictionary.
		/// </summary>
		/// <returns> An instance of IDictionaryEnumerator or null. </returns>
		protected virtual IDictionaryEnumerator CoreGetAssociativeObjectEnumeratorDict()
		{
			return ((IDictionary)this.InnerAsDictionary).GetEnumerator();
		}

		/// <summary>
		/// 	Gets the enumerator (tick one) for the current associative object instance. Derived types can override the default behavior of returning the enumerator from the result of InnerAsDictionary.
		/// </summary>
		/// <returns> An instance of IEnumerator`1 or null. </returns>
		protected virtual IEnumerator<KeyValuePair<string, object>> CoreGetAssociativeObjectEnumeratorTickOne()
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).GetEnumerator();
		}

		/// <summary>
		/// 	Gets the value of the current associative object instance. Derived types can override the default behavior of returning 'this' (properties should overrride this behavior for example).
		/// </summary>
		/// <returns> A value or null. </returns>
		protected virtual object CoreGetAssociativeObjectValue()
		{
			// default (except for Property)
			return this;
		}

		/// <summary>
		/// 	Gets the enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator or null. </returns>
		public IEnumerator GetAssociativeObjectEnumerator()
		{
			return this.CoreGetAssociativeObjectEnumerator();
		}

		/// <summary>
		/// 	Gets the dictionary enumerator for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IDictionaryEnumerator or null. </returns>
		public IDictionaryEnumerator GetAssociativeObjectEnumeratorDict()
		{
			return this.CoreGetAssociativeObjectEnumeratorDict();
		}

		/// <summary>
		/// 	Gets the enumerator (tick one) for the current associative object instance.
		/// </summary>
		/// <returns> An instance of IEnumerator`1 or null. </returns>
		public IEnumerator<KeyValuePair<string, object>> GetAssociativeObjectEnumeratorTickOne()
		{
			return this.CoreGetAssociativeObjectEnumeratorTickOne();
		}

		/// <summary>
		/// 	Gets the value of the current associative object instance.
		/// </summary>
		/// <returns> A value or null. </returns>
		public object GetAssociativeObjectValue()
		{
			return this.CoreGetAssociativeObjectValue();
		}

		/// <summary>
		/// 	Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see
		///  	cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see
		///  	cref="T:System.Collections.IDictionary" /> object. </returns>
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.CoreGetAssociativeObjectEnumeratorDict();
		}

		/// <summary>
		/// 	Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.CoreGetAssociativeObjectEnumerator();
		}

		/// <summary>
		/// 	Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection. </returns>
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return this.CoreGetAssociativeObjectEnumeratorTickOne();
		}

		/// <summary>
		/// 	Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.
		/// </summary>
		/// <param name="key"> The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	object is read-only.-or- The
		/// 	<see cref="T:System.Collections.IDictionary" />
		/// 	has a fixed size.</exception>
		/// <filterpriority>2</filterpriority>
		void IDictionary.Remove(object key)
		{
			((IDictionary)this.InnerAsDictionary).Remove(key);
		}

		/// <summary>
		/// 	Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" /> .
		/// </summary>
		/// <returns> true if the element is successfully removed; otherwise, false. This method also returns false if <paramref
		///  	name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" /> . </returns>
		/// <param name="key"> The key of the element to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.Generic.IDictionary`2" />
		/// 	is read-only.</exception>
		bool IDictionary<string, object>.Remove(string key)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Remove(key);
		}

		/// <summary>
		/// 	Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
		/// </summary>
		/// <returns> true if <paramref name="item" /> was successfully removed from the <see
		///  	cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. This method also returns false if <paramref
		///  	name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
		/// <param name="item"> The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
		/// <exception cref="T:System.NotSupportedException">The
		/// 	<see cref="T:System.Collections.Generic.ICollection`1" />
		/// 	is read-only.</exception>
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).Remove(item);
		}

		/// <summary>
		/// 	Gets the value associated with the specified key.
		/// </summary>
		/// <returns> true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false. </returns>
		/// <param name="key"> The key whose value to get. </param>
		/// <param name="value"> When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref
		///  	name="value" /> parameter. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="key" />
		/// 	is null.</exception>
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return ((IDictionary<string, object>)this.InnerAsDictionary).TryGetValue(key, out value);
		}

		#endregion
	}
}