/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Core.Plumbing;

namespace TextMetal.Core.TokenModel
{
	/// <summary>
	/// Provides a wldcard token replacement strategy which returns
	/// the data using reflection or dictionary semantics against an object property path.
	/// </summary>
	public class DynamicWildcardTokenReplacementStrategy : IWildcardTokenReplacementStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DynamicWildcardTokenReplacementStrategy class.
		/// </summary>
		/// <param name="targets">The tagret object instances to evaluate (in linear order) during wildcard token replacement.</param>
		public DynamicWildcardTokenReplacementStrategy(object[] targets)
			:
				this(targets, true)
		{
		}

		/// <summary>
		/// Initializes a new instance of the DynamicWildcardTokenReplacementStrategy class.
		/// This overload overrides the default strict setting (true).
		/// </summary>
		/// <param name="targets">The tagret object instances to evaluate (in linear order) during wildcard token replacement.</param>
		/// <param name="strict">A value indicating if exceptions are thrown for bad token matches.</param>
		public DynamicWildcardTokenReplacementStrategy(object[] targets, bool strict)
		{
			this.targets = targets;
			this.strict = strict;
		}

		#endregion

		#region Fields/Constants

		private readonly bool strict;
		private readonly object[] targets;

		#endregion

		#region Properties/Indexers/Events

		public bool Strict
		{
			get
			{
				return this.strict;
			}
		}

		/// <summary>
		/// Gets the target object instance value to evaluate (in linear order) during wildcard token replacement.
		/// </summary>
		public object[] Targets
		{
			get
			{
				return this.targets;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Evaluate a token using any parameters specified.
		/// </summary>
		/// <param name="token">The wildcard token to evaludate.</param>			
		/// <param name="parameters">Should be null for value semantics; or a valid object array for function semantics.</param>
		/// <returns>An approapriate token replacement value.</returns>
		public object Evaluate(string token, object[] parameters)
		{
			object value;

			if (!this.GetByPath(token, out value))
			{
				if (this.Strict)
					throw new ArgumentException("TODO (enhancement): add meaningful message " + token);
			}

			return value;
		}

		public bool GetByPath(string path, out object value)
		{
			value = null;

			if ((object)this.Targets != null)
			{
				foreach (object target in this.Targets)
				{
					if (Reflexion.GetLogicalPropertyValue(target, path, out value))
						return true;
				}
			}

			if (this.Strict)
				throw new ArgumentException("TODO (enhancement): add meaningful message " + path);

			return false;
		}

		public bool SetByPath(string path, object value)
		{
			object unused;

			// is this needed?
			if (this.Strict && !this.GetByPath(path, out unused))
				throw new ArgumentException("TODO (enhancement): add meaningful message " + path);

			if ((object)this.Targets != null)
			{
				foreach (object target in this.Targets)
				{
					if (Reflexion.SetLogicalPropertyValue(target, path, value))
						return true;
				}
			}

			if (this.Strict)
				throw new ArgumentException("TODO (enhancement): add meaningful message " + path);

			return false;
		}

		#endregion
	}
}