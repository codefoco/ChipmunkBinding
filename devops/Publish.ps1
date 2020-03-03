param ([string] $PackageId)

. .\devops\BuildFunctions.ps1

if (-Not (Test-Should-Deploy)) {
	return
}

$nupkgFile  = $PackageId + '.nupkg'
$snupkgFile = $PackageId + '.snupkg'

& nuget push $nupkgFile -Source https://www.nuget.org/api/v2/package

if (Test-Path -Path $snupkgFile) {
	& nuget push $snupkgFile -Source https://www.nuget.org/api/v2/package
}

