SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

CALL %ScriptDir%/PublishProject.cmd src\Push\Ivy.Push.Core

CALL %ScriptDir%/PublishProjectSeries.cmd src\Push\Firebase\Ivy.Push.Firebase

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)