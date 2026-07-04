# Launcher IIS Express for Visual Studio F5 (handles paths with spaces)
$ErrorActionPreference = "Stop"

$webPath = $PSScriptRoot.TrimEnd('\', '/')
$iis = Join-Path ${env:ProgramFiles} "IIS Express\iisexpress.exe"
if (-not (Test-Path $iis)) {
    $iis = Join-Path ${env:ProgramFiles(x86)} "IIS Express\iisexpress.exe"
}
if (-not (Test-Path $iis)) {
    Write-Error "IIS Express not found. Install ASP.NET and web development workload."
}

$dll = Join-Path $webPath "bin\PersonalTaskManager.Web.dll"
if (-not (Test-Path $dll)) {
    Write-Host "Build first: Rebuild Solution in Visual Studio." -ForegroundColor Yellow
}

Write-Host "IIS Express: http://localhost:5050/" -ForegroundColor Green
Write-Host "Web root: $webPath" -ForegroundColor DarkGray
Write-Host "Press Ctrl+C to stop" -ForegroundColor DarkGray

$iisArgs = @(
    "/path:$webPath"
    "/port:5050"
    "/clr:v4.0"
)
& $iis @iisArgs
