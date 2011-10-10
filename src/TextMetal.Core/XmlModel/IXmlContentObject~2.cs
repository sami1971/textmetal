/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Core.XmlModel
{
	public interface IXmlContentObject<TParent, TContent> : IXmlContentObject
		where TParent : class, IXmlObject
		where TContent : class, IXmlObject
	{
		#region Properties/Indexers/Events

		new TContent Content
		{
			get;
			set;
		}

		new TParent Parent
		{
			get;
			set;
		}

		#endregion
	}
}