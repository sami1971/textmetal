﻿/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using TextMetal.Core.AssociativeModel;
using TextMetal.Core.XmlModel;

namespace TextMetal.Core.SourceModel.Primative
{
	public class AssociativeXmlSourceStrategy : XmlPersistEngineSourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the AssociativeXmlSourceStrategy class.
		/// </summary>
		public AssociativeXmlSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override IXmlPersistEngine GetXmlPersistEngine(IDictionary<string, IList<string>> properties)
		{
			IXmlPersistEngine xpe;

			xpe = new XmlPersistEngine();
			xpe.RegisterKnownXmlObject<ArrayConstruct>();
			xpe.RegisterKnownXmlObject<ModelConstruct>();
			xpe.RegisterKnownXmlObject<ObjectConstruct>();
			xpe.RegisterKnownXmlObject<PropertyConstruct>();

			return xpe;
		}

		#endregion
	}
}