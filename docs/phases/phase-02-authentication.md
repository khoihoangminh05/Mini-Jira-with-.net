# Phase 2 — Authentication & Layout

**Trạng thái:** 🟡 Chờ test & PR

---

## Mục tiêu

Login, Logout, Đăng ký; Forms Authentication; Session; `_Layout` Bootstrap; menu theo trạng thái đăng nhập.

---

## Deliverables

- `AccountController`: Register, Login, Logout
- ViewModels: `LoginViewModel`, `RegisterViewModel`
- Password hashing (không plain text)
- `Views/Shared/_Layout.cshtml` + `_LoginPartial.cshtml`
- `[Authorize]` trên area cần bảo vệ
- Filter redirect nếu chưa login

---

## Acceptance Criteria (mentor)

- [ ] User tự đăng ký tài khoản mới
- [ ] Login thành công → redirect Dashboard/Home
- [ ] Logout xóa session/cookie
- [ ] Trang protected không vào được khi chưa login

---

## Kế hoạch kiểm tra (Phase 2)

### Chức năng
- [ ] Đăng ký username trùng → lỗi validation
- [ ] Đăng ký email sai format → lỗi client + server
- [ ] Login sai password → thông báo lỗi, không lộ user tồn tại hay không (tùy policy)
- [ ] Sau login, refresh trang vẫn giữ phiên
- [ ] Logout → không truy cập `/Project` được

### Validation
- [ ] Form Register/Login có `data-val` và server `ModelState`
- [ ] POST có `[ValidateAntiForgeryToken]`

### UI
- [ ] Layout responsive cơ bản (Bootstrap navbar)
- [ ] Flash message (TempData) khi đăng ký thành công

### Bảo mật
- [ ] Password lưu dạng hash trong DB
- [ ] Không log password

---

**Tiếp theo:** [Phase 3 — Project CRUD](phase-03-project-crud.md)
