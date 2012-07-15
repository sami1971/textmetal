//-----------------------------------------------------------------------
// <copyright file="FacetField.cs" company="Beemway">
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
	public enum FacetField
	{
		[Description("name")]
		Name = 0,

		[Description("code")]
		Code = 1,

		[Description("name")]
		BucketName = 2,

		[Description("code")]
		BucketCode = 3,

		[Description("count")]
		BucketCount = 4,

		[Description("selected")]
		BucketSelected = 5
	}
}