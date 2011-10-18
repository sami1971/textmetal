//------------------------------------------------------------------------------
// <copyright company="Microsoft Corporation">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

// FxCop enforces that we add this attribute

[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

namespace TextMetal.ConnectionDialogApi
{
	internal sealed class NativeMethods
	{
		#region Constructors/Destructors

		private NativeMethods()
		{
		}

		#endregion

		#region Fields/Constants

		internal const int
			CLSCTX_INPROC_SERVER = 1;

		internal const int
			DBPROMPTOPTIONS_DISABLE_PROVIDER_SELECTION = 0x10;

		internal const int
			DBPROMPTOPTIONS_PROPERTYSHEET = 0x02;

		internal const int
			DBSOURCETYPE_DATASOURCE_MDP = 3;

		internal const int
			DBSOURCETYPE_DATASOURCE_TDP = 1;

		internal const int
			DB_E_CANCELED = unchecked((int)0x80040E4E);

		internal const int
			HELPINFO_WINDOW = 0x0001;

		internal const int KEY_QUERY_VALUE = 0x1;
		internal const int KEY_WOW64_32KEY = 0x0200;
		internal const int KEY_WOW64_64KEY = 0x0100;

		internal const int
			SC_CONTEXTHELP = 0xF180;

		internal const ushort
			SQL_DRIVER_PROMPT = 2;

		// ODBC return values
		internal const short
			SQL_NO_DATA = 100;

		internal const int
			WM_CONTEXTMENU = 0x007B;

		internal const int
			WM_HELP = 0x0053;

		internal const int
			WM_SETFOCUS = 0x0007;

		internal const int
			WM_SYSCOMMAND = 0x0112;

		internal static readonly UIntPtr HKEY_LOCAL_MACHINE = new UIntPtr(((uint)0x80000002));
		internal static Guid CLSID_DataLinks = new Guid("2206CDB2-19C1-11d1-89E0-00C04FD7A829");
		internal static Guid CLSID_MSDASQL_ENUMERATOR = new Guid("C8B522CD-5CF3-11ce-ADE5-00AA0044773D");
		internal static Guid CLSID_OLEDB_ENUMERATOR = new Guid("C8B522D0-5CF3-11ce-ADE5-00AA0044773D");
		internal static Guid IID_IUnknown = new Guid("00000000-0000-0000-c000-000000000046");

		#endregion

		#region Methods/Operators

		internal static short HIWORD(int dwValue)
		{
			return (short)((dwValue >> 16) & 0xffff);
		}

		[DllImport("kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWow64Process(IntPtr hProcess, out bool pIsWow64);

		internal static short LOWORD(int dwValue)
		{
			return (short)(dwValue & 0xffff);
		}

		[DllImport("advapi32")]
		internal static extern uint RegCloseKey(UIntPtr hKey);

		[DllImport("advapi32.dll")]
		internal static extern int RegEnumValue(UIntPtr hkey, uint index, StringBuilder lpValueName, ref uint lpcbValueName, IntPtr reserved, IntPtr lpType, IntPtr lpData, IntPtr lpcbData);

		[DllImport("advapi32")]
		internal static extern int RegOpenKeyEx(UIntPtr hKey, string lpSubKey, int ulOptions, int samDesired, out UIntPtr phkResult);

		[DllImport("advapi32.dll")]
		internal static extern int RegQueryInfoKey(UIntPtr hkey, byte[] lpClass, IntPtr lpcbClass, IntPtr lpReserved, out uint lpcSubKeys, IntPtr lpcbMaxSubKeyLen, IntPtr lpcbMaxClassLen, out uint lpcValues, IntPtr lpcbMaxValueNameLen, IntPtr lpcbMaxValueLen, IntPtr lpcbSecurityDescriptor, IntPtr lpftLastWriteTime);

		[DllImport("advapi32")]
		internal static extern int RegQueryValueEx(UIntPtr hKey, string lpValueName, uint lpReserved, ref uint lpType, IntPtr lpData, ref int lpchData);

		[DllImport("odbc32.dll")]
		internal static extern short SQLAllocConnect(IntPtr EnvironmentHandle, out IntPtr ConnectionHandle);

		[DllImport("odbc32.dll")]
		internal static extern short SQLAllocEnv(out IntPtr EnvironmentHandle);

		[DllImport("odbc32.dll")]
		internal static extern short SQLDisconnect(IntPtr ConnectionHandle);

		[DllImport("odbc32.dll", EntryPoint = "SQLDriverConnectW", CharSet = CharSet.Unicode)]
		internal static extern short SQLDriverConnect(IntPtr hdbc, IntPtr hwnd, string szConnStrIn, short cbConnStrIn, StringBuilder szConnStrOut, short cbConnStrOutMax, out short pcbConnStrOut, ushort fDriverCompletion);

		[DllImport("odbc32.dll")]
		internal static extern short SQLFreeConnect(IntPtr ConnectionHandle);

		[DllImport("odbc32.dll")]
		internal static extern short SQLFreeEnv(IntPtr EnvironmentHandle);

		[DllImport("odbccp32.dll", CharSet = CharSet.Unicode)]
		internal static extern bool SQLGetInstalledDrivers(char[] lpszBuf, int cbBufMax, ref int pcbBufOut);

		[DllImport("odbccp32.dll", CharSet = CharSet.Unicode)]
		internal static extern int SQLGetPrivateProfileString(string lpszSection, string lpszEntry, string lpszDefault, StringBuilder RetBuffer, int cbRetBuffer, string lpszFilename);

		internal static bool SQL_SUCCEEDED(short rc)
		{
			return (((rc) & (~1)) == 0);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		[StructLayout(LayoutKind.Sequential)]
		internal class HELPINFO
		{
			public int cbSize = Marshal.SizeOf(typeof(HELPINFO));
			public int iContextType;
			public int iCtrlId;
			public IntPtr hItemHandle;
			public int dwContextId;
			public POINT MousePos;
		}

		[ComImport]
		[Guid("2206CCB0-19C1-11D1-89E0-00C04FD7A829")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IDBPromptInitialize
		{
			void PromptDataSource(
				[In] [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
				[In] IntPtr hwndParent,
				[In] [MarshalAs(UnmanagedType.U4)] int dwPromptOptions,
				[In] [MarshalAs(UnmanagedType.U4)] int cSourceTypeFilter,
				[In] IntPtr rgSourceTypeFilter,
				[In] [MarshalAs(UnmanagedType.LPWStr)] string pwszszzProviderFilter,
				[In] ref Guid riid,
				[In] [Out] [MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);

			void Unused_PromptFileName();
		}

		[ComImport]
		[Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface IDataInitialize
		{
			void GetDataSource(
				[In] [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter,
				[In] [MarshalAs(UnmanagedType.U4)] int dwClsCtx,
				[In] [MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString,
				[In] ref Guid riid,
				[In] [Out] [MarshalAs(UnmanagedType.IUnknown)] ref object ppDataSource);

			void GetInitializationString(
				[In] [MarshalAs(UnmanagedType.IUnknown)] object pDataSource,
				[In] [MarshalAs(UnmanagedType.I1)] bool fIncludePassword,
				[Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppwszInitString);

			void Unused_CreateDBInstance();

			void Unused_CreateDBInstanceEx();

			void Unused_LoadStringFromStorage();

			void Unused_WriteStringToStorage();
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct POINT
		{
			public int x;
			public int y;
		}

		#endregion

		// Used to check if OS is 64 bits
	}
}