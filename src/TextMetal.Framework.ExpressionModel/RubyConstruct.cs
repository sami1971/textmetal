/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Scripting.Hosting;

using TextMetal.Common.Core.StringTokens;
using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.ExpressionModel
{
	[XmlElementMapping(LocalName = "Ruby", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementModel = ChildElementModel.Sterile)]
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

		private string script;

		#endregion

		#region Properties/Indexers/Events

		[XmlChildElementMapping(LocalName = "Script", NamespaceUri = "http://www.textmetal.com/api/v4.4.0", ChildElementType = ChildElementType.TextValue)]
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

		#endregion

		#region Methods/Operators

		protected override object CoreEvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			ScriptRuntime scriptRuntime;
			ScriptEngine scriptEngine;
			ScriptScope scriptScope;
			List<string> paths;
			dynamic result;

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy();
			scriptRuntime = ScriptRuntime.CreateFromConfiguration();
			scriptEngine = scriptRuntime.GetEngine("Ruby");
			scriptScope = scriptEngine.CreateScope();

			paths = scriptEngine.GetSearchPaths().ToList();
			paths.Clear();
			//paths.Add(System.IO.Directory.GetCurrentDirectory());
			scriptEngine.SetSearchPaths(paths);

			scriptScope.SetVariable("__tm__", new RubyProxy(dynamicWildcardTokenReplacementStrategy));

			result = scriptEngine.Execute(this.Script, scriptScope);
			//result = scriptEngine.ExecuteFile("", scriptScope);

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

		private sealed class RubyProxy
		{
			#region Constructors/Destructors

			public RubyProxy(DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy)
			{
				if ((object)dynamicWildcardTokenReplacementStrategy == null)
					throw new ArgumentNullException("dynamicWildcardTokenReplacementStrategy");

				this.dynamicWildcardTokenReplacementStrategy = dynamicWildcardTokenReplacementStrategy;
			}

			#endregion

			#region Fields/Constants

			private readonly DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;

			#endregion

			#region Properties/Indexers/Events

			private DynamicWildcardTokenReplacementStrategy DynamicWildcardTokenReplacementStrategy
			{
				get
				{
					return this.dynamicWildcardTokenReplacementStrategy;
				}
			}

			#endregion

			#region Methods/Operators

			public bool Def(string token)
			{
				object value;

				return this.DynamicWildcardTokenReplacementStrategy.GetByPath(token, out value);
			}

			public object Get(string token)
			{
				object value;

				if (!this.DynamicWildcardTokenReplacementStrategy.GetByPath(token, out value))
					return null;

				return value;
			}

			public bool Set(string token, object value)
			{
				return this.DynamicWildcardTokenReplacementStrategy.SetByPath(token, value);
			}

			#endregion
		}

		#endregion
	}
}