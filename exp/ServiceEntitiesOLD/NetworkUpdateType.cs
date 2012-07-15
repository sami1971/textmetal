//-----------------------------------------------------------------------
// <copyright file="NetworkUpdateType.cs" company="Beemway">
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
	public enum NetworkUpdateTypes
	{
		[Description("ANSW")]
		AnswerUpdate = 1,

		[Description("APPS")]
		ApplicationUpdate = 2,

		[Description("CONN")]
		ConnectionUpdate = 4,

		[Description("JOBS")]
		PostedAJob = 8,

		[Description("JGRP")]
		JoinedAGroup = 16,

		[Description("PICT")]
		ChangedAPicture = 32,

		[Description("RECU")]
		Recommendations = 64,

		[Description("PRFU")]
		ChangedProfile = 128,

		[Description("QSTN")]
		QuestionUpdate = 256,

		[Description("STAT")]
		[Obsolete]
		StatusUpdate = 512,

		[Description("SHAR")]
		SharedItem = 1024,

		[Description("")]
		All = 2048
	}
}