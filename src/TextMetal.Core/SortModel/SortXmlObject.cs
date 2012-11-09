/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;

using TextMetal.Core.TemplateModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SortModel
{
	public abstract class SortXmlObject : XmlObject, ISortXmlObject
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the SortXmlObject class.
		/// </summary>
		protected SortXmlObject()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract IEnumerable CoreEvaluateSort(TemplatingContext templatingContext, IEnumerable values);

		public IEnumerable EvaluateSort(TemplatingContext templatingContext, IEnumerable values)
		{
			return this.EvaluateSort(templatingContext, values);
		}

		#endregion
	}
}