SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

CALL %ScriptDir%/PublishProjectSeries.cmd src\Pdf\Templating\Ivy.Pdf.Templating

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)