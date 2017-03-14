ECHO OFF

ECHO Are you sure you're ready to push all of the IBFramework packages?
PAUSE

CALL PushPackage.cmd IBFramework.Core
CALL PushPackage.cmd IBFramework.Data.Common
CALL PushPackage.cmd IBFramework.Data.MySQL
CALL PushPackage.cmd IBFramework.IoC
CALL PushPackage.cmd IBFramework.Utility