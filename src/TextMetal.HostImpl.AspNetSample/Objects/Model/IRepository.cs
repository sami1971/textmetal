﻿/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.HostImpl.AspNetSample.Objects.Model
{
	public partial interface IRepository
	{
		#region Methods/Operators

		bool TrySendEmailTemplate(string templateReosurceName, object modelObject);

		bool TryWriteEventLogEntry(string eventText);

		#endregion
	}
}