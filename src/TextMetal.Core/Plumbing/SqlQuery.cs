/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace TextMetal.Core.Plumbing
{
	[Serializable]
	[XmlRoot(ElementName = "SqlQuery", Namespace = "http://www.textmetal.com/api/v4.4.0")]
	public sealed class SqlQuery
	{
		#region Constructors/Destructors

		public SqlQuery()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly List<SqlQuery> subQueries = new List<SqlQuery>();
		private string key;
		private int? order;
		private string text;
		private CommandType type = CommandType.Text;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttribute("key")]
		public string Key
		{
			get
			{
				return (this.key ?? "").Trim();
			}
			set
			{
				this.key = (value ?? "").Trim();
			}
		}

		[XmlIgnore]
		public int? Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		[XmlArray(ElementName = "SubQueries", Order = 1)]
		[XmlArrayItem(ElementName = "SqlQuery")]
		public List<SqlQuery> SubQueries
		{
			get
			{
				return this.subQueries;
			}
		}

		[XmlElement("Text", Order = 0)]
		public string Text
		{
			get
			{
				return (this.text ?? "").Trim();
			}
			set
			{
				this.text = (value ?? "").Trim();
			}
		}

		[XmlAttribute("type")]
		public CommandType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		[XmlAttribute("order")]
		public string _Order
		{
			get
			{
				return this.Order.SafeToString();
			}
			set
			{
				int ivalue;

				if (DataType.IsNullOrWhiteSpace(value))
					this.Order = null;
				else
				{
					if (!DataType.TryParse<int>(value, out ivalue))
						this.Order = null;
					else
						this.Order = ivalue;
				}
			}
		}

		#endregion
	}
}