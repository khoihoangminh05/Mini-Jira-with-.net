# Phase 10 — Deploy

**Branch:** `feature/phase-10-deploy`  
**Phụ thuộc:** Phase 6 tối thiểu (MVP); Phase 9 khuyến khích  
**Ước lượng:** 4–5 ngày

---

## Mục tiêu

Deploy app lên internet: AWS EC2 Windows/IIS hoặc Azure App Service. HTTPS, connection Oracle cloud, CI build cơ bản.

---

## Deliverables

- Publish profile / script deploy
- `Web.Release.config` transform connection string
- IIS site chạy MVC 4.8
- Domain hoặc public IP + HTTPS (Let's Encrypt / ACM)
- `docs/DEPLOY.md` hướng dẫn tái deploy
- GitHub Action build (optional)

---

## Acceptance Criteria

- [ ] URL public truy cập được từ internet
- [ ] Login + Kanban + Report hoạt động trên môi trường production
- [ ] HTTPS bật, HTTP redirect HTTPS

---

## Kế hoạch kiểm tra (Phase 10)

### Infrastructure
- [ ] App pool .NET 4.x, pipeline integrated
- [ ] Oracle firewall cho phép EC2 connect
- [ ] Connection string production qua env/transform — không trong Git

### Smoke test production
- [ ] Register user mới
- [ ] CRUD project + task
- [ ] Kanban đổi status
- [ ] Report % đúng

### Monitoring cơ bản
- [ ] Log lỗi 500 ghi vào file hoặc CloudWatch
- [ ] Backup DB script documented

### Release
- [ ] Tag `v1.0.0` trên `main`
- [ ] README có link demo (nếu public)

---

## Hoàn thành dự án

Khi phase 10 pass → cập nhật `docs/PHASES.md` tất cả phase 🟢.
