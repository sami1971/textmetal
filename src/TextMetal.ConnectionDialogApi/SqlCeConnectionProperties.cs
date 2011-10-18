//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data.SqlServerCe;
using System.IO;

namespace TextMetal.ConnectionDialogApi
{
	public class SqlCeConnectionProperties : AdoDotNetConnectionProperties
	{
		#region Constructors/Destructors

		public SqlCeConnectionProperties()
			: base("System.Data.SqlClient")
		{
		}

		#endregion

		#region Fields/Constants

		private const int m_intDatabaseFileNeedsUpgrading = 25138;

		#endregion

		#region Properties/Indexers/Events

		public override bool IsComplete
		{
			get
			{
				string dataSource = this["Data Source"] as string;

				if (String.IsNullOrEmpty(dataSource))
					return false;

				// Ensure file extension: 
				if (!(Path.GetExtension(dataSource).Equals(".sdf", StringComparison.OrdinalIgnoreCase)))
					return false;

				return true;
			}
		}

		#endregion

		#region Methods/Operators

		public override void Reset()
		{
			base.Reset();
		}

		public override void Test()
		{
			string testString = this.ToTestString();

			// Create a connection object
			SqlCeConnection connection = new SqlCeConnection();

			// Try to open it
			try
			{
				connection.ConnectionString = this.ToFullString();
				connection.Open();
			}
			catch (SqlCeException e)
			{
				// Customize the error message for upgrade required
				if (e.NativeError == m_intDatabaseFileNeedsUpgrading)
					throw new InvalidOperationException(Resources.SqlCeConnectionProperties_FileNeedsUpgrading);
				throw;
			}
			finally
			{
				connection.Dispose();
			}
		}

		protected override string ToTestString()
		{
			bool savedPooling = (bool)this.ConnectionStringBuilder["Pooling"];
			bool wasDefault = !this.ConnectionStringBuilder.ShouldSerialize("Pooling");
			this.ConnectionStringBuilder["Pooling"] = false;
			string testString = this.ConnectionStringBuilder.ConnectionString;
			this.ConnectionStringBuilder["Pooling"] = savedPooling;
			if (wasDefault)
				this.ConnectionStringBuilder.Remove("Pooling");
			return testString;
		}

		#endregion
	}
}