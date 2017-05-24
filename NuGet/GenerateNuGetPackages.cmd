:: This batch file assumes that you have NuGet.exe in your path variables

:: Package version comes directly from the project.json file!
:: If you need to update the package version, simply update the version at the top of the file.

REM ECHO OFF

:: Setup Package Directory
RD /s /q Packages
MKDIR Packages
CD Packages
SET PackageDir=%CD%

CD ../../src



:: Build IBFramework.Core
::CALL :PackageProjects IBFramework.Core

:: Build IBFramework.Data.Common
::CALL :PackageProjects IBFramework.Data.Common

:: Build IBFramework.Data.MySQL
::CALL :PackageProjects IBFramework.Data.MySQL

:: Build IBFramework.IoC
::CALL :PackageProjects IBFramework.IoC

:: Build IBFramework.Utility
::CALL :PackageProjects IBFramework.Utility

:: Build IBFramework.TestUtilities
::CALL :PackageProjects test\IBFramework.TestUtilities


:: Standard Projects

:: Build Amazon S3
CALL :CreateProjectPackage Amazon\S3\Ivy.Amazon.S3

:: Build Auth0
CALL :CreateProjectPackage Auth0\Ivy.Auth0

:: Build Caching
CALL :CreateProjectPackage Caching\Ivy.Caching

:: Build IoC
CALL :CreateProjectPackage IoC\Ivy.IoC

:: Build MailChimp
CALL :CreateProjectPackage MailChimp\Ivy.MailChimp

:: Build TestUtilities
CALL :CreateProjectPackage Test\Ivy.TestUtilities

:: Build Transformer
CALL :CreateProjectPackage Transformer\Ivy.Transformer

:: Build Utility
CALL :CreateProjectPackage Utility\Ivy.Utility

:: Build Validation
CALL :CreateProjectPackage Validation\Ivy.Validation

:: Build Web
CALL :CreateProjectPackage Web\Ivy.Web




:: Data Projects

:: Core Data
CALL :PackageProject Data\Ivy.Data.Core

:: Common Data
CALL :PackageProject Data\Ivy.Data.Common

:: Common Data IoC
CALL :PackageProject Data\Ivy.Data.Common.IoC

:: MySQL Data
CALL :PackageProject Data\MySQL\Ivy.Data.MySQL

:: MySQL IoC
CALL :PackageProject Data\MySQL\Ivy.Data.MySQL.IoC



PAUSE
EXIT

:CreateProjectPackage

	SET currentProj=%1
	SET coreProject=%currentProj%.Core	
	SET iocProject=%currentProj%.IoC
	
	:: Build Core
	CALL :PackageProject %coreProject%
	
	:: Build Project
	CALL :PackageProject %currentProj%
	
	:: Build IoC
	CALL :PackageProject %iocProject%

EXIT /b

:PackageProject

	SET OrigDir=%CD%

	:: Copy out the param
	SET ProjectName=%1
	
	:: Pack and relocate the nupkg file
	CD %ProjectName%

	dotnet pack
		
	CD Bin\Debug

	
	MOVE /y *.nupkg %PackageDir%

	:: Return to the original directory
	CD %OrigDir%

EXIT /b