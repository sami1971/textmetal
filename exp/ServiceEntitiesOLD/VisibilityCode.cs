//-----------------------------------------------------------------------
// <copyright file="VisibilityCode.cs" company="Beemway">
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
	[Flags]
	public enum VisibilityCode
	{
		Unknown = 0,

		[Description("anyone")]
		Anyone = 1,

		[Description("connections-only")]
		ConnectionsOnly = 2
	}
}