# Architecture

## General principles

1. **One script = one responsibility.** No GodScript.
2. **EventBus for cross-module communication.** No direct reference between two
   non-hierarchical modules (e.g. Audio never references Combat; it listens to
   events).
3. **Abstract Base classes + project variants.** `EnemyBase` lives in the toolkit;
   `MyJamSlimeEnemy : EnemyBase` lives in the jam project.
4. **ScriptableObjects for externalized configuration.** No hardcoded values.
5. **ObjectPool for anything spawned at runtime.** No `Instantiate` or `Destroy`
   in a gameplay loop.

## Module coupling

```
Core
  ↑
  ├── Player, Combat, Entities, Audio, UI, Camera, Items, ...
  │     (may reference Core and publish to the EventBus)
  │
  └── No module inside Core references the other modules
```

More precisely:

- The **Core** module knows nothing about other modules.
- Other modules may reference **Core** and **Utils**.
- Modules don't reference each other directly, except for obvious hierarchies
  (e.g. `UI` may read `Combat.HealthComponent` because the HUD must display
  the health bar).
- To notify another module, **publish an event** through the EventBus.

This rule has one exception: gameplay modules can reference Combat and Player
because those are natural dependencies (an enemy hits a player, a weapon emits
damage). Decoupling them entirely would add overhead with no real benefit.

## GameObject lifecycle

1. A Bootstrapper in the "Boot" scene instantiates the persistent singletons
   (`GameManager`, `ObjectPool`, `AudioManager`, `SceneLoader`, ...).
2. The `Bootstrapper` loads the "MainMenu" scene through `SceneLoader.LoadScene`.
3. The user clicks "Play" -> `MainMenuController.OnPlay()` loads the "Gameplay"
   scene.
4. The Gameplay scene contains a `PlayerSpawnPoint` that spawns the Player prefab.
5. When the player dies, its `HealthComponent` notifies its listeners.
   `PlayerDeathHook` publishes a `PlayerDiedEvent` on the EventBus.
6. `GameOverWatcher` listens for that event and switches the `GameManager` to
   `GameState.GameOver`. The `UIManager` enables the GameOver screen.

## Global states

```
Boot -> MainMenu -> Loading -> Gameplay <-> Paused
                              -> GameOver
                              -> Win
                              -> Cutscene
```

Only `GameManager` may drive these transitions. Any interested module subscribes
to `GameStateChangedEvent`.

## Quality rules (NASA Power of 10, adapted)

Every script follows these rules. Justified exceptions are flagged inline with
a comment naming the rule (e.g. `// R3 exempt: ...`).

| # | Rule | Application |
|---|---|---|
| R1 | No recursion | Iterative loops only |
| R2 | Fixed loop bounds | Always a constant upper bound |
| R3 | No `Instantiate` / `Destroy` in gameplay | Use ObjectPool |
| R4 | Short functions | ~60 lines max, split otherwise |
| R5 | At least 2 `Debug.Assert` per non-trivial function | Validate preconditions |
| R6 | Minimal data scope | No needless statics |
| R7 | Check every return value | Explicit null checks |
| R8 | No complex macros | Plain `#define` only |
| R9 | Limit indirection | No hidden delegate chains |
| R10 | Zero warnings | Clean compilation |

The `// R5`, `// R2`, etc. comments inside the toolkit code mark the spots
where the rule is intentionally enforced.
