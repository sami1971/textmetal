/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using TextMetal.Core.XmlModel;

namespace LinkedInform.LinkedInRestApi.ServiceEntities
{
	[XmlElementMapping(AllowAnonymousChildren = false)]
	public sealed class SoXmlTextObject : SoXmlObject, IXmlTextObject
	{
		#region Constructors/Destructors

		public SoXmlTextObject()
		{
		}

		#endregion

		#region Fields/Constants

		private XmlName name;

		private string text;

		#endregion

		#region Properties/Indexers/Events

		public XmlName Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		#endregion
	}
}