/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
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
		/// 	Initializes a new instance of the TextSourceStrategy class.
		/// </summary>
		public TextSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			const string CMDLN_TOKEN_FIRST_ROW_CONTAINS_COLUMN_HEADINGS = "FirstRowIsHeader";
			const string CMDLN_TOKEN_FIELD_DELIMITER = "FieldDelimiter";
			//const string CMDLN_TOKEN_ROW_DELIMITER = "RowDelimiter";
			string line;
			ModelConstruct modelConstruct;
			ArrayConstruct arrayConstruct;
			IList<string> values;
			bool firstRowIsHeader = false;
			string fieldDelimiter = null;
			string[] headers = null;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			modelConstruct = new ModelConstruct();
			arrayConstruct = new ArrayConstruct();
			arrayConstruct.Name = "TextFileLines";
			modelConstruct.Items.Add(arrayConstruct);

			if (properties.TryGetValue(CMDLN_TOKEN_FIELD_DELIMITER, out values))
			{
				if (values.Count == 1 && DataType.TryParse<string>(values[0], out fieldDelimiter))
#pragma warning disable 1717
					fieldDelimiter = fieldDelimiter;
#pragma warning restore 1717
			}

			if (properties.TryGetValue(CMDLN_TOKEN_FIRST_ROW_CONTAINS_COLUMN_HEADINGS, out values))
			{
				if (values.Count == 1 && DataType.TryParse<bool>(values[0], out firstRowIsHeader))
#pragma warning disable 1717
					firstRowIsHeader = firstRowIsHeader;
#pragma warning restore 1717
			}

			using (StreamReader streamReader = File.OpenText(sourceFilePath))
			{
				int i = 0;

				while (((line = streamReader.ReadLine()) ?? "").Trim() != "")
				{
					ObjectConstruct objectConstruct;
					PropertyConstruct propertyConstruct;
					string[] temp;

					objectConstruct = new ObjectConstruct();
					arrayConstruct.Items.Add(objectConstruct);

					if (DataType.IsNullOrWhiteSpace(fieldDelimiter))
					{
						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "TextFileLine";
						propertyConstruct.Value = line;
						objectConstruct.Items.Add(propertyConstruct);
					}
					else
					{
						temp = line.Split(fieldDelimiter.ToCharArray());

						if (firstRowIsHeader && i == 0)
						{
							headers = temp;
							arrayConstruct.Items.Remove(objectConstruct);
							i++;
							continue;
						}

						if ((object)temp != null)
						{
							int j = 0;

							foreach (string pmet in temp)
							{
								propertyConstruct = new PropertyConstruct();

								if (firstRowIsHeader && (object)headers != null)
									propertyConstruct.Name = string.Format("{0}", headers[j++]);
								else
									propertyConstruct.Name = string.Format("TextFileField_{0:00000000}", j++);

								propertyConstruct.Value = pmet;
								objectConstruct.Items.Add(propertyConstruct);
							}
						}
					}

					i++;
				}
			}

			return modelConstruct;
		}

		#endregion
	}
}