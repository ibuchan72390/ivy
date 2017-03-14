:: Param received is the name of the NuPkg file to push
:: Assuming that nuget.exe is already in your path variable


SET PackageName=%1

CD Packages

nuget.exe push %PackageName%.*.nupkg 76d68764-3361-4bb7-acb2-310b12ea5429 -source https://www.myget.org/F/ib-framework/api/v2/package