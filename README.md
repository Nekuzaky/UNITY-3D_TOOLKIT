# 3D GAMEJAM TOOLKIT

> A Unity 3D toolkit of 370+ generic, ready-to-use C# scripts covering every
> major 3D game genre: action, RPG, racing, tactical, puzzle, stealth, survival,
> crafting, RTS, tower defense, rhythm, cards, sports, local multiplayer.
> All modules are decoupled, scriptable-friendly, with no unvetted external
> dependencies.

---

## Why this toolkit?

- Start a game jam in minutes instead of hours.
- Avoid re-writing the same building blocks (pool, event bus, save, AI, UI...).
- Give the Game Designer ready-to-configure inspector solutions.
- Enforce a clean architecture that stays maintainable even under sprint pressure.

**This toolkit does not contain any game-specific logic.** All modules are
generic and adaptable. Base classes (`EnemyBase`, `WeaponBase`, ...) are meant
to be extended by project classes.

---

## Stack

- Unity 2020.3 LTS or higher (tested on Unity 6 / URP 17)
- C# only, no unvetted external dependencies
- Universal Render Pipeline (URP)
- New Input System (`com.unity.inputsystem`)
- AI Navigation (`com.unity.ai.navigation`)
- TextMesh Pro (included with `com.unity.ugui`)

---

## Getting started

1. Clone the repo into `Assets/Scripts/` (or copy the folder).
2. Add an empty GameObject in the scene for `Bootstrapper`, `GameManager`,
   `ObjectPool`, `SceneLoader`, `AudioManager`.
3. Configure a `GlobalConfig` (ScriptableObject) and a `GlobalConfigProvider`.
4. Create a "Boot" scene and a "MainMenu" scene.
5. Drop a `PlayerSpawnPoint` + a Player prefab in the gameplay scene.
6. Follow the per-module guides in [Docs/](Docs/).

---

## Architecture

```
Assets/Scripts/
├── AI/                Behavior Tree, NavMesh, FieldOfView, Waypoints
├── Audio/             AudioManager, SFX pool, Music crossfade, Mixer
├── Camera/            Follow, Orbit, FirstPerson, TopDown, Shake, Hitstop
├── CardGame/          CardDefinition, Deck, Hand, CardPlayer, HandLayout
├── Combat/            HealthComponent, Damage, Weapons, Projectiles, Status Effects
├── Core/              GameManager, EventBus, ObjectPool, SceneLoader, Singleton
├── Crafting/          Recipe, RecipeDatabase, CraftingStation, ResourceNode
├── Data/              SaveManager, Localization, LevelDatabase, Difficulty
├── Entities/          EnemyBase, BossBase, AI variants, Spawners, Waves
├── Grid3D/            GridCoord, GridSettings, Occupancy, Pathfinder, Mover
├── Interaction/       IInteractable, Door, Chest, Lever, Pressure plate, Dialogue
├── Items/             Inventory, Pickups, LootTable, Equipment
├── LocalMultiplayer/  LocalPlayerManager, SplitScreenSetup, ScoreBoard
├── Misc/              Achievements, Cheat codes, Debug menu, Performance overlay
├── Physics/           Triggers, Bounce / Push / Gravity / Kill / Teleport zones
├── Player/            Controller3D, CharacterController3D, Look, Stats, Stamina
├── Progression/       LevelTable, ExperienceSystem, SkillTree, StatModifier
├── Puzzle/            Grabbable, WeightSwitch, LockChain, ResetZone, ColorChannel
├── Quest/             QuestBase, Objectives, Manager, UI, Triggers
├── Racing/            RaceCheckpoint, LapTracker, RaceManager, RaceUI
├── Rhythm/            BeatClock, NoteSpawner, TimingJudge, RhythmScore
├── RTS/               Selectable, SelectionManager, OrderHandler, Economy, Placer
├── Spawning/          SpawnPoint, Timed / Trigger / Area / Path / Procedural
├── Sports/            Goal, BallController, MatchScore
├── Stealth/           AwarenessLevel, SuspicionMeter, NoiseEmitter, HidingSpot
├── Survival/          Need, NeedsManager, NeedDamageOnDeplete, WarmthZone
├── Time/              DayNightCycle, Weather, GameTimer, TimeScale
├── TowerDefense/      TowerSlot, TowerConfig, TowerShooter
├── TurnBased/         TurnManager, ActionPoints, TurnActorBase, TurnUI
├── UI/                HUD, Menus, Bars, Score, Loading, Dialogue, Tooltip
├── Utils/             Timer, Cooldown, Extensions, Math, Easing, Bezier, Random
├── Vehicles/          Car, Boat, Plane, Hover, VehicleStats
└── VFX/               Particle pool, Trail, Hit flash, Screen flash, Decals
```

See [Docs/Architecture.md](Docs/Architecture.md) for the architecture rules.

---

## Naming conventions

| Type | Convention |
|---|---|
| `namespace` | PascalCase |
| `class` | PascalCase |
| `Method` | PascalCase |
| `public` field | PascalCase |
| `private` field | _camelCase |
| `protected` field | _camelCase |
| local variable | camelCase |
| Array (private) | _camelCases |
| List (private) | _camelCaseList |
| Dictionary (private) | _camelCaseDict |

See [Docs/CodingConventions.md](Docs/CodingConventions.md) for details.

---

## Documentation

| Document | Contents |
|---|---|
| [Architecture](Docs/Architecture.md) | Architecture rules, EventBus, ObjectPool |
| [QuickStart](Docs/QuickStart.md) | Step-by-step to start a jam |
| [CodingConventions](Docs/CodingConventions.md) | Naming, structure, quality |
| [Modules](Docs/Modules.md) | Per-module overview |
| [EventBus](Docs/EventBus.md) | How to publish / subscribe to events |
| [ObjectPool](Docs/ObjectPool.md) | How to configure and use the pool |
| [SaveSystem](Docs/SaveSystem.md) | How to save / load |
| [Combat](Docs/Combat.md) | Damage, hitbox, projectiles, statuses |
| [Player](Docs/Player.md) | Controllers, stats, input, states |
| [UI](Docs/UI.md) | HUD, menus, screens |
| [Audio](Docs/Audio.md) | Manager, SFX, music, mixer |
| [Camera](Docs/Camera.md) | Modes, shake, transitions |
| [AI](Docs/AI.md) | StateMachine, BehaviorTree, NavMesh, vision |
| [Genres](Docs/Genres.md) | Which modules to pick per game genre |
| [Cheatsheet](Docs/Cheatsheet.md) | Snippets for the most common operations |
| [Examples](Docs/Examples.md) | Three micro-game prototypes |

---

## License

See [LICENSE](LICENSE).
