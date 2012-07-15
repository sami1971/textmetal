//-----------------------------------------------------------------------
// <copyright file="Person.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "person", NamespaceUri = "")]
	public class Person : SoXmlObject
	{
		#region Constructors/Destructors

		public Person()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "person-activities", NamespaceUri = "")]
		public Activities Activities
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "api-standard-profile-request", NamespaceUri = "")]
		public ApiRequest ApiStandardProfileRequest
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "associations", NamespaceUri = "")]
		public string Associations
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "certifications", NamespaceUri = "")]
		public Certifications Certifications
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "connections", NamespaceUri = "")]
		public Connections Connections
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "current-share", NamespaceUri = "")]
		public Share CurrentShare
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "current-status", NamespaceUri = "")]
		public string CurrentStatus
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "current-status-timestamp", NamespaceUri = "")]
		public long CurrentStatusTimestamp
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "date-of-birth", NamespaceUri = "")]
		public Date DateOfBirth
		{
			get;
			set;
		}

		public string DisplayDistance
		{
			get
			{
				if (this.Distance > 0)
					return this.Distance.ToString();
				else
					return string.Empty;
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "distance", NamespaceUri = "")]
		public int Distance
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "educations", NamespaceUri = "")]
		public Education Educations
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "first-name", NamespaceUri = "")]
		public string FirstName
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "headline", NamespaceUri = "")]
		public string Headline
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "honors", NamespaceUri = "")]
		public string Honors
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "im-accounts", NamespaceUri = "")]
		public IMAccounts IMAccounts
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

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "industry", NamespaceUri = "")]
		public string Industry
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "interests", NamespaceUri = "")]
		public string Interests
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "last-name", NamespaceUri = "")]
		public string LastName
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "location", NamespaceUri = "")]
		public Location Location
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "main-address", NamespaceUri = "")]
		public string MainAddress
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "member-groups", NamespaceUri = "")]
		public MemberGroups MemberGroups
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "member-url-resources", NamespaceUri = "")]
		public MemberUrlResources MemberUrlResources
		{
			get;
			set;
		}

		public string Name
		{
			get
			{
				string name = string.Empty;
				if (string.IsNullOrEmpty(this.FirstName) == false)
					name = string.Format("{0} ", this.FirstName);

				if (string.IsNullOrEmpty(this.LastName) == false && this.LastName.Equals("Private") == false)
					name += this.LastName;

				return name;
			}
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "num-connections", NamespaceUri = "")]
		public int NumberOfConnections
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "num-connections-capped", NamespaceUri = "")]
		public bool NumberOfConnectionsCapped
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "num-recommenders", NamespaceUri = "")]
		public int NumberOfRecommenders
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "phone-numbers", NamespaceUri = "")]
		public PhoneNumbers PhoneNumbers
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "picture-url", NamespaceUri = "")]
		public string PictureUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "positions", NamespaceUri = "")]
		public Positions Positions
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "proposal-comments", NamespaceUri = "")]
		public string ProposalComments
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "public-profile-url", NamespaceUri = "")]
		public string PublicProfileUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "recommendations-received", NamespaceUri = "")]
		public RecommendationsReceived RecommendationsReceived
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "relation-to-viewer", NamespaceUri = "")]
		public Relation RelationToViewer
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "site-public-profile-request", NamespaceUri = "")]
		public SiteRequest SitePublicProfileUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "site-standard-profile-request", NamespaceUri = "")]
		public SiteRequest SiteStandardProfileUrl
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "specialties", NamespaceUri = "")]
		public string Specialties
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "summary", NamespaceUri = "")]
		public string Summary
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "three-current-positions", NamespaceUri = "")]
		public ThreeCurrentPositions ThreeCurrentPositions
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "three-past-positions", NamespaceUri = "")]
		public ThreePastPositions ThreePastPositions
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "twitter-accounts", NamespaceUri = "")]
		public TwitterAccounts TwitterAccounts
		{
			get;
			set;
		}

		public IList<Activity> _Activities
		{
			get
			{
				if ((object)this.Activities == null)
					return new List<Activity>();

				if ((object)this.Activities.Children == null)
					return new List<Activity>();

				return this.Activities.Children.Cast<Activity>().ToList();
			}
		}

		public IList<Certification> _Certifications
		{
			get
			{
				if ((object)this.Certifications == null)
					return new List<Certification>();

				if ((object)this.Certifications.Children == null)
					return new List<Certification>();

				return this.Certifications.Children.Cast<Certification>().ToList();
			}
		}

		public IList<Education> _Educations
		{
			get
			{
				if ((object)this.Educations == null)
					return new List<Education>();

				if ((object)this.Educations.Children == null)
					return new List<Education>();

				return this.Educations.Children.Cast<Education>().ToList();
			}
		}

		public IList<IMAccount> _IMAccounts
		{
			get
			{
				if ((object)this.IMAccounts == null)
					return new List<IMAccount>();

				if ((object)this.IMAccounts.Children == null)
					return new List<IMAccount>();

				return this.IMAccounts.Children.Cast<IMAccount>().ToList();
			}
		}

		public IList<MemberGroup> _MemberGroups
		{
			get
			{
				if ((object)this.MemberGroups == null)
					return new List<MemberGroup>();

				if ((object)this.MemberGroups.Children == null)
					return new List<MemberGroup>();

				return this.MemberGroups.Children.Cast<MemberGroup>().ToList();
			}
		}

		public IList<MemberUrl> _MemberUrlResources
		{
			get
			{
				if ((object)this.MemberUrlResources == null)
					return new List<MemberUrl>();

				if ((object)this.MemberUrlResources.Children == null)
					return new List<MemberUrl>();

				return this.MemberUrlResources.Children.Cast<MemberUrl>().ToList();
			}
		}

		public IList<PhoneNumber> _PhoneNumbers
		{
			get
			{
				if ((object)this.PhoneNumbers == null)
					return new List<PhoneNumber>();

				if ((object)this.PhoneNumbers.Children == null)
					return new List<PhoneNumber>();

				return this.PhoneNumbers.Children.Cast<PhoneNumber>().ToList();
			}
		}

		public IList<Position> _Positions
		{
			get
			{
				if ((object)this.Positions == null)
					return new List<Position>();

				if ((object)this.Positions.Children == null)
					return new List<Position>();

				return this.Positions.Children.Cast<Position>().ToList();
			}
		}

		public IList<Recommendation> _RecommendationsReceived
		{
			get
			{
				if ((object)this.RecommendationsReceived == null)
					return new List<Recommendation>();

				if ((object)this.RecommendationsReceived.Children == null)
					return new List<Recommendation>();

				return this.RecommendationsReceived.Children.Cast<Recommendation>().ToList();
			}
		}

		public IList<Position> _ThreeCurrentPositions
		{
			get
			{
				if ((object)this.ThreeCurrentPositions == null)
					return new List<Position>();

				if ((object)this.ThreeCurrentPositions.Children == null)
					return new List<Position>();

				return this.ThreeCurrentPositions.Children.Cast<Position>().ToList();
			}
		}

		public IList<Position> _ThreePastPositions
		{
			get
			{
				if ((object)this.ThreePastPositions == null)
					return new List<Position>();

				if ((object)this.ThreePastPositions.Children == null)
					return new List<Position>();

				return this.ThreePastPositions.Children.Cast<Position>().ToList();
			}
		}

		public IList<TwitterAccount> _TwitterAccounts
		{
			get
			{
				if ((object)this.TwitterAccounts == null)
					return new List<TwitterAccount>();

				if ((object)this.TwitterAccounts.Children == null)
					return new List<TwitterAccount>();

				return this.TwitterAccounts.Children.Cast<TwitterAccount>().ToList();
			}
		}

		#endregion
	}
}