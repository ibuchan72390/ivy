ECHO OFF

ECHO Are you sure you're ready to push all of the IBFramework packages?
PAUSE


ECHO --------------------------------
ECHO Publishing IBFramework.Core
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Core


ECHO --------------------------------
ECHO Publishing IBFramework.Data.Common
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Data.Common


ECHO --------------------------------
ECHO Publishing IBFramework.Data.MySQL
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Data.MySQL


ECHO --------------------------------
ECHO Publishing IBFramework.IoC
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.IoC


ECHO --------------------------------
ECHO Publishing IBFramework.Utility
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Utility
