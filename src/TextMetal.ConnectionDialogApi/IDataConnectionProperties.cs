//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace TextMetal.ConnectionDialogApi
{
	public interface IDataConnectionProperties
	{
		#region Properties/Indexers/Events

		event EventHandler PropertyChanged;

		object this[string propertyName]
		{
			get;
			set;
		}

		bool IsComplete
		{
			get;
		}

		bool IsExtensible
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void Add(string propertyName);

		bool Contains(string propertyName);

		void Parse(string s);

		void Remove(string propertyName);

		void Reset();

		void Reset(string propertyName);

		void Test();

		string ToDisplayString();

		string ToFullString();

		#endregion
	}
}