/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Core.TemplateModel
{
	/// <summary>
	/// 	Provides for template mechanics.
	/// </summary>
	public interface ITemplateMechanism
	{
		#region Methods/Operators

		/// <summary>
		/// 	Expands the template tree into the templating context current output.
		/// </summary>
		/// <param name="templatingContext"> The templating context. </param>
		void ExpandTemplate(TemplatingContext templatingContext);

		#endregion
	}
}