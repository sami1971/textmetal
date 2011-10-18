//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace TextMetal.ConnectionDialogApi
{
	public interface IDataConnectionUIControl
	{
		#region Methods/Operators

		void Initialize(IDataConnectionProperties connectionProperties);

		void LoadProperties();

		#endregion
	}
}