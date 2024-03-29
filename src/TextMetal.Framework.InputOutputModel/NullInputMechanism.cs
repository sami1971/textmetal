﻿/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

using TextMetal.Common.Core;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.InputOutputModel
{
	public class NullInputMechanism : IInputMechanism
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the NullInputMechanism class.
		/// </summary>
		public NullInputMechanism()
		{
		}

		#endregion

		#region Methods/Operators

		public void Dispose()
		{
		}

		public Assembly LoadAssembly(string assemblyName)
		{
			Assembly assembly;

			if (DataType.IsNullOrWhiteSpace(assemblyName))
				return null;

			assembly = Assembly.Load(assemblyName);

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