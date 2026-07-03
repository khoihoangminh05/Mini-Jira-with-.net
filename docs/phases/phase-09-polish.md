# Phase 9 — Polish & Extras

**Branch:** `feature/phase-09-polish`  
**Phụ thuộc:** Phase 8  
**Ước lượng:** 4–5 ngày

---

## Mục tiêu

Nâng portfolio: filter task, labels màu, activity log, dashboard tổng quan, dark mode, keyboard shortcuts.

---

## Deliverables (chọn theo thời gian — tối thiểu 3 mục)

| Feature | Ưu tiên |
|---------|---------|
| Filter Kanban (priority, overdue) | Cao |
| Labels / tags cho task | Trung bình |
| Activity log (đổi status, tạo task) | Trung bình |
| Dashboard tổng hợp | Cao |
| Dark mode toggle | Thấp |
| Export CSV task list | Thấp |

---

## Acceptance Criteria

- [ ] Ít nhất 3 tính năng polish hoàn chỉnh có test checklist
- [ ] UI nhất quán với design system Bootstrap đã dùng

---

## Kế hoạch kiểm tra (Phase 9)

### Filter
- [ ] Lọc High priority → chỉ card High hiển thị
- [ ] Clear filter → hiện lại tất cả

### Activity log
- [ ] Sau drag/status change → 1 dòng log mới
- [ ] Log hiển thị trên task detail hoặc project timeline

### Dashboard
- [ ] Tổng task / overdue / done across projects
- [ ] Link nhanh tới Kanban từng project

### Quality
- [ ] Không regression phase 2–8
- [ ] Lighthouse accessibility cơ bản (form labels)

---

**Tiếp theo:** [Phase 10 — Deploy](phase-10-deploy.md)
