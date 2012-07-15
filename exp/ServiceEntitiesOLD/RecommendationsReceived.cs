using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "recommendations-received", NamespaceUri = "")]
	public class RecommendationsReceived : PagedCollection<Recommendation>
	{
	}
}