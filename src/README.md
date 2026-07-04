# Source Code — Personal Task Manager

## Solution

Mở file **`PersonalTaskManager.sln`** bằng Visual Studio 2022.

```
PersonalTaskManager.sln
├── PersonalTaskManager.Web/          ← Startup project (F5)
├── PersonalTaskManager.Core/         ← Entities, Enums
└── PersonalTaskManager.Infrastructure/ ← EF6 DbContext, Oracle
```

## Yêu cầu

- Visual Studio 2022 (workload **ASP.NET and web development**)
- .NET Framework 4.8 Developer Pack
- Oracle Database 12c+ (XE / Autonomous / local)
- Oracle client: dùng **Oracle.ManagedDataAccess** (NuGet — không cần cài Oracle Client riêng)

## Lần đầu setup

### 0. Script tự động (khuyến nghị)

```powershell
cd "D:\LapTrinh\.net\love in .net\src"
powershell -ExecutionPolicy Bypass -File .\setup-vs.ps1
```

Script sẽ: restore NuGet, tạo IIS Express config, build solution.

### 1. Restore NuGet packages

Trong Visual Studio: chuột phải Solution → **Restore NuGet Packages**

Hoặc command line (từ thư mục `src/`):

```powershell
# Tải nuget.exe một lần (nếu chưa có)
Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe

.\nuget.exe restore PersonalTaskManager.sln
```

### 2. Cấu hình Oracle

1. Tạo user `APP_USER` — xem `database/scripts/000_create_app_user.sql`
2. Chạy `database/scripts/001_create_tables.sql`
3. Sửa `PersonalTaskManager.Web/Web.config`:

```xml
<add name="TaskManagerDb"
     connectionString="User Id=APP_USER;Password=***;Data Source=localhost:1521/XEPDB1;"
     providerName="Oracle.ManagedDataAccess.Client" />
```

> **Không commit password thật.** Dùng password placeholder trong repo; override local khi dev.

### 3. Chạy ứng dụng

1. Mở **`src/PersonalTaskManager.sln`** (không mở thư mục gốc repo)
2. Chuột phải **PersonalTaskManager.Web** → **Set as Startup Project**
3. Nhấn **F5** → trình duyệt mở `http://localhost:5050/`
4. Trang Login hiển thị → đăng ký / đăng nhập

### IIS Express "exited with code 0" ngay sau F5

**Nguyên nhân thường gặp:** đường dẫn project có **khoảng trắng** (`love in .net`) + IIS Express nhận sai tham số `/path`.

**Đã sửa:** F5 gọi `PersonalTaskManager.Web\iisexpress-run.ps1` (giống logic `run-web.ps1`).

| Trường hợp | Ý nghĩa |
|------------|---------|
| Bấm **Stop** (Shift+F5) | Bình thường — code 0 = thoát sạch |
| F5 thoát ngay | Rebuild → F5 lại; hoặc dùng `.\run-web.ps1` |

**F5 trong VS** (attach trực tiếp `iisexpress.exe`, không qua PowerShell):
1. Tắt mọi `iisexpress.exe` cũ (Task Manager)
2. **Rebuild Solution**
3. **F5** → Output hiện `Starting IIS Express...` → browser mở `http://localhost:5050/`

Nếu F5 vẫn lỗi, chạy `PersonalTaskManager.Web\iisexpress-run.cmd` hoặc `.\run-web.ps1`.

## Visual Studio — lỗi "The application for the project is not installed"

**Nguyên nhân:** VS 2026 không cài (hoặc không hỗ trợ) legacy **ASP.NET Web Application** project type cho MVC .NET Framework.

**Đã xử lý trong repo:** Project Web đổi sang type **C# class library** — VS load bình thường.

### Chạy app

**Cách 1 — F5 trong Visual Studio** (đã cấu hình `PersonalTaskManager.Web.csproj.user`):
1. Set **PersonalTaskManager.Web** làm Startup Project
2. **F5** → IIS Express mở `http://localhost:5050/`

**Cách 2 — PowerShell:**
```powershell
cd "D:\LapTrinh\.net\love in .net\src"
.\run-web.ps1
```

### (Tùy chọn) Cài thêm component VS để hỗ trợ web project đầy đủ

Visual Studio Installer → **Modify** → workload **ASP.NET and web development** → trong tab Individual components bật:
- **ASP.NET and web development tools**
- **.NET Framework 4.8 targeting pack** / **.NET Framework project and item templates**

Sau đó restart VS. (Không bắt buộc với cấu hình hiện tại.)

### Nếu vẫn Unloaded

1. Đóng VS → xóa `src\.vs` → `dotnet restore` + `nuget restore` trong `src\`
2. Mở lại `src\PersonalTaskManager.sln`
3. Rebuild Solution

## Build command line

```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" `
  PersonalTaskManager.sln /p:Configuration=Debug /restore
```

## Phase hiện tại

**Phase 1 — Scaffold + Database** ✅ (code)
Tiếp theo: **Phase 2 — Authentication**

Xem [docs/phases/phase-01-scaffold-database.md](../docs/phases/phase-01-scaffold-database.md)
