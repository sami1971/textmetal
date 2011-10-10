﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.XmlModel
{
	public interface IXmlContentObject : IXmlObject
	{
		#region Properties/Indexers/Events

		IXmlObject Content
		{
			get;
			set;
		}

		#endregion
	}
}