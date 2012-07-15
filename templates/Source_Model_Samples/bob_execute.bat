@echo off

REM
REM	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

echo *** bob_execute ***
"..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"bob_master_template.xml" ^
	-sourcefile:"*" ^
	-basedir:".\output" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.Primative.NullSourceStrategy, TextMetal.Core" ^
	-strict:"true" ^
	-debug:"false"
IF %ERRORLEVEL% NEQ 0 goto pkgError

	
goto pkgSuccess


:pkgError
echo An error occured.
pause > nul
goto :eof

:pkgSuccess
echo Completed successfully.
pause > nul
goto :eof
