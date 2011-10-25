﻿/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.TemplateModel
{
	[XmlElementMapping(LocalName = "Free", NamespaceUri = "http://code.google.com/p/textmetal/rev3", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class FreeConstruct : TemplateXmlObject
	{
		#region Constructors/Destructors

		public FreeConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string token;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "token", NamespaceUri = "")]
		public string Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreExpandTemplate(TemplatingContext templatingContext)
		{
			string token;
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			token = templatingContext.Tokenizer.ExpandTokens(this.Token, dynamicWildcardTokenReplacementStrategy);

			if (!templatingContext.CurrentVariableTable.ContainsKey(token))
			{
				if (templatingContext.Tokenizer.StrictMatching)
					throw new InvalidOperationException("TODO (enhancement): add meaningful message");

				return;
			}

			templatingContext.CurrentVariableTable.Remove(token);
		}

		#endregion
	}
}