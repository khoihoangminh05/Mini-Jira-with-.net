# Database Design

## Tổng quan

- **RDBMS:** Oracle 11g+ (hoặc Oracle XE / Autonomous)
- **ORM:** Entity Framework 6 Code First (phase 1–6)
- **Nâng cao:** Stored Procedures trong `database/procedures/` (phase 7+)

## Bảng chính

### USERS

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| USER_ID | NUMBER PK | Sequence `SEQ_USERS` |
| USERNAME | VARCHAR2(50) | Unique, NOT NULL |
| EMAIL | VARCHAR2(100) | Unique, NOT NULL |
| PASSWORD_HASH | VARCHAR2(256) | NOT NULL |
| CREATED_AT | TIMESTAMP | DEFAULT SYSTIMESTAMP |

### PROJECTS

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| PROJECT_ID | NUMBER PK | Sequence `SEQ_PROJECTS` |
| USER_ID | NUMBER FK → USERS | NOT NULL |
| NAME | VARCHAR2(200) | NOT NULL |
| DESCRIPTION | VARCHAR2(2000) | Nullable |
| CREATED_AT | TIMESTAMP | |
| IS_DELETED | NUMBER(1) | 0/1, soft delete |

### TASKS

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| TASK_ID | NUMBER PK | Sequence `SEQ_TASKS` |
| PROJECT_ID | NUMBER FK → PROJECTS | NOT NULL |
| TITLE | VARCHAR2(200) | NOT NULL |
| DESCRIPTION | CLOB | Nullable |
| PRIORITY | NUMBER | 1=Low, 2=Medium, 3=High |
| STATUS | NUMBER | 1=ToDo, 2=InProgress, 3=Done |
| DEADLINE | DATE | Nullable |
| SORT_ORDER | NUMBER | Thứ tự trong cột Kanban |
| CREATED_AT | TIMESTAMP | |
| IS_DELETED | NUMBER(1) | Soft delete |

## Enum mapping (C#)

```csharp
public enum TaskPriority { Low = 1, Medium = 2, High = 3 }
public enum TaskStatus { ToDo = 1, InProgress = 2, Done = 3 }
```

## Index đề xuất

```sql
CREATE INDEX IDX_PROJECTS_USER ON PROJECTS(USER_ID, IS_DELETED);
CREATE INDEX IDX_TASKS_PROJECT ON TASKS(PROJECT_ID, STATUS, SORT_ORDER);
CREATE INDEX IDX_TASKS_DEADLINE ON TASKS(DEADLINE) WHERE IS_DELETED = 0;
```

## EF6 Code First

- `ApplicationDbContext` trong Infrastructure
- Fluent API config table/column name Oracle (UPPER_CASE nếu cần)
- Migration hoặc SQL script generate từ model (tùy workflow team)

### Connection string (mẫu — không commit password thật)

```xml
<connectionStrings>
  <add name="TaskManagerDb"
       connectionString="User Id=APP_USER;Password=***;Data Source=localhost:1521/XEPDB1;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

## LINQ patterns thường dùng

```csharp
// % hoàn thành theo project
var total = tasks.Count();
var done = tasks.Count(t => t.Status == TaskStatus.Done);
var percent = total == 0 ? 0 : (int)(100.0 * done / total);

// Task quá hạn
var overdue = tasks.Where(t =>
    t.Status != TaskStatus.Done &&
    t.Deadline.HasValue &&
    t.Deadline.Value < DateTime.Today);
```

## Stored Procedures (phase 7+)

| Procedure | Mục đích |
|-----------|----------|
| `PKG_TASK.MOVE_TASK` | Đổi status + sort order trong transaction |
| `PKG_TASK.GET_BOARD` | REF CURSOR trả tasks theo project |
| `PKG_AUDIT.LOG_ACTIVITY` | Ghi nhật ký thay đổi |

## Seed data (dev)

- 1 user test: `demo` / password hash tương ứng
- 2 project mẫu, 5–10 task các trạng thái khác nhau

Script: `database/scripts/999_seed_dev.sql`
