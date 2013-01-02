/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextMetal.Common.Core.StringTokens
{
	/// <summary>
	/// Replaces a tokenized input string with replacement values. A token is in the following format: ${token(`arg0`, `arg1`, ...)} token: a required 'key' into a 'dictionary' of token replacement strategies. A missing token is considered invalid and no replacement will be made. An unknown token considered invalid and no replacement will be made. The minimum length of a token is 1; the maximum length of a token is 1024. Tokens are case insensative. An token may be proceded by an optional function call operator with zero or more arguments. Each function call argument must be enclosed in tick marks e.g. `some value`. Recursion/nested token expressions is not supported.
	/// </summary>
	public sealed class Tokenizer
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the Tokenizer class.
		/// </summary>
		/// <param name="tokenReplacementStrategies"> A dictionary of token replacement strategies. </param>
		/// <param name="strictMatching"> A value indicating if exceptions are thrown for bad token matches. </param>
		public Tokenizer(IDictionary<string, ITokenReplacementStrategy> tokenReplacementStrategies, bool strictMatching)
		{
			if ((object)tokenReplacementStrategies == null)
				throw new ArgumentNullException("tokenReplacementStrategies");

			this.tokenReplacementStrategies = tokenReplacementStrategies;
			this.strictMatching = strictMatching;
		}

		/// <summary>
		/// Initializes a new instance of the Tokenizer class.
		/// </summary>
		/// <param name="strictMatching"> A value indicating if exceptions are thrown for bad token matches. </param>
		public Tokenizer(bool strictMatching)
			: this(new Dictionary<string, ITokenReplacementStrategy>(StringComparer.InvariantCultureIgnoreCase), strictMatching)
		{
			this.strictMatching = strictMatching;
		}

		#endregion

		#region Fields/Constants

		private const string TOKENIZER_REGEX =
			@"\$ \{" +
			@"(?: [ ]* ( " + TOKEN_ID_REGEX + " ){1,1} )" +
			@"(?: [ ]* \( ( [ ]* (?: ` [^`]* ` [ ]* (?: , [ ]* ` [^`]* ` [ ]* )* ){0,1} ){0,1} \) ){0,1}" +
			@"[ ]* \}";

		private const string TOKEN_ID_REGEX = @"[a-zA-Z_#\.\:][a-zA-Z_0-9]{0,1023}";

		private readonly List<string> previousExpansionTokens = new List<string>();
		private readonly bool strictMatching;
		private readonly IDictionary<string, ITokenReplacementStrategy> tokenReplacementStrategies;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the tokenizer regular expression.
		/// </summary>
		public static string TokenizerRegEx
		{
			get
			{
				return TOKENIZER_REGEX;
			}
		}

		/// <summary>
		/// Gets an ordered array of the previous execution of expansion tokens encountered.
		/// </summary>
		public string[] OrderedPreviousExpansionTokens
		{
			get
			{
				return this.PreviousExpansionTokens.Distinct().OrderBy(x => x).ToArray();
			}
		}

		private List<string> PreviousExpansionTokens
		{
			get
			{
				return this.previousExpansionTokens;
			}
		}

		/// <summary>
		/// Gets a value indicating if exceptions are thrown for bad token matches.
		/// </summary>
		public bool StrictMatching
		{
			get
			{
				return this.strictMatching;
			}
		}

		/// <summary>
		/// Gets a dictionary of token replacement strategies.
		/// </summary>
		public IDictionary<string, ITokenReplacementStrategy> TokenReplacementStrategies
		{
			get
			{
				return this.tokenReplacementStrategies;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// A private method to parse an argument array from a tokenized call list.
		/// </summary>
		/// <param name="call"> The call list from a tokenized call site. </param>
		/// <returns> A string array of call site arguments. </returns>
		private static string[] GetArgs(string call)
		{
			string[] args;

			if (DataType.IsNullOrWhiteSpace((call ?? "").Trim()))
				return new string[] { };

			// fixup argument list
			call = Regex.Replace(call, "^ [ ]* `", "", RegexOptions.IgnorePatternWhitespace);
			call = Regex.Replace(call, "` [ ]* $", "", RegexOptions.IgnorePatternWhitespace);
			call = Regex.Replace(call, "` [ ]* , [ ]* `", "`", RegexOptions.IgnorePatternWhitespace);

			args = call.Split('`');

			return args;
		}

		/// <summary>
		/// A private method that obeys the strict matching semantics flag in effect and if enabled, will throw an exception. Otherwise, returns the original unmatched value without alteration.
		/// </summary>
		/// <param name="strictMatching"> A value indicating whether strict matching semantics are in effect. </param>
		/// <param name="originalValue"> The original unmatched value. </param>
		/// <param name="matchPoint"> A short description of where the match failure occured. </param>
		/// <returns> The original value if strict matching semantics are disabled. </returns>
		private static string GetOriginalValueOrThrowExecption(bool strictMatching, string originalValue, string matchPoint)
		{
			if (strictMatching)
				throw new InvalidOperationException(string.Format("Failed to recognize '{0}' due to '{1}' match error; strict matching enabled.", originalValue, matchPoint));
			else
				return originalValue;
		}

		/// <summary>
		/// Replaces a tokenized input string with replacement values. No wildcard support is assumed.
		/// </summary>
		/// <param name="tokenizedValue"> The input string containing tokenized values. </param>
		/// <returns> A string value with all possible replacements made. </returns>
		public string ExpandTokens(string tokenizedValue)
		{
			return this.ExpandTokens(tokenizedValue, null);
		}

		/// <summary>
		/// Replaces a tokenized input string with replacement values. Wildcard support is optional.
		/// </summary>
		/// <param name="tokenizedValue"> The input string containing tokenized values. </param>
		/// <param name="optionalWildcardTokenReplacementStrategy"> An optional wildcard token replacement strategy. </param>
		/// <returns> A string value with all possible replacements made. </returns>
		public string ExpandTokens(string tokenizedValue, IWildcardTokenReplacementStrategy optionalWildcardTokenReplacementStrategy)
		{
			if (DataType.IsNullOrWhiteSpace(tokenizedValue))
				return tokenizedValue;

			// clean token collection
			this.PreviousExpansionTokens.Clear();

			tokenizedValue = Regex.Replace(tokenizedValue, TokenizerRegEx, (m) => this.ReplacementMatcherEx(m, optionalWildcardTokenReplacementStrategy), RegexOptions.IgnorePatternWhitespace);

			return tokenizedValue;
		}

		/// <summary>
		/// Private method used to match and process tokenized regular expressions.
		/// </summary>
		/// <param name="match"> The regular express match object. </param>
		/// <param name="wildcardTokenReplacementStrategy"> The wildcard token replacement strategy to use in the event a predefined token replacement strategy lookup failed. </param>
		/// <returns> The token-resolved string value. </returns>
		private string ReplacementMatcherEx(Match match, IWildcardTokenReplacementStrategy wildcardTokenReplacementStrategy)
		{
			// ${ token (`arg0`, ..) }

			string token = null;
			string[] argumentList = null;
			object replacementValue = null;
			ITokenReplacementStrategy tokenReplacementStrategy;
			bool keyNotFound, tryWildcard;

			token = match.Groups[1].Success ? match.Groups[1].Value : null;

			argumentList = match.Groups[2].Success ? GetArgs(match.Groups[2].Value) : null;

			if (DataType.IsNullOrWhiteSpace(token))
				return GetOriginalValueOrThrowExecption(this.StrictMatching, match.Value, "token missing");

			// add to token collection
			this.PreviousExpansionTokens.Add(token);

			keyNotFound = !this.TokenReplacementStrategies.TryGetValue(token, out tokenReplacementStrategy);
			tryWildcard = keyNotFound && (object)wildcardTokenReplacementStrategy != null;

			if (keyNotFound && !tryWildcard)
				return GetOriginalValueOrThrowExecption(this.StrictMatching, match.Value, "token unknown");

			try
			{
				if (!tryWildcard)
					replacementValue = tokenReplacementStrategy.Evaluate(argumentList);
				else
					replacementValue = wildcardTokenReplacementStrategy.Evaluate(token, argumentList);
			}
			catch (Exception ex)
			{
				return GetOriginalValueOrThrowExecption(this.StrictMatching, match.Value, string.Format("function exception {{{0}}}", ex.Message));
			}

			return replacementValue.SafeToString();
		}

		#endregion
	}
}