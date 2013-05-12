/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using Microsoft.Scripting.Hosting;

using TextMetal.Common.Core;
using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "Ruby", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementModel = ChildElementModel.Sterile)]
	public sealed class RubyConstruct : ExpressionXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the RubyConstruct class.
		/// </summary>
		public RubyConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private string expr;
		private string file;
		private string script;
		private RubySource src;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "expr", NamespaceUri = "")]
		public string Expr
		{
			get
			{
				return this.expr;
			}
			set
			{
				this.expr = value;
			}
		}

		[XmlAttributeMapping(LocalName = "file", NamespaceUri = "")]
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		[XmlChildElementMapping(LocalName = "Script", NamespaceUri = "http://www.textmetal.com/api/v5.0.0", ChildElementType = ChildElementType.TextValue)]
		public string Script
		{
			get
			{
				return this.script;
			}
			set
			{
				this.script = value;
			}
		}

		public RubySource Src
		{
			get
			{
				return this.src;
			}
			set
			{
				this.src = value;
			}
		}

		[XmlAttributeMapping(LocalName = "src", NamespaceUri = "")]
		public string _Src
		{
			get
			{
				return this.Src.SafeToString();
			}
			set
			{
				RubySource src;

				if (!DataType.TryParse<RubySource>(value, out src))
					this.Src = RubySource.Unknown;
				else
					this.Src = src;
			}
		}

		#endregion

		#region Methods/Operators

		protected override object CoreEvaluateExpression(ITemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			ScriptRuntimeSetup scriptRuntimeSetup;
			ScriptRuntime scriptRuntime;
			ScriptEngine scriptEngine;
			ScriptScope scriptScope;
			List<string> paths;
			dynamic result;
			dynamic textMetal;
			dynamic dvalue;
			Func<string, object> func;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();

			scriptRuntimeSetup = new ScriptRuntimeSetup();
			scriptRuntimeSetup.LanguageSetups.Add(
				new LanguageSetup(
					"IronRuby.Runtime.RubyContext, IronRuby",
					"IronRuby",
					new[] { "IronRuby", "Ruby", "rb" },
					new[] { ".rb" }));

			scriptRuntime = new ScriptRuntime(scriptRuntimeSetup);
			scriptEngine = scriptRuntime.GetEngine("Ruby");
			scriptScope = scriptEngine.CreateScope();

			paths = scriptEngine.GetSearchPaths().ToList();
			paths.Clear();
			//paths.Add(System.IO.Directory.GetCurrentDirectory());
			scriptEngine.SetSearchPaths(paths);

			textMetal = new ExpandoObject();
			func = (token) => dynamicWildcardTokenReplacementStrategy.Evaluate(token, null);
			textMetal.EvaluateToken = func;
			//TODO: templatingContext.Tokenizer.ExpandTokens(tokenizedValue, dynamicWildcardTokenReplacementStrategy);

			scriptScope.SetVariable("textMetal", textMetal);

			foreach (KeyValuePair<string, object> variableEntry in templatingContext.CurrentVariableTable)
			{
				if (scriptScope.TryGetVariable(variableEntry.Key, out dvalue))
					throw new InvalidOperationException(string.Format("Cannot set variable '{0}' in Ruby script scope; the specified variable name already exists.", variableEntry.Key));

				scriptScope.SetVariable(variableEntry.Key, variableEntry.Value);
			}

			switch (this.Src)
			{
				case RubySource.Script:
					result = scriptEngine.Execute(this.Script, scriptScope);
					break;
				case RubySource.Expr:
					result = scriptEngine.Execute(this.Expr, scriptScope);
					break;
				case RubySource.File:
					string file;
					file = templatingContext.Tokenizer.ExpandTokens(this.File, dynamicWildcardTokenReplacementStrategy);
					result = scriptEngine.Execute(templatingContext.Input.LoadContent(file), scriptScope);
					break;
				default:
					result = null;
					break;
			}

			return result;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private sealed class RubyHost
		{
			#region Constructors/Destructors

			/// <summary>
			/// Initializes a new instance of the RubyHost class.
			/// </summary>
			public RubyHost()
			{
			}

			#endregion
		}

		public enum RubySource
		{
			Unknown = 0,
			Script,
			Expr,
			File
		}

		#endregion
	}
}