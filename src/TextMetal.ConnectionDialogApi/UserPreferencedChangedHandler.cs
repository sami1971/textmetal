//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using Microsoft.Win32;

namespace TextMetal.ConnectionDialogApi
{
	internal sealed class UserPreferenceChangedHandler : IComponent
	{
		#region Constructors/Destructors

		public UserPreferenceChangedHandler(Form form)
		{
			Debug.Assert(form != null);
			SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.HandleUserPreferenceChanged);
			this._form = form;
		}

		~UserPreferenceChangedHandler()
		{
			this.Dispose(false);
		}

		#endregion

		#region Fields/Constants

		private Form _form;

		#endregion

		#region Properties/Indexers/Events

		public event EventHandler Disposed;

		public ISite Site
		{
			get
			{
				return this._form.Site;
			}
			set
			{
				// This shouldn't be called
			}
		}

		#endregion

		#region Methods/Operators

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(this.HandleUserPreferenceChanged);
				if (this.Disposed != null)
					this.Disposed(this, EventArgs.Empty);
			}
		}

		private void HandleUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			// Need to update the font
			IUIService uiService = (this._form.Site != null) ? this._form.Site.GetService(typeof(IUIService)) as IUIService : null;
			if (uiService != null)
			{
				Font newFont = uiService.Styles["DialogFont"] as Font;
				if (newFont != null)
					this._form.Font = newFont;
			}
		}

		#endregion
	}
}