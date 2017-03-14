ECHO OFF

SET PackageName=IBFramework.Utility

ECHO Are you sure you're ready to push the package for %PackageName%?
PAUSE

CALL PushPackage.cmd %PackageName%