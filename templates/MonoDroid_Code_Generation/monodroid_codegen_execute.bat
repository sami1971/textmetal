@echo off

REM
REM	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

echo *** monodroid_codegen_execute - profile ***
"..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"profile_master_template.xml" ^
	-sourcefile:"profile_linear_text_source.txt" ^
	-basedir:".\output\src\profile" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.Primative.TextSourceStrategy, TextMetal.Core" ^
	-strict:"true" ^
	-debug:"false"
IF %ERRORLEVEL% NEQ 0 goto pkgError


echo *** monodroid_codegen_execute - estimate ***
"..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"estimate_master_template.xml" ^
	-sourcefile:"estimate_linear_text_source.txt" ^
	-basedir:".\output\src\estimate" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.Primative.TextSourceStrategy, TextMetal.Core" ^
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
