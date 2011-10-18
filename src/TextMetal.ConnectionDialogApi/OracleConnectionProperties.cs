//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace TextMetal.ConnectionDialogApi
{
	public class OracleConnectionProperties : AdoDotNetConnectionProperties
	{
		#region Constructors/Destructors

		public OracleConnectionProperties()
			: base("System.Data.OracleClient")
		{
			this.LocalReset();
		}

		#endregion

		#region Properties/Indexers/Events

		public override bool IsComplete
		{
			get
			{
				if (!(this.ConnectionStringBuilder["Data Source"] is string) ||
				    (this.ConnectionStringBuilder["Data Source"] as string).Length == 0)
					return false;
				if (!(bool)this.ConnectionStringBuilder["Integrated Security"] &&
				    (!(this.ConnectionStringBuilder["User ID"] is string) ||
				     (this.ConnectionStringBuilder["User ID"] as string).Length == 0))
					return false;
				return true;
			}
		}

		#endregion

		#region Methods/Operators

		private void LocalReset()
		{
			// We always start with unicode turned on
			this["Unicode"] = true;
		}

		public override void Reset()
		{
			base.Reset();
			this.LocalReset();
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