# Phase 8 — Drag-Drop Kanban

**Branch:** `feature/phase-08-dragdrop`  
**Phụ thuộc:** Phase 7  
**Ước lượng:** 4–5 ngày

---

## Mục tiêu

Kanban **kéo thả** giữa cột (SortableJS). Lưu `Status` + `SortOrder`. Giới thiệu Oracle SP `MOVE_TASK` (optional nhưng khuyến khích học SP).

---

## Deliverables

- SortableJS tích hợp 3 cột
- `POST /Task/Move` — `{ taskId, newStatus, newSortOrder }`
- Repository gọi EF transaction hoặc `PKG_TASK.MOVE_TASK`
- Reorder trong cùng cột

---

## Acceptance Criteria

- [ ] Kéo card sang cột khác → status đổi trong DB
- [ ] Kéo trong cột → sort order cập nhật
- [ ] F5 sau drag — layout giữ nguyên

---

## Kế hoạch kiểm tra (Phase 8)

### Drag-drop
- [ ] ToDo → Done trực tiếp (nếu cho phép) hoặc chỉ láng giềng — document rule
- [ ] Drag nhiều task liên tiếp ổn định
- [ ] Mobile: fallback nút bấm phase 5 vẫn hoạt động

### Oracle SP (nếu làm)
- [ ] SP chạy trong transaction
- [ ] C# gọi qua `OracleCommand` với parameters đúng

### Performance
- [ ] Board 50 task vẫn mượt (dev data)

---

**Tiếp theo:** [Phase 9 — Polish](phase-09-polish.md)
