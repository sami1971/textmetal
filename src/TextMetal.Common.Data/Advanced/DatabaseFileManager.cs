/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Reflection;
using System.Web;

using TextMetal.Common.Core;

namespace TextMetal.Common.Data.Advanced
{
	public sealed class DatabaseFileManager
	{
		#region Constructors/Destructors

		public DatabaseFileManager(INativeDatabaseFileCreator nativeDatabaseFileCreator, string underlyingDatabaseFilePath)
		{
			if ((object)nativeDatabaseFileCreator == null)
				throw new ArgumentNullException("nativeDatabaseFileCreator");

			if ((object)underlyingDatabaseFilePath == null)
				throw new ArgumentNullException("underlyingDatabaseFilePath");

			if (DataType.IsWhiteSpace(underlyingDatabaseFilePath))
				throw new ArgumentOutOfRangeException("underlyingDatabaseFilePath");

			this.nativeDatabaseFileCreator = nativeDatabaseFileCreator;
			this.underlyingDatabaseFilePath = underlyingDatabaseFilePath;
		}

		#endregion

		#region Fields/Constants

		private readonly INativeDatabaseFileCreator nativeDatabaseFileCreator;
		private readonly string underlyingDatabaseFilePath;

		#endregion

		#region Properties/Indexers/Events

		public static string DatabaseDirectoryPath
		{
			get
			{
				return AppConfig.GetAppSetting<string>("TextMetal.WebHostSample.Objects.Model::DatabaseDirectoryPath");
			}
		}

		public static string DatabaseFileName
		{
			get
			{
				return AppConfig.GetAppSetting<string>("TextMetal.WebHostSample.Objects.Model::DatabaseFileName");
			}
		}

		public static string DatabaseFilePath
		{
			get
			{
				string value;

				// {0} == GetApplicationUserSpecificDirectoryPath()
				value = Path.Combine(string.Format(DatabaseDirectoryPath ?? "", GetApplicationUserSpecificDirectoryPath()), DatabaseFileName);

				return value;
			}
		}

		public static bool KillDatabaseFile
		{
			get
			{
				bool value;

				if (!AppConfig.HasAppSetting("TextMetal.WebHostSample.Objects.Model::KillDatabaseFile"))
					return false;

				value = AppConfig.GetAppSetting<bool>("TextMetal.WebHostSample.Objects.Model::KillDatabaseFile");

				return value;
			}
		}

		public static bool UseDatabaseFile
		{
			get
			{
				bool value;

				value = AppConfig.GetAppSetting<bool>("TextMetal.WebHostSample.Objects.Model::UseDatabaseFile");

				return value;
			}
		}

		private INativeDatabaseFileCreator NativeDatabaseFileCreator
		{
			get
			{
				return this.nativeDatabaseFileCreator;
			}
		}

		public string UnderlyingDatabaseFilePath
		{
			get
			{
				return this.underlyingDatabaseFilePath;
			}
		}

		#endregion

		#region Methods/Operators

		private static string GetApplicationUserSpecificDirectoryPath()
		{
			Assembly assembly;
			AssemblyInformation assemblyInformation;
			string userSpecificDirectoryPath;

			if (ExecutionPathStorage.IsInHttpContext)
				userSpecificDirectoryPath = Path.GetFullPath(HttpContext.Current.Server.MapPath("~/"));
			else
			{
				assembly = Assembly.GetExecutingAssembly();
				assemblyInformation = new AssemblyInformation(assembly);

				if ((object)assemblyInformation.Company != null &&
				    (object)assemblyInformation.Product != null &&
				    (object)assemblyInformation.Win32FileVersion != null)
				{
					userSpecificDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					userSpecificDirectoryPath = Path.Combine(userSpecificDirectoryPath, assemblyInformation.Company);
					userSpecificDirectoryPath = Path.Combine(userSpecificDirectoryPath, assemblyInformation.Product);
					userSpecificDirectoryPath = Path.Combine(userSpecificDirectoryPath, assemblyInformation.Win32FileVersion);
				}
				else
					userSpecificDirectoryPath = Path.GetFullPath(".");
			}

			return userSpecificDirectoryPath;
		}

		/*private static string GetConnectionString(string databaseFilePath)
		{
			if ((object)databaseFilePath == null)
				throw new ArgumentNullException("databaseFilePath");

			if (DataType.IsNullOrWhiteSpace(databaseFilePath))
				throw new ArgumentOutOfRangeException("databaseFilePath");

			return string.Format("Data Source={0}", databaseFilePath);
		}*/

		public static void InitDatabase(INativeDatabaseFileCreator nativeDatabaseFileCreator, IUnitOfWorkContextFactory unitOfWorkContextFactory, Type type, string resource)
		{
			DatabaseHistory history;
			DatabaseFileManager databaseFileManager;

			if ((object)nativeDatabaseFileCreator == null)
				throw new ArgumentNullException("nativeDatabaseFileCreator");

			if ((object)unitOfWorkContextFactory == null)
				throw new ArgumentNullException("unitOfWorkContextFactory");

			if ((object)type == null)
				throw new ArgumentNullException("type");

			if ((object)resource == null)
				throw new ArgumentNullException("resource");

			if (DataType.IsNullOrWhiteSpace(resource))
				throw new ArgumentOutOfRangeException("resource");

			if (UseDatabaseFile)
			{
				if (!DataType.IsNullOrWhiteSpace(DatabaseFilePath))
				{
					if (KillDatabaseFile)
					{
						if (File.Exists(DatabaseFilePath))
							File.Delete(DatabaseFilePath);
					}

					databaseFileManager = new DatabaseFileManager(nativeDatabaseFileCreator, DatabaseFilePath);
					databaseFileManager.EnsureDatabaseFile();
				}

				if (!Cerealization.TryGetFromAssemblyResource<DatabaseHistory>(type, resource, out history))
					throw new InvalidOperationException(string.Format("Unable to deserialize instance of '{0}' from the manifest resource name '{1}' in the assembly '{2}'.", typeof(DatabaseHistory).FullName, resource, type.Assembly.FullName));

				using (IUnitOfWorkContext unitOfWorkContext = unitOfWorkContextFactory.GetUnitOfWorkContext())
				{
					history.PerformSchemaUpgrade(unitOfWorkContext);

					unitOfWorkContext.Complete();
				}
			}
		}

		private bool EnsureDatabaseFile()
		{
			string databaseFilePath;
			string databaseDirectoryPath;
			bool retval = false;

			databaseFilePath = Path.GetFullPath(this.UnderlyingDatabaseFilePath);
			databaseDirectoryPath = Path.GetDirectoryName(databaseFilePath);

			if (!Directory.Exists(databaseDirectoryPath))
				Directory.CreateDirectory(databaseDirectoryPath);

			if (!File.Exists(databaseFilePath))
				retval = this.NativeDatabaseFileCreator.CreateNativeDatabaseFile(databaseFilePath);

			return retval;
		}

		#endregion
	}
}