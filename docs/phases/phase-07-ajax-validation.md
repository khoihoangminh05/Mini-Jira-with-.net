# Phase 7 — Ajax & Validation Polish

**Branch:** `feature/phase-07-ajax`  
**Phụ thuộc:** Phase 6  
**Ước lượng:** 3–4 ngày

---

## Mục tiêu

Cập nhật trạng thái task **không reload trang** (Ajax/fetch). Hoàn thiện validation toàn app. JSON API endpoints trong MVC.

---

## Deliverables

- `POST /Task/UpdateStatus` → `JsonResult`
- jQuery `$.ajax` hoặc `fetch` + CSRF token
- Toast notification (Toastr / Bootstrap toast)
- Optimistic UI hoặc loading state trên card
- Rà soát validation tất cả form CRUD

---

## Acceptance Criteria

- [ ] Bấm Start/Complete trên Kanban → card chuyển cột không F5
- [ ] Lỗi server → hiển thị message, UI rollback
- [ ] Mọi form CRUD pass checklist validation mentor

---

## Kế hoạch kiểm tra (Phase 7)

### Ajax
- [ ] Network tab: POST trả `{ success: true }`
- [ ] CSRF token gửi kèm request
- [ ] Double-click không tạo duplicate request (disable button)

### Validation audit
- [ ] Register, Login, Project, Task — đủ client + server
- [ ] Remote validation (optional): check username tồn tại

### Regression
- [ ] Chức năng phase 2–6 vẫn hoạt động khi JS tắt (fallback full post)

---

**Tiếp theo:** [Phase 8 — Drag-Drop](phase-08-drag-drop.md)
