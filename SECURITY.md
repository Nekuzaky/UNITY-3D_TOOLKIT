# Security Policy

## Supported versions

This project is a personal toolkit for Unity 3D game jams. Only the `main`
branch is supported; no formal LTS branches exist.

## Reporting a vulnerability

If you find a security-sensitive issue (e.g. a script that could let a
malicious asset execute arbitrary code, or any unintended behaviour with
security implications), please report it privately rather than opening a
public issue.

You can report through one of the following channels:

1. **GitHub Private Vulnerability Reporting** (preferred):
   [Open a draft advisory](https://github.com/Nekuzaky/UNITY-3D_TOOLKIT/security/advisories/new)
   so the discussion stays private until a fix is published.
2. **GitHub issue with the `security` label** if the issue is low-impact and
   you are comfortable disclosing it publicly.

When reporting, please include:

- the affected script(s) and their path,
- the Unity / URP version you were testing on,
- a minimal reproduction (script, scene setup, or sample project),
- the impact you observed.

## Disclosure timeline

- Acknowledgement: within 7 days.
- Fix or mitigation: target 30 days for confirmed issues.
- Public advisory: published once a fix is available on `main`.

## Scope

In scope:

- C# scripts under `Assets/Scripts/`.
- Project settings, packages, and CI workflows under `.github/`.

Out of scope:

- Unity Engine bugs (please report to Unity directly).
- Third-party packages bundled by Unity (report to the package author).
- Security issues in user-supplied game content (your jam scripts, prefabs,
  scenes) that derive from this toolkit but aren't part of it.

## Hardening notes

The toolkit follows a simplified subset of the NASA Power of 10 rules
(documented in [Docs/Architecture.md](Docs/Architecture.md)) to limit common
classes of bug. None of those rules replace a security review.
