using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "three-past-positions", NamespaceUri = "")]
	public class ThreePastPositions : PagedCollection<Position>
	{
	}
}