//-----------------------------------------------------------------------
// <copyright file="Question.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "question", NamespaceUri = "")]
	public class Question : SoXmlObject
	{
		#region Constructors/Destructors

		public Question()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IList<Answer> Answers
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<Answer>().ToList();
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "author", NamespaceUri = "")]
		public Person Author
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "id", NamespaceUri = "")]
		public string Id
		{
			get;
			set;
		}

		//[XmlArray("question-categories")]
		//[XmlArrayItem("question-category")]
		public IList<QuestionCategory> QuestionCategories
		{
			get
			{
				if ((object)this.Children == null)
					return null;

				return this.Children.Cast<QuestionCategory>().ToList();
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "title", NamespaceUri = "")]
		public string Title
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "web-url", NamespaceUri = "")]
		public string WebUrl
		{
			get;
			set;
		}

		#endregion
	}
}