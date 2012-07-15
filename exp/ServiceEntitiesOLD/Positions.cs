using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "positions", NamespaceUri = "")]
	public class Positions : PagedCollection<Position>
	{
	}
}