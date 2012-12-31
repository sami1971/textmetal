/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Reflection;

namespace TextMetal.Common.Core
{
	/// <summary>
	/// Provides easy access assembly related attribute data.
	/// </summary>
	public class AssemblyInformation
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the AssemblyInformation class.
		/// </summary>
		/// <param name="assembly"> The target assembly to examine for information. </param>
		public AssemblyInformation(Assembly assembly)
		{
			AssemblyTitleAttribute aTiA;
			//AssemblyVersionAttribute ava;
			AssemblyDescriptionAttribute ada;
			AssemblyProductAttribute apa;
			AssemblyCopyrightAttribute aCopA;
			AssemblyCompanyAttribute aComA;
			AssemblyConfigurationAttribute aConA;
			AssemblyFileVersionAttribute afva;
			AssemblyInformationalVersionAttribute aiva;
			AssemblyTrademarkAttribute aTrA;

			if ((object)assembly == null)
				throw new ArgumentNullException("assembly");

			aTiA = Reflexion.GetOneAttribute<AssemblyTitleAttribute>(assembly);

			if ((object)aTiA != null)
				this.title = aTiA.Title;

			//ava = Reflexion.GetOneAttribute<AssemblyVersionAttribute>(assembly);

			//if ((object)ava != null)
			//    this.ava = ava.ava;

			this.assemblyVersion = assembly.GetName().Version.ToString();

			ada = Reflexion.GetOneAttribute<AssemblyDescriptionAttribute>(assembly);

			if ((object)ada != null)
				this.description = ada.Description;

			apa = Reflexion.GetOneAttribute<AssemblyProductAttribute>(assembly);

			if ((object)apa != null)
				this.product = apa.Product;

			aCopA = Reflexion.GetOneAttribute<AssemblyCopyrightAttribute>(assembly);

			if ((object)aCopA != null)
				this.copyright = aCopA.Copyright;

			aComA = Reflexion.GetOneAttribute<AssemblyCompanyAttribute>(assembly);

			if ((object)aComA != null)
				this.company = aComA.Company;

			aConA = Reflexion.GetOneAttribute<AssemblyConfigurationAttribute>(assembly);

			if ((object)aConA != null)
				this.configuration = aConA.Configuration;

			afva = Reflexion.GetOneAttribute<AssemblyFileVersionAttribute>(assembly);

			if ((object)afva != null)
				this.win32FileVersion = afva.Version;

			aiva = Reflexion.GetOneAttribute<AssemblyInformationalVersionAttribute>(assembly);

			if ((object)aiva != null)
				this.informationalVersion = aiva.InformationalVersion;

			aTrA = Reflexion.GetOneAttribute<AssemblyTrademarkAttribute>(assembly);

			if ((object)aTrA != null)
				this.trademark = aTrA.Trademark;
		}

		#endregion

		#region Fields/Constants

		private readonly string assemblyVersion;
		private readonly string company;
		private readonly string configuration;
		private readonly string copyright;
		private readonly string description;
		private readonly string informationalVersion;
		private readonly string product;
		private readonly string title;
		private readonly string trademark;
		private readonly string win32FileVersion;

		#endregion

		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the assembly version.
		/// </summary>
		public string AssemblyVersion
		{
			get
			{
				return this.assemblyVersion;
			}
		}

		/// <summary>
		/// Gets the assembly company.
		/// </summary>
		public string Company
		{
			get
			{
				return this.company;
			}
		}

		/// <summary>
		/// Gets the assembly configuration.
		/// </summary>
		public string Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		/// <summary>
		/// Gets the assembly copyright.
		/// </summary>
		public string Copyright
		{
			get
			{
				return this.copyright;
			}
		}

		/// <summary>
		/// Gets the assembly description.
		/// </summary>
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>
		/// Gets the assembly informational version.
		/// </summary>
		public string InformationalVersion
		{
			get
			{
				return this.informationalVersion;
			}
		}

		/// <summary>
		/// Gets the assembly product.
		/// </summary>
		public string Product
		{
			get
			{
				return this.product;
			}
		}

		/// <summary>
		/// Gets the assembly title.
		/// </summary>
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		/// <summary>
		/// Gets the assembly trademark.
		/// </summary>
		public string Trademark
		{
			get
			{
				return this.trademark;
			}
		}

		/// <summary>
		/// Gets the assembly Win32 file version.
		/// </summary>
		public string Win32FileVersion
		{
			get
			{
				return this.win32FileVersion;
			}
		}

		#endregion
	}
}