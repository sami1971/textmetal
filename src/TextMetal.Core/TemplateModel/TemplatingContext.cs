/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core.InputOutputModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	public sealed class TemplatingContext
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the TemplatingContext class.
		/// </summary>
		/// <param name="xpe">The XML persist engine in-effect.</param>
		/// <param name="tokenizer">The tokenizer in-efect.</param>
		/// <param name="input">The input mechanism in-effect.</param>
		/// <param name="output">The output mechanism in-effect.</param>
		public TemplatingContext(IXmlPersistEngine xpe, Tokenizer tokenizer, IInputMechanism input, IOutputMechanism output)
		{
			if ((object)xpe == null)
				throw new ArgumentNullException("xpe");

			if ((object)tokenizer == null)
				throw new ArgumentNullException("tokenizer");

			if ((object)input == null)
				throw new ArgumentNullException("input");

			if ((object)output == null)
				throw new ArgumentNullException("output");

			this.xpe = xpe;
			this.tokenizer = tokenizer;
			this.input = input;
			this.output = output;
		}

		#endregion

		#region Fields/Constants

		private readonly IInputMechanism input;
		private readonly Stack<object> iteratorModels = new Stack<object>();
		private readonly IOutputMechanism output;
		private readonly Tokenizer tokenizer;
		private readonly Stack<Dictionary<string, object>> variableTables = new Stack<Dictionary<string, object>>();
		private readonly IXmlPersistEngine xpe;

		#endregion

		#region Properties/Indexers/Events

		public IDictionary<string, object> CurrentVariableTable
		{
			get
			{
				return this.VariableTables.Count > 0 ? this.VariableTables.Peek() : null;
			}
		}

		public IInputMechanism Input
		{
			get
			{
				return this.input;
			}
		}

		public Stack<object> IteratorModels
		{
			get
			{
				return this.iteratorModels;
			}
		}

		public IOutputMechanism Output
		{
			get
			{
				return this.output;
			}
		}

		public Tokenizer Tokenizer
		{
			get
			{
				return this.tokenizer;
			}
		}

		public Stack<Dictionary<string, object>> VariableTables
		{
			get
			{
				return this.variableTables;
			}
		}

		private IXmlPersistEngine Xpe
		{
			get
			{
				return this.xpe;
			}
		}

		#endregion

		#region Methods/Operators

		public void AddReference(Type xmlObjectType)
		{
			if ((object)xmlObjectType == null)
				throw new ArgumentNullException("xmlObjectType");

			this.Xpe.RegisterKnownXmlObject(xmlObjectType);
		}

		public void AddReference(XmlName xmlName, Type xmlObjectType)
		{
			if ((object)xmlName == null)
				throw new ArgumentNullException("xmlName");

			if ((object)xmlObjectType == null)
				throw new ArgumentNullException("xmlObjectType");

			this.Xpe.RegisterKnownXmlObject(xmlName, xmlObjectType);
		}

		public void ClearReferences()
		{
			this.Xpe.ClearAllKnowns();
		}

		public DynamicWildcardTokenReplacementStrategy GetDynamicWildcardTokenReplacementStrategy()
		{
			return this.GetDynamicWildcardTokenReplacementStrategy(this.Tokenizer.StrictMatching);
		}

		public DynamicWildcardTokenReplacementStrategy GetDynamicWildcardTokenReplacementStrategy(bool strict)
		{
			List<object> temp;
			List<Dictionary<string, object>> temp2;

			temp = new List<object>(this.IteratorModels);
			temp2 = new List<Dictionary<string, object>>(this.VariableTables);

			temp.InsertRange(0, temp2.ToArray());

			return new DynamicWildcardTokenReplacementStrategy(temp.ToArray(), strict);
		}

		public void SetReference(Type xmlObjectType)
		{
			this.Xpe.RegisterKnownXmlTextObject(xmlObjectType);
		}

		#endregion
	}
}