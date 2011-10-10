/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;

using TextMetal.Core.Plumbing;
using TextMetal.Core.TemplateModel;
using TextMetal.Core.TokenModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.ExpressionModel
{
	/// <summary>
	/// This class uses the C# compiler style of numeric promotions.
	/// </summary>
	[XmlElementMapping(LocalName = "BinaryExpression", NamespaceUri = "http://code.google.com/p/textmetal/rev3", AllowAnonymousChildren = false)]
	public sealed class BinaryExpressionConstruct : XmlSterileObject<IExpressionXmlObject>, IExpressionXmlObject
	{
		#region Constructors/Destructors

		public BinaryExpressionConstruct()
		{
		}

		#endregion

		#region Fields/Constants

		private BinaryOperator binaryOperator;
		private ExpressionContainerConstruct leftExpression;
		private ExpressionContainerConstruct rightExpression;

		#endregion

		#region Properties/Indexers/Events

		[XmlAttributeMapping(LocalName = "operator", NamespaceUri = "")]
		public BinaryOperator BinaryOperator
		{
			get
			{
				return this.binaryOperator;
			}
			set
			{
				this.binaryOperator = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "LeftExpression", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct LeftExpression
		{
			get
			{
				return this.leftExpression;
			}
			set
			{
				this.leftExpression = value;
			}
		}

		[XmlChildElementMappingAttribute(ChildElementType = ChildElementType.ParentQualified, LocalName = "RightExpression", NamespaceUri = "http://code.google.com/p/textmetal/rev3")]
		public ExpressionContainerConstruct RightExpression
		{
			get
			{
				return this.rightExpression;
			}
			set
			{
				this.rightExpression = value;
			}
		}

		#endregion

		#region Methods/Operators

		public object EvaluateExpression(TemplatingContext templatingContext)
		{
			DynamicWildcardTokenReplacementStrategy dynamicWildcardTokenReplacementStrategy;
			object leftObj = null, rightObj = null;
			Type leftType, rightType;
			Func<object> onDemandRightExpressionEvaluator;

			Type[] numericTypes = new Type[]
			                      {
			                      	typeof(Byte),
			                      	typeof(Int16),
			                      	typeof(Int32),
			                      	typeof(Int64),
			                      	typeof(SByte),
			                      	typeof(UInt16),
			                      	typeof(UInt32),
			                      	typeof(UInt64),
			                      	typeof(Single),
			                      	typeof(Double),
			                      	typeof(Decimal),
			                      	typeof(Byte?),
			                      	typeof(Int16?),
			                      	typeof(Int32?),
			                      	typeof(Int64?),
			                      	typeof(SByte?),
			                      	typeof(UInt16?),
			                      	typeof(UInt32?),
			                      	typeof(UInt64?),
			                      	typeof(Single?),
			                      	typeof(Double?),
			                      	typeof(Decimal?)
			                      };

			if ((object)templatingContext == null)
				throw new ArgumentNullException("templatingContext");

			dynamicWildcardTokenReplacementStrategy = templatingContext.GetDynamicWildcardTokenReplacementStrategy(false);

			if ((object)this.LeftExpression != null)
				leftObj = this.LeftExpression.EvaluateExpression(templatingContext);

			onDemandRightExpressionEvaluator = () =>
			                                   {
			                                   	if ((object)this.RightExpression != null)
			                                   		return this.RightExpression.EvaluateExpression(templatingContext);
			                                   	else
			                                   		return null;
			                                   };

			if ((object)leftObj == null)
				return null;

			leftType = leftObj.GetType();

			switch (this.BinaryOperator)
			{
				case BinaryOperator.Eq:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs == rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs == rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs == rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs == rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs == rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs == rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs == rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs == rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv == 0;
					}

					break;
				}
				case BinaryOperator.Ne:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs != rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs != rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs != rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs != rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs != rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs != rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs != rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs != rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv != 0;
					}

					break;
				}
				case BinaryOperator.Lt:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs < rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs < rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs < rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs < rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs < rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs < rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs < rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs < rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv < 0;
					}

					break;
				}
				case BinaryOperator.Le:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs <= rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs <= rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs <= rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs <= rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs <= rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs <= rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs <= rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs <= rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv <= 0;
					}

					break;
				}
				case BinaryOperator.Gt:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs > rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs > rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs > rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs > rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs > rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs > rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs > rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs > rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv > 0;
					}

					break;
				}
				case BinaryOperator.Ge:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs >= rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs >= rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs >= rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs >= rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs >= rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs >= rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs >= rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs >= rhs;
						}
					}
					else if (typeof(IComparable).IsAssignableFrom(leftType) &&
					         typeof(IComparable).IsAssignableFrom(rightType) &&
					         rightType.IsAssignableFrom(leftType) &&
					         leftType.IsAssignableFrom(rightType))
					{
						IComparable lhs, rhs;
						int crv;

						lhs = leftObj.ChangeType<IComparable>();
						rhs = rightObj.ChangeType<IComparable>();

						if ((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message |((crv = lhs.CompareTo(rightObj)) != (rhs.CompareTo(leftObj) * -1))");

						return crv >= 0;
					}

					break;
				}
				case BinaryOperator.Add:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs + rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs + rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs + rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs + rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs + rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs + rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs + rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs + rhs;
						}
					}

					break;
				}
				case BinaryOperator.Sub:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs - rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs - rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs - rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs - rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs - rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs - rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs - rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs - rhs;
						}
					}

					break;
				}
				case BinaryOperator.Mul:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs * rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs * rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs * rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs * rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs * rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs * rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs * rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs * rhs;
						}
					}

					break;
				}
				case BinaryOperator.Div:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs / rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs / rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs / rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs / rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs / rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs / rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs / rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs / rhs;
						}
					}

					break;
				}
				case BinaryOperator.Mod:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(Decimal) || leftType == typeof(Decimal?) ||
						    rightType == typeof(Decimal) || rightType == typeof(Decimal?))
						{
							if (leftType != typeof(Single) && leftType != typeof(Single?) &&
							    rightType != typeof(Single) && rightType != typeof(Single?) &&
							    leftType != typeof(Double) && leftType != typeof(Double?) &&
							    rightType != typeof(Double) && rightType != typeof(Double?))
							{
								Decimal lhs, rhs;

								lhs = leftObj.ChangeType<Decimal>();
								rhs = rightObj.ChangeType<Decimal>();

								return lhs % rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Double) || leftType == typeof(Double?) ||
						         rightType == typeof(Double) || rightType == typeof(Double?))
						{
							Double lhs, rhs;

							lhs = leftObj.ChangeType<Double>();
							rhs = rightObj.ChangeType<Double>();

							return lhs % rhs;
						}
						else if (leftType == typeof(Single) || leftType == typeof(Single?) ||
						         rightType == typeof(Single) || rightType == typeof(Single?))
						{
							Single lhs, rhs;

							lhs = leftObj.ChangeType<Single>();
							rhs = rightObj.ChangeType<Single>();

							return lhs % rhs;
						}
						else if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						         rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs % rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs % rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs % rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs % rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs % rhs;
						}
					}

					break;
				}
				case BinaryOperator.Band:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						    rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs & rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs & rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs & rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs & rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs & rhs;
						}
					}

					break;
				}
				case BinaryOperator.Bor:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						    rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs | rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs | rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs | rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs | rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs | rhs;
						}
					}

					break;
				}
				case BinaryOperator.Bxor:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if (leftType == typeof(UInt64) || leftType == typeof(UInt64?) ||
						    rightType == typeof(UInt64) || rightType == typeof(UInt64?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?) &&
							    leftType != typeof(Int64) && leftType != typeof(Int64?) &&
							    rightType != typeof(Int64) && rightType != typeof(Int64?))
							{
								UInt64 lhs, rhs;

								lhs = leftObj.ChangeType<UInt64>();
								rhs = rightObj.ChangeType<UInt64>();

								return lhs ^ rhs;
							}
							else
							{
								// bad
							}
						}
						else if (leftType == typeof(Int64) || leftType == typeof(Int64?) ||
						         rightType == typeof(Int64) || rightType == typeof(Int64?))
						{
							Int64 lhs, rhs;

							lhs = leftObj.ChangeType<Int64>();
							rhs = rightObj.ChangeType<Int64>();

							return lhs ^ rhs;
						}
						else if (leftType == typeof(UInt32) || leftType == typeof(UInt32?) ||
						         rightType == typeof(UInt32) || rightType == typeof(UInt32?))
						{
							if (leftType != typeof(SByte) && leftType != typeof(SByte?) &&
							    rightType != typeof(SByte) && rightType != typeof(SByte?) &&
							    leftType != typeof(Int16) && leftType != typeof(Int16?) &&
							    rightType != typeof(Int16) && rightType != typeof(Int16?) &&
							    leftType != typeof(Int32) && leftType != typeof(Int32?) &&
							    rightType != typeof(Int32) && rightType != typeof(Int32?))
							{
								UInt32 lhs, rhs;

								lhs = leftObj.ChangeType<UInt32>();
								rhs = rightObj.ChangeType<UInt32>();

								return lhs ^ rhs;
							}
							else
							{
								Int64 lhs, rhs;

								lhs = leftObj.ChangeType<Int64>();
								rhs = rightObj.ChangeType<Int64>();

								return lhs ^ rhs;
							}
						}
						else
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs ^ rhs;
						}
					}

					break;
				}
				case BinaryOperator.Bls:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if ((leftType == typeof(Int32) || leftType == typeof(Int32?)) &&
						    (rightType == typeof(Int32) || rightType == typeof(Int32?)))
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs << rhs;
						}
					}

					break;
				}
				case BinaryOperator.Brs:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if ((leftType == typeof(Int32) || leftType == typeof(Int32?)) &&
						    (rightType == typeof(Int32) || rightType == typeof(Int32?)))
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs >> rhs;
						}
					}

					break;
				}
				case BinaryOperator.Bsr:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if ((leftType == typeof(Int32) || leftType == typeof(Int32?)) &&
						    (rightType == typeof(Int32) || rightType == typeof(Int32?)))
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs >> rhs;
						}
					}

					break;
				}
				case BinaryOperator.Bur:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (numericTypes.Count(t => t == leftType) == 1 && numericTypes.Count(t => t == rightType) == 1)
					{
						if ((leftType == typeof(Int32) || leftType == typeof(Int32?)) &&
						    (rightType == typeof(Int32) || rightType == typeof(Int32?)))
						{
							Int32 lhs, rhs;

							lhs = leftObj.ChangeType<Int32>();
							rhs = rightObj.ChangeType<Int32>();

							return lhs >> rhs;
						}
					}

					break;
				}
				case BinaryOperator.StrLk:
				{
					if (leftType == typeof(String))
					{
						rightObj = onDemandRightExpressionEvaluator();

						if ((object)rightObj == null)
							return false;

						rightType = rightObj.GetType();

						if (rightType == typeof(String))
						{
							string lhs, rhs;

							lhs = leftObj.ChangeType<string>();
							rhs = rightObj.ChangeType<string>();

							return lhs.Contains(rhs);
						}
						/*else
						{
							string lhs, rhs;

							lhs = leftObj.SafeToString();
							rhs = rightObj.SafeToString();

							return lhs.Contains(rhs);
						}*/
					}

					break;
				}
				case BinaryOperator.And:
				{
					if (leftType == typeof(Boolean) || leftType == typeof(Boolean?))
					{
						bool lhs;

						lhs = leftObj.ChangeType<bool>();

						// short circuit evaluate
						if (!lhs)
							return false;

						rightObj = onDemandRightExpressionEvaluator();

						if ((object)rightObj == null)
							return null;

						rightType = rightObj.GetType();

						if (rightType == typeof(Boolean) || rightType == typeof(Boolean?))
						{
							bool rhs;

							rhs = rightObj.ChangeType<bool>();

							return rhs;
						}
					}

					break;
				}
				case BinaryOperator.Or:
				{
					if (leftType == typeof(Boolean) || leftType == typeof(Boolean?))
					{
						bool lhs;

						lhs = leftObj.ChangeType<bool>();

						// short circuit evaluate
						if (lhs)
							return true;

						rightObj = onDemandRightExpressionEvaluator();

						if ((object)rightObj == null)
							return null;

						rightType = rightObj.GetType();

						if (rightType == typeof(Boolean) || rightType == typeof(Boolean?))
						{
							bool rhs;

							rhs = rightObj.ChangeType<bool>();

							return rhs;
						}
					}

					break;
				}
				case BinaryOperator.Xor:
				{
					if (leftType == typeof(Boolean) || leftType == typeof(Boolean?))
					{
						bool lhs;

						lhs = leftObj.ChangeType<bool>();

						// no short circuit evaluate possible here

						rightObj = onDemandRightExpressionEvaluator();

						if ((object)rightObj == null)
							return null;

						rightType = rightObj.GetType();

						if (rightType == typeof(Boolean) || rightType == typeof(Boolean?))
						{
							bool rhs;

							rhs = rightObj.ChangeType<bool>();

							return (lhs && !rhs) || (rhs && !lhs);
						}
					}

					break;
				}
				case BinaryOperator.ObjAs:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (rightType == typeof(String))
					{
						string rhs;

						rhs = rightObj.ChangeType<string>();

						if (DataType.IsWhiteSpace(rhs))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message | DataType.IsNullOrWhiteSpace(rhs)");

						rightType = Type.GetType(rhs, false);

						if ((object)rightType == null)
							throw new InvalidOperationException("TODO (enhancement): add meaningful message | Type.GetType");

						return DataType.ChangeType(leftObj, rightType);
					}

					break;
				}
				case BinaryOperator.Parse:
				{
					if (leftType == typeof(String))
					{
						rightObj = onDemandRightExpressionEvaluator();

						if ((object)rightObj == null)
							return null;

						rightType = rightObj.GetType();

						if (rightType == typeof(String))
						{
							string lhs, rhs;
							object result;

							lhs = leftObj.ChangeType<string>();
							rhs = rightObj.ChangeType<string>();

							if (DataType.IsWhiteSpace(rhs))
								throw new InvalidOperationException("TODO (enhancement): add meaningful message | DataType.IsNullOrWhiteSpace(rhs)");

							rightType = Type.GetType(rhs, false);

							if ((object)rightType == null)
								throw new InvalidOperationException("TODO (enhancement): add meaningful message | Type.GetType");

							if (!DataType.TryParse(rightType, lhs, out result))
								throw new InvalidOperationException("TODO (enhancement): add meaningful message | DataType.TryParse");

							return result;
						}
					}

					break;
				}
				case BinaryOperator.ObjIs:
				{
					rightObj = onDemandRightExpressionEvaluator();

					if ((object)rightObj == null)
						return null;

					rightType = rightObj.GetType();

					if (rightType == typeof(String))
					{
						string rhs;

						rhs = rightObj.ChangeType<string>();

						if (DataType.IsWhiteSpace(rhs))
							throw new InvalidOperationException("TODO (enhancement): add meaningful message | DataType.IsNullOrWhiteSpace(rhs)");

						rightType = Type.GetType(rhs, false);

						if ((object)rightType == null)
							throw new InvalidOperationException("TODO (enhancement): add meaningful message | Type.GetType");

						return rightType.IsAssignableFrom(leftType);
					}

					break;
				}
				case BinaryOperator.VarPut:
				{
					if (typeof(AspectConstruct).IsAssignableFrom(leftType))
					{
						AspectConstruct lhs;
						ExpressionContainerConstruct expressionContainerConstruct;
						ValueConstruct valueConstruct;

						rightObj = onDemandRightExpressionEvaluator();

						rightType = (object)rightObj != null ? rightObj.GetType() : null;

						lhs = leftObj.ChangeType<AspectConstruct>();

						expressionContainerConstruct = new ExpressionContainerConstruct();

						valueConstruct = new ValueConstruct()
						                 {
						                 	Type = (object)rightType != null ? rightType.FullName : null,
						                 	__ = rightObj
						                 };

						expressionContainerConstruct.Content = valueConstruct;

						new AssignConstruct()
						{
							Token = lhs.Name,
							Expression = expressionContainerConstruct
						}.ExpandTemplate(templatingContext);

						return rightObj;
					}

					break;
				}
				default:
				{
					throw new InvalidOperationException("TODO (enhancement): add meaningful message | binary operator is not recognized");
				}
			}

			throw new InvalidOperationException("TODO (enhancement): add meaningful message | type is not supported by the binary operator");
		}

		#endregion
	}
}