/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Reflection;
using System.Runtime.InteropServices;

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyCompany("Daniel Bullington")]
[assembly: AssemblyProduct("TextMetal")]
[assembly: AssemblyCopyright("©2002-2012 Daniel Bullington")]
[assembly: AssemblyDescription("Distributed under the MIT license:\r\nhttp://www.opensource.org/licenses/mit-license.php\r\n\r\nThis project was forked from:\r\nhttp://code.google.com/p/softwareishardwork/")]
[assembly: AssemblyTrademark("π")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("4.4.1.*")]
[assembly: AssemblyFileVersion("4.4.1.0")]
[assembly: AssemblyInformationalVersion("2012.07.17")]
[assembly: AssemblyDelaySign(false)]
[assembly: ComVisible(false)]

#if DEFINE_CLR_VERSION_20
// this causes NUnit to die in CLR 4.0

[assembly: AllowPartiallyTrustedCallers]
#endif