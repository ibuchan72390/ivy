SET configurePackages=%1

IF "%ScriptDir%" EQU "" (
	SET ScriptDir=%CD%/Scripts
)

IF "%1" NEQ "true" (
	CALL Scripts\ClearAndConfigurePackages.cmd
	CD ../
)

:: Build Auth0.Core
CALL %ScriptDir%/PublishProject.cmd src\Auth0\Ivy.Auth0.Core

:: Build Auth0.Authorization
CALL %ScriptDir%/PublishProjectSeries.cmd src\Auth0\Authorization\Ivy.Auth0.Authorization

:: Build Auth0.Management
CALL %ScriptDir%/PublishProjectSeries.cmd src\Auth0\Management\Ivy.Auth0.Management

:: Build Auth0.Web
CALL %ScriptDir%/PublishProjectSeries.cmd src\Auth0\Web\Ivy.Auth0.Web

IF "%1" NEQ "true" (
	:: return to orig directory for command line
	CD NuGet
)