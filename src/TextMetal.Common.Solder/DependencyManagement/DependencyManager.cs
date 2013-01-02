/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;

using TextMetal.Common.Core;

namespace TextMetal.Common.Solder.DependencyManagement
{
	/// <summary>
	/// Provides dependency registration and resoltuion services. Uses reader-writer lock for asynchronous protection (i.e. thread-safety).
	/// </summary>
	public sealed class DependencyManager : IDependencyManager
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the DependencyManager class.
		/// </summary>
		public DependencyManager()
		{
		}

		#endregion

		#region Fields/Constants

		private const int LOCK_AQUIRE_TIMEOUT_MILLISECONDS = 500;
		private static readonly object synchObj = new object();
		private static bool ready;
		private readonly Dictionary<KeyValuePair<Type, string>, IDependencyResolution> dependencyResolutionRegistrations = new Dictionary<KeyValuePair<Type, string>, IDependencyResolution>();
		private readonly ReaderWriterLock readerWriterLock = new ReaderWriterLock();
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the singleton instance associated with the current application domain. Most applications will use this instance instead of creating their own instance.
		/// </summary>
		public static IDependencyManager AppDomainInstance
		{
			get
			{
				return LazySingleton.appDomainInstance;
			}
		}

		/// <summary>
		/// Gets the current app setting for disabling of resolution auto-wiring.
		/// </summary>
		private static bool DisableResolutionAutoWire
		{
			get
			{
				string svalue;
				bool bvalue;

				svalue = ConfigurationManager.AppSettings[string.Format("{0}::DisableResolutionAutoWire", typeof(DependencyManager).FullName)];

				if (bool.TryParse(svalue, out bvalue))
					return bvalue;
				else
					return false;
			}
		}

		private Dictionary<KeyValuePair<Type, string>, IDependencyResolution> DependencyResolutionRegistrations
		{
			get
			{
				return this.dependencyResolutionRegistrations;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the current instance has been disposed.
		/// </summary>
		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
			private set
			{
				this.disposed = value;
			}
		}

		private ReaderWriterLock ReaderWriterLock
		{
			get
			{
				return this.readerWriterLock;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Private method to handle the assembly load events on application domains.
		/// </summary>
		/// <param name="sender"> The sending object. </param>
		/// <param name="args"> The event arguments. </param>
		private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			ScanAssemblies(new Assembly[] { args.LoadedAssembly });
		}

		/// <summary>
		/// Private method to handle the assembly unload events on application domains.
		/// </summary>
		/// <param name="sender"> The sending object. </param>
		/// <param name="e"> The event arguments. </param>
		private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
		{
			TearDownApplicationDomain();
		}

		/// <summary>
		/// Private method that will scan all asemblies specified to perform auto-wiring of dependencies.
		/// </summary>
		/// <param name="assemblies"> An arry of ssemblies to scan and load dependency resolutions automatically ("auto-wire" feature). </param>
		private static void ScanAssemblies(Assembly[] assemblies)
		{
			Type[] assemblyTypes;
			MethodInfo[] methodInfos;
			DependencyRegistrationAttribute dependencyRegistrationAttribute;
			Action dependencyRegistrationMethod;

			if ((object)assemblies != null)
			{
				foreach (Assembly assembly in assemblies)
				{
					dependencyRegistrationAttribute = Reflexion.GetOneAttribute<DependencyRegistrationAttribute>(assembly);

					if ((object)dependencyRegistrationAttribute == null)
						continue;

					assemblyTypes = assembly.GetTypes();

					if ((object)assemblyTypes != null)
					{
						foreach (Type assemblyType in assemblyTypes)
						{
							dependencyRegistrationAttribute = Reflexion.GetOneAttribute<DependencyRegistrationAttribute>(assemblyType);

							if ((object)dependencyRegistrationAttribute == null)
								continue;

							if (!assemblyType.IsPublic)
								continue;

							methodInfos = assemblyType.GetMethods(BindingFlags.Public | BindingFlags.Static);

							if ((object)methodInfos != null)
							{
								foreach (MethodInfo methodInfo in methodInfos)
								{
									dependencyRegistrationAttribute = Reflexion.GetOneAttribute<DependencyRegistrationAttribute>(methodInfo);

									if ((object)dependencyRegistrationAttribute == null)
										continue;

									if (!methodInfo.IsStatic)
										continue;

									if (!methodInfo.IsPublic)
										continue;

									dependencyRegistrationMethod = (Action)(Delegate.CreateDelegate(typeof(Action), methodInfo, false));

									if ((object)dependencyRegistrationMethod == null)
										continue;

									dependencyRegistrationMethod();
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Private thread-safe method which bootstraps an app domain for dependency management.
		/// </summary>
		private static void SetUpApplicationDomain()
		{
			lock (synchObj)
			{
				if (ready)
					return;

				AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
				AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomain_DomainUnload);
				ScanAssemblies(AppDomain.CurrentDomain.GetAssemblies());

				ready = true;
			}
		}

		/// <summary>
		/// Private thread-safe method which dismantles an app domain for dependency management.
		/// </summary>
		private static void TearDownApplicationDomain()
		{
			lock (synchObj)
			{
				if (!ready)
					return;

				// cleanup
				if ((object)AppDomainInstance != null)
					AppDomainInstance.Dispose();

				AppDomain.CurrentDomain.DomainUnload -= new EventHandler(CurrentDomain_DomainUnload);
				AppDomain.CurrentDomain.AssemblyLoad -= new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
			}
		}

		/// <summary>
		/// Adds a new dependency resolution for a given target type and selector key. Throws a DependencyException if the target type and selector key combination has been previously registered in this instance. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type of resolution. </typeparam>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		/// <param name="dependencyResolution"> The dependency resolution. </param>
		public void AddResolution<TObject>(string selectorKey, IDependencyResolution dependencyResolution)
		{
			Type targetType;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			if ((object)dependencyResolution == null)
				throw new ArgumentNullException("dependencyResolution");

			targetType = typeof(TObject);

			this.AddResolution(targetType, selectorKey, dependencyResolution);
		}

		/// <summary>
		/// Adds a new dependency resolution for a given target type and selector key. Throws a DependencyException if the target type and selector key combination has been previously registered in this instance. This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type of resolution. </param>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		/// <param name="dependencyResolution"> The dependency resolution. </param>
		public void AddResolution(Type targetType, string selectorKey, IDependencyResolution dependencyResolution)
		{
			KeyValuePair<Type, string> trait;
			LockCookie lockCookie;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			if ((object)dependencyResolution == null)
				throw new ArgumentNullException("dependencyResolution");

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					trait = new KeyValuePair<Type, string>(targetType, selectorKey);

					if (this.DependencyResolutionRegistrations.ContainsKey(trait))
						throw new DependencyException(string.Format("Dependency resolution already exists in the dependency manager for target type '{0}' and selector key '{1}'.", targetType.FullName, selectorKey));

					this.DependencyResolutionRegistrations.Add(trait, dependencyResolution);
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Clears all registered dependency resolutions from this instance.
		/// </summary>
		/// <returns> A value indicating if at least one dependency resolution was removed. </returns>
		public bool ClearAllResolutions()
		{
			bool result;
			LockCookie lockCookie;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					result = this.DependencyResolutionRegistrations.Count > 0;

					this.DependencyResolutionRegistrations.Clear();

					return result;
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Clears all registered dependency resolutions of the specified target type from this instance. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type of removal. </typeparam>
		/// <returns> A value indicating if at least one dependency resolution was removed. </returns>
		public bool ClearTypeResolutions<TObject>()
		{
			Type targetType;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			targetType = typeof(TObject);

			return this.ClearTypeResolutions(targetType);
		}

		/// <summary>
		/// Clears all registered dependency resolutions of the specified target type from this instance. This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type of removal. </param>
		/// <returns> A value indicating if at least one dependency resolution was removed. </returns>
		public bool ClearTypeResolutions(Type targetType)
		{
			List<KeyValuePair<Type, string>> traitsToRemove;
			LockCookie lockCookie;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					traitsToRemove = new List<KeyValuePair<Type, string>>();

					foreach (KeyValuePair<KeyValuePair<Type, string>, IDependencyResolution> dependencyResolutionRegistration in this.DependencyResolutionRegistrations)
					{
						if (dependencyResolutionRegistration.Key.Key == targetType)
							traitsToRemove.Add(dependencyResolutionRegistration.Key);
					}

					foreach (KeyValuePair<Type, string> trait in traitsToRemove)
						this.DependencyResolutionRegistrations.Remove(trait);

					return traitsToRemove.Count > 0;
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Clears all dependency resolutions and cleans up any resources. Once disposed, the instance cannot be reused.
		/// </summary>
		public void Dispose()
		{
			LockCookie lockCookie;

			if (this.Disposed)
				return;

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					this.DependencyResolutionRegistrations.Clear();
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>
		/// Gets a value indicating whether there are any registered dependency resolutions of the specified target type in this instance. If selector key is a null value, then this method will return true if any resolution exists for the specified target type, regardless of selector key; otherwise, this method will return true only if a resolution exists for the specified target type and selector key. This is the generic overload.
		/// </summary>
		/// <param name="selectorKey"> An null or zero or greater length string selector key. </param>
		/// <typeparam name="TObject"> The target type of the check. </typeparam>
		/// <returns> A value indicating if at least one dependency resolution is present. </returns>
		public bool HasTypeResolution<TObject>(string selectorKey)
		{
			Type targetType;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			targetType = typeof(TObject);

			return this.HasTypeResolution(targetType, selectorKey);
		}

		/// <summary>
		/// Gets a value indicating whether there are any registered dependency resolutions of the specified target type in this instance. If selector key is a null value, then this method will return true if any resolution exists for the specified target type, regardless of selector key; otherwise, this method will return true only if a resolution exists for the specified target type and selector key. This is the non-generic overload.
		/// </summary>
		/// <param name="selectorKey"> An null or zero or greater length string selector key. </param>
		/// <param name="targetType"> The target type of the check. </param>
		/// <returns> A value indicating if at least one dependency resolution is present. </returns>
		public bool HasTypeResolution(Type targetType, string selectorKey)
		{
			List<KeyValuePair<Type, string>> traitsToRemove;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			// selector key can be null in this context

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				traitsToRemove = new List<KeyValuePair<Type, string>>();

				return this.DependencyResolutionRegistrations.Keys.Count(x => x.Key == targetType
				                                                              && ((object)selectorKey == null || x.Value == selectorKey)) > 0;
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Removes the registered dependency resolution of the specified target type and selector key from this instance. Throws a DependencyException if the target type and selector key combination has not been previously registered in this instance. This is the generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type of removal. </typeparam>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		public void RemoveResolution<TObject>(string selectorKey)
		{
			Type targetType;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			targetType = typeof(TObject);

			this.RemoveResolution(targetType, selectorKey);
		}

		/// <summary>
		/// Removes the registered dependency resolution of the specified target type and selector key from this instance. Throws a DependencyException if the target type and selector key combination has not been previously registered in this instance. This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type of removal. </param>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		public void RemoveResolution(Type targetType, string selectorKey)
		{
			KeyValuePair<Type, string> trait;
			LockCookie lockCookie;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				// cop a writer lock
				lockCookie = this.ReaderWriterLock.UpgradeToWriterLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

				try
				{
					trait = new KeyValuePair<Type, string>(targetType, selectorKey);

					if (!this.DependencyResolutionRegistrations.ContainsKey(trait))
						throw new DependencyException(string.Format("Dependency resolution does not exist in the dependency manager for target type '{0}' and selector key '{1}'.", targetType.FullName, selectorKey));

					this.DependencyResolutionRegistrations.Remove(trait);
				}
				finally
				{
					this.ReaderWriterLock.DowngradeFromWriterLock(ref lockCookie);
				}
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		/// <summary>
		/// Resolves a dependency for the given target type and selector key combination. Throws a DependencyException if the target type and selector key combination has not been previously registered in this instance. Throws a DependencyException if the resolved value cannot be assigned to the target type. This is the non-generic overload.
		/// </summary>
		/// <typeparam name="TObject"> The target type of resolution. </typeparam>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		/// <returns> An object instance of assisgnable to the target type. </returns>
		public TObject ResolveDependency<TObject>(string selectorKey)
		{
			Type targetType;
			TObject value;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			targetType = typeof(TObject);

			value = (TObject)this.ResolveDependency(targetType, selectorKey);

			return value;
		}

		/// <summary>
		/// Resolves a dependency for the given target type and selector key combination. Throws a DependencyException if the target type and selector key combination has not been previously registered in this instance. Throws a DependencyException if the resolved value cannot be assigned to the target type. This is the non-generic overload.
		/// </summary>
		/// <param name="targetType"> The target type of resolution. </param>
		/// <param name="selectorKey"> An non-null, zero or greater length string selector key. </param>
		/// <returns> An object instance of assisgnable to the target type. </returns>
		public object ResolveDependency(Type targetType, string selectorKey)
		{
			object value;
			KeyValuePair<Type, string> trait;
			IDependencyResolution dependencyResolution;
			Type typeOfValue;

			if (this.Disposed)
				throw new ObjectDisposedException(typeof(DependencyManager).FullName);

			if ((object)targetType == null)
				throw new ArgumentNullException("targetType");

			if ((object)selectorKey == null)
				throw new ArgumentNullException("selectorKey");

			// cop a reader lock
			this.ReaderWriterLock.AcquireReaderLock(LOCK_AQUIRE_TIMEOUT_MILLISECONDS);

			try
			{
				trait = new KeyValuePair<Type, string>(targetType, selectorKey);

				if (!this.DependencyResolutionRegistrations.ContainsKey(trait))
				{
// To keep from shooting oneself in the foot, this must be explicitly enabled at compile-time.
#if !ALLOW_FCFS_INDIRECT_RESOLUTION
					throw new DependencyException(string.Format("Dependency resolution in the dependency manager failed to match for target type '{0}' and selector key '{1}'.", targetType.FullName, selectorKey));
#else
	// no direct resolution; lets try FCFS indirect resolution...
					var candidateResolutions = this.DependencyResolutionRegistrations.Keys.Where(x => targetType.IsAssignableFrom(x.Key)
						&& (selectorKey == "" || x.Value == selectorKey));

					if (candidateResolutions.Count() < 1) // nothing to offer up
						throw new DependencyException(string.Format("Dependency resolution in the dependency manager failed to match for target type '{0}' and selector key '{1}'.", targetType.FullName, selectorKey));
					
					// pick the 'best' one...???
					trait = candidateResolutions.First();
#endif
				}

				dependencyResolution = this.DependencyResolutionRegistrations[trait];

				if ((object)dependencyResolution == null)
					throw new InvalidOperationException("dependencyResolution");

				value = dependencyResolution.Resolve();

				if ((object)value != null)
				{
					typeOfValue = value.GetType();

					if (!targetType.IsAssignableFrom(typeOfValue))
						throw new DependencyException(string.Format("Dependency resolution in the dependency manager matched for target type '{0}' and selector key '{1}' but the resolved value is not assignable from resolved type '{2}'.", targetType.FullName, selectorKey, typeOfValue.FullName));
				}

				return value;
			}
			finally
			{
				this.ReaderWriterLock.ReleaseReaderLock();
			}
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		/// <summary>
		/// http://www.yoda.arachsys.com/csharp/singleton.html
		/// </summary>
		private class LazySingleton
		{
			#region Constructors/Destructors

			static LazySingleton()
			{
				if (!DisableResolutionAutoWire)
					LazySetUpApplicationDomain();
			}

			#endregion

			#region Fields/Constants

			internal static readonly DependencyManager appDomainInstance = new DependencyManager();

			#endregion

			#region Methods/Operators

			private static void LazySetUpApplicationDomain()
			{
				SetUpApplicationDomain();
			}

			#endregion
		}

		#endregion
	}
}