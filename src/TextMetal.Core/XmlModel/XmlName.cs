/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.XmlModel
{
	public sealed class XmlName
	{
		#region Constructors/Destructors

		public XmlName()
		{
		}

		#endregion

		#region Fields/Constants

		private string localName;
		private string namespaceUri;

		#endregion

		#region Properties/Indexers/Events

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

		private static bool TestEquals(XmlName a, XmlName b)
		{
			return (a.LocalName == b.LocalName) &&
			       (a.NamespaceUri == b.NamespaceUri);
		}

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

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			return (this.NamespaceUri.SafeToString() + "#" + this.LocalName.SafeToString());
		}

		public static bool operator ==(XmlName a, XmlName b)
		{
			return TestEquals(a, b);
		}

		public static bool operator !=(XmlName a, XmlName b)
		{
			return !TestEquals(a, b);
		}

		#endregion
	}
}