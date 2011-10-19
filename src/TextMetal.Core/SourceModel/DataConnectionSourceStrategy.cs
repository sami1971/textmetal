/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using TextMetal.ConnectionDialogApi;
using TextMetal.Core.Plumbing;

namespace TextMetal.Core.SourceModel
{
	public abstract class DataConnectionSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		protected DataConnectionSourceStrategy(Type connectionType)
		{
			this.connectionType = connectionType;
		}

		#endregion

		#region Fields/Constants

		private string connectionString;
		private Type connectionType;

		#endregion

		#region Properties/Indexers/Events

		protected string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			private set
			{
				this.connectionString = value;
			}
		}

		protected Type ConnectionType
		{
			get
			{
				return this.connectionType;
			}
			private set
			{
				this.connectionType = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			this.ConnectionString = sourceFilePath;

			if (this.ConnectionString == "?")
			{
				if (!this.GetConnectionStringInteractive())
					return null;
			}

			if (DataType.IsNullOrWhiteSpace(this.ConnectionString))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return new object();
		}

		private bool GetConnectionStringInteractive()
		{
			DataConnectionDialog dataConnectionDialog;

			using (dataConnectionDialog = new DataConnectionDialog())
			{
				DataSource.AddStandardDataSources(dataConnectionDialog);

				if (DataConnectionDialog.Show(dataConnectionDialog) == DialogResult.OK)
				{
					this.ConnectionString = dataConnectionDialog.ConnectionString;
					this.ConnectionType = dataConnectionDialog.SelectedDataProvider.TargetConnectionType;

					return true;
				}
			}

			return false;
		}

		#endregion
	}
}