@echo off

REM
REM	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

echo *** reflection_execute (mono) ***
"C:\Program Files\Mono-2.10.5\bin\mono.exe" "..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"empty_template.xml" ^
	-sourcefile:"..\..\src\TextMetal.Console\bin\Debug\TextMetal.Core.dll" ^
	-basedir:".\output" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.Primative.ReflectionSourceStrategy, TextMetal.Core" ^
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
goto :eof
