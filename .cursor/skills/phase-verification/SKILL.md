---
name: phase-verification
description: Runs the verification checklist before merging a Personal Task Manager phase. Use before PR, after completing a phase, or when the user asks to verify or test a phase.
---

# Phase Verification

## Steps

1. Open `docs/phases/phase-XX-*.md` for the active phase
2. Copy **Kế hoạch kiểm tra** section into response as checklist
3. Execute each item (build, manual test, code review)
4. Mark pass/fail; fix failures before PR

## Universal checks (every phase)

- [ ] Solution builds Debug without errors
- [ ] No secrets in git diff
- [ ] Code follows `docs/CONVENTIONS.md`
- [ ] Business logic methods have comments where non-obvious
- [ ] PR description includes: phase, changes, how to test

## Security (phase 2+)

- [ ] `ModelState.IsValid` on POST forms
- [ ] Anti-forgery on POST
- [ ] User A cannot access User B data (test URL tampering)

## Report format

```markdown
## Phase X Verification

| Check | Status | Notes |
|-------|--------|-------|
| Build | ✅/❌ | |
| ... | | |

**Gate:** Ready for PR / Blocked — <reason>
```

## Phase gate

All items in phase file + universal checks must pass before merge to `develop`.
