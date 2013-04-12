/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class PropertyForm : TmForm
	{
		#region Constructors/Destructors

		public PropertyForm(object target)
		{
			this.InitializeComponent();

			this.target = target;
		}

		#endregion

		#region Fields/Constants

		private readonly object target;

		#endregion

		#region Properties/Indexers/Events

		public event EventHandler PropertyUpdate;

		private object Target
		{
			get
			{
				return this.target;
			}
		}

		#endregion

		#region Methods/Operators

		private void PropertyForm_Load(object sender, EventArgs e)
		{
			this.pgShape.SelectedObject = this.Target;
		}

		private void pgShape_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if ((object)this.PropertyUpdate != null)
				this.PropertyUpdate(null, null);
		}

		#endregion
	}
}