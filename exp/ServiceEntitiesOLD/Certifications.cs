using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "certifications", NamespaceUri = "")]
	public class Certifications : SoXmlObject
	{
		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "total", NamespaceUri = "")]
		public int Total
		{
			get;
			set;
		}

		#endregion
	}
}