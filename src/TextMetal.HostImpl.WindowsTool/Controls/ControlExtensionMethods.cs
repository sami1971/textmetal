/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using TextMetal.Common.Core;
using TextMetal.HostImpl.WindowsTool.Forms;

namespace TextMetal.HostImpl.WindowsTool.Controls
{
	public static class ControlExtensionMethods
	{
		#region Fields/Constants

		private static readonly IDictionary<Control, Color> previousBackgroundColors = new Dictionary<Control, Color>();

		#endregion

		#region Methods/Operators

		public static void CoreBindSelectionItems<TValue>(this ComboBox comboBox, IListItem<TValue>[] listItems, bool useZerothItemIndex)
		{
			List<IListItem<TValue>> tempListItems;

			if ((object)comboBox == null)
				throw new ArgumentNullException("comboBox");

			tempListItems = new List<IListItem<TValue>>();

			if ((object)listItems != null)
				tempListItems.AddRange(listItems);

			if (useZerothItemIndex)
				tempListItems.Insert(0, ListItem<TValue>.Empty);

			foreach (IListItem<TValue> listItem in tempListItems)
				listItem.Text = (listItem.Text ?? "").ToUpper();

			comboBox.BeginUpdate();
			comboBox.Items.Clear();
			comboBox.Items.AddRange(tempListItems.ToArray());
			comboBox.SelectedIndex = useZerothItemIndex ? 0 : -1;
			comboBox.EndUpdate();
		}

		public static TmForm CoreGetParentForm(this Control control)
		{
			// Iterative method...
			Control current;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			current = control;

			while ((object)current != null)
			{
				if (current is TmForm)
					return (TmForm)current;

				current = current.Parent;
			}

			return null;
		}

		public static string CoreGetSelectedText(this ComboBox comboBox, bool useZerothItemIndex)
		{
			if ((object)comboBox == null)
				throw new ArgumentNullException("comboBox");

			return comboBox.CoreGetValue();
		}

		public static TValue CoreGetSelectedValue<TValue>(this ComboBox comboBox)
		{
			IListItem<TValue> listItem;

			if ((object)comboBox == null)
				throw new ArgumentNullException("comboBox");

			if (comboBox.SelectedIndex == -1)
				return default(TValue);

			listItem = (IListItem<TValue>)comboBox.SelectedItem;

			if ((object)listItem == null)
				return default(TValue);

			return (TValue)listItem.Value;
		}

		public static bool? CoreGetValue(this CheckBox checkBox)
		{
			if ((object)checkBox == null)
				throw new ArgumentNullException("checkBox");

			switch (checkBox.CheckState)
			{
				case CheckState.Checked:
					return true;
				case CheckState.Unchecked:
					return false;
				default:
					return null;
			}
		}

		public static string CoreGetValue(this Control control)
		{
			if ((object)control == null)
				throw new ArgumentNullException("control");

			if (control is MaskedTextBox)
				return (object)((MaskedTextBox)control).MaskedTextProvider == null ? "" : ((MaskedTextBox)control).MaskedTextProvider.ToString(false, false);
			else
				return control.Text;
		}

		public static TValue CoreGetValue<TValue>(this Control control)
		{
			TValue value;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			if (DataType.TryParse<TValue>(control.Text, out value))
				return value;
			else
				return default(TValue);
		}

		public static object CoreGetValue(this Control control, Type targetType)
		{
			object value;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if (DataType.TryParse(targetType, control.Text, out value))
				return value;
			else
				return DataType.DefaultValue(targetType);
		}

		public static bool? CoreGetValue(this RadioButton radioButton)
		{
			if ((object)radioButton == null)
				throw new ArgumentNullException("radioButton");

			return radioButton.Checked;
		}

		public static void CoreInputValidation(this Control control, bool isValid)
		{
			Color previousBackgroundColor;

			if (previousBackgroundColors.TryGetValue(control, out previousBackgroundColor))
				previousBackgroundColors.Remove(control);
			else
				previousBackgroundColor = control.BackColor;

			if (!isValid)
			{
				previousBackgroundColors.Add(control, control.BackColor);
				control.BackColor = Color.Pink;
				control.CoreGetParentForm().CoreSetToolTipText(control, "Input validation failed.");
			}
			else
			{
				control.BackColor = previousBackgroundColor;
				control.CoreGetParentForm().CoreSetToolTipText(control, "");
			}
		}

		public static bool CoreIsEmpty(this Control control)
		{
			string value;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			value = CoreGetValue(control);

			return DataType.IsNullOrWhiteSpace(value);
		}

		public static bool CoreIsValid(this Control control)
		{
			if ((object)control == null)
				throw new ArgumentNullException("control");

			if (control is MaskedTextBox)
				return ((MaskedTextBox)control).MaskCompleted;
			else
				return true;
		}

		public static bool CoreIsValid<TValue>(this Control control)
		{
			TValue value;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			if (!CoreIsValid(control))
				return false;
			else
				return DataType.TryParse<TValue>(control.Text, out value);
		}

		public static bool CoreIsValid(this Control control, Type targetType)
		{
			object value;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if (!CoreIsValid(control))
				return false;
			else
				return DataType.TryParse(targetType, control.Text, out value);
		}

		public static void CoreSetParentFormDirty(this Control control, bool isDirty)
		{
			TmForm parent;

			if ((object)control == null)
				throw new ArgumentNullException("control");

			parent = control.CoreGetParentForm();

			if ((object)parent != null)
				parent.CoreIsDirty = true;
		}

		public static void CoreSetSelectedText(this ComboBox comboBox, string value, bool useZerothItemIndex)
		{
			int index;

			if ((object)comboBox == null)
				throw new ArgumentNullException("comboBox");

			index = comboBox.FindString(value);

			comboBox.SelectedIndex = index;

			if (index == -1)
				comboBox.CoreSetValue(value);
		}

		public static void CoreSetSelectedValue<TValue>(this ComboBox comboBox, TValue value, bool useZerothItemIndex)
		{
			int index;

			if ((object)comboBox == null)
				throw new ArgumentNullException("comboBox");

			if ((object)value == (object)default(TValue))
			{
				comboBox.SelectedIndex = useZerothItemIndex ? 0 : -1;
				return;
			}

			index = 0;
			foreach (IListItem<TValue> item in comboBox.Items)
			{
				if (value.Equals(item.Value))
				{
					comboBox.SelectedIndex = index;
					break;
				}

				index++;
			}
		}

		public static void CoreSetValue(this CheckBox checkBox, bool? value)
		{
			if ((object)checkBox == null)
				throw new ArgumentNullException("checkBox");

			if (value == null)
				checkBox.CheckState = CheckState.Indeterminate;
			else
				checkBox.CheckState = (bool)value ? CheckState.Checked : CheckState.Unchecked;
		}

		public static void CoreSetValue(this RadioButton radioButton, bool? value)
		{
			if ((object)radioButton == null)
				throw new ArgumentNullException("radioButton");

			radioButton.Checked = value ?? false;
		}

		public static void CoreSetValue(this Control control, string value)
		{
			if ((object)control == null)
				throw new ArgumentNullException("control");

			control.Text = value;
		}

		public static void CoreSetValue<TValue>(this Control control, object value, string format)
		{
			if ((object)control == null)
				throw new ArgumentNullException("control");

			if ((object)value == null)
				CoreSetValue(control, string.Empty);
			else
				CoreSetValue(control, value.SafeToString(format));
		}

		#endregion
	}
}