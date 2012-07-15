using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "authority", NamespaceUri = "")]
	public class Authority : SoXmlObject
	{
		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get;
			set;
		}

		#endregion
	}

	[XmlElementMapping(LocalName = "certification", NamespaceUri = "")]
	public class Certification : SoXmlObject
	{
		#region Properties/Indexers/Events

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "authority", NamespaceUri = "")]
		public Authority Authority
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "end-date", NamespaceUri = "")]
		public Date EndDate
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "id", NamespaceUri = "")]
		public string Id
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "name", NamespaceUri = "")]
		public string Name
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.TextValue, LocalName = "number", NamespaceUri = "")]
		public string Number
		{
			get;
			set;
		}

		[XmlChildElementMapping(ChildElementType = ChildElementType.Unqualified, LocalName = "start-date", NamespaceUri = "")]
		public Date StartDate
		{
			get;
			set;
		}

		#endregion
	}
}