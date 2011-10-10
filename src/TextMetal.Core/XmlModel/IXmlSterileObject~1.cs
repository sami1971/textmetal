/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.XmlModel
{
	public interface IXmlSterileObject<TParent> : IXmlSterileObject
		where TParent : class, IXmlObject
	{
		#region Properties/Indexers/Events

		new TParent Parent
		{
			get;
			set;
		}

		#endregion
	}
}