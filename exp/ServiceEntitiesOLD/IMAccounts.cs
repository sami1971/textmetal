using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "im-accounts", NamespaceUri = "")]
	public class IMAccounts : PagedCollection<IMAccount>
	{
	}
}