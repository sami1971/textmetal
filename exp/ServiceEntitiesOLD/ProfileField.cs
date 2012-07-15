//-----------------------------------------------------------------------
// <copyright file="ProfileField.cs" company="Beemway">
//     Copyright (c) Beemway. All rights reserved.
// </copyright>
// <license>
//     Microsoft Public License (Ms-PL http://opensource.org/licenses/ms-pl.html).
//     Contributors may add their own copyright notice above.
// </license>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	public enum ProfileField
	{
		[Description("id")]
		PersonId = 0,

		[Description("first-name")]
		FirstName = 1,

		[Description("last-name")]
		LastName = 2,

		[Description("headline")]
		Headline = 3,

		[Description("industry")]
		Industry = 4,

		[Description("distance")]
		Distance = 5,

		[Description("current-status")]
		CurrentStatus = 6,

		[Description("current-status-timestamp")]
		CurrentStatusTimestamp = 7,

		[Description("connections")]
		Connections = 8,

		[Description("num-connections")]
		NumberOfConnections = 9,

		[Description("num-connections-capped")]
		NumberOfConnectionsCapped = 10,

		[Description("summary")]
		Summary = 11,

		[Description("specialties")]
		Specialties = 12,

		[Description("proposal-comments")]
		ProposalComments = 13,

		[Description("associations")]
		Associations = 14,

		[Description("honors")]
		Honors = 15,

		[Description("three-current-positions")]
		ThreeCurrentPositions = 16,

		[Description("three-past-positions")]
		ThreePastPositions = 17,

		[Description("num-recommenders")]
		NumberOfRecommenders = 18,

		[Description("phone-numbers")]
		PhoneNumbers = 19,

		[Description("im-accounts")]
		IMAccounts = 20,

		[Description("twitter-accounts")]
		TwitterAccounts = 21,

		[Description("date-of-birth")]
		DateOfBirth = 22,

		[Description("main-address")]
		MainAddress = 23,

		[Description("picture-url")]
		PictureUrl = 24,

		[Description("site-standard-profile-request:(url)")]
		SiteStandardProfileRequestUrl = 25,

		[Description("api-public-profile-request:(url)")]
		ApiPublicProfileRequestUrl = 26,

		[Description("site-public-profile-request:(url)")]
		SitePublicProfileRequestUrl = 27,

		[Description("public-profile-url")]
		PublicProfileUrl = 28,

		[Description("current-share")]
		CurrentShare = 29,

		[Description("interests")]
		Interests = 30,

		[Description("name")]
		LocationName = 101,

		[Description("country:(code)")]
		LocationCountryCode = 102,

		[Description("distance")]
		RelationToViewerDistance = 201,

		[Description("num-related-connections")]
		RelationToViewerNumberOfRelatedConnections = 202,

		[Description("related-connections")]
		RelationToViewerRelatedConnections = 203,

		[Description("url")]
		MemberUrlUrl = 301,

		[Description("name")]
		MemberUrlName = 302,

		[Description("url")]
		ApiStandardProfileRequestUrl = 401,

		[Description("headers")]
		ApiStandardProfileRequestHeaders = 402,

		[Description("id")]
		PositionId = 501,

		[Description("title")]
		PositionTitle = 502,

		[Description("summary")]
		PositionSummary = 503,

		[Description("start-date")]
		PositionStartDate = 504,

		[Description("end-date")]
		PositionEndDate = 505,

		[Description("is-current")]
		PositionIsCurrent = 506,

		[Description("name")]
		PositionCompanyName = 1401,

		[Description("ticker")]
		PositionCompanyTicker = 1402,

		[Description("industry")]
		PositionCompanyIndustry = 1403,

		[Description("size")]
		PositionCompanySize = 1404,

		[Description("type")]
		PositionCompanyType = 1405,

		[Description("id")]
		EducationId = 601,

		[Description("school-name")]
		EducationSchoolName = 602,

		[Description("field-of-study")]
		EducationFieldOfStudy = 603,

		[Description("start-date")]
		EducationStartDate = 604,

		[Description("end-date")]
		EducationEndDate = 605,

		[Description("degree")]
		EducationDegree = 606,

		[Description("activities")]
		EducationActivities = 607,

		[Description("id")]
		RecommendationId = 701,

		[Description("recommendation-type")]
		RecommendationRecommendationType = 702,

		[Description("recommender")]
		RecommendationRecommender = 703,

		[Description("certifications")]
		Certifications = 801,

		[Description("start-date")]
		CertificationStartDate = 802,

		[Description("end-date")]
		CertificationEndDate = 803,

		[Description("id")]
		CertificationId = 804,

		[Description("name")]
		CertificationName = 805,

		[Description("number")]
		CertificationNumber = 806,

		[Description("authority:(name)")]
		CertificationAuthority = 807,

		[Description("languages")]
		Languages = 1001,

		[Description("publications")]
		Publications = 1101,

		[Description("patents")]
		Patents = 1201,

		[Description("skills")]
		Skills = 1301,

		[Description("id")]
		LanguageId = 1002,

		[Description("language:(name)")]
		LanguageName = 1003,

		[Description("proficiency:(name)")]
		LanguageProficiency = 1004,

		[Description("id")]
		PatentId = 1202,

		[Description("title")]
		PatentTitle = 1203,

		[Description("summary")]
		PatentSummary = 1204,

		[Description("number")]
		PatentNumber = 1205,

		[Description("status:(name)")]
		PatentStatus = 1206,

		[Description("office:(name)")]
		PatentOffice = 1207,

		[Description("inventors:(name)")]
		PatentInventors = 1208,

		[Description("date")]
		PatentDate = 1209,

		[Description("url")]
		PatentUrl = 1210,

		[Description("id")]
		SkillId = 1302,

		[Description("skill:(name)")]
		SkillName = 1303,

		[Description("proficiency:(name)")]
		SkillProficiency = 1304,

		[Description("years:(name)")]
		SkillYears = 1305,

		[Description("id")]
		PublicationId = 1102,

		[Description("title")]
		PublicationTitle = 1103,

		[Description("publisher:(name)")]
		PublicationPublisher = 1104,

		[Description("authors:(name)")]
		PublicationAuthors = 1105,

		[Description("date")]
		PublicationDate = 1106,

		[Description("url")]
		PublicationUrl = 1107,

		[Description("summary")]
		PublicationSummary = 1108,

		[Description("url")]
		MemberGroupName = 1401,
	}
}