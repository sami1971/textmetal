/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.XmlModel
{
	public sealed class XmlPersistEngine : IXmlPersistEngine
	{
		#region Constructors/Destructors

		public XmlPersistEngine()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly Dictionary<XmlName, Type> knownXmlObjectTypeRegistrations = new Dictionary<XmlName, Type>();
		private Type knownXmlTextObjectTypeRegistration;

		#endregion

		#region Properties/Indexers/Events

		private Dictionary<XmlName, Type> KnownXmlObjectTypeRegistrations
		{
			get
			{
				return this.knownXmlObjectTypeRegistrations;
			}
		}

		private Type KnownXmlTextObjectTypeRegistration
		{
			get
			{
				return this.knownXmlTextObjectTypeRegistration;
			}
			set
			{
				this.knownXmlTextObjectTypeRegistration = value;
			}
		}

		#endregion

		#region Methods/Operators

		public void ClearAllKnowns()
		{
			this.KnownXmlObjectTypeRegistrations.Clear();
			this.KnownXmlTextObjectTypeRegistration = null;
		}

		public IXmlObject DeserializeFromXml(string fileName)
		{
			IXmlObject document;

			if ((object)fileName == null)
				throw new ArgumentNullException("fileName");

			if (DataType.IsWhiteSpace(fileName))
				throw new ArgumentOutOfRangeException("fileName");

			using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				document = this.DeserializeFromXml(stream);
				return document;
			}
		}

		public IXmlObject DeserializeFromXml(Stream stream)
		{
			XmlTextReader xmlTextReader;
			IXmlObject document;

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			// DO NOT USE A USGIN BLOCK HERE (CALLER OWNS STREAM) !!!
			xmlTextReader = new XmlTextReader(stream);
			document = this.DeserializeFromXml(xmlTextReader);
			return document;
		}

		private IXmlObject DeserializeFromXml(Stack<IXmlObject> contextStack, XmlName previousElementXmlName, XmlName currentElementXmlName, IDictionary<XmlName, string> attributes, IXmlTextObject overrideCurrentXmlTextObject)
		{
			IXmlObject currentXmlObject;
			Type currentType;
			PropertyInfo[] currentPropertyInfos;
			XmlElementMappingAttribute currentXmlElementMappingAttribute;

			IXmlObject parentXmlObject = null;
			Type parentType;
			PropertyInfo[] parentPropertyInfos;
			XmlElementMappingAttribute parentXmlElementMappingAttribute = null;

			Dictionary<PropertyInfo, XmlAttributeMappingAttribute> parentPropertyToAttributeMappings;
			Dictionary<PropertyInfo, XmlChildElementMappingAttribute> parentPropertyToChildElementMappings;

			Dictionary<PropertyInfo, XmlAttributeMappingAttribute> currentPropertyToAttributeMappings;
			Dictionary<PropertyInfo, XmlChildElementMappingAttribute> currentPropertyToChildElementMappings;

			KeyValuePair<PropertyInfo, XmlChildElementMappingAttribute>? parentPropertyToChildElementMapping = null;

			int attributeCount;

			XmlAttributeMappingAttribute xmlAttributeMappingAttribute;
			XmlChildElementMappingAttribute xmlChildElementMappingAttribute;
			XmlElementMappingAttribute parentOfChildXmlElementMappingAttribute;

			Match match;
			string parentName;
			string propertyName;
			const string PROP_REGEX =
				@"( [a-zA-Z_][a-zA-Z_0-9\-]+ ) \. ( [a-zA-Z_][a-zA-Z_0-9\-]+ )";

			if ((object)contextStack == null)
				throw new ArgumentNullException("contextStack");

			if ((object)currentElementXmlName == null)
				throw new ArgumentNullException("currentElementXmlName");

			if ((object)attributes == null)
				throw new ArgumentNullException("attributes");

			if (DataType.IsNullOrWhiteSpace(currentElementXmlName.LocalName))
				throw new ArgumentOutOfRangeException("currentElementXmlName");

			if (contextStack.Count > 0)
			{
				// element on stack is parent
				parentXmlObject = contextStack.Peek();

				if ((object)parentXmlObject == null)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				parentType = parentXmlObject.GetType();
				parentPropertyInfos = parentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

				if ((object)parentPropertyInfos == null)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				parentXmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(parentType);

				if ((object)parentXmlElementMappingAttribute == null)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				// create mapping tables
				parentPropertyToAttributeMappings = new Dictionary<PropertyInfo, XmlAttributeMappingAttribute>();
				parentPropertyToChildElementMappings = new Dictionary<PropertyInfo, XmlChildElementMappingAttribute>();

				foreach (PropertyInfo parentPropertyInfo in parentPropertyInfos)
				{
					xmlAttributeMappingAttribute = Reflexion.GetOneAttribute<XmlAttributeMappingAttribute>(parentPropertyInfo);
					xmlChildElementMappingAttribute = Reflexion.GetOneAttribute<XmlChildElementMappingAttribute>(parentPropertyInfo);

					attributeCount = 0;
					attributeCount += (object)xmlAttributeMappingAttribute == null ? 0 : 1;
					attributeCount += (object)xmlChildElementMappingAttribute == null ? 0 : 1;

					if (attributeCount > 1)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if ((object)xmlAttributeMappingAttribute != null)
						parentPropertyToAttributeMappings.Add(parentPropertyInfo, xmlAttributeMappingAttribute);
					else if ((object)xmlChildElementMappingAttribute != null)
						parentPropertyToChildElementMappings.Add(parentPropertyInfo, xmlChildElementMappingAttribute);
				}

				// is this a text element node?
				if ((object)overrideCurrentXmlTextObject != null)
				{
					string svalue;
					object ovalue;

					parentPropertyToChildElementMapping = parentPropertyToChildElementMappings
						.Where(x => x.Value.ChildElementType == ChildElementType.TextValue)
						.Select(x => (KeyValuePair<PropertyInfo, XmlChildElementMappingAttribute>?)x).Where(x => x.Value.Value.LocalName == overrideCurrentXmlTextObject.Name.LocalName &&
						                                                                                         x.Value.Value.NamespaceUri == overrideCurrentXmlTextObject.Name.NamespaceUri).SingleOrDefault();

					if ((object)parentPropertyToChildElementMapping == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (!parentPropertyToChildElementMapping.Value.Key.CanWrite)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					svalue = overrideCurrentXmlTextObject.Text;

					if (!DataType.TryParse(parentPropertyToChildElementMapping.Value.Key.PropertyType, svalue, out ovalue))
						ovalue = DataType.DefaultValue(parentPropertyToChildElementMapping.Value.Key.PropertyType);

					if (!Reflexion.SetLogicalPropertyValue(parentXmlObject, parentPropertyToChildElementMapping.Value.Key.Name, ovalue))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					return null;
				}

				// check if this is parent DOT property (<Parent.Property />) convention
				match = Regex.Match(currentElementXmlName.LocalName, PROP_REGEX, RegexOptions.IgnorePatternWhitespace);

				if ((object)match != null && match.Success)
				{
					parentName = match.Groups[1].Value;
					propertyName = match.Groups[2].Value;
				}
				else
				{
					parentName = null;
					propertyName = currentElementXmlName.LocalName;
				}

				if ((object)previousElementXmlName != null &&
				    (object)parentName != null &&
				    parentName != previousElementXmlName.LocalName)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				// check to see if this is an accessor element
				parentPropertyToChildElementMapping = parentPropertyToChildElementMappings
					.Where(x => x.Value.ChildElementType != ChildElementType.TextValue)
					.Select(x => (KeyValuePair<PropertyInfo, XmlChildElementMappingAttribute>?)x)
					.Where(x => (x.Value.Value.NamespaceUri == currentElementXmlName.NamespaceUri &&
					             x.Value.Value.LocalName == propertyName)).SingleOrDefault();

				if ((object)parentPropertyToChildElementMapping != null)
				{
					if (!parentPropertyToChildElementMapping.Value.Key.CanWrite)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					parentOfChildXmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(parentPropertyToChildElementMapping.Value.Key.PropertyType);

					if ((object)parentOfChildXmlElementMappingAttribute == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					// override
					currentElementXmlName.LocalName = parentOfChildXmlElementMappingAttribute.LocalName;
					currentElementXmlName.NamespaceUri = parentOfChildXmlElementMappingAttribute.NamespaceUri;
				}
			}

			currentXmlObject = this.ResolveXmlObject(currentElementXmlName);

			if ((object)currentXmlObject == null)
				throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message '{0}'", currentElementXmlName));

			currentXmlObject.Parent = parentXmlObject;

			currentType = currentXmlObject.GetType();
			currentPropertyInfos = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			if ((object)currentPropertyInfos == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			currentXmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(currentType);

			if ((object)currentXmlElementMappingAttribute == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			// create mapping tables
			currentPropertyToAttributeMappings = new Dictionary<PropertyInfo, XmlAttributeMappingAttribute>();
			currentPropertyToChildElementMappings = new Dictionary<PropertyInfo, XmlChildElementMappingAttribute>();

			foreach (PropertyInfo currentPropertyInfo in currentPropertyInfos)
			{
				xmlAttributeMappingAttribute = Reflexion.GetOneAttribute<XmlAttributeMappingAttribute>(currentPropertyInfo);
				xmlChildElementMappingAttribute = Reflexion.GetOneAttribute<XmlChildElementMappingAttribute>(currentPropertyInfo);

				attributeCount = 0;
				attributeCount += (object)xmlAttributeMappingAttribute == null ? 0 : 1;
				attributeCount += (object)xmlChildElementMappingAttribute == null ? 0 : 1;

				if (attributeCount > 1)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				if ((object)xmlAttributeMappingAttribute != null)
					currentPropertyToAttributeMappings.Add(currentPropertyInfo, xmlAttributeMappingAttribute);
				else if ((object)xmlChildElementMappingAttribute != null)
					currentPropertyToChildElementMappings.Add(currentPropertyInfo, xmlChildElementMappingAttribute);
			}

			// iterate over attributes of current element
			if ((object)currentPropertyToAttributeMappings != null)
			{
				foreach (KeyValuePair<PropertyInfo, XmlAttributeMappingAttribute> currentPropertyToAttributeMapping in currentPropertyToAttributeMappings.OrderBy(m => m.Value.Order).ThenBy(m => m.Value.LocalName))
				{
					string svalue;
					object ovalue;
					var _currentPropertyToAttributeMapping = currentPropertyToAttributeMapping;

					if (!currentPropertyToAttributeMapping.Key.CanWrite)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					svalue = attributes.Where(a => a.Key.LocalName == _currentPropertyToAttributeMapping.Value.LocalName &&
					                               a.Key.NamespaceUri == _currentPropertyToAttributeMapping.Value.NamespaceUri)
						.Select(a => a.Value).SingleOrDefault();

					if (!DataType.TryParse(currentPropertyToAttributeMapping.Key.PropertyType, svalue, out ovalue))
						ovalue = DataType.DefaultValue(currentPropertyToAttributeMapping.Key.PropertyType);

					if (!Reflexion.SetLogicalPropertyValue(currentXmlObject, currentPropertyToAttributeMapping.Key.Name, ovalue))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");
				}
			}

			if ((object)parentPropertyToChildElementMapping != null)
			{
				if (!Reflexion.SetLogicalPropertyValue(parentXmlObject, parentPropertyToChildElementMapping.Value.Key.Name, currentXmlObject))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");
			}
			else if ((object)parentXmlElementMappingAttribute != null)
			{
				if ((object)parentXmlObject == null)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				if (parentXmlElementMappingAttribute.ChildElementModel == ChildElementModel.Content)
					parentXmlObject.Content = currentXmlObject;
				else if (parentXmlElementMappingAttribute.ChildElementModel == ChildElementModel.Items)
				{
					if ((object)parentXmlObject.Items == null)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					// new collection type check
					//if (!parentXmlElementMappingAttribute.AnonymousChildrenAllowedType.IsAssignableFrom(currentType))
					//throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					parentXmlObject.Items.Add(currentXmlObject);
				}
			}

			return currentXmlObject;
		}

		public IXmlObject DeserializeFromXml(XmlTextReader xmlTextReader)
		{
			XmlName elementXmlName = null, attributeXmlName, previousElementXmlName;

			IXmlObject documentXmlObject = null;
			IXmlObject currentXmlObject;

			Stack<IXmlObject> contextStack;
			bool isEmptyElement, isTextElement;
			Dictionary<XmlName, string> attributes;

			if ((object)xmlTextReader == null)
				throw new ArgumentNullException("xmlTextReader");

			attributes = new Dictionary<XmlName, string>();
			contextStack = new Stack<IXmlObject>();

			while (xmlTextReader.Read())
			{
				if (xmlTextReader.NodeType == XmlNodeType.CDATA ||
				    xmlTextReader.NodeType == XmlNodeType.Text)
				{
					attributes.Clear();

					isTextElement = this.IsTextElement(contextStack, elementXmlName ?? new XmlName());
					currentXmlObject = this.DeserializeFromXmlText(contextStack, xmlTextReader.Value, isTextElement ? elementXmlName : null);

					if (isTextElement)
						this.DeserializeFromXml(contextStack, null, elementXmlName, attributes, (IXmlTextObject)currentXmlObject);
				}
				else if (xmlTextReader.NodeType == XmlNodeType.Element) // we only care about elements
				{
					Debug.WriteLine(string.Format("{2} <{0}{1}>", xmlTextReader.LocalName, xmlTextReader.IsEmptyElement ? " /" : "", xmlTextReader.IsEmptyElement ? "empty" : "start"));

					previousElementXmlName = elementXmlName;

					elementXmlName = new XmlName()
					                 {
					                 	LocalName = xmlTextReader.LocalName,
					                 	NamespaceUri = xmlTextReader.NamespaceURI
					                 };

					isEmptyElement = xmlTextReader.IsEmptyElement;
					isTextElement = this.IsTextElement(contextStack, elementXmlName);

					// iterate over attributes of current element
					attributes.Clear();

					for (int ai = 0; ai < xmlTextReader.AttributeCount; ai++)
					{
						xmlTextReader.MoveToAttribute(ai);

						attributeXmlName = new XmlName()
						                   {
						                   	LocalName = xmlTextReader.LocalName,
						                   	NamespaceUri = xmlTextReader.NamespaceURI
						                   };

						attributes.Add(attributeXmlName, xmlTextReader.Value);
					}

					attributeXmlName = null;

					if (true /*!isTextElement // remember the dummy element*/)
					{
						if (!isTextElement)
						{
							// deserialize current XML object
							currentXmlObject = this.DeserializeFromXml(contextStack, previousElementXmlName, elementXmlName, attributes, null);
						}
						else
						{
							// dummy current XML object ...
							// (the parent so depth counts are correct and IsTextElement() works)
							currentXmlObject = contextStack.Peek();
						}

						if (contextStack.Count <= 0)
						{
							// document element is current element when no context present
							documentXmlObject = currentXmlObject;
						}

						// push current XML object as parent XML object if there are children
						if (!isEmptyElement)
							contextStack.Push(currentXmlObject);
					}
				}
				else if (xmlTextReader.NodeType == XmlNodeType.EndElement)
				{
					Debug.WriteLine(string.Format("end <{0}>", xmlTextReader.LocalName));

					elementXmlName = new XmlName()
					                 {
					                 	LocalName = xmlTextReader.LocalName,
					                 	NamespaceUri = xmlTextReader.NamespaceURI
					                 };

					isTextElement = this.IsTextElement(contextStack, elementXmlName);

					if (true /*!isTextElement // remember the dummy element*/)
					{
						if (contextStack.Count < 1)
							throw new InvalidOperationException("TODO (enhancement): add meaningful message");

						// pop element off stack (unwind)
						contextStack.Pop();
					}

					elementXmlName = null;
				}
			}

			if (contextStack.Count != 0)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return documentXmlObject;
		}

		private IXmlTextObject DeserializeFromXmlText(Stack<IXmlObject> contextStack, string textValue, XmlName xmlName)
		{
			IXmlTextObject currentXmlTextObject;
			IXmlObject parentXmlObject;
			Type parentType;
			XmlElementMappingAttribute parentXmlElementMappingAttribute;

			if ((object)contextStack == null)
				throw new ArgumentNullException("contextStack");

			if ((object)textValue == null)
				throw new ArgumentNullException("textValue");

			if (contextStack.Count <= 0)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			currentXmlTextObject = this.ResolveXmlTextObject(textValue);

			if ((object)currentXmlTextObject == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			parentXmlObject = contextStack.Peek();

			if ((object)parentXmlObject == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			parentType = parentXmlObject.GetType();
			parentXmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(parentType);

			if ((object)parentXmlElementMappingAttribute == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			// we indeed need these checks; it is not ok to have text element and not allow anonymous children
			if (parentXmlElementMappingAttribute.ChildElementModel != ChildElementModel.Items)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if ((object)parentXmlObject.Items == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if ((object)xmlName != null)
				currentXmlTextObject.Name = xmlName;
			else
			{
				currentXmlTextObject.Parent = parentXmlObject;
				parentXmlObject.Items.Add(currentXmlTextObject);
			}

			return currentXmlTextObject;
		}

		private bool IsTextElement(Stack<IXmlObject> contextStack, XmlName xmlName)
		{
			IXmlObject parentXmlObject;
			Type parentType;
			PropertyInfo[] parentPropertyInfos;

			XmlAttributeMappingAttribute xmlAttributeMappingAttribute;
			XmlChildElementMappingAttribute xmlChildElementMappingAttribute;

			int attributeCount;

			if ((object)contextStack == null)
				throw new ArgumentNullException("contextStack");

			if ((object)xmlName == null)
				throw new ArgumentNullException("xmlName");

			if (contextStack.Count < 1)
				return false;

			parentXmlObject = contextStack.Peek();
			parentType = parentXmlObject.GetType();
			parentPropertyInfos = parentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			if ((object)parentPropertyInfos != null)
			{
				foreach (PropertyInfo parentPropertyInfo in parentPropertyInfos)
				{
					xmlAttributeMappingAttribute = Reflexion.GetOneAttribute<XmlAttributeMappingAttribute>(parentPropertyInfo);
					xmlChildElementMappingAttribute = Reflexion.GetOneAttribute<XmlChildElementMappingAttribute>(parentPropertyInfo);

					attributeCount = 0;
					attributeCount += (object)xmlAttributeMappingAttribute == null ? 0 : 1;
					attributeCount += (object)xmlChildElementMappingAttribute == null ? 0 : 1;

					if (attributeCount > 1)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if ((object)xmlChildElementMappingAttribute == null)
						continue;

					if (xmlChildElementMappingAttribute.ChildElementType == ChildElementType.TextValue &&
					    xmlChildElementMappingAttribute.LocalName == xmlName.LocalName &&
					    xmlChildElementMappingAttribute.NamespaceUri == xmlName.NamespaceUri)
						return true;
				}
			}

			return false;
		}

		public void RegisterKnownXmlObject<TObject>()
			where TObject : IXmlObject
		{
			Type targetType;

			targetType = typeof(TObject);

			this.RegisterKnownXmlObject(targetType);
		}

		public void RegisterKnownXmlObject(Type targetType)
		{
			XmlName xmlName;
			XmlElementMappingAttribute xmlElementMappingAttribute;

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			xmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(targetType);

			if ((object)xmlElementMappingAttribute == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			xmlName = new XmlName()
			          {
			          	LocalName = xmlElementMappingAttribute.LocalName,
			          	NamespaceUri = xmlElementMappingAttribute.NamespaceUri
			          };

			if (this.KnownXmlObjectTypeRegistrations.ContainsKey(xmlName))
				throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message | XML for target type '{0}'.", targetType.FullName));

			if (!typeof(IXmlObject).IsAssignableFrom(targetType))
				throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message | '{0}'.", targetType.FullName));

			this.KnownXmlObjectTypeRegistrations.Add(xmlName, targetType);
		}

		public void RegisterKnownXmlTextObject<TObject>()
			where TObject : IXmlTextObject
		{
			Type targetType;

			targetType = typeof(TObject);

			this.RegisterKnownXmlTextObject(targetType);
		}

		public void RegisterKnownXmlTextObject(Type targetType)
		{
			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)this.KnownXmlTextObjectTypeRegistration != null)
				throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message | XML for target type '{0}'.", targetType.FullName));

			if (!typeof(IXmlTextObject).IsAssignableFrom(targetType))
				throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message | XML for target type '{0}'.", targetType.FullName));

			this.KnownXmlTextObjectTypeRegistration = targetType;
		}

		private IXmlObject ResolveXmlObject(XmlName xmlName)
		{
			object value;
			IXmlObject xmlObject;
			Type targetType;

			if ((object)xmlName == null)
				throw new ArgumentNullException("xmlName");

			if (!this.KnownXmlObjectTypeRegistrations.TryGetValue(xmlName, out targetType))
				return null;

			if ((object)targetType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			value = Activator.CreateInstance(targetType);

			xmlObject = value as IXmlObject;

			if ((object)xmlObject == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			return xmlObject;
		}

		private IXmlTextObject ResolveXmlTextObject(string text)
		{
			object value;
			IXmlTextObject xmlTextObject;
			Type targetType;

			if ((object)this.KnownXmlTextObjectTypeRegistration == null)
				return null;

			targetType = this.KnownXmlTextObjectTypeRegistration;

			if ((object)targetType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			value = Activator.CreateInstance(targetType);

			xmlTextObject = value as IXmlTextObject;

			if ((object)xmlTextObject == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			xmlTextObject.Text = text;

			return xmlTextObject;
		}

		public void SerializeToXml(IXmlObject document, string fileName)
		{
			if ((object)document == null)
				throw new ArgumentNullException("document");

			if ((object)fileName == null)
				throw new ArgumentNullException("fileName");

			if (DataType.IsWhiteSpace(fileName))
				throw new ArgumentOutOfRangeException("fileName");

			using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
				this.SerializeToXml(document, stream);
		}

		public void SerializeToXml(IXmlObject document, Stream stream)
		{
			XmlTextWriter xmlTextWriter;

			if ((object)document == null)
				throw new ArgumentNullException("document");

			if ((object)stream == null)
				throw new ArgumentNullException("stream");

			// DO NOT USE A USGIN BLOCK HERE (CALLER OWNS STREAM) !!!
			xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8);
			this.SerializeToXml(document, xmlTextWriter);
			xmlTextWriter.Flush();
		}

		/// <summary>
		/// MUST FLUSH XMLTEXTWRITER!
		/// </summary>
		/// <param name="document"></param>
		/// <param name="xmlTextWriter"></param>
		public void SerializeToXml(IXmlObject document, XmlTextWriter xmlTextWriter)
		{
			if ((object)document == null)
				throw new ArgumentNullException("document");

			if ((object)xmlTextWriter == null)
				throw new ArgumentNullException("xmlTextWriter");

			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.Indentation = 1;
			xmlTextWriter.IndentChar = '\t';

			this.SerializeToXml(xmlTextWriter, document, null);
		}

		private void SerializeToXml(XmlTextWriter xmlTextWriter, IXmlObject currentXmlObject, XmlName overrideXmlName)
		{
			IXmlTextObject currentXmlTextObject;
			Type currentType;
			PropertyInfo[] currentPropertyInfos;
			XmlElementMappingAttribute currentXmlElementMappingAttribute;

			Dictionary<PropertyInfo, XmlAttributeMappingAttribute> currentPropertyToAttributeMappings;
			Dictionary<PropertyInfo, XmlChildElementMappingAttribute> currentPropertyToChildElementMappings;

			XmlAttributeMappingAttribute xmlAttributeMappingAttribute;
			XmlChildElementMappingAttribute xmlChildElementMappingAttribute;

			int attributeCount;
			object ovalue;
			string svalue;

			if ((object)xmlTextWriter == null)
				throw new ArgumentNullException("xmlTextWriter");

			if ((object)currentXmlObject == null)
				throw new ArgumentNullException("currentXmlObject");

			currentXmlTextObject = currentXmlObject as IXmlTextObject;

			if ((object)currentXmlTextObject != null)
			{
				if ((object)currentXmlTextObject.Name == null ||
				    DataType.IsNullOrWhiteSpace(currentXmlTextObject.Name.LocalName))
				{
					xmlTextWriter.WriteCData(currentXmlTextObject.Text);
					return;
				}
			}

			currentType = currentXmlObject.GetType();
			currentPropertyInfos = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

			if ((object)currentPropertyInfos == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			currentXmlElementMappingAttribute = Reflexion.GetOneAttribute<XmlElementMappingAttribute>(currentType);

			if ((object)currentXmlElementMappingAttribute == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			// create mapping tables
			currentPropertyToAttributeMappings = new Dictionary<PropertyInfo, XmlAttributeMappingAttribute>();
			currentPropertyToChildElementMappings = new Dictionary<PropertyInfo, XmlChildElementMappingAttribute>();

			foreach (PropertyInfo currentPropertyInfo in currentPropertyInfos)
			{
				xmlAttributeMappingAttribute = Reflexion.GetOneAttribute<XmlAttributeMappingAttribute>(currentPropertyInfo);
				xmlChildElementMappingAttribute = Reflexion.GetOneAttribute<XmlChildElementMappingAttribute>(currentPropertyInfo);

				attributeCount = 0;
				attributeCount += (object)xmlAttributeMappingAttribute == null ? 0 : 1;
				attributeCount += (object)xmlChildElementMappingAttribute == null ? 0 : 1;

				if (attributeCount > 1)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				if ((object)xmlAttributeMappingAttribute != null)
					currentPropertyToAttributeMappings.Add(currentPropertyInfo, xmlAttributeMappingAttribute);
				else if ((object)xmlChildElementMappingAttribute != null)
					currentPropertyToChildElementMappings.Add(currentPropertyInfo, xmlChildElementMappingAttribute);
			}

			// begin current element
			if ((object)overrideXmlName != null &&
			    !DataType.IsNullOrWhiteSpace(overrideXmlName.LocalName))
				xmlTextWriter.WriteStartElement(overrideXmlName.LocalName, overrideXmlName.NamespaceUri);
			else
			{
				if (DataType.IsNullOrWhiteSpace(currentXmlElementMappingAttribute.LocalName))
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				xmlTextWriter.WriteStartElement(currentXmlElementMappingAttribute.LocalName, currentXmlElementMappingAttribute.NamespaceUri);
			}

			// write attributes
			if ((object)currentPropertyToAttributeMappings != null)
			{
				foreach (KeyValuePair<PropertyInfo, XmlAttributeMappingAttribute> currentPropertyToAttributeMapping in currentPropertyToAttributeMappings.OrderBy(m => m.Value.Order).ThenBy(m => m.Value.LocalName))
				{
					if (!currentPropertyToAttributeMapping.Key.CanRead)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (!Reflexion.GetLogicalPropertyValue(currentXmlObject, currentPropertyToAttributeMapping.Key.Name, out ovalue))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					svalue = ovalue.SafeToString();

					xmlTextWriter.WriteStartAttribute(currentPropertyToAttributeMapping.Value.LocalName, currentPropertyToAttributeMapping.Value.NamespaceUri);
					xmlTextWriter.WriteString(svalue);
					xmlTextWriter.WriteEndAttribute();
				}
			}

			if ((object)currentPropertyToChildElementMappings != null)
			{
				// write text elements
				foreach (KeyValuePair<PropertyInfo, XmlChildElementMappingAttribute> currentPropertyToChildElementMapping in currentPropertyToChildElementMappings.Where(m => m.Value.ChildElementType == ChildElementType.TextValue).OrderBy(m => m.Value.Order).ThenBy(m => m.Value.LocalName))
				{
					if (!currentPropertyToChildElementMapping.Key.CanRead)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (DataType.IsNullOrWhiteSpace(currentPropertyToChildElementMapping.Value.LocalName))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (!Reflexion.GetLogicalPropertyValue(currentXmlObject, currentPropertyToChildElementMapping.Key.Name, out ovalue))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					svalue = ovalue.SafeToString();

					xmlTextWriter.WriteElementString("", currentPropertyToChildElementMapping.Value.LocalName, currentPropertyToChildElementMapping.Value.NamespaceUri, svalue);
				}

				// write accessor elements	
				foreach (KeyValuePair<PropertyInfo, XmlChildElementMappingAttribute> currentPropertyToChildElementMapping in currentPropertyToChildElementMappings.Where(m => m.Value.ChildElementType != ChildElementType.TextValue).OrderBy(m => m.Value.Order).ThenBy(m => m.Value.LocalName))
				{
					IXmlObject childElement;
					XmlName xmlName;

					if (!currentPropertyToChildElementMapping.Key.CanRead)
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");

					if (!typeof(IXmlObject).IsAssignableFrom(currentPropertyToChildElementMapping.Key.PropertyType))
						throw new InvalidOperationException(string.Format("TODO (enhancement): add meaningful message | '{0}'", currentPropertyToChildElementMapping.Key.PropertyType.FullName));

					object _out;
					if (!Reflexion.GetLogicalPropertyValue(currentXmlObject, currentPropertyToChildElementMapping.Key.Name, out _out))
						throw new InvalidOperationException("TODO (enhancement): add meaningful message");
					childElement = (IXmlObject)_out;

					if ((object)childElement != null)
					{
						if (currentPropertyToChildElementMapping.Value.ChildElementType == ChildElementType.ParentQualified)
						{
							xmlName = new XmlName()
							          {
							          	LocalName = string.Format("{0}.{1}",
							          	                          currentXmlElementMappingAttribute.LocalName,
							          	                          currentPropertyToChildElementMapping.Value.LocalName),
							          	NamespaceUri = currentXmlElementMappingAttribute.NamespaceUri
							          };
						}
						else if (currentPropertyToChildElementMapping.Value.ChildElementType == ChildElementType.Unqualified)
						{
							xmlName = new XmlName()
							          {
							          	LocalName = string.Format("{0}",
							          	                          currentPropertyToChildElementMapping.Value.LocalName),
							          	NamespaceUri = currentXmlElementMappingAttribute.NamespaceUri
							          };
						}
						else
							throw new InvalidOperationException("TODO (enhancement): add meaningful message");

						this.SerializeToXml(xmlTextWriter, childElement, xmlName);
					}
				}
			}

			// write anonymous child elements
			if (currentXmlElementMappingAttribute.ChildElementModel == ChildElementModel.Items &&
			    (object)currentXmlObject.Items != null)
			{
				foreach (IXmlObject childElement in currentXmlObject.Items)
					this.SerializeToXml(xmlTextWriter, childElement, null);
			}
			else if (currentXmlElementMappingAttribute.ChildElementModel == ChildElementModel.Content &&
			         (object)currentXmlObject.Content != null)
				this.SerializeToXml(xmlTextWriter, currentXmlObject.Content, null);

			// end current element
			xmlTextWriter.WriteEndElement();
		}

		#endregion
	}
}