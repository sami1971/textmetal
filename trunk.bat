@echo off

REM
REM	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

cd "."
"C:\Windows\System32\cmd.exe" /E:ON /V:ON /T:0E /K "setenv.cmd /Release /x64 /win7"

