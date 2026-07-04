# Database Scripts

## Chạy setup nhanh (Oracle 26ai Free local)

```powershell
cd database\scripts
$env:ORACLE_HOME = "C:\app\ADMIN\product\26ai\dbhomeFree"
& "$env:ORACLE_HOME\bin\sqlplus.exe" "/ as sysdba" "@run_setup.sql"
```

**Connection string app** (Oracle Free trên Windows thường dùng):

```
User Id=APP_USER;Password=***;Data Source=localhost:1522/freepdb1;
```

| Tham số | Giá trị máy bạn |
|---------|-----------------|
| Port | **1522** (không phải 1521) |
| PDB | **freepdb1** (không phải XEPDB1) |

## Thư mục

| Path | Nội dung |
|------|----------|
| `scripts/` | DDL, sequences, indexes, seed data |
| `scripts/002_create_activity_log.sql` | Bảng ACTIVITY_LOG — **Phase 9** (chạy sau 001) |
| `procedures/` | Oracle packages & SP — `001_pkg_task_move.sql` (Phase 8) |

## Quy ước đặt tên file

```
001_create_users.sql
002_create_projects.sql
003_create_tasks.sql
999_seed_dev.sql
```

## Tham chiếu

- Schema chi tiết: [docs/DATABASE.md](../docs/DATABASE.md)
