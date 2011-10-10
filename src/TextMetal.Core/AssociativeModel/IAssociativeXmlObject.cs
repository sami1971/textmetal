/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.XmlModel;

namespace TextMetal.Core.AssociativeModel
{
	public interface IAssociativeXmlObject : IAssociativeMechanism, IXmlObject
	{
		#region Properties/Indexers/Events

		string Name
		{
			get;
		}

		#endregion
	}
}