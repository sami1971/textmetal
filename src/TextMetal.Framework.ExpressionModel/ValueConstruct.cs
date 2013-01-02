/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Expressions;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "Value", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class ValueConstruct : ExpressionXmlObject, IValue
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ValueConstruct class.
		/// </summary>
		public ValueConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private object _;
		private string type;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "data", NamespaceUri = "", Order = 2)]
		public string Data
		{
			get
			{
				return this.__.SafeToString(null, null, true);
			}
			set
			{
				this.SetObjectValue(value);
			}
		}

		[XmlAttributeMapping(LocalName = "type", NamespaceUri = "", Order = 1)]
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public object __
		{
			get
			{
				return this._;
			}
			set
			{
				this._ = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override object CoreEvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			return this.__;
		}

		private void SetObjectValue(string data)
		{
			object value;
			Type valueType;

			if (DataType.IsNullOrWhiteSpace(this.Type))
				valueType = typeof(string);
			else
				valueType = System.Type.GetType(this.Type, false);

			if ((object)valueType == null)
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			if (!DataType.TryParse(valueType, data, out value))
				throw new InvalidOperationException("TODO (enhancement): add meaningful message");

			this._ = value;
		}

		#endregion
	}
}