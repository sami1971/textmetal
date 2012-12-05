/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;
using System.Windows.Forms;

using TextMetal.HostImpl.WindowsTool.Controls;

using Message = TextMetal.Common.Core.Message;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class MessageForm : TmForm
	{
		#region Constructors/Destructors

		public MessageForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private Message[] messages;

		#endregion

		#region Properties/Indexers/Events

		public string Message
		{
			get
			{
				return this.lblMessage.CoreGetValue();
			}
			set
			{
				this.lblMessage.CoreSetValue(value);
			}
		}

		public Message[] Messages
		{
			get
			{
				return this.messages;
			}
			set
			{
				this.messages = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void Cancel()
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close(); // direct
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.CoreText = string.Format("{0} Studio", Program.AssemblyInformation.Product);

			this.RefreshMessages();
		}

		private void Okay()
		{
			this.DialogResult = DialogResult.OK;
			this.Close(); // direct
		}

		private void RefreshMessages()
		{
			TreeNode tnCategory;
			TreeNode tnMessage;

			if ((object)this.messages != null)
			{
				var categories = this.messages.Select(m => (m.Category ?? "").Trim()).Distinct();

				foreach (string category in categories)
				{
					string _category = category; // prevent closure issue

					if ((category ?? "").Trim() != "")
					{
						tnCategory = new TreeNode(category, 0, 0);
						this.tvMessages.Nodes.Add(tnCategory);
					}
					else
						tnCategory = null;

					foreach (Message message in this.messages.Where(m => (m.Category ?? "").Trim() == _category))
					{
						tnMessage = new TreeNode(message.Description, (int)message.Severity, (int)message.Severity);

						if ((object)tnCategory != null)
							tnCategory.Nodes.Add(tnMessage);
						else
							this.tvMessages.Nodes.Add(tnMessage);
					}
				}
			}

			this.tvMessages.ExpandAll();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Cancel();
		}

		private void btnOkay_Click(object sender, EventArgs e)
		{
			this.Okay();
		}

		#endregion
	}
}