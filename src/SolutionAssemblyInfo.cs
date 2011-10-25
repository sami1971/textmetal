/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("Daniel Bullington")]
[assembly: AssemblyProduct("TextMetal")]
[assembly: AssemblyCopyright("©2002-2011 Daniel Bullington")]
[assembly: AssemblyDescription("Distributed under the MIT license:\r\nhttp://www.opensource.org/licenses/mit-license.php\r\n\r\nThis project was forked from:\r\nhttp://code.google.com/p/softwareishardwork/")]
[assembly: AssemblyTrademark("π")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("4.3.0.*")]
[assembly: AssemblyFileVersion("4.3.0.0")]
[assembly: AssemblyInformationalVersion("2011.10.25")]
[assembly: AssemblyDelaySign(false)]
[assembly: ComVisible(false)]

#if !CLR_40
// this causes NUnit to die in CLR 4.0

[assembly: AllowPartiallyTrustedCallers]
#endif