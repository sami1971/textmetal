﻿//-----------------------------------------------------------------------
// <copyright file="Modified.cs" company="Beemway">
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
	public enum Modified
	{
		All = 0,

		[Description("updated")]
		Updated = 1,

		[Description("new")]
		New = 2
	}
}