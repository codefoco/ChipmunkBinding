param ([string] $PackageId)

. .\devops\BuildFunctions.ps1

if (-Not (Test-Should-Deploy)) {
	return
}

$nupkgFile  = $PackageId + '.nupkg'
$snupkgFile = $PackageId + '.snupkg'

& nuget push $nupkgFile -Source https://api.nuget.org/v3/index.json

if (Test-Path -Path $snupkgFile) {
	Write-Output "Pushing Symbol Package..."

	& nuget push $snupkgFile -Source https://api.nuget.org/v3/index.json
} else {
	Write-Output "Skipping Symbol Registration..."
}

