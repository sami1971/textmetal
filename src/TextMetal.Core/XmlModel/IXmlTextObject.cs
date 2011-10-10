/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.XmlModel
{
	public interface IXmlTextObject : IXmlObject
	{
		#region Properties/Indexers/Events

		XmlName Name
		{
			get;
			set;
		}

		string Text
		{
			get;
			set;
		}

		#endregion
	}
}