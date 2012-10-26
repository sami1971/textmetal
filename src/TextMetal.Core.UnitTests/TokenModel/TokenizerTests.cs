/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NMock2;
using NMock2.Actions;
using NMock2.Matchers;

using NUnit.Framework;

using TextMetal.Core.TokenModel;
using TestingFramework.Core.Customization;

namespace TextMetal.Core.UnitTests.TokenModel
{
	/// <summary>
	/// 	Unit tests.
	/// </summary>
	[TestFixture]
	public class TokenizerTests
	{
		#region Constructors/Destructors

		public TokenizerTests()
		{
		}

		#endregion

		#region Methods/Operators

		[Test]
		public void ShouldCreateTest()
		{
			Tokenizer tokenizer;
			Mockery mockery;
			IDictionary<string, ITokenReplacementStrategy> mockTokenReplacementStrategies;

			mockery = new Mockery();
			mockTokenReplacementStrategies = mockery.NewMock<IDictionary<string, ITokenReplacementStrategy>>();

			tokenizer = new Tokenizer(true);

			Assert.IsNotNull(tokenizer);
			Assert.IsNotNull(tokenizer.TokenReplacementStrategies);
			Assert.IsTrue(tokenizer.StrictMatching);

			tokenizer = new Tokenizer(mockTokenReplacementStrategies, true);

			Assert.IsNotNull(tokenizer);
			Assert.IsNotNull(tokenizer.TokenReplacementStrategies);
			Assert.IsTrue(tokenizer.StrictMatching);
		}

		[Test]
		public void ShouldExpandTokensLooseMatchingTest()
		{
			Tokenizer tokenizer;
			Mockery mockery;
			IDictionary<string, ITokenReplacementStrategy> mockTokenReplacementStrategies;
			ITokenReplacementStrategy mockTokenReplacementStrategy;
			string tokenizedValue;
			string expandedValue;
			string expectedValue;

			mockery = new Mockery();
			mockTokenReplacementStrategies = mockery.NewMock<IDictionary<string, ITokenReplacementStrategy>>();
			mockTokenReplacementStrategy = mockery.NewMock<ITokenReplacementStrategy>();

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myValueSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken0"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken1"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { "a" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken2"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { "a", "b" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myUnkSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myErrSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Throw.Exception(new Exception()));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("a"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("b"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("c"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("d"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Throw.Exception(new Exception()));

			tokenizer = new Tokenizer(mockTokenReplacementStrategies, false);

			tokenizedValue = "";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...{myNoSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...{myNoSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myValueSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken0()}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken1(`a`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken2(`a`,  `b`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myUnkSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...${myUnkSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myErrSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...${myErrSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${a}...${c}...${b}...${d}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "............${d}...";
			Assert.AreEqual(expectedValue, expandedValue);

			Assert.IsNotNull(tokenizer.OrderedPreviousExpansionTokens);
			Assert.AreEqual("a,b,c,d", string.Join(",", tokenizer.OrderedPreviousExpansionTokens));

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExpandTokensStrictMatchingTest()
		{
			Tokenizer tokenizer;
			Mockery mockery;
			IDictionary<string, ITokenReplacementStrategy> mockTokenReplacementStrategies;
			ITokenReplacementStrategy mockTokenReplacementStrategy;
			string tokenizedValue;
			string expandedValue;
			string expectedValue;
			Exception capturedException;

			mockery = new Mockery();
			mockTokenReplacementStrategies = mockery.NewMock<IDictionary<string, ITokenReplacementStrategy>>();
			mockTokenReplacementStrategy = mockery.NewMock<ITokenReplacementStrategy>();

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myValueSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken0"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken1"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { "a" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken2"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { new object[] { "a", "b" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myUnkSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myErrSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Throw.Exception(new Exception()));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("a"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("b"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("c"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("d"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", mockTokenReplacementStrategy), Return.Value(true));
			Expect.Once.On(mockTokenReplacementStrategy).Method("Evaluate").With(new object[] { null }).Will(Throw.Exception(new Exception()));

			tokenizer = new Tokenizer(mockTokenReplacementStrategies, true);

			tokenizedValue = "";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...{myNoSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...{myNoSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myValueSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken0()}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken1(`a`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken2(`a`,  `b`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myUnkSemanticToken}...";
			capturedException = Assert.Throws<InvalidOperationException>(delegate
			                                                             {
			                                                             	expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			                                                             });
			Assert.IsNotNull(capturedException);

			tokenizedValue = "...${myErrSemanticToken}...";
			capturedException = Assert.Throws<InvalidOperationException>(delegate
			                                                             {
			                                                             	expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			                                                             });
			Assert.IsNotNull(capturedException);

			tokenizedValue = "...${a}...${c}...${b}...${d}...";
			capturedException = Assert.Throws<InvalidOperationException>(delegate
			                                                             {
			                                                             	expandedValue = tokenizer.ExpandTokens(tokenizedValue);
			                                                             });
			Assert.IsNotNull(capturedException);

			Assert.IsNotNull(tokenizer.OrderedPreviousExpansionTokens);
			Assert.AreEqual("a,b,c,d", string.Join(",", tokenizer.OrderedPreviousExpansionTokens));

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		public void ShouldExpandTokensWildcardLooseMatchingTest()
		{
			Tokenizer tokenizer;
			Mockery mockery;
			IDictionary<string, ITokenReplacementStrategy> mockTokenReplacementStrategies;
			IWildcardTokenReplacementStrategy mockWildcardTokenReplacementStrategy;
			string tokenizedValue;
			string expandedValue;
			string expectedValue;

			mockery = new Mockery();
			mockTokenReplacementStrategies = mockery.NewMock<IDictionary<string, ITokenReplacementStrategy>>();
			mockWildcardTokenReplacementStrategy = mockery.NewMock<IWildcardTokenReplacementStrategy>();

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myValueSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "myValueSemanticToken", null }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken0"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "myFunctionSemanticToken0", new object[] { } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken1"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "myFunctionSemanticToken1", new object[] { "a" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myFunctionSemanticToken2"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "myFunctionSemanticToken2", new object[] { "a", "b" } }).Will(Return.Value("testValue"));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("myErrSemanticToken"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "myErrSemanticToken", null }).Will(Throw.Exception(new Exception()));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("a"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "a", null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("b"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "b", null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("c"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "c", null }).Will(Return.Value(""));

			Expect.Once.On(mockTokenReplacementStrategies).Method("TryGetValue").With(new EqualMatcher("d"), new AndMatcher(new ArgumentsMatcher.OutMatcher(), ForceTrueMatcher.Instance)).Will(new SetNamedParameterAction("value", null), Return.Value(false));
			Expect.Once.On(mockWildcardTokenReplacementStrategy).Method("Evaluate").With(new object[] { "d", null }).Will(Throw.Exception(new Exception()));

			tokenizer = new Tokenizer(mockTokenReplacementStrategies, false);

			tokenizedValue = "";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...{myNoSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...{myNoSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myValueSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken0()}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken1(`a`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myFunctionSemanticToken2(`a`,  `b`)}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...testValue...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${myErrSemanticToken}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "...${myErrSemanticToken}...";
			Assert.AreEqual(expectedValue, expandedValue);

			tokenizedValue = "...${a}...${c}...${b}...${d}...";
			expandedValue = tokenizer.ExpandTokens(tokenizedValue, mockWildcardTokenReplacementStrategy);
			expectedValue = "............${d}...";
			Assert.AreEqual(expectedValue, expandedValue);

			Assert.IsNotNull(tokenizer.OrderedPreviousExpansionTokens);
			Assert.AreEqual("a,b,c,d", string.Join(",", tokenizer.OrderedPreviousExpansionTokens));

			mockery.VerifyAllExpectationsHaveBeenMet();
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ShouldFailOnNullTokenReplStratsCreateTest()
		{
			Tokenizer tokenizer;
			Mockery mockery;
			IDictionary<string, ITokenReplacementStrategy> mockTokenReplacementStrategies;

			mockery = new Mockery();
			mockTokenReplacementStrategies = null;

			tokenizer = new Tokenizer(mockTokenReplacementStrategies, true);
		}

		[Test]
		public void ShouldValidateTokenizerRegExTest()
		{
			Match match;

			match = Regex.Match("${myToken(``,``,``,``}", Tokenizer.TokenizerRegEx);
			Assert.IsNotNull(match);
			Assert.IsFalse(match.Success);
		}

		#endregion
	}
}