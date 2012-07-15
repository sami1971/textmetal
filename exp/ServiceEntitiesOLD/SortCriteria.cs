//-----------------------------------------------------------------------
// <copyright file="SortCriteria.cs" company="Beemway">
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
	public enum SortCriteria
	{
		[Description("connections")]
		Connections = 0,

		[Description("recommenders")]
		Recommenders = 1,

		[Description("distance")]
		Distance = 2,

		[Description("relevance")]
		Relevance = 3
	}
}