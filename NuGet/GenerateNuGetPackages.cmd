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


:: Standard Projects

:: Build Amazon Elastic Transcoder
CALL :CreateProjectPackage Amazon\ElasticTranscoder\Ivy.Amazon.ElasticTranscoder

:: Build Amazon S3
CALL :CreateProjectPackage Amazon\S3\Ivy.Amazon.S3

:: Build Auth0.Core
CALL :PackageProject Auth0\Ivy.Auth0.Core

:: Build Auth0.Authorization
CALL :CreateProjectPackage Auth0\Authorization\Ivy.Auth0.Authorization

:: Build Auth0.Management
CALL :CreateProjectPackage Auth0\Management\Ivy.Auth0.Management

:: Build Auth0.Web
CALL :CreateProjectPackage Auth0\Web\Ivy.Auth0.Web

:: Build Caching
CALL :CreateProjectPackage Caching\Ivy.Caching

:: Build IoC
CALL :CreateProjectPackage IoC\Ivy.IoC

:: Build MailChimp
CALL :CreateProjectPackage MailChimp\Ivy.MailChimp

:: Build PayPal
CALL :CreateProjectPackage PayPal\Ivy.PayPal

:: Build TestUtilities
CALL :PackageProject Test\Ivy.TestUtilities

:: Build Transformer
CALL :CreateProjectPackage Transformer\Ivy.Transformer

:: Build Utility
CALL :CreateProjectPackage Utility\Ivy.Utility

:: Build Validation (Core Only)
CALL :PackageProject Validation\Ivy.Validation.Core

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

	dotnet pack --include-source
		
	CD Bin\Debug

	
	MOVE /y *.nupkg %PackageDir%

	:: Return to the original directory
	CD %OrigDir%

EXIT /b