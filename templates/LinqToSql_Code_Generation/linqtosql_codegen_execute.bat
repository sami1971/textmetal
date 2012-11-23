@echo off

REM
REM	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

set BUILD_FLAVOR_DIR=Debug
set BUILD_TOOL_CFG=Debug
set SQL_METAL_EXE=C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\SqlMetal.exe

set PACKAGE_DIR=.\output
set PACKAGE_DIR_EXISTS=%PACKAGE_DIR%\nul

set L2S_DIR=%PACKAGE_DIR%\src\TextMetal.WebHostSample.Objects.Model\L2S
set L2S_DIR_EXISTS=%L2S_DIR%\nul


:pkgDir

IF NOT EXIST %PACKAGE_DIR_EXISTS% GOTO noPkgDir
goto delPkgDir

:noPkgDir
@echo Creating output directory...
mkdir "%PACKAGE_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
mkdir "%PACKAGE_DIR%\lib"
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


copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.pdb" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.dll" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.xml" "%PACKAGE_DIR%\lib\TextMetal\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "..\..\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.pdb" "%PACKAGE_DIR%\lib\TextMetal\."


echo *** linqtosql_codegen_execute ***
"..\..\src\TextMetal.HostImpl.ConsoleTool\bin\Debug\TextMetal.exe" ^
	-templatefile:"master_template.xml" ^
	-sourcefile:"Data Source=(local);User ID=TextMetalWebHostSampleLogin;Password=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH;Initial Catalog=TextMetalWebHostSample" ^
	-basedir:".\output\src" ^
	-sourcestrategy:"TextMetal.Framework.SourceModel.DatabaseSchema.Sql.SqlSchemaSourceStrategy, TextMetal.Framework.SourceModel" ^
	-strict:"true" ^
	-property:"ClrNamespace=TextMetal.WebHostSample.Objects.Model" ^
	-property:"ClrSuperType=Object" ^
	-property:"LinqToSqlDataContextRootNamespace=TextMetal.WebHostSample.Objects.Model.L2S" ^
	-property:"LinqToSqlTargetDataContextName=TxtMtlPrimaryDataContext" ^
	-property:"ConnectionType=System.Data.SqlClient.SqlConnection, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" ^
	-property:"DataSourceTag=net.sqlserver"
IF %ERRORLEVEL% NEQ 0 goto pkgError


IF NOT EXIST %L2S_DIR_EXISTS% GOTO noL2SDir
goto skipL2SDir

:noL2SDir
mkdir "%L2S_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
:skipL2SDir


echo *** linqtosql_dbmlgen_execute ***
"%SQL_METAL_EXE%" ^
	/language:"C#" ^
	/pluralize ^
	/namespace:"TextMetal.WebHostSample.Objects.Model.L2S" ^
	/context:"TxtMtlPrimaryDataContext" ^
	/dbml:".\output\src\TextMetal.WebHostSample.Objects.Model\L2S\TxtMtlPrimaryDataContext.dbml" ^
	/conn:"Data Source=(local);User ID=TextMetalWebHostSampleLogin;Password=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH;Initial Catalog=TextMetalWebHostSample"
IF %ERRORLEVEL% NEQ 0 goto pkgError


"%SQL_METAL_EXE%" ^
	/language:"C#" ^
	/code:".\output\src\TextMetal.WebHostSample.Objects.Model\L2S\TxtMtlPrimaryDataContext.designer.cs" ^
	".\output\src\TextMetal.WebHostSample.Objects.Model\L2S\TxtMtlPrimaryDataContext.dbml"
IF %ERRORLEVEL% NEQ 0 goto pkgError


goto pkgSuccess


:pkgError
echo An error occured.
pause > nul
goto :eof

:pkgSuccess
echo Completed successfully.
goto :eof
