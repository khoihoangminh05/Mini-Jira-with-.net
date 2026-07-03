# Phase 3 — Project CRUD

**Branch:** `feature/phase-03-project`  
**Phụ thuộc:** Phase 2  
**Ước lượng:** 3–4 ngày

---

## Mục tiêu

CRUD dự án cá nhân: mỗi user chỉ thấy/sửa project của mình. Học LINQ filter theo `UserId`, TempData/ViewBag.

---

## Deliverables

- `ProjectController`: Index, Create, Edit, Delete (soft delete)
- `ProjectRepository` + `IProjectRepository`
- Views: danh sách card/table, form create/edit
- ViewModels với validation Name, Description

---

## Acceptance Criteria (mentor)

- [ ] Tạo project mới (vd: "Học .NET", "Làm đồ án")
- [ ] Sửa tên/mô tả project
- [ ] Xóa project (confirm dialog)
- [ ] Chỉ hiển thị project của user đang login

---

## Kế hoạch kiểm tra (Phase 3)

### CRUD
- [ ] Create project → hiện trong Index
- [ ] Edit → thay đổi persist sau F5
- [ ] Delete → biến mất khỏi list (soft delete trong DB)

### LINQ & Security
- [ ] Login user B → không thấy project của user A
- [ ] Gõ URL `/Project/Edit/5` với project của user khác → 403 hoặc NotFound

### Validation
- [ ] Tên project rỗng → lỗi client + server
- [ ] Tên > max length → lỗi

### UX
- [ ] Nút "Tạo dự án" rõ ràng trên Index
- [ ] Empty state khi chưa có project

---

**Tiếp theo:** [Phase 4 — Task CRUD](phase-04-task-crud.md)
