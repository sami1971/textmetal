/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

using TextMetal.Common.Core;
using TextMetal.HostImpl.WindowsTool.Forms;

namespace TextMetal.HostImpl.WindowsTool
{
	internal static class Program
	{
		#region Fields/Constants

		private static AssemblyInformation assemblyInformation;

		#endregion

		#region Properties/Indexers/Events

		public static AssemblyInformation AssemblyInformation
		{
			get
			{
				return assemblyInformation;
			}
			private set
			{
				assemblyInformation = value;
			}
		}

		private static bool HookUnhandledExceptionEvents
		{
			get
			{
				return !Debugger.IsAttached &&
				       AppConfig.GetAppSetting<bool>("TextMetal.HostImpl.WindowsTool::HookUnhandledExceptionEvents");
			}
		}

		private static bool ShowSplashScreen
		{
			get
			{
				return AppConfig.GetAppSetting<bool>("TextMetal.HostImpl.WindowsTool::ShowSplashScreen");
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static int Main()
		{
			if (HookUnhandledExceptionEvents)
				return TryStartup();
			else
				return Startup();
		}

		private static void OnApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			ShowNestedExceptionsAndThrowBrickAtProcess(new ApplicationException("OnApplicationThreadException", e.Exception));
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			ShowNestedExceptionsAndThrowBrickAtProcess(new ApplicationException("OnUnhandledException", e.ExceptionObject as Exception));
		}

		internal static void ShowNestedExceptionsAndThrowBrickAtProcess(Exception e)
		{
			string message;

			message = Reflexion.GetErrors(e, 0);

			MessageBox.Show("A fatal error occured:\r\n" + message + "\r\nThe application will now terminate.", AssemblyInformation.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);

			Environment.Exit(-1);
		}

		private static int Startup()
		{
			AssemblyInformation = new AssemblyInformation(Assembly.GetEntryAssembly());

			AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

			if (HookUnhandledExceptionEvents)
			{
				AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
				Application.ThreadException += OnApplicationThreadException;
			}

			Control.CheckForIllegalCrossThreadCalls = true;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (ShowSplashScreen)
				Application.Run(new SplashApplicationContext(new MainForm(), new SplashForm()));
			else
				Application.Run(new MainForm());

			return 0;
		}

		private static int TryStartup()
		{
			try
			{
				return Startup();
			}
			catch (Exception ex)
			{
				ShowNestedExceptionsAndThrowBrickAtProcess(new ApplicationException("Main", ex));
			}

			return -1;
		}

		#endregion
	}
}