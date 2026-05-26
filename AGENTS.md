# AGENTS.md — AI agent instructions for CanKonkuk

Purpose
- Provide concise, actionable guidance for AI coding agents working in this repository.

Quick facts
- Unity editor: see [ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt#L1-L2)
- Main code: [Assets/Player.cs](Assets/Player.cs)
- Project file: [Assembly-CSharp.csproj](Assembly-CSharp.csproj)

Quick start (human)
- Open the project in Unity Hub (version from ProjectVersion.txt). Use Unity Editor for scene edits and builds.
- Open the project folder in VS Code for code edits.

Build & run (CLI)
- Typical Unity CLI build (example for Windows):
  - `"C:/Program Files/Unity/Hub/Editor/<VERSION>/Editor/Unity.exe" -projectPath <repo-root> -buildTarget Win64 -quit -batchmode -nographics -executeMethod BuildScript.PerformBuild`
- Agents should not run the Unity Editor on CI/machine without explicit user permission.

Project notes for agents
- This is a Unity game project. Primary source lives in `Assets/` and settings in `ProjectSettings/`.
- Avoid editing or committing `Library/`, `Temp/`, `obj/`, or `UserSettings/` — they are generated.
- Use [Assembly-CSharp.csproj](Assembly-CSharp.csproj) for IDE integration and language/version hints.
- Scenes and assets are binary; prefer editing C# scripts and referencing scene names rather than modifying scenes unless asked.

Conventions & etiquette
- Keep changes minimal and focused; run localized edits to C# scripts under `Assets/`.
- If a change requires rebuilding in Unity, request confirmation before running Unity Editor or CI builds.
- Link to existing docs instead of copying large text. See `ProjectSettings/` for configuration.

Common tasks an agent can perform
- Fix and refactor C# scripts under `Assets/`.
- Add or update editor scripts that don't require Unity Editor GUI interaction.
- Propose changes to scene setup as text-based instructions or patch serialized YAML only when necessary.

Contact / next steps
- Ask the repo owner before performing Editor-level operations (builds, scene edits, package installs).
