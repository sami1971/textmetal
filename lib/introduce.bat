@echo off

copy "C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll" ".\AspNetMvc\."
IF %ERRORLEVEL% NEQ 0 goto pkgError

copy "C:\Program Files (x86)\Reference Assemblies\Microsoft\WindowsPowerShell\v1.0\System.Management.Automation.dll" ".\PowerShell\."
IF %ERRORLEVEL% NEQ 0 goto pkgError


goto pkgSuccess




:pkgError
echo An error occured.
goto :eof

:pkgSuccess
echo Completed successfully.
goto :eof