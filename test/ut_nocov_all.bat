@echo off

set NUNIT_EXE=..\lib\PrivateBuilt\TestingFramework.Runner.Console.exe
set ASSEMBLY_LIST=TextMetal.Core

echo NUNIT_EXE=%NUNIT_EXE%
echo ASSEMBLY_LIST=%ASSEMBLY_LIST%

"%NUNIT_EXE%" "..\src\TextMetal.Core.UnitTests\UnitTesting.nunit"
IF %ERRORLEVEL% NEQ 0 GOTO testCovError

GOTO testCovSuccess


:testCovError
echo An error occured.
pause > nul
GOTO :eof

:testCovSuccess
echo Completed successfully.
GOTO :eof
