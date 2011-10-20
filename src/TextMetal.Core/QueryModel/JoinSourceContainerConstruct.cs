/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.QueryModel
{
	[XmlElementMapping(LocalName = "JoinSourceContainer", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Content)]
	public sealed class JoinSourceContainerConstruct : QueryXmlObject
	{
		#region Constructors/Destructors

		public JoinSourceContainerConstruct()
		{
		}

		#endregion
	}
}