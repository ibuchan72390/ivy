:: Param received is the name of the NuPkg file to push
:: Assuming that nuget.exe is already in your path variable


SET PackageName=%1

CD Packages

SET pkg=%PackageName%.nupkg
SET symbols=%PackageName%.symbols.nupkg

CALL :PushPackage %pkg%
CALL :PushPackage %symbols%

CD ../


:PushPackage 
		
	IF EXIST (%1){
		nuget.exe push %1 76d68764-3361-4bb7-acb2-310b12ea5429 -source https://www.myget.org/F/ib-framework/api/v2/package
	} ELSE {
		ECHO Unable to find %1
	}

EXIT /b