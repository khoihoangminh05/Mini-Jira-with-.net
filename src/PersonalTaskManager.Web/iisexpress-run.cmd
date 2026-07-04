@echo off
REM IIS Express launcher — path co khoang trang (F5 fallback / run-web)
set "IIS=%ProgramFiles%\IIS Express\iisexpress.exe"
if not exist "%IIS%" set "IIS=%ProgramFiles(x86)%\IIS Express\iisexpress.exe"
if not exist "%IIS%" (
    echo IIS Express not found.
    exit /b 1
)

REM Cat backslash cuoi — KHONG dung %~dp0. (IIS khong doc duoc web.config)
set "WEBDIR=%~dp0"
if "%WEBDIR:~-1%"=="\" set "WEBDIR=%WEBDIR:~0,-1%"
echo Starting http://localhost:5050/
"%IIS%" /path:"%WEBDIR%" /port:5050 /clr:v4.0
