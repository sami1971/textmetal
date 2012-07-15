using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "three-current-positions", NamespaceUri = "")]
	public class ThreeCurrentPositions : PagedCollection<Position>
	{
	}
}