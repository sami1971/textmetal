/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.ObjectModel;

namespace TextMetal.Core.XmlModel
{
	/// <summary>
	/// 	Provides a concrete implementation for XML object collections.
	/// </summary>
	public class XmlObjectCollection<TXmlObject> : Collection<TXmlObject>, IXmlObjectCollection<TXmlObject>
		where TXmlObject : IXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the XmlObjectCollection class.
		/// </summary>
		/// <param name="site"> The containing site XML object. </param>
		public XmlObjectCollection(IXmlObject site)
		{
			if ((object)site == null)
				throw new ArgumentNullException("site");

			this.site = site;
		}

		#endregion

		#region Fields/Constants

		private readonly IXmlObject site;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets the site XML object or null if this is unattached.
		/// </summary>
		public IXmlObject Site
		{
			get
			{
				return this.site;
			}
		}

		#endregion
	}
}