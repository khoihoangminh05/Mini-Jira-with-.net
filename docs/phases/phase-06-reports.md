# Phase 6 — Reports (Progress Bar)

**Branch:** `feature/phase-06-reports`  
**Phụ thuộc:** Phase 5  
**Ước lượng:** 2–3 ngày

---

## Mục tiêu

Trang báo cáo: % công việc hoàn thành **theo từng project** — Bootstrap progress bar. Học LINQ aggregate.

---

## Deliverables

- `ReportController.Index`
- View: list project + `progress` bar + số liệu (total/done/todo)
- LINQ tính `percent = done / total * 100` (total=0 → 0%)

---

## Acceptance Criteria (mentor)

- [ ] Mỗi project hiển thị % hoàn thành
- [ ] Progress bar trực quan (màu theo %)
- [ ] Số liệu khớp với task thực tế trên Kanban

---

## Kế hoạch kiểm tra (Phase 6)

### Tính toán
- [ ] Project 0 task → 0% (không chia cho 0)
- [ ] 3/4 Done → 75%
- [ ] Thêm task Done trên Kanban → % cập nhật sau refresh

### UI
- [ ] Progress bar Bootstrap (`progress`, `progress-bar`)
- [ ] Hiển thị text: `3/10 hoàn thành (30%)`

### Scope
- [ ] Chỉ project của current user

---

## Milestone: MVP mentor hoàn thành

Sau phase 6, **toàn bộ yêu cầu sếp** đã đủ. Phase 7+ là nâng cao portfolio.

---

**Tiếp theo:** [Phase 7 — Ajax & Validation](phase-07-ajax-validation.md)
