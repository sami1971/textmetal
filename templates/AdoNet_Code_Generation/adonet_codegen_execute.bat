@echo off

REM
REM	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

set BUILD_FLAVOR_DIR=Debug
set BUILD_TOOL_CFG=Debug

set PACKAGE_DIR=.\output
set PACKAGE_DIR_EXISTS=%PACKAGE_DIR%\nul

:pkgDir

IF NOT EXIST %PACKAGE_DIR_EXISTS% GOTO noPkgDir
goto delPkgDir

:noPkgDir
@echo Creating output directory...
mkdir "%PACKAGE_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\lib"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\lib\SQLite"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\lib\PrivateBuilt"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\lib\TextMetal"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\src"
IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild

:delPkgDir
@echo Cleaning output directory...
del "%PACKAGE_DIR%\*.*" /Q /S
rem IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild


:pkgBuild

copy "..\..\lib\PrivateBuilt\*.*"  "%PACKAGE_DIR%\lib\PrivateBuilt\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\lib\SQLite\x64\*.*"  "%PACKAGE_DIR%\lib\SQLite\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Core.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Core.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Core.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Plumbing\bin\%BUILD_FLAVOR_DIR%\TextMetal.Plumbing.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Plumbing\bin\%BUILD_FLAVOR_DIR%\TextMetal.Plumbing.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Plumbing\bin\%BUILD_FLAVOR_DIR%\TextMetal.Plumbing.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Console\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Console\bin\%BUILD_FLAVOR_DIR%\TextMetal.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Console\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe.config" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Console\bin\%BUILD_FLAVOR_DIR%\TextMetal.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Hosting.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Web.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Hosting.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Web.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Hosting.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Web.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Hosting.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Tool.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Hosting.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Tool.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Hosting.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.Hosting.Tool.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


echo *** adonet_codegen_execute ***
"..\..\src\TextMetal.Console\bin\Debug\TextMetal.exe" ^
	-templatefile:"master_template.xml" ^
	-sourcefile:"Data Source=(local);User ID=TextMetalWebHostSampleLogin;Password=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH;Initial Catalog=TextMetalWebHostSample" ^
	-basedir:".\output\src" ^
	-sourcestrategy:"TextMetal.Core.SourceModel.DatabaseSchema.Sql.SqlSchemaSourceStrategy, TextMetal.Core" ^
	-strict:"true" ^
	-property:"ClrNamespace=TextMetal.WebHostSample.Objects.Model" ^
	-property:"ClrSuperType=Object" ^
	-property:"ConnectionType=System.Data.SqlClient.SqlConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" ^
	-property:"DataSourceTag=net.sqlserver"
IF %ERRORLEVEL% NEQ 0 goto pkgError


goto pkgSuccess


:pkgError
echo An error occured.
pause > nul
goto :eof

:pkgSuccess
echo Completed successfully.
goto :eof
