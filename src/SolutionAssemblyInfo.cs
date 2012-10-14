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
[assembly: AssemblyProduct("textmetal.com")]
[assembly: AssemblyCopyright("©2002-2012 Daniel Bullington")]
[assembly: AssemblyDescription("Distributed under the MIT license:\r\nhttp://www.opensource.org/licenses/mit-license.php")]
[assembly: AssemblyTrademark("π")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("4.4.4.*")]
[assembly: AssemblyFileVersion("4.4.4.0")]
[assembly: AssemblyInformationalVersion("2012.10.14")]
[assembly: AssemblyDelaySign(false)]
[assembly: ComVisible(false)]

#if DEFINE_CLR_VERSION_20
// this causes NUnit to die in CLR 4.0

[assembly: AllowPartiallyTrustedCallers]
#endif