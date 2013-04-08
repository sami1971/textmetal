﻿//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by:
// TextMetal 5.0.0.26315;
// 		Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
//		Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
//		Project URL: https://github.com/dpbullington/textmetal
//
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
//
// </auto-generated>
//------------------------------------------------------------------------------

/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using TextMetal.Common.Core;
using TextMetal.Common.Data;

namespace TextMetal.HostImpl.AspNetSample.Objects.Model
{
	public partial class Repository : IRepository
	{		
		#region Constructors/Destructors
		
		public Repository()
		{
		}
		
		#endregion
		
		#region Fields/Constants
		
		private const string CONNECTION_STRING_NAME = "TextMetal.HostImpl.AspNetSample.Objects.Model::ConnectionString";
						
		#endregion
		
		#region Properties/Indexers/Events
		
		public static string ConnectionString
		{
			get
			{
				string connectionString;

				connectionString = AppConfig.GetConnectionString(CONNECTION_STRING_NAME);

				OnPreProcessConnectionString(ref connectionString);
				
				return connectionString;
			}
		}
		
		public static Type ConnectionType
		{
			get
			{
				return Type.GetType(AppConfig.GetConnectionProvider(CONNECTION_STRING_NAME), true);
			}
		}
		
		public static string DataSourceTag
		{
			get
			{
				string value;

				if (!AppConfig.HasAppSetting("TextMetal.HostImpl.AspNetSample.Objects.Model::DataSourceTag"))
					return null;

				value = AppConfig.GetAppSetting<string>("TextMetal.HostImpl.AspNetSample.Objects.Model::DataSourceTag");

				return value;
			}
		}
				
		#endregion
		
		#region Methods/Operators
		
		public static IUnitOfWorkContext GetUnitOfWorkContext()
		{
			return UnitOfWorkContext.Create(ConnectionType, ConnectionString, true);
		}

		static partial void OnPreProcessConnectionString(ref string connectionString);
		
		#endregion
		
		#region Classes/Structs/Interfaces/Enums/Delegates

		public sealed class UowcFactory : IUnitOfWorkContextFactory
		{
			#region Constructors/Destructors

			private UowcFactory()
			{
			}

			#endregion

			#region Fields/Constants

			private static readonly IUnitOfWorkContextFactory instance = new UowcFactory();

			#endregion

			#region Properties/Indexers/Events

			public static IUnitOfWorkContextFactory Instance
			{
				get
				{
					return instance;
				}
			}

			#endregion

			#region Methods/Operators

			public IUnitOfWorkContext GetUnitOfWorkContext()
			{
				return Repository.GetUnitOfWorkContext();
			}

			#endregion
		}

		#endregion
	}
}
