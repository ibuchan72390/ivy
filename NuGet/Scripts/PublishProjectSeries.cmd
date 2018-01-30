SET currentProj=%1
SET coreProject=%currentProj%.Core	
SET iocProject=%currentProj%.IoC

:: Build Core
CALL %CD%\NuGet\Scripts\PublishProject.cmd %coreProject%

:: Build Project
CALL %CD%\NuGet\Scripts\PublishProject.cmd %currentProj%

:: Build IoC
CALL %CD%\NuGet\Scripts\PublishProject.cmd %iocProject%
