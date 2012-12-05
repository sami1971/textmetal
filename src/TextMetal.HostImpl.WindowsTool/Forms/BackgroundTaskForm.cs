/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class BackgroundTaskForm : TmForm
	{
		#region Constructors/Destructors

		public BackgroundTaskForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private bool asyncCanceledOut;
		private Exception asyncErrorOut;

		private Func<object, object> asyncMethod;
		private object asyncParameterIn;
		private object asyncResultOut;

		#endregion

		#region Properties/Indexers/Events

		private bool AsyncCanceledOut
		{
			get
			{
				return this.asyncCanceledOut;
			}
			set
			{
				this.asyncCanceledOut = value;
			}
		}

		private Exception AsyncErrorOut
		{
			get
			{
				return this.asyncErrorOut;
			}
			set
			{
				this.asyncErrorOut = value;
			}
		}

		private Func<object, object> AsyncMethod
		{
			get
			{
				return this.asyncMethod;
			}
			set
			{
				this.asyncMethod = value;
			}
		}

		private object AsyncParameterIn
		{
			get
			{
				return this.asyncParameterIn;
			}
			set
			{
				this.asyncParameterIn = value;
			}
		}

		private object AsyncResultOut
		{
			get
			{
				return this.asyncResultOut;
			}
			set
			{
				this.asyncResultOut = value;
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				const int CS_NOCLOSE = 0x200;
				CreateParams cp;

				cp = base.CreateParams;
				cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;

				return cp;
			}
		}

		#endregion

		#region Methods/Operators

		public static DialogResult Show(
			IWin32Window owner,
			string caption,
			Func<object, object> asyncMethod,
			object asyncParameterIn,
			out bool asyncWasCanceled, out Exception asyncExceptionOrNull, out object asyncDoneParameter)
		{
			DialogResult result;

			if ((object)asyncMethod == null)
				throw new ArgumentNullException("asyncMethod");

			using (BackgroundTaskForm backgroundTaskForm = new BackgroundTaskForm())
			{
				backgroundTaskForm.btnCancel.Enabled = false;
				backgroundTaskForm.lblCaption.Text = caption;
				backgroundTaskForm.AsyncMethod = asyncMethod;
				backgroundTaskForm.AsyncParameterIn = asyncParameterIn;

				result = backgroundTaskForm.ShowDialog(owner);

				asyncWasCanceled = backgroundTaskForm.AsyncCanceledOut;
				asyncExceptionOrNull = backgroundTaskForm.AsyncErrorOut;
				asyncDoneParameter = backgroundTaskForm.AsyncResultOut;
			}

			return result;
		}

		private void Cancel()
		{
			this.tmrMain.Enabled = false;
			this.DialogResult = DialogResult.Cancel;
			this.Close(); // direct
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.CoreText = string.Format("{0}", Program.AssemblyInformation.Product);
			this.tmrMain.Enabled = true;
		}

		protected override void CoreShown()
		{
			base.CoreSetup();

			this.backgroundWorker.RunWorkerAsync(this.AsyncParameterIn);
		}

		private void DoWork(DoWorkEventArgs e)
		{
			if ((object)this.AsyncMethod != null)
				e.Result = this.AsyncMethod(e.Argument);
		}

		private void RunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			this.tmrMain.Enabled = false;

			this.AsyncCanceledOut = e.Cancelled;
			this.AsyncErrorOut = e.Error;

			if ((object)e.Error == null)
				this.AsyncResultOut = e.Result;

			this.DialogResult = DialogResult.OK;
			this.Close(); // direct
		}

		private void TimerTick()
		{
			int value;

			value = this.pbarMain.Value;

			if (value < 99)
				value += 9;
			else
				value = 0;

			this.pbarMain.Value = value;
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.DoWork(e);
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.RunWorkerCompleted(e);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Cancel();
		}

		private void tmrMain_Tick(object sender, EventArgs e)
		{
			this.TimerTick();
		}

		#endregion
	}
}