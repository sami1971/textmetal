@echo off

set NCOVER_EXE=..\lib\PrivateBuilt\TestingFramework.Profiler.Console.exe
set NUNIT_EXE=..\lib\PrivateBuilt\TestingFramework.Runner.Console.exe
set ASSEMBLY_LIST=TextMetal.Core

echo NCOVER_EXE=%NCOVER_EXE%
echo NUNIT_EXE=%NUNIT_EXE%
echo ASSEMBLY_LIST=%ASSEMBLY_LIST%

"%NCOVER_EXE%" "%NUNIT_EXE%" "..\src\TextMetal.Core.UnitTests\UnitTesting.nunit" /run:"TextMetal.Core.UnitTests.AssociativeModel" //a "%ASSEMBLY_LIST%"
IF %ERRORLEVEL% NEQ 0 GOTO testCovError

GOTO testCovSuccess


:testCovError
echo An error occured.
pause > nul
GOTO :eof

:testCovSuccess
echo Completed successfully.
GOTO :eof
