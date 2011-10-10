/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SortModel
{
	[XmlElementMapping(LocalName = "Descending", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class DescendingConstruct : OrderConstruct
	{
		#region Constructors/Destructors

		public DescendingConstruct()
			: base(false)
		{
		}

		#endregion
	}
}