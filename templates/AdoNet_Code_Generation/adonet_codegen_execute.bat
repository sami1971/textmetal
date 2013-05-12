@echo off

REM
REM	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

set BUILD_FLAVOR_DIR=Debug
set BUILD_TOOL_CFG=Debug

set PACKAGE_DIR=.\output
set SRC_DIR=%PACKAGE_DIR%\src
set LIB_DIR=%PACKAGE_DIR%\lib
set PACKAGE_DIR_EXISTS=%PACKAGE_DIR%\nul

set CLR_NAMESPACE=TextMetal.HostImpl.AspNetSample.Objects.Model
set ADO_NET_CONNECTION_STRING=Data Source=(local);User ID=TextMetalWebHostSampleLogin;Password=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH;Initial Catalog=TextMetalWebHostSample

:pkgDir

IF NOT EXIST %PACKAGE_DIR_EXISTS% GOTO noPkgDir
goto delPkgDir

:noPkgDir
@echo Creating output directory...
mkdir "%PACKAGE_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%LIB_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%LIB_DIR%\SQLite"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%LIB_DIR%\PrivateBuilt"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%LIB_DIR%\TextMetal"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%SRC_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild

:delPkgDir
@echo Cleaning output directory...
del "%PACKAGE_DIR%\*.*" /Q /S
rem IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild


:pkgBuild

copy "..\..\lib\PrivateBuilt\*.*"  %LIB_DIR%\PrivateBuilt\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\lib\SQLite\x64\*.*"  %LIB_DIR%\SQLite\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.dll" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.xml" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.pdb" %LIB_DIR%\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


echo *** adonet_codegen_execute ***
"..\..\src\TextMetal.HostImpl.ConsoleTool\bin\Debug\TextMetal.exe" ^
	-templatefile:"master_template.xml" ^
	-sourcefile:"%ADO_NET_CONNECTION_STRING%" ^
	-basedir:"%SRC_DIR%" ^
	-sourcestrategy:"TextMetal.Framework.SourceModel.DatabaseSchema.Sql.SqlSchemaSourceStrategy, TextMetal.Framework.SourceModel" ^
	-strict:"true" ^
	-property:"ClrNamespace=%CLR_NAMESPACE%" ^
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
