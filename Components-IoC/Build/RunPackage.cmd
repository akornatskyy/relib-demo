@echo off

CALL Variables.cmd

msbuild ReusableLibrary.Demo.msbuild /t:Package /p:Build=%Build% /p:BuildType=Release
pause