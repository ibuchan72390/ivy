:: This batch file assumes that you have NuGet.exe in your path variables

:: Package version comes directly from the project.json file!
:: If you need to update the package version, simply update the version at the top of the file.

REM ECHO OFF


SET ScriptDir=%CD%/Scripts

:: Setup Package Directory
CALL %ScriptDir%/ClearAndConfigurePackages.cmd

CD ../

:: Standard Projects

:: Build Amazon Elastic Transcoder
CALL NuGet\GenerateAmazonElasticTranscoder.cmd true

:: Build Amazon S3
CALL NuGet\GenerateAmazonS3.cmd true

:: Build Auth0.Core
CALL NuGet\GenerateAuth0.cmd true

:: Build Caching
CALL NuGet\GenerateCaching.cmd true

:: Build IoC
CALL NuGet\GenerateIoC.cmd true

:: Build Mailing.Core
CALL NuGet\GenerateMailing.cmd true

:: Build PayPal
CALL NuGet\GeneratePayPal.cmd true

:: Build TestUtilities
CALL NuGet\GenerateTestUtilities.cmd true

:: Build Transformer
CALL NuGet\GenerateTransformer.cmd true

:: Build Utility
CALL NuGet\GenerateUtility.cmd true

:: Build Validation (Core Only)
CALL NuGet\GenerateValidation.cmd true

:: Build Web
CALL NuGet\GenerateWeb.cmd true

:: Data Projects
CALL NuGet\GenerateData.cmd true

:: Return to our original directory
CD NuGet

PAUSE
EXIT