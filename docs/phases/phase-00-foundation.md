# Phase 0 — Foundation (Nền tảng dự án)

**Branch:** `chore/phase-00-foundation`  
**Trạng thái:** 🟢 Hoàn thành (docs + khung repo)  
**Phụ thuộc:** Không  
**Ước lượng:** 2–3 ngày

---

## Mục tiêu

Thiết lập **khung dự án hoàn chỉnh** trước khi viết business code: tài liệu, convention, Cursor rules/skills, cấu trúc thư mục, Git workflow.

**Không** yêu cầu app chạy được ở phase này — chỉ cần repo có tổ chức và agent/developer biết làm gì tiếp.

---

## Deliverables

| # | Hạng mục | Vị trí |
|---|----------|--------|
| 1 | README tổng quan | `README.md` |
| 2 | Roadmap tất cả phase | `docs/PHASES.md` |
| 3 | Kiến trúc | `docs/ARCHITECTURE.md` |
| 4 | Convention | `docs/CONVENTIONS.md` |
| 5 | Database design | `docs/DATABASE.md` |
| 6 | Git workflow | `docs/GIT-WORKFLOW.md` |
| 7 | Hướng dẫn Agent | `AGENTS.md` |
| 8 | Chi tiết từng phase | `docs/phases/phase-*.md` |
| 9 | Cursor rules | `.cursor/rules/*.mdc` |
| 10 | Cursor skills | `.cursor/skills/*/SKILL.md` |
| 11 | `.gitignore` cho .NET | `.gitignore` |
| 12 | Khung thư mục `src/`, `database/` | Placeholder / README con |

---

## Acceptance Criteria

- [ ] Developer đọc `README.md` + `docs/PHASES.md` hiểu được toàn bộ lộ trình
- [ ] Mỗi phase (0–10) có file `.md` riêng với **kế hoạch kiểm tra** cuối file
- [ ] `.cursor/rules/` có ít nhất 3 rule: core, C#/MVC, Razor
- [ ] `.cursor/skills/` có skill `implement-phase` và `phase-verification`
- [ ] `AGENTS.md` mô tả rõ agent phải đọc gì trước khi code
- [ ] Repo có `.gitignore` chuẩn Visual Studio
- [ ] Thư mục `src/`, `database/` đã tạo với README placeholder

---

## Công việc chi tiết

### 1. Tài liệu
- Hoàn thiện nội dung các file docs (đã tạo ở phase 0)
- Đảm bảo mapping yêu cầu mentor → phase rõ ràng

### 2. Cursor Agent setup
- Rules: always-apply core + file-specific cho `.cs` và `.cshtml`
- Skills: workflow implement từng phase + checklist verify

### 3. Git
- `git init`, branch `develop` và `main`
- Commit phase 0, hướng dẫn push lên GitHub/GitLab trong `GIT-WORKFLOW.md`

### 4. Khung thư mục
```
src/README.md              # Hướng dẫn tạo solution ở Phase 1
database/scripts/.gitkeep
database/procedures/.gitkeep
```

---

## Kế hoạch kiểm tra (Phase 0)

### A. Tài liệu

- [ ] `docs/PHASES.md` liệt kê đủ phase 0–10 với link tới file chi tiết
- [ ] Mỗi file `docs/phases/phase-*.md` có: Mục tiêu, Deliverables, Acceptance Criteria, **Kế hoạch kiểm tra**
- [ ] `ARCHITECTURE.md` có sơ đồ layer và ER diagram
- [ ] `CONVENTIONS.md` có ví dụ code C# và commit message

### B. Cursor Agent

- [ ] Mở project trong Cursor → rules `project-core` được apply
- [ ] Mở file `.cs` → rules `csharp-mvc` được suggest
- [ ] Skill `implement-phase` có bước: đọc phase file → branch → implement → checklist

### C. Repository

- [ ] `git status` sạch sau commit phase 0
- [ ] Không có file nhạy cảm trong repo
- [ ] `.gitignore` loại `bin/`, `obj/`, `.vs/`, `packages/`

### D. Review với mentor (nếu có)

- [ ] Mentor đồng ý cấu trúc phase và Git flow
- [ ] Xác nhận EF Code First vs Database First (khuyến nghị: Code First)

---

## Bước tiếp theo

→ **Phase 1:** [phase-01-scaffold-database.md](phase-01-scaffold-database.md) — Tạo solution MVC 4.8, EF6, kết nối Oracle, schema ban đầu.

---

**Hoàn thành phase khi:** 100% checklist trên ✅ + PR merged `develop`.
