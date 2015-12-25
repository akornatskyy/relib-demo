@echo off

CALL Variables.cmd

msbuild ReusableLibrary.Demo.msbuild /t:Clean /p:BuildType=Release
msbuild ReusableLibrary.Demo.msbuild /t:Clean /p:BuildType=Debug
pause