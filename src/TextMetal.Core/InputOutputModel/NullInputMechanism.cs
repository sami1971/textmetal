/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Core.Plumbing;
using TextMetal.Core.TemplateModel;

namespace TextMetal.Core.InputOutputModel
{
	public class NullInputMechanism : IInputMechanism
	{
		#region Constructors/Destructors

		public NullInputMechanism()
		{
		}

		#endregion

		#region Methods/Operators

		public void Dispose()
		{
		}

		public Assembly LoadAssembly(string assembluName)
		{
			Assembly assembly;

			if (DataType.IsNullOrWhiteSpace(assembluName))
				return null;

			assembly = Assembly.Load(assembluName);

			return assembly;
		}

		public string LoadContent(string resourceName)
		{
			throw new NotImplementedException();
		}

		public ITemplateXmlObject LoadFragment(string resourceName)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}