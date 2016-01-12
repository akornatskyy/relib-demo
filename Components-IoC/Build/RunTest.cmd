@echo off

CALL Variables.cmd

msbuild ReusableLibrary.Demo.msbuild /t:Test /p:Build=%Build% /p:BuildType=Release
pause