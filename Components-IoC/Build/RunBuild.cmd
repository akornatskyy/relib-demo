@echo off

CALL Variables.cmd

msbuild ReusableLibrary.Demo.msbuild /t:Prepare;Build /p:Build=%Build% /p:BuildType=Release
pause