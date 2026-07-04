# Chạy Personal Task Manager qua IIS Express (không cần F5 trong VS)
$ErrorActionPreference = "Stop"
$webPath = Join-Path $PSScriptRoot "PersonalTaskManager.Web"
$iis = "${env:ProgramFiles}\IIS Express\iisexpress.exe"
if (-not (Test-Path $iis)) {
    $iis = "${env:ProgramFiles(x86)}\IIS Express\iisexpress.exe"
}
if (-not (Test-Path $iis)) {
    Write-Error "Khong tim thay IIS Express. Cai ASP.NET and web development trong Visual Studio Installer."
}

Write-Host "Build solution..." -ForegroundColor Cyan
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
& $msbuild (Join-Path $PSScriptRoot "PersonalTaskManager.sln") /p:Configuration=Debug /v:minimal

Write-Host ""
Write-Host "Mo trinh duyet: http://localhost:5050/" -ForegroundColor Green
Write-Host "Nhan Ctrl+C de dung IIS Express" -ForegroundColor Yellow
& $iis "/path:$webPath" "/port:5050"
