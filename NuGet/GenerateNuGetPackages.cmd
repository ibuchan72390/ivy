:: This batch file assumes that you have NuGet.exe in your path variables

:: Package version comes directly from the project.json file!
:: If you need to update the package version, simply update the version at the top of the file.

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

:: Build IBFramework.TestUtilities
CALL :PackageProject test\IBFramework.TestUtilities

PAUSE
EXIT

:PackageProject

	:: Copy out the param
	SET ProjectName=%1
	SET Config=%ProjectName%.nuspec
	
	:: Pack and relocate the nupkg file
	CD %ProjectName%

	dotnet pack
		
	CD Bin\Debug

	
	MOVE /y *.nupkg %PackageDir%

	:: Return to the original directory
	CD ../../../

EXIT /b