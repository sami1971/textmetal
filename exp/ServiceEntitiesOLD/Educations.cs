using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(LocalName = "educations", NamespaceUri = "")]
	public class Educations : PagedCollection<Education>
	{
		#region Constructors/Destructors

		public Educations()
		{
		}

		#endregion
	}
}