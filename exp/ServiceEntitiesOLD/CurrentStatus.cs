//-----------------------------------------------------------------------
// <copyright file="CurrentStatus.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Xml;
using System.Xml.Schema;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "current-status", NamespaceUri = "")]
	public class CurrentStatus : SoXmlObject
	{
		#region Constructors/Destructors

		public CurrentStatus()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public string Status
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteValue(this.Status);
		}

		#endregion
	}
}