/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;

using TextMetal.Core.AssociativeModel;
using TextMetal.Core.Plumbing;

namespace TextMetal.Core.SourceModel.Primative
{
	public class XmlSchemaSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		public XmlSchemaSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static void EnumSchema(IAssociativeXmlObject parentAssociativeXmlObject, XmlSchemaObjectCollection currentXmlSchemaObjectCollection)
		{
			XmlSchemaElement xmlSchemaElement;
			XmlSchemaComplexType xmlSchemaComplexType;
			XmlSchemaSequence xmlSchemaSequence;
			XmlSchemaSimpleType xmlSchemaSimpleType;
			ArrayConstruct arrayConstruct, arrayConstruct2;
			ObjectConstruct objectConstruct, objectConstruct2;
			PropertyConstruct propertyConstruct, propertyConstruct2;

			if ((object)parentAssociativeXmlObject == null)
				throw new ArgumentNullException("parentAssociativeXmlObject");

			if ((object)currentXmlSchemaObjectCollection == null)
				throw new ArgumentNullException("currentXmlSchemaObjectCollection");

			arrayConstruct = new ArrayConstruct();
			arrayConstruct.Name = "XmlSchemaElements";
			parentAssociativeXmlObject.Items.Add(arrayConstruct);

			foreach (XmlSchemaObject xmlSchemaObject in currentXmlSchemaObjectCollection)
			{
				objectConstruct = new ObjectConstruct();
				arrayConstruct.Items.Add(objectConstruct);

				xmlSchemaElement = xmlSchemaObject as XmlSchemaElement;

				if ((object)xmlSchemaElement != null)
				{
					if (DataType.IsNullOrWhiteSpace(xmlSchemaElement.Name) &&
					    !DataType.IsNullOrWhiteSpace(xmlSchemaElement.RefName.Name))
					{
						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementIsRef";
						propertyConstruct.Value = "true";
						objectConstruct.Items.Add(propertyConstruct);

						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementLocalName";
						propertyConstruct.Value = xmlSchemaElement.RefName.Name;
						objectConstruct.Items.Add(propertyConstruct);

						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementNamespace";
						propertyConstruct.Value = xmlSchemaElement.RefName.Namespace;
						objectConstruct.Items.Add(propertyConstruct);

						continue;
					}
					else
					{
						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementIsRef";
						propertyConstruct.Value = "false";
						objectConstruct.Items.Add(propertyConstruct);

						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementLocalName";
						propertyConstruct.Value = xmlSchemaElement.QualifiedName.Name;
						objectConstruct.Items.Add(propertyConstruct);

						propertyConstruct = new PropertyConstruct();
						propertyConstruct.Name = "XmlSchemaElementNamespace";
						propertyConstruct.Value = xmlSchemaElement.QualifiedName.Namespace;
						objectConstruct.Items.Add(propertyConstruct);

						xmlSchemaComplexType = xmlSchemaElement.ElementSchemaType as XmlSchemaComplexType;
						xmlSchemaSimpleType = xmlSchemaElement.ElementSchemaType as XmlSchemaSimpleType;

						if ((object)xmlSchemaSimpleType != null)
						{
							propertyConstruct = new PropertyConstruct();
							propertyConstruct.Name = "XmlSchemaElementSimpleType";
							propertyConstruct.Value = xmlSchemaSimpleType.Datatype.TypeCode.SafeToString();
							objectConstruct.Items.Add(propertyConstruct);
						}
						else if ((object)xmlSchemaComplexType != null)
						{
							arrayConstruct2 = new ArrayConstruct();
							arrayConstruct2.Name = "XmlSchemaAttributes";
							objectConstruct.Items.Add(arrayConstruct2);

							if ((object)xmlSchemaComplexType.Attributes != null)
							{
								foreach (XmlSchemaAttribute xmlSchemaAttribute in xmlSchemaComplexType.Attributes)
								{
									objectConstruct2 = new ObjectConstruct();
									arrayConstruct2.Items.Add(objectConstruct2);

									propertyConstruct2 = new PropertyConstruct();
									propertyConstruct2.Name = "XmlSchemaElementLocalName";
									propertyConstruct2.Value = xmlSchemaAttribute.QualifiedName.Name;
									objectConstruct2.Items.Add(propertyConstruct2);

									propertyConstruct2 = new PropertyConstruct();
									propertyConstruct2.Name = "XmlSchemaElementNamespace";
									propertyConstruct2.Value = xmlSchemaAttribute.QualifiedName.Namespace;
									objectConstruct2.Items.Add(propertyConstruct2);

									propertyConstruct2 = new PropertyConstruct();
									propertyConstruct2.Name = "XmlSchemaElementNamespace";
									propertyConstruct2.Value = xmlSchemaAttribute.AttributeSchemaType.TypeCode.SafeToString();
									objectConstruct2.Items.Add(propertyConstruct2);
								}
							}

							xmlSchemaSequence = xmlSchemaComplexType.ContentTypeParticle as XmlSchemaSequence;

							if ((object)xmlSchemaSequence != null)
								EnumSchema(objectConstruct, xmlSchemaSequence.Items);
						}
					}
				}
			}
		}

		private static void ValidationCallback(object sender, ValidationEventArgs args)
		{
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			XmlSchemaSet xmlSchemaSet;
			XmlSchema xmlSchema;
			ModelConstruct modelConstruct;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			modelConstruct = new ModelConstruct();

			using (Stream stream = File.Open(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				xmlSchema = XmlSchema.Read(stream, ValidationCallback);

			xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(xmlSchema);
			xmlSchemaSet.Compile();

			xmlSchema = xmlSchemaSet.Schemas().Cast<XmlSchema>().ToList()[0];

			EnumSchema(modelConstruct, xmlSchema.Items);

			return modelConstruct;
		}

		#endregion
	}
}