# Chạy Personal Task Manager qua IIS Express (không cần F5 trong VS)
$ErrorActionPreference = "Stop"
$webPath = Join-Path $PSScriptRoot "PersonalTaskManager.Web"

Write-Host "Build solution..." -ForegroundColor Cyan
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
& $msbuild (Join-Path $PSScriptRoot "PersonalTaskManager.sln") /p:Configuration=Debug /v:minimal

Write-Host ""
& (Join-Path $webPath "iisexpress-run.cmd")
