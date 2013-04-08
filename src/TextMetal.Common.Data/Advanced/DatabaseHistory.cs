/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;

using TextMetal.Common.Core;

namespace TextMetal.Common.Data.Advanced
{
	/// <summary>
	/// Represents an ordered set of historical revsions to a database (file).
	/// </summary>
	[Serializable]
	[XmlRoot(ElementName = "History", Namespace = "http://www.textmetal.com/api/v5.0.0")]
	public sealed class DatabaseHistory
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DatabaseHistory class.
		/// </summary>
		public DatabaseHistory()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<DatabaseRevision> revisions = new List<DatabaseRevision>();
		private string doesSchemaTrackingExistCommandText;
		private string getSchemaVersionCommandText;
		private string incrementSchemaVersionCommandText;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets or sets the SQL required to determine if schema tracking is enabled in the database (file).
		/// </summary>
		[XmlElement("DoesSchemaTrackingExistCommandText", Order = 0)]
		public string DoesSchemaTrackingExistCommandText
		{
			get
			{
				return (this.doesSchemaTrackingExistCommandText ?? "").Trim();
			}
			set
			{
				this.doesSchemaTrackingExistCommandText = (value ?? "").Trim();
			}
		}

		/// <summary>
		/// Gets or sets the SQL required to determine the schema version of the database (file).
		/// </summary>
		[XmlElement("GetSchemaVersionCommandText", Order = 1)]
		public string GetSchemaVersionCommandText
		{
			get
			{
				return (this.getSchemaVersionCommandText ?? "").Trim();
			}
			set
			{
				this.getSchemaVersionCommandText = (value ?? "").Trim();
			}
		}

		/// <summary>
		/// Gets or sets the SQL required to increment the schema version in the database (file).
		/// </summary>
		[XmlElement("IncrementSchemaVersionCommandText", Order = 2)]
		public string IncrementSchemaVersionCommandText
		{
			get
			{
				return (this.incrementSchemaVersionCommandText ?? "").Trim();
			}
			set
			{
				this.incrementSchemaVersionCommandText = (value ?? "").Trim();
			}
		}

		/// <summary>
		/// Gets a list of ordered revisions.
		/// </summary>
		[XmlArray(ElementName = "Revisions", Order = 3)]
		[XmlArrayItem(ElementName = "Revision")]
		public List<DatabaseRevision> Revisions
		{
			get
			{
				return this.revisions;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// For a given UnitOfWorkContext, perform a schema upgrade if necessary. The ordered set of revisions are executed from version+1 to version[n].
		/// </summary>
		/// <param name="unitOfWorkContext"> The target UnitOfWorkContext. </param>
		/// <returns> A value indicating whether any changes were needed against the target database (file). </returns>
		public bool PerformSchemaUpgrade(IUnitOfWorkContext unitOfWorkContext)
		{
			bool changed = false;
			DatabaseRevision revision;
			string svalue;
			int ivalue;
			int schemaRevision, currentSchemaRevision, recordsAffected;
			IList<IDictionary<string, object>> results;

			if ((object)unitOfWorkContext == null)
				throw new ArgumentNullException("unitOfWorkContext");

			if (this.Revisions.Count < 1)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			ivalue = unitOfWorkContext.FetchScalar<int>(CommandType.Text, this.DoesSchemaTrackingExistCommandText, null);

			if (ivalue != 1)
			{
				// revision -1
				schemaRevision = -1;
			}
			else
			{
				svalue = unitOfWorkContext.FetchScalar<string>(CommandType.Text, this.GetSchemaVersionCommandText, null);

				if (!DataType.TryParse(svalue, out schemaRevision))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}

			currentSchemaRevision = this.Revisions.Max(x => x.Number);

			if (schemaRevision != currentSchemaRevision)
			{
				changed = true;

				for (int workingRevision = schemaRevision + 1; workingRevision <= currentSchemaRevision; workingRevision++)
				{
					int _workingRevision = workingRevision;

					revision = this.Revisions.SingleOrDefault(rh => rh.Number == _workingRevision);

					if ((object)revision == null)
						throw new InvalidOperationException(String.Format("The revision number '{0}' was not found. Revsions must be sequential without any gaps.", _workingRevision));

					if (revision.Statements.Count < 1)
						throw new InvalidOperationException("A revsion must have at least one statement.");

					foreach (string statement in revision.Statements)
						unitOfWorkContext.ExecuteDictionary(CommandType.Text, statement, null, out recordsAffected);

					results = unitOfWorkContext.ExecuteDictionary(CommandType.Text, this.IncrementSchemaVersionCommandText, null, out recordsAffected);

					if (recordsAffected != 1)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");
				}
			}

			ivalue = unitOfWorkContext.FetchScalar<int>(CommandType.Text, this.DoesSchemaTrackingExistCommandText, null);

			if (ivalue != 1)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			svalue = unitOfWorkContext.FetchScalar<string>(CommandType.Text, this.GetSchemaVersionCommandText, null);

			if (!DataType.TryParse(svalue, out schemaRevision))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (schemaRevision != currentSchemaRevision)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return changed;
		}

		#endregion
	}
}