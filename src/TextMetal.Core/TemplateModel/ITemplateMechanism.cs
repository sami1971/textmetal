/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.TemplateModel
{
	public interface ITemplateMechanism
	{
		#region Methods/Operators

		void ExpandTemplate(TemplatingContext templatingContext);

		#endregion
	}
}