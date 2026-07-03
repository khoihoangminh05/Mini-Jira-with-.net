# Phase 4 — Task CRUD

**Branch:** `feature/phase-04-task`  
**Phụ thuộc:** Phase 3  
**Ước lượng:** 4–5 ngày

---

## Mục tiêu

CRUD Task trong từng Project: Title, Mô tả, Priority (Cao/TB/Thấp), Status (ToDo/InProgress/Done), Deadline.

---

## Deliverables

- `TaskController` + `TaskRepository`
- ViewModels: Create, Edit, List
- Dropdown Priority & Status
- Date picker deadline (Flatpickr hoặc `input type="date"`)
- Danh sách task theo project (table view trước Kanban)

---

## Acceptance Criteria (mentor)

- [ ] Tạo/sửa/xóa task trong project
- [ ] Đủ field: tên, mô tả, priority, status, deadline
- [ ] LINQ lọc task theo project và user

---

## Kế hoạch kiểm tra (Phase 4)

### CRUD
- [ ] Task gắn đúng `ProjectId`
- [ ] Edit đổi priority/status/deadline
- [ ] Delete task (soft delete)

### Validation
- [ ] Title required
- [ ] Deadline có thể optional; nếu có rule "không quá khứ" → test cả client & server
- [ ] Enum hiển thị tiếng Việt trên UI

### LINQ
- [ ] `Tasks.Where(t => t.ProjectId == id && t.Project.UserId == currentUser)`
- [ ] Sắp xếp theo CreatedAt hoặc SortOrder

### Navigation
- [ ] Từ Project Index → vào danh sách task của project
- [ ] Breadcrumb hoặc link quay lại project

---

**Tiếp theo:** [Phase 5 — Kanban Basic](phase-05-kanban-basic.md)
