//-----------------------------------------------------------------------
// <copyright file="LinkedInApiError.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "error", NamespaceUri = "")]
	public class LinkedInApiError : SoXmlObject
	{
		#region Constructors/Destructors

		public LinkedInApiError()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "error-code", NamespaceUri = "")]
		public string ErrorCode
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "message", NamespaceUri = "")]
		public string Message
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "request-id", NamespaceUri = "")]
		public string RequestId
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "status", NamespaceUri = "")]
		public int Status
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "timestamp", NamespaceUri = "")]
		public long Timestamp
		{
			get;
			set;
		}

		#endregion
	}
}