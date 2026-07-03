# Personal Task Manager (Mini Jira/Trello)

Hệ thống quản lý công việc cá nhân xây dựng trên **ASP.NET MVC 4.8**, **Entity Framework**, và **Oracle Database** (stored procedures cho các luồng nâng cao).

## Mục tiêu học tập

| # | Chủ đề | Mô tả ngắn |
|---|--------|------------|
| 1 | MVC | Model – View – Controller, Routing, Razor |
| 2 | Data Access | EF Database First / Code First, LINQ |
| 3 | Session & Layout | Login/Logout, `_Layout`, partial views |
| 4 | UI/UX | Bootstrap, Validation (client + server) |
| 5 | Nâng cao | Ajax, Kanban drag-drop, báo cáo, deploy |

## Tài liệu dự án

| File | Nội dung |
|------|----------|
| [docs/PHASES.md](docs/PHASES.md) | **Roadmap tổng hợp** — tất cả phase + checklist kiểm tra |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | Kiến trúc solution, layer, luồng dữ liệu |
| [docs/CONVENTIONS.md](docs/CONVENTIONS.md) | Coding convention C#, Razor, Git |
| [docs/DATABASE.md](docs/DATABASE.md) | Schema, EF, Oracle SP |
| [docs/GIT-WORKFLOW.md](docs/GIT-WORKFLOW.md) | Branch, PR, commit message |
| [AGENTS.md](AGENTS.md) | Hướng dẫn cho AI agent khi code |
| [docs/phases/](docs/phases/) | Chi tiết từng phase |

## Tech stack

- **Backend:** ASP.NET MVC 5 trên .NET Framework 4.8
- **ORM:** Entity Framework 6 (Code First ưu tiên; SP cho phase nâng cao)
- **Database:** Oracle (Oracle.ManagedDataAccess)
- **Frontend:** Razor, Bootstrap 5, jQuery, jQuery Validation, SortableJS (phase sau)
- **Auth:** Forms Authentication + Session
- **Deploy:** AWS EC2 (Windows/IIS) hoặc Azure App Service

## Cấu trúc solution (dự kiến)

```
PersonalTaskManager.sln
├── src/
│   ├── PersonalTaskManager.Web/          # MVC — Controllers, Views, ViewModels
│   ├── PersonalTaskManager.Core/         # Entities, Interfaces, Enums, DTOs
│   └── PersonalTaskManager.Infrastructure/ # EF DbContext, Repositories, Oracle SP
├── database/
│   ├── scripts/                          # DDL, seed data
│   └── procedures/                       # Oracle packages & SP
├── docs/
└── .cursor/                              # Rules & skills cho Cursor Agent
```

## Bắt đầu nhanh

> Phase 0 chỉ thiết lập khung. Code chạy được bắt đầu từ **Phase 1**.

1. Đọc [docs/PHASES.md](docs/PHASES.md) — xác định phase hiện tại
2. Tạo branch theo [docs/GIT-WORKFLOW.md](docs/GIT-WORKFLOW.md)
3. Làm xong phase → chạy checklist kiểm tra trong file phase tương ứng
4. Tạo PR, nhờ mentor review, merge vào `develop`

## Yêu cầu tối thiểu (theo mentor)

- [x] Login / Logout / Đăng ký
- [ ] CRUD Project
- [ ] CRUD Task (priority, status, deadline)
- [ ] Kanban 3 cột + nút chuyển trạng thái
- [ ] Báo cáo % hoàn thành (progress bar)
- [ ] Validation client + server
- [ ] Git flow (feature branch + PR)

## Mở rộng (vượt yêu cầu mentor)

- Ajax cập nhật trạng thái không reload
- Drag-and-drop Kanban (SortableJS)
- Activity log, labels, filter, dashboard
- Deploy HTTPS lên cloud

## License

Dự án học tập cá nhân.
