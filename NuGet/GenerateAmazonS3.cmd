SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

CALL %ScriptDir%/PublishProjectSeries.cmd src\Amazon\S3\Ivy.Amazon.S3

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)