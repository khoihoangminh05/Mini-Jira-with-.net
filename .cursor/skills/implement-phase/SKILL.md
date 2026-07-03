---
name: implement-phase
description: Implements a Personal Task Manager roadmap phase in ASP.NET MVC 4.8. Use when starting or continuing work on phase 1-10, scaffolding features, or when the user references docs/phases or PHASES.md.
---

# Implement Phase

## Prerequisites

1. Read `AGENTS.md`
2. Read `docs/PHASES.md` — identify current phase
3. Read `docs/phases/phase-XX-*.md` for deliverables and acceptance criteria
4. Read `docs/ARCHITECTURE.md` and `docs/CONVENTIONS.md`

## Workflow

```
Task Progress:
- [ ] Confirm phase number with user or PHASES.md status
- [ ] Create branch: feature/phase-XX-short-name
- [ ] List deliverables from phase file
- [ ] Implement only in-scope items
- [ ] Run phase-verification skill checklist
- [ ] Summarize changes and test steps for PR
```

## Implementation rules

- **Web:** `src/PersonalTaskManager.Web/` — Controllers, Views, ViewModels
- **Core:** Entities, interfaces, enums
- **Infrastructure:** DbContext, repositories
- **Database:** scripts in `database/scripts/`

## Phase boundaries

| Phase | Do | Don't |
|-------|-----|-------|
| 1 | Solution, EF, schema | Auth UI |
| 2 | Login/register only | Project CRUD |
| 5 | Button status change | Drag-drop (phase 8) |
| 7 | Ajax status | Full SPA rewrite |

## After implementation

Invoke verification: read `.cursor/skills/phase-verification/SKILL.md`

Do not commit or push unless user requests.

## Reference

- DB: [docs/DATABASE.md](../../../docs/DATABASE.md)
- Git: [docs/GIT-WORKFLOW.md](../../../docs/GIT-WORKFLOW.md)
