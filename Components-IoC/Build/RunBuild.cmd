@echo off

CALL Variables.cmd

msbuild ReusableLibrary.Demo.msbuild /t:Build;Test;Integration;Package /p:CCNetNumericLabel=%Build% /p:BuildType=Release
pause