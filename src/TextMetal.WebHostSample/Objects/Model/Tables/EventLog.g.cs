﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by:
// TextMetal 4.3.0.27741;
// 		Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
//		Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
//		Project URL: http://code.google.com/p/textmetal/
//
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------

/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;

namespace TextMetal.WebHostSample.Objects.Model.Tables
{
	public partial class EventLog : Object
	{		
		#region Constructors/Destructors
		
		public EventLog()
		{
		}
		
		#endregion
		
		#region Fields/Constants

		// public const string EVENT_LOG_ID = "EventLogId";
		// public const string EVENT_TEXT = "EventText";
		// public const string CREATION_TIMESTAMP = "CreationTimestamp";
		// public const string MODIFICATION_TIMESTAMP = "ModificationTimestamp";
		// public const string LOGICAL_DELETE = "LogicalDelete";
		private Nullable<Int32> @eventLogId;
		private String @eventText;
		private Nullable<DateTime> @creationTimestamp;
		private Nullable<DateTime> @modificationTimestamp;
		private Nullable<Boolean> @logicalDelete;

		#endregion

		#region Properties/Indexers/Events
		
		public virtual bool IsNew
		{
			get
			{
				return this.EventLogId == default(Nullable<Int32>);
			}
			set
			{
				if(value)
					this.EventLogId =  default(Nullable<Int32>);
			}
		}
		
		/* PRIMARY_KEY */
		public Nullable<Int32> EventLogId
		{
			get
			{
				return this.@eventLogId;
			}
			set
			{
				this.@eventLogId = value;
			}
		}
		
		public String EventText
		{
			get
			{
				return this.@eventText;
			}
			set
			{
				this.@eventText = value;
			}
		}
		
		public Nullable<DateTime> CreationTimestamp
		{
			get
			{
				return this.@creationTimestamp;
			}
			set
			{
				this.@creationTimestamp = value;
			}
		}
		
		public Nullable<DateTime> ModificationTimestamp
		{
			get
			{
				return this.@modificationTimestamp;
			}
			set
			{
				this.@modificationTimestamp = value;
			}
		}
		
		public Nullable<Boolean> LogicalDelete
		{
			get
			{
				return this.@logicalDelete;
			}
			set
			{
				this.@logicalDelete = value;
			}
		}
		
		#endregion
	}
}
