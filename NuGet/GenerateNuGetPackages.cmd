:: This batch file assumes that you have NuGet.exe in your path variables

ECHO OFF

:: Setup Package Directory
RD /s /q Packages
MKDIR Packages
CD Packages
SET PackageDir=%CD%

CD ../../src



:: Build IBFramework.Core
CALL :PackageProject IBFramework.Core

:: Build IBFramework.Data.Common
CALL :PackageProject IBFramework.Data.Common

:: Build IBFramework.Data.MySQL
CALL :PackageProject IBFramework.Data.MySQL

:: Build IBFramework.IoC
CALL :PackageProject IBFramework.IoC

:: Build IBFramework.Utility
CALL :PackageProject IBFramework.Utility

EXIT

:PackageProject

	:: Copy out the param
	SET ProjectName=%1
	
	:: Pack and relocate the nupkg file
	CD %ProjectName%
	nuget pack %ProjectName%.nuspec
	MOVE /y *.nupkg %PackageDir%
	
	:: Return to the original directory
	CD ../

EXIT /b