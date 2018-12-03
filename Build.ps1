
param (
    [string]$BuildEnv = "Debug",
    [string]$BuildLocation,
    [string]$RunTime = "win10-x64"
)

if ([string]::IsNullOrWhiteSpace($BuildLocation)) {
    Write-Error "Missing Build Location."
    exit
}

Get-ChildItem -Path $BuildLocation -Recurse -Force . | Remove-Item -Recurse -Force

dotnet publish .\MySecondApp.Console\MySecondApp.Console.csproj -c $buildEnv -o $buildLocation -f netcoreapp2.1 --self-contained --runtime $RunTime

$Global:AppSettingsDestination = Join-Path $buildLocation "appsettings.json"

Remove-Item $Global:AppSettingsDestination
Copy-Item ".\MySecondApp.Console\appsettings.$($buildEnv).json" -Destination $Global:AppSettingsDestination