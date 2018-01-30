SET OrigDir=%CD%

:: Copy out the param
SET ProjectName=%1

:: Pack and relocate the nupkg file
CD %ProjectName%

dotnet pack --include-source
	
CD Bin\Debug


MOVE /y *.nupkg %PackageDir%

:: Return to the original directory
CD %OrigDir%
