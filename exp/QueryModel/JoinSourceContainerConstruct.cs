/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "JoinSourceContainer", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Content)]
	public sealed class JoinSourceContainerConstruct : QueryXmlObject
	{
		#region Constructors/Destructors

		public JoinSourceContainerConstruct()
		{
		}

		#endregion
	}
}