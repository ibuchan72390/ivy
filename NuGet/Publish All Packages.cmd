ECHO OFF

ECHO Are you sure you're ready to push all of the Ivy packages?
PAUSE


ECHO --------------------------------
ECHO Publishing Ivy.Amazon.S3 Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Amazon.S3


ECHO --------------------------------
ECHO Publishing Ivy.Auth0 Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Auth0


ECHO --------------------------------
ECHO Publishing Ivy.Caching Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Caching


ECHO --------------------------------
ECHO Publishing Ivy.Data Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Data


ECHO --------------------------------
ECHO Publishing Ivy.IoC Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.IoC


ECHO --------------------------------
ECHO Publishing Ivy.MailChimp Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.MailChimp


ECHO --------------------------------
ECHO Publishing Ivy.TestUtilities
ECHO --------------------------------

CALL PushPackage.cmd Ivy.TestUtilities


ECHO --------------------------------
ECHO Publishing Ivy.Transformer Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Transformer


ECHO --------------------------------
ECHO Publishing Ivy.Utility Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Utility


ECHO --------------------------------
ECHO Publishing Ivy.Validation Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Validation


ECHO --------------------------------
ECHO Publishing Ivy.Web Projects
ECHO --------------------------------

CALL PushPackage.cmd Ivy.Web