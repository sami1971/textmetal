//-----------------------------------------------------------------------
// <copyright file="UpdateContent.cs" company="Beemway">
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
	[XmlElementMapping(LocalName = "update-content", NamespaceUri = "")]
	public class UpdateContent : SoXmlObject
	{
		#region Constructors/Destructors

		public UpdateContent()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "job", NamespaceUri = "")]
		public Job Job
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "person", NamespaceUri = "")]
		public Person Person
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "question", NamespaceUri = "")]
		public Question Question
		{
			get;
			set;
		}

		#endregion
	}
}