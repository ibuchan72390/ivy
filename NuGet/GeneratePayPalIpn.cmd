SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

CALL %ScriptDir%/PublishProjectSeries.cmd src\PayPal\Ipn\Ivy.PayPal.Ipn

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)