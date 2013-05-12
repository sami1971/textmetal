/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Common.Xml;
using TextMetal.Framework.Core;

namespace TextMetal.Framework.SortModel
{
	public abstract class SortXmlObject : XmlObject, ISortXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the SortXmlObject class.
		/// </summary>
		protected SortXmlObject()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract IEnumerable CoreEvaluateSort(ITemplatingContext templatingContext, IEnumerable values);

		public IEnumerable EvaluateSort(ITemplatingContext templatingContext, IEnumerable values)
		{
			return this.EvaluateSort(templatingContext, values);
		}

		#endregion
	}
}