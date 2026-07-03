# Coding Conventions

> Agent và developer phải tuân thủ file này. Cursor rules trong `.cursor/rules/` mirror các quy tắc quan trọng nhất.

## C# naming

| Loại | Convention | Ví dụ |
|------|------------|-------|
| Class, Method, Property | PascalCase | `TaskController`, `GetByProjectId()` |
| Parameter, local variable | camelCase | `projectId`, `taskList` |
| Private field | `_camelCase` | `_taskRepository` |
| Interface | `I` + PascalCase | `ITaskRepository` |
| Constant | PascalCase | `MaxTitleLength` |
| Enum type | PascalCase | `TaskPriority` |
| Enum member | PascalCase | `High`, `InProgress` |

## C# style

```csharp
// ✅ Controller action — validate trước, logic sau
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(TaskCreateViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    // Business logic: gọi repository, không query EF trực tiếp trong View
    var task = _taskRepository.Create(model, CurrentUserId);
    return RedirectToAction("Index", new { projectId = model.ProjectId });
}
```

**Quy tắc:**
- Một file = một public class chính (trừ nested class nhỏ)
- Comment **tiếng Việt hoặc Anh** ở method có logic nghiệp vụ (không comment code hiển nhiên)
- Không dùng `var` khi kiểu không rõ ràng từ RHS
- `async`/`await` chỉ khi có I/O thật (phase sau nếu cần)
- Không swallow exception — log và xử lý phù hợp

## Razor / View

- ViewModel riêng cho mỗi form — **không** bind trực tiếp Entity vào View
- Dùng `@Html.LabelFor`, `@Html.TextBoxFor`, `@Html.ValidationMessageFor`
- Partial view prefix `_`: `_TaskCard.cshtml`
- Layout chung: `Views/Shared/_Layout.cshtml`

## LINQ

```csharp
// ✅ Luôn filter theo user sở hữu project
var tasks = _context.Tasks
    .Where(t => t.Project.UserId == userId && !t.IsDeleted)
    .OrderBy(t => t.SortOrder)
    .ToList();

// ❌ Không query thiếu điều kiện UserId
var tasks = _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
```

## Git

| Loại branch | Pattern | Ví dụ |
|-------------|---------|-------|
| Feature | `feature/<mô-tả-ngắn>` | `feature/phase-02-auth` |
| Fix | `fix/<mô-tả>` | `fix/task-deadline-validation` |
| Chore | `chore/<mô-tả>` | `chore/phase-00-foundation` |

### Commit message

```
<type>(<scope>): <mô tả ngắn>

[type]: feat | fix | docs | chore | refactor | test
[scope]: auth | project | task | kanban | db | deploy
```

Ví dụ:
```
feat(auth): add register with email validation
fix(task): enforce deadline not in the past on server
docs(phases): add phase 5 verification checklist
```

## Pull Request template (tóm tắt)

```markdown
## Phase
Phase X — <tên>

## Thay đổi
- ...

## Cách test
1. ...
2. ...

## Checklist
- [ ] Build OK
- [ ] Server-side validation
- [ ] Không leak data cross-user
```

## Validation bắt buộc

Mọi form CRUD:

1. **Client:** `[Required]`, `[StringLength]`, jQuery unobtrusive
2. **Server:** `if (!ModelState.IsValid) return View(model);`
3. **Anti-forgery:** `[ValidateAntiForgeryToken]` trên POST

## File đặt tên

| Loại | Pattern |
|------|---------|
| Controller | `{Name}Controller.cs` |
| ViewModel | `{Action}{Entity}ViewModel.cs` |
| View | `{Action}.cshtml` |
| Repository | `{Entity}Repository.cs` |

## Oracle / SQL scripts

- File DDL: `database/scripts/001_create_users.sql`
- Stored procedure: `database/procedures/PKG_TASK.pks` / `.pkb`
- Mỗi script có header comment: mục đích, author, date

## Agent khi generate code

1. Đọc `AGENTS.md` và phase hiện tại trong `docs/phases/`
2. Không thêm feature ngoài scope phase (trừ khi user yêu cầu)
3. Giữ diff nhỏ, focused
4. Không commit connection string thật
5. Ưu tiên pattern đã có trong codebase
