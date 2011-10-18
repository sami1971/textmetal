//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextMetal.ConnectionDialogApi
{
	public class ContextHelpEventArgs : HelpEventArgs
	{
		#region Constructors/Destructors

		public ContextHelpEventArgs(DataConnectionDialogContext context, Point mousePos)
			: base(mousePos)
		{
			this._context = context;
		}

		#endregion

		#region Fields/Constants

		private DataConnectionDialogContext _context;

		#endregion

		#region Properties/Indexers/Events

		public DataConnectionDialogContext Context
		{
			get
			{
				return this._context;
			}
		}

		#endregion
	}
}