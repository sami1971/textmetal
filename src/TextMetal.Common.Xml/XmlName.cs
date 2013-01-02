/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;

namespace TextMetal.Common.Xml
{
	/// <summary>
	/// Represents a local name and namespace URI of an XML element.
	/// </summary>
	public sealed class XmlName
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the XmlName class.
		/// </summary>
		public XmlName()
		{
		}

		#endregion

		#region Fields/Constants

		private string localName;
		private string namespaceUri;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets or sets the local name of the XML element.
		/// </summary>
		public string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		/// <summary>
		/// Gets or sets the namespace URI of the XML element.
		/// </summary>
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Performs a custom equals test against two XML name objects using value semantics over the local name and namespace URI.
		/// </summary>
		/// <param name="a"> The first XML name to test. </param>
		/// <param name="b"> The second XML name object to test. </param>
		/// <returns> A value indicating whther the two XML name objects are equal using value semantics. </returns>
		private static bool TestEquals(XmlName a, XmlName b)
		{
			return (a.LocalName == b.LocalName) &&
			       (a.NamespaceUri == b.NamespaceUri);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> .
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" /> ; otherwise, false.
		/// </returns>
		/// <param name="obj">
		/// The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" /> .
		/// </param>
		/// <filterpriority> 2 </filterpriority>
		public override bool Equals(object obj)
		{
			XmlName that;

			if ((object)obj == null)
				return false;

			that = obj as XmlName;

			if ((object)that == null)
				return false;

			return TestEquals(this, that);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" /> .
		/// </returns>
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> .
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" /> .
		/// </returns>
		public override string ToString()
		{
			return (this.NamespaceUri.SafeToString() + "#" + this.LocalName.SafeToString());
		}

		/// <summary>
		/// Determines whether two specified XML name objects are equal.
		/// </summary>
		/// <param name="a"> The first XML name to test. </param>
		/// <param name="b"> The second XML name object to test. </param>
		/// <returns> A value indicating whther the two XML name objects are equal using value semantics. </returns>
		public static bool operator ==(XmlName a, XmlName b)
		{
			return TestEquals(a, b);
		}

		/// <summary>
		/// Determines whether two specified XML name objects are not equal.
		/// </summary>
		/// <param name="a"> The first XML name to test. </param>
		/// <param name="b"> The second XML name object to test. </param>
		/// <returns> A value indicating whther the two XML name objects are not equal using value semantics. </returns>
		public static bool operator !=(XmlName a, XmlName b)
		{
			return !TestEquals(a, b);
		}

		#endregion
	}
}