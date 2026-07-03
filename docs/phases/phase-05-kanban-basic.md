# Phase 5 — Kanban Basic (Nút chuyển trạng thái)

**Branch:** `feature/phase-05-kanban`  
**Phụ thuộc:** Phase 4  
**Ước lượng:** 3–4 ngày

---

## Mục tiêu

Giao diện Kanban 3 cột: **To Do | In Progress | Done**. Nút nhanh: Start (→ In Progress), Complete (→ Done), Reopen (→ To Do).

*Đáp ứng yêu cầu mentor — chưa cần drag-drop.*

---

## Deliverables

- `KanbanController` hoặc action trong `TaskController`
- View `Kanban/Index.cshtml` — 3 cột Bootstrap
- Partial `_TaskCard.cshtml`
- POST actions: `Start`, `Complete`, `Reopen` (+ anti-forgery)
- Màu sắc theo priority (badge)

---

## Acceptance Criteria (mentor)

- [ ] Task hiển thị đúng cột theo status
- [ ] Bấm "Start" chuyển ToDo → InProgress
- [ ] Bấm hoàn thành chuyển → Done
- [ ] Reload trang — trạng thái đúng

---

## Kế hoạch kiểm tra (Phase 5)

### UI
- [ ] 3 cột rõ ràng, card hiển thị title, priority, deadline
- [ ] Task Done ẩn nút Start hoặc disable đúng logic
- [ ] Overdue deadline highlight (optional nhưng khuyến khích)

### Chức năng
- [ ] Chuyển trạng thái cập nhật DB
- [ ] User khác không đổi được task của project người khác

### Responsive
- [ ] Mobile: cột xếp dọc hoặc scroll ngang chấp nhận được

---

**Tiếp theo:** [Phase 6 — Reports](phase-06-reports.md)
