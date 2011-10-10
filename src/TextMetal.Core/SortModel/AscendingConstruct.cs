/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SortModel
{
	[XmlElementMapping(LocalName = "Ascending", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class AscendingConstruct : OrderConstruct
	{
		#region Constructors/Destructors

		public AscendingConstruct()
			: base(true)
		{
		}

		#endregion
	}
}