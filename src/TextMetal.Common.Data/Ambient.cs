/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace TextMetal.Common.Data
{
	public static class Ambient
	{
		#region Methods/Operators

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, TResult> callbackMethod, T1 p1)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, TResult> callbackMethod, T1 p1, T2 p2)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, TResult> callbackMethod, T1 p1, T2 p2, T3 p3)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97, T98 p98)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98);
		}

		public static TResult ExecuteAmbientUnitOfWorkAware<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, T99, TResult>(IUnitOfWorkContextFactory unitOfWorkContextFactory, AmbientCallback<IUnitOfWorkContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, T99, TResult> callbackMethod, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97, T98 p98, T99 p99)
		{
			TResult retval;

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)UnitOfWorkContext.Current == null)
			{
				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					retval = callbackMethod(unitOfWorkContext, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99);

					unitOfWorkContext.Complete();

					return retval;
				}
			}
			else
				return callbackMethod(UnitOfWorkContext.Current, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51, p52, p53, p54, p55, p56, p57, p58, p59, p60, p61, p62, p63, p64, p65, p66, p67, p68, p69, p70, p71, p72, p73, p74, p75, p76, p77, p78, p79, p80, p81, p82, p83, p84, p85, p86, p87, p88, p89, p90, p91, p92, p93, p94, p95, p96, p97, p98, p99);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public delegate TResult AmbientCallback<T1, TResult>(T1 p1);

		public delegate TResult AmbientCallback<T1, T2, TResult>(T1 p1, T2 p2);

		public delegate TResult AmbientCallback<T1, T2, T3, TResult>(T1 p1, T2 p2, T3 p3);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, TResult>(T1 p1, T2 p2, T3 p3, T4 p4);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97, T98 p98);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, T99, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97, T98 p98, T99 p99);

		public delegate TResult AmbientCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50, T51, T52, T53, T54, T55, T56, T57, T58, T59, T60, T61, T62, T63, T64, T65, T66, T67, T68, T69, T70, T71, T72, T73, T74, T75, T76, T77, T78, T79, T80, T81, T82, T83, T84, T85, T86, T87, T88, T89, T90, T91, T92, T93, T94, T95, T96, T97, T98, T99, T100, TResult>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10, T11 p11, T12 p12, T13 p13, T14 p14, T15 p15, T16 p16, T17 p17, T18 p18, T19 p19, T20 p20, T21 p21, T22 p22, T23 p23, T24 p24, T25 p25, T26 p26, T27 p27, T28 p28, T29 p29, T30 p30, T31 p31, T32 p32, T33 p33, T34 p34, T35 p35, T36 p36, T37 p37, T38 p38, T39 p39, T40 p40, T41 p41, T42 p42, T43 p43, T44 p44, T45 p45, T46 p46, T47 p47, T48 p48, T49 p49, T50 p50, T51 p51, T52 p52, T53 p53, T54 p54, T55 p55, T56 p56, T57 p57, T58 p58, T59 p59, T60 p60, T61 p61, T62 p62, T63 p63, T64 p64, T65 p65, T66 p66, T67 p67, T68 p68, T69 p69, T70 p70, T71 p71, T72 p72, T73 p73, T74 p74, T75 p75, T76 p76, T77 p77, T78 p78, T79 p79, T80 p80, T81 p81, T82 p82, T83 p83, T84 p84, T85 p85, T86 p86, T87 p87, T88 p88, T89 p89, T90 p90, T91 p91, T92 p92, T93 p93, T94 p94, T95 p95, T96 p96, T97 p97, T98 p98, T99 p99, T100 p100);

		#endregion
	}
}