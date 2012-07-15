//-----------------------------------------------------------------------
// <copyright file="IsLiked.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "is-liked", NamespaceUri = "")]
	public class IsLiked : SoXmlObject
	{
		#region Constructors/Destructors

		public IsLiked()
		{
		}

		#endregion

		#region Fields/Constants

		private const string IsLikedElementName = "is-liked";
		private const string RootElementName = "is-liked";

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "is-liked", NamespaceUri = "")]
		public bool IsNetworkUpdateLiked
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
			writer.WriteValue(this.IsNetworkUpdateLiked.ToString().ToLowerInvariant());
		}

		#endregion
	}
}