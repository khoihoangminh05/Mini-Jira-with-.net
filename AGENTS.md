# AGENTS.md — Hướng dẫn cho Cursor Agent

> File này là **entry point** cho AI agent khi code trong repo. Đọc file này trước mọi task implementation.

## Vai trò dự án

**Personal Task Manager** — Mini Jira/Trello trên ASP.NET MVC 4.8 + Oracle + EF6.

## Trước khi code

1. Đọc [docs/PHASES.md](docs/PHASES.md) — xác định **phase hiện tại**
2. Đọc file chi tiết: `docs/phases/phase-XX-*.md`
3. Đọc [docs/CONVENTIONS.md](docs/CONVENTIONS.md) và [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)
4. Chỉ implement **trong scope phase** — không thêm feature tương lai trừ khi user yêu cầu
5. Tạo branch theo [docs/GIT-WORKFLOW.md](docs/GIT-WORKFLOW.md)

## Stack cố định

| Layer | Công nghệ |
|-------|-----------|
| Web | ASP.NET MVC 5, .NET Framework 4.8, Razor |
| Data | EF6 Code First, Oracle.ManagedDataAccess |
| UI | Bootstrap 5, jQuery, jQuery Validation |
| Auth | Forms Authentication + Session |

**Không** chuyển sang .NET Core / React trừ khi user đổi yêu cầu.

## Quy tắc code bắt buộc

- PascalCase / camelCase theo C# convention
- ViewModel cho View — không expose Entity trực tiếp
- `ModelState.IsValid` trên mọi POST form
- `[ValidateAntiForgeryToken]` trên POST
- LINQ luôn filter `UserId` — không data leak
- Comment ở action/method có logic nghiệp vụ
- Connection string thật **không** commit

## Cấu trúc solution (sau Phase 1)

```
src/PersonalTaskManager.Web/
src/PersonalTaskManager.Core/
src/PersonalTaskManager.Infrastructure/
database/scripts/
database/procedures/
```

## Skills dự án (đọc khi cần)

| Skill | Khi dùng |
|-------|----------|
| `.cursor/skills/implement-phase/SKILL.md` | Bắt đầu implement một phase |
| `.cursor/skills/phase-verification/SKILL.md` | Trước khi tạo PR — chạy checklist |

## Cursor rules

| Rule | Scope |
|------|-------|
| `project-core.mdc` | Luôn apply |
| `csharp-mvc.mdc` | `**/*.cs` |
| `razor-views.mdc` | `**/*.cshtml` |

## Workflow mỗi task

```
Đọc phase file
  → Tạo/switch branch
  → Implement deliverables
  → Chạy checklist phase-verification
  → Báo user tạo PR (không tự commit trừ khi user yêu cầu)
```

## Phase hiện tại

**Phase 3 — Project CRUD** (Index, Create, Edit, Delete soft)

Sang Phase 4 khi checklist `docs/phases/phase-03-project-crud.md` hoàn tất.

## Liên hệ tài liệu

- DB schema: [docs/DATABASE.md](docs/DATABASE.md)
- Git: [docs/GIT-WORKFLOW.md](docs/GIT-WORKFLOW.md)
- Roadmap: [docs/PHASES.md](docs/PHASES.md)
