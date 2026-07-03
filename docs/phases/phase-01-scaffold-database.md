# Phase 1 — Scaffold + Database

**Branch:** `feature/phase-01-scaffold`  
**Phụ thuộc:** Phase 0  
**Ước lượng:** 4–5 ngày

---

## Mục tiêu

Tạo solution ASP.NET MVC 4.8, cấu trúc 3 project (Web, Core, Infrastructure), cấu hình EF6 Code First kết nối Oracle, tạo bảng Users/Projects/Tasks.

---

## Deliverables

- `PersonalTaskManager.sln` trong `src/`
- Projects: Web, Core, Infrastructure
- `ApplicationDbContext`, Entities, Enums
- Script SQL hoặc migration tạo bảng + sequences
- App chạy được trang Home (placeholder)
- `Web.config` connection string (dùng placeholder / User Secrets local)

---

## Acceptance Criteria

- [ ] F5 chạy được, trang chủ hiển thị không lỗi
- [ ] EF tạo/kết nối được schema Oracle (3 bảng chính)
- [ ] Build 3 project không warning nghiêm trọng
- [ ] README `src/` cập nhật hướng dẫn build

---

## Kế hoạch kiểm tra (Phase 1)

### Build & Run
- [ ] Solution build `Debug|Any CPU` thành công
- [ ] IIS Express mở được `/`
- [ ] Không lỗi binding redirect / Oracle provider

### Database
- [ ] Bảng USERS, PROJECTS, TASKS tồn tại trên Oracle
- [ ] FK PROJECTS→USERS, TASKS→PROJECTS đúng
- [ ] Sequence hoặc identity strategy hoạt động khi insert test

### Code structure
- [ ] Entity nằm trong Core, DbContext trong Infrastructure
- [ ] Web reference Infrastructure, không reference Entity trực tiếp từ View
- [ ] Enums `TaskPriority`, `TaskStatus` đã định nghĩa

### Git & Docs
- [ ] PR `feature/phase-01-scaffold` → `develop`
- [ ] Connection string thật **không** commit (dùng transform hoặc `.config` local ignore)

---

**Tiếp theo:** [Phase 2 — Authentication](phase-02-authentication.md)
