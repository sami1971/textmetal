/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;

using TextMetal.Common.Core;
using TextMetal.Framework.AssociativeModel;

namespace TextMetal.Framework.SourceModel.Primative
{
	public class TextSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the TextSourceStrategy class.
		/// </summary>
		public TextSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			const string PROP_TOKEN_FIRST_ROW_CONTAINS_COLUMN_HEADINGS = "FirstRowIsHeader";
			const string PROP_TOKEN_FIELD_DELIMITER = "FieldDelimiter";
			//const string PROP_TOKEN_ROW_DELIMITER = "RowDelimiter";
			string line;
			ObjectConstruct objectConstruct00;
			ArrayConstruct arrayConstruct00;
			IList<string> values;
			bool firstRowIsHeader = false;
			string firstRowIsHeaderStr;
			string fieldDelimiter;
			string[] headers = null;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			objectConstruct00 = new ObjectConstruct();
			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "TextFileLines";
			objectConstruct00.Items.Add(arrayConstruct00);

			firstRowIsHeaderStr = null;
			if (properties.TryGetValue(PROP_TOKEN_FIRST_ROW_CONTAINS_COLUMN_HEADINGS, out values))
			{
				if ((object)values != null && values.Count == 1)
				{
					firstRowIsHeaderStr = values[0];
					if (!DataType.TryParse<bool>(firstRowIsHeaderStr, out firstRowIsHeader))
						firstRowIsHeader = false;
				}
			}

			fieldDelimiter = null;
			if (properties.TryGetValue(PROP_TOKEN_FIELD_DELIMITER, out values))
			{
				if ((object)values != null && values.Count == 1)
					fieldDelimiter = values[0];
			}

			using (StreamReader streamReader = File.OpenText(sourceFilePath))
			{
				int i = 0;

				while (((line = streamReader.ReadLine()) ?? "").Trim() != "")
				{
					ObjectConstruct objectConstruct01;
					PropertyConstruct propertyConstruct01;
					string[] temp;

					objectConstruct01 = new ObjectConstruct();
					arrayConstruct00.Items.Add(objectConstruct01);

					if (DataType.IsNullOrWhiteSpace(fieldDelimiter))
					{
						propertyConstruct01 = new PropertyConstruct();
						propertyConstruct01.Name = "TextFileLine";
						propertyConstruct01.RawValue = line;
						objectConstruct01.Items.Add(propertyConstruct01);
					}
					else
					{
						temp = line.Split(fieldDelimiter.ToCharArray());

						if (firstRowIsHeader && i == 0)
						{
							headers = temp;
							arrayConstruct00.Items.Remove(objectConstruct01);
							i++;
							continue;
						}

						if ((object)temp != null)
						{
							int j = 0;

							foreach (string pmet in temp)
							{
								propertyConstruct01 = new PropertyConstruct();

								if (firstRowIsHeader && (object)headers != null)
									propertyConstruct01.Name = string.Format("{0}", headers[j++]);
								else
									propertyConstruct01.Name = string.Format("TextFileField_{0:00000000}", j++);

								propertyConstruct01.RawValue = pmet;
								objectConstruct01.Items.Add(propertyConstruct01);
							}
						}
					}

					i++;
				}
			}

			return objectConstruct00;
		}

		#endregion
	}
}