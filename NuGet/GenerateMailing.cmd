SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

:: Build Mailing.Core
CALL %ScriptDir%/PublishProject.cmd src\Mailing\Ivy.Mailing.Core

:: Build MailChimp
CALL %ScriptDir%/PublishProjectSeries.cmd src\Mailing\MailChimp\Ivy.Mailing.MailChimp

:: Build ActiveCampaign
CALL %ScriptDir%/PublishProjectSeries.cmd src\Mailing\ActiveCampaign\Ivy.Mailing.ActiveCampaign

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)