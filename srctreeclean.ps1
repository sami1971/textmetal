#
#	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls 
$root = [System.Environment]::CurrentDirectory

$filesToKill = Get-ChildItem $root -Recurse -Include "bin", "obj", "clientbin", "debug", "release", "output", "*.suo", "*.cache", "*.resharper", "*.user", "*.visualstate.xml", "*.pidb", "*.userprefs", "_ReSharper.*" -Force

foreach ($fileToKill in $filesToKill)
{
	"Deleting: " + $fileToKill.FullName
	del $fileToKill.FullName -Recurse -Force
}