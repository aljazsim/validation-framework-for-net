$ErrorPreference = 'Stop'

$msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe"
$nuget = "C:\Program Files (x86)\NuGet\nuget.exe"

$projectFilePath = "..\Source\ValidationFramework\ValidationFramework.csproj";
$nugetPackageFilePath = "..\Source\ValidationFramework\bin\Release\ValidationFramework.1.0.1.nupkg"

& "$msbuild" "$projectFilePath" /t:pack /p:Configuration=Release

& "$nuget" push "$nugetPackageFilePath" xxx -Source https://api.nuget.org/v3/index.json