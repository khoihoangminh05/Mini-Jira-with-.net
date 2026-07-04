# Thiết lập Visual Studio + IIS Express cho Personal Task Manager
$ErrorActionPreference = "Stop"
$srcRoot = $PSScriptRoot
$sln = Join-Path $srcRoot "PersonalTaskManager.sln"
$webPath = Join-Path $srcRoot "PersonalTaskManager.Web"
$vsConfigDir = Join-Path $srcRoot ".vs\PersonalTaskManager\config"
$appHost = Join-Path $vsConfigDir "applicationhost.config"
$nuget = Join-Path $srcRoot "nuget.exe"

Write-Host "=== Xoa cache VS cu (neu project Unloaded) ===" -ForegroundColor Cyan
$vsDir = Join-Path $srcRoot ".vs"
if (Test-Path $vsDir) {
    Remove-Item $vsDir -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "=== Restore (dotnet + nuget) ===" -ForegroundColor Cyan
if (-not (Test-Path $nuget)) {
    Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nuget
}
dotnet restore $sln
& $nuget restore $sln

Write-Host "=== Tao IIS Express config ===" -ForegroundColor Cyan
New-Item -ItemType Directory -Force -Path $vsConfigDir | Out-Null
$physicalPath = (Resolve-Path $webPath).Path
@(
'<?xml version="1.0" encoding="UTF-8"?>'
'<configuration>'
'  <configSections>'
'    <section name="system.applicationHost" overrideModeDefault="Allow" />'
'  </configSections>'
'  <system.applicationHost>'
'    <applicationPools>'
'      <add name="Clr4IntegratedAppPool" managedRuntimeVersion="v4.0" managedPipelineMode="Integrated" />'
'    </applicationPools>'
'    <sites>'
'      <site name="PersonalTaskManager.Web" id="2">'
'        <application path="/" applicationPool="Clr4IntegratedAppPool">'
"          <virtualDirectory path=`"/`" physicalPath=`"$physicalPath`" />"
'        </application>'
'        <bindings>'
'          <binding protocol="http" bindingInformation="*:5050:localhost" />'
'          <binding protocol="https" bindingInformation="*:44301:localhost" />'
'        </bindings>'
'      </site>'
'    </sites>'
'  </system.applicationHost>'
'</configuration>'
) -join "`n" | Set-Content -Path $appHost -Encoding UTF8

Write-Host "=== Build solution ===" -ForegroundColor Cyan
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
& $msbuild $sln /p:Configuration=Debug /v:minimal

Write-Host ""
Write-Host "Xong! Mo Visual Studio:" -ForegroundColor Green
Write-Host "  $sln"
Write-Host "1. Chuot phai PersonalTaskManager.Web -> Set as Startup Project"
Write-Host "2. Nhan F5 (IIS Express: https://localhost:44301/)"
