# Git Workflow

## Branch model

```
main          ← production-ready (protected)
  └── develop ← integration branch (default cho PR)
        ├── feature/phase-02-auth
        ├── feature/phase-03-project
        └── fix/task-validation
```

## Quy trình

1. **Clone** repo, checkout `develop`
2. **Tạo branch** từ `develop`: `git checkout -b feature/phase-XX-mô-tả`
3. **Commit** thường xuyên, message theo [CONVENTIONS.md](CONVENTIONS.md)
4. **Push** và tạo **Pull Request** → target `develop`
5. **Mentor review** → request changes hoặc approve
6. **Merge** (squash hoặc merge commit — thống nhất team)
7. **Xóa branch** feature sau merge

## Quy tắc PR

- 1 PR ≈ 1 phase hoặc 1 feature nhỏ trong phase
- Mô tả rõ: phase, thay đổi, hướng dẫn test
- Không merge khi build fail
- Không commit: `bin/`, `obj/`, `packages/`, `.vs/`, connection string có password thật

## Protected branches (khuyến nghị trên GitHub)

| Branch | Rule |
|--------|------|
| `main` | Require PR, no direct push |
| `develop` | Require PR + 1 approval (mentor) |

## Khởi tạo repo lần đầu (Phase 0)

```bash
git init
git checkout -b develop
git add .
git commit -m "chore: phase 0 foundation - docs, rules, project structure"
# Tạo repo trên GitHub/GitLab rồi:
git remote add origin <url>
git push -u origin develop
git checkout -b main
git push -u origin main
```

## Tag release (sau phase 10)

```bash
git tag -a v1.0.0 -m "MVP deploy - mentor requirements complete"
git push origin v1.0.0
```
