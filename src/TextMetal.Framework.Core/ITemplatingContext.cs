/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;

namespace TextMetal.Framework.Core
{
	public interface ITemplatingContext
	{
		#region Properties/Indexers/Events

		IDictionary<string, object> CurrentVariableTable
		{
			get;
		}

		IInputMechanism Input
		{
			get;
		}

		Stack<object> IteratorModels
		{
			get;
		}

		IOutputMechanism Output
		{
			get;
		}

		Tokenizer Tokenizer
		{
			get;
		}

		Stack<Dictionary<string, object>> VariableTables
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void AddReference(Type xmlObjectType);

		void AddReference(XmlName xmlName, Type xmlObjectType);

		void ClearReferences();

		DynamicWildcardTokenReplacementStrategy GetDynamicWildcardTokenReplacementStrategy();

		DynamicWildcardTokenReplacementStrategy GetDynamicWildcardTokenReplacementStrategy(bool strict);

		void SetReference(Type xmlObjectType);

		#endregion
	}
}