ECHO OFF

ECHO Are you sure you're ready to push all of the IBFramework packages?
PAUSE

CALL PushPackage.cmd IBFramework.Core

ECHO --------------------------------
ECHO Completed IBFramework.Core
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Data.Common

ECHO --------------------------------
ECHO Completed IBFramework.Data.Common
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Data.MySQL

ECHO --------------------------------
ECHO Completed IBFramework.Data.MySQL
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.IoC

ECHO --------------------------------
ECHO Completed IBFramework.IoC
ECHO --------------------------------

CALL PushPackage.cmd IBFramework.Utility

ECHO --------------------------------
ECHO Completed IBFramework.Utility
ECHO --------------------------------