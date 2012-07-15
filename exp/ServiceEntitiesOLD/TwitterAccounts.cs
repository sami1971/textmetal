using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "twitter-accounts", NamespaceUri = "")]
	public class TwitterAccounts : PagedCollection<TwitterAccount>
	{
	}
}