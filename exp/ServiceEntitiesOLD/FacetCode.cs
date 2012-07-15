//-----------------------------------------------------------------------
// <copyright file="FacetCode.cs" company="Beemway">
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
	public enum FacetCode
	{
		[Description("location")]
		Location = 0,

		[Description("industry")]
		Industry = 1,

		[Description("network")]
		Network = 2,

		[Description("language")]
		Language = 3,

		[Description("current-company")]
		CurrentCompany = 4,

		[Description("past-company")]
		PastCompany = 5,

		[Description("school")]
		School = 6
	}
}