SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

:: Core Data
CALL %ScriptDir%/PublishProject.cmd src\Data\Ivy.Data.Core

:: Common Data
CALL %ScriptDir%/PublishProject.cmd src\Data\Ivy.Data.Common

:: Common Data IoC
CALL %ScriptDir%/PublishProject.cmd src\Data\Ivy.Data.Common.IoC

:: MySQL Data
CALL %ScriptDir%/PublishProject.cmd src\Data\MySQL\Ivy.Data.MySQL

:: MySQL IoC
CALL %ScriptDir%/PublishProject.cmd src\Data\MySQL\Ivy.Data.MySQL.IoC

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)