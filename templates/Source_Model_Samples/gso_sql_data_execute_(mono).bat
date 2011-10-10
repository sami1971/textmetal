@echo off

REM
REM	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

echo *** sql_data_execute (mono) ***
"C:\Program Files\Mono-2.10.5\bin\mono.exe" "..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"empty_template.xml" ^
	-sourcefile:"gso_sql_data_source.xml" ^
	-basedir:".\output" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.Primative.SqlDataSourceStrategy, TextMetal.Core" ^
	-strict:"true" ^
	-debug:"false" ^
	-property:"ConnectionType=System.Data.SqlClient.SqlConnection, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" ^
	-property:"ConnectionString=Data Source=(local);User ID=TextMetalWebHostSampleLogin;Password=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH;Initial Catalog=TextMetalWebHostSample" ^
	-property:"GetSchemaOnly=true"
IF %ERRORLEVEL% NEQ 0 goto pkgError

	
goto pkgSuccess


:pkgError
echo An error occured.
pause > nul
goto :eof

:pkgSuccess
echo Completed successfully.
goto :eof
