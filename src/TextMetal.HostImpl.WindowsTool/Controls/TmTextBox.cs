﻿/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TextMetal.Common.Core;

namespace TextMetal.HostImpl.WindowsTool.Controls
{
	public class TmTextBox : TextBox
	{
		#region Constructors/Destructors

		public TmTextBox()
		{
		}

		#endregion

		#region Fields/Constants

		private bool isInputValid = true;
		private Color? oldBackColor;
		private string valueType;

		#endregion

		#region Properties/Indexers/Events

		private bool IsInputValid
		{
			get
			{
				return this.isInputValid;
			}
			set
			{
				this.isInputValid = value;
			}
		}

		private Color? OldBackColor
		{
			get
			{
				return this.oldBackColor;
			}
			set
			{
				this.oldBackColor = value;
			}
		}

		public override string Text
		{
			get
			{
				return (base.Text ?? "").ToUpper();
			}
			set
			{
				base.Text = (value ?? "").ToUpper();
			}
		}

		public string ValueType
		{
			get
			{
				return this.valueType;
			}
			set
			{
				this.valueType = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			// handle a strange TextBox anomoly per MSDN forums:			
			this.BeginInvoke((MethodInvoker)this.SelectAll);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (char.IsLower(e.KeyChar))
				e.KeyChar = char.ToUpper(e.KeyChar);

			base.OnKeyPress(e);
		}

		protected override void OnReadOnlyChanged(EventArgs e)
		{
			base.OnReadOnlyChanged(e);

			if (this.ReadOnly)
			{
				this.OldBackColor = base.BackColor;
				this.BackColor = SystemColors.Control;
			}
			else
			{
				if (this.OldBackColor != null)
					this.BackColor = (Color)this.OldBackColor;

				this.OldBackColor = null;
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnValidating(CancelEventArgs e)
		{
			bool wasValid;
			bool isValid;

			base.OnValidating(e);

			wasValid = this.IsInputValid;

			if (this.CoreIsEmpty())
			{
				this.CoreInputValidation(true);
				this.IsInputValid = true;
			}
			else
			{
				if (DataType.IsNullOrWhiteSpace(this.ValueType))
					isValid = this.IsInputValid = this.CoreIsValid();
				else
					isValid = this.IsInputValid = this.CoreIsValid(Type.GetType(this.ValueType, false) ?? typeof(string));

				if (!(!wasValid && !isValid)) // prevent weirdness
					this.CoreInputValidation(isValid);
			}

			e.Cancel = !this.IsInputValid;
		}

		#endregion
	}
}