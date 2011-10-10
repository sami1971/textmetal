/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.InputOutputModel
{
	public interface IInputMechanism : IDisposable
	{
		#region Methods/Operators

		Assembly LoadAssembly(string assembluName);

		string LoadContent(string resourceName);

		ITemplateXmlObject LoadFragment(string resourceName);

		#endregion
	}
}