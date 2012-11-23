/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TextMetal.Common.Data.Advanced
{
	/// <summary>
	/// 	Represents a single historical revsion to a database (file).
	/// </summary>
	[Serializable]
	public sealed class DatabaseRevision
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the DatabaseRevision class.
		/// </summary>
		public DatabaseRevision()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<string> statements = new List<string>();
		private int number;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// 	Gets or sets the revision number.
		/// </summary>
		[XmlAttribute("number")]
		public int Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		/// <summary>
		/// 	Gets an ordered list of statements to execute for this revision.
		/// </summary>
		[XmlArray(ElementName = "Statements")]
		[XmlArrayItem(ElementName = "Statement")]
		public List<string> Statements
		{
			get
			{
				return this.statements;
			}
		}

		#endregion
	}
}