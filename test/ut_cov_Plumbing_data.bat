@echo off

set NCOVER_EXE=..\lib\PrivateBuilt\TestingFramework.Profiler.Console.exe
set NUNIT_EXE=..\lib\PrivateBuilt\TestingFramework.Runner.Console.exe
set ASSEMBLY_LIST=TextMetal.Plumbing

echo NCOVER_EXE=%NCOVER_EXE%
echo NUNIT_EXE=%NUNIT_EXE%
echo ASSEMBLY_LIST=%ASSEMBLY_LIST%

"%NCOVER_EXE%" "%NUNIT_EXE%" "..\src\TextMetal.Plumbing.UnitTests\UnitTesting.nunit" /run:"TextMetal.Plumbing.UnitTests.data" //a "%ASSEMBLY_LIST%"
IF %ERRORLEVEL% NEQ 0 GOTO testCovError

GOTO testCovSuccess


:testCovError
echo An error occured.
pause > nul
GOTO :eof

:testCovSuccess
echo Completed successfully.
GOTO :eof
