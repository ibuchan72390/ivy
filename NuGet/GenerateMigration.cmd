SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

:: Migration Core
CALL %ScriptDir%/PublishProjectSeries.cmd src\Migration\Ivy.Migration

:: Migration MySQL
CALL %ScriptDir%/PublishProjectSeries.cmd src\Migration\MySQL\Ivy.Migration.MySQL


IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)