@echo off

REM
REM	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

set PACKAGE_DIR=.\pkg
set PACKAGE_DIR_EXISTS=%PACKAGE_DIR%\nul
set BUILD_EXE=C:\WINDOWS\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe

cd "."

rem if "%1" == "" goto pkgUsage
if "%1" == "" goto flavorRelease
if /i %1 == -release       goto flavorRelease
if /i %1 == -debug     goto flavorDebug
goto pkgUsage



:flavorRelease

@echo Using [Release] build flavor directory...
set BUILD_FLAVOR_DIR=Release
set BUILD_TOOL_CFG=Release
goto pkgDir


:flavorDebug

@echo Using [Debug] build flavor directory...
set BUILD_FLAVOR_DIR=Debug
set BUILD_TOOL_CFG=Debug
goto pkgDir





:pkgDir

IF NOT EXIST %PACKAGE_DIR_EXISTS% GOTO noPkgDir
goto delPkgDir

:noPkgDir
@echo Creating pkg directory...
mkdir "%PACKAGE_DIR%"
IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild

:delPkgDir
@echo Cleaning pkg directory...
del "%PACKAGE_DIR%\*.*" /Q /S
rem IF %ERRORLEVEL% NEQ 0 goto pkgError
goto pkgBuild





:pkgBuild

@echo BUILD_EXE=%BUILD_EXE%
@echo BUILD_TOOL_CFG=%BUILD_TOOL_CFG%

"%BUILD_EXE%" ".\src\TextMetal.sln" /t:clean /p:Configuration=%BUILD_TOOL_CFG%
IF %ERRORLEVEL% NEQ 0 goto pkgError

"%BUILD_EXE%" ".\src\TextMetal.sln" /t:build /p:Configuration=%BUILD_TOOL_CFG%
IF %ERRORLEVEL% NEQ 0 goto pkgError



:pkgCopy

@echo BUILD_FLAVOR_DIR=%BUILD_FLAVOR_DIR%

copy ".\lib\AspNetMvc\*.*" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\lib\IronRuby\*.*" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\lib\PrivateBuilt\*.*" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\lib\SQLite\x64\*.*" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError




copy ".\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Core.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Data\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Data.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Expressions\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Expressions.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Solder\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Solder.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Common.Xml\bin\%BUILD_FLAVOR_DIR%\TextMetal.Common.Xml.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.AssociativeModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.AssociativeModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.Core\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.Core.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.DebuggerProfilerModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.DebuggerProfilerModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.ExpressionModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.ExpressionModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.HostingModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.HostingModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.InputOutputModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.InputOutputModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.SortModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SortModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.SourceModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.SourceModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.Framework.TemplateModel\bin\%BUILD_FLAVOR_DIR%\TextMetal.Framework.TemplateModel.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\TextMetal.exe.config" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.HostImpl.VsIdeConv.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\VisualStudioConversion.exe" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.VsIdeConv.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\VisualStudioConversion.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.VsIdeConv.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\VisualStudioConversion.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.VsIdeConv.ConsoleTool\bin\%BUILD_FLAVOR_DIR%\VisualStudioConversion.exe.config" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.HostImpl.WindowsTool\bin\%BUILD_FLAVOR_DIR%\TextMetalStudio.exe" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.WindowsTool\bin\%BUILD_FLAVOR_DIR%\TextMetalStudio.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.WindowsTool\bin\%BUILD_FLAVOR_DIR%\TextMetalStudio.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.WindowsTool\bin\%BUILD_FLAVOR_DIR%\TextMetalStudio.exe.config" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.Tool\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Tool.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError



copy ".\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.dll" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.xml" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy ".\src\TextMetal.HostImpl.Web\bin\%BUILD_FLAVOR_DIR%\TextMetal.HostImpl.Web.pdb" "%PACKAGE_DIR%\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


goto pkgSuccess




:pkgError
echo An error occured.
goto :eof

:pkgSuccess
echo Completed successfully.
goto :eof


:pkgUsage
echo Error in script usage. The correct usage is:
echo     %0 [flavor]
echo where [flavor] is: -release ^| -debug
echo:
echo For example:
echo     %0 -debug
goto :eof