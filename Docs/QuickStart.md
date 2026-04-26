# QuickStart

How to start a game jam in 15 minutes with the toolkit.

## 1. Create the Boot scene

1. Create a `Boot.unity` scene.
2. Add an empty GameObject named `[Bootstrap]`:
   - `Bootstrapper` component.
   - `_persistentPrefabs` field: drag the `_GameManager`, `_ObjectPool`,
     `_SceneLoader`, `_AudioManager`, `_GlobalConfigProvider`, `_ScoreManager`,
     `_SaveManager`, `_UIManager` prefabs (see below).
   - `_firstSceneName`: `"MainMenu"`.

## 2. Prepare the persistent prefabs

Create one prefab per singleton, each with its script and references:

| Prefab | Main script |
|---|---|
| `_GameManager` | `Core/GameManager` |
| `_ObjectPool` | `Core/ObjectPool` (pool config inside) |
| `_SceneLoader` | `Core/SceneLoader` |
| `_AudioManager` | `Audio/AudioManager` (with its 2 music sources) |
| `_ScoreManager` | `Core/ScoreManager` |
| `_GlobalConfigProvider` | `Core/GlobalConfigProvider` (assign a GlobalConfig SO) |
| `_SaveManager` | `Data/SaveManager` |
| `_UIManager` | `UI/UIManager` (with the HUD/Menu/... panels) |

## 3. Create the MainMenu scene

1. Add a Canvas with a `MainMenu` panel.
2. "Play" button -> `MainMenuController.OnPlay()` (set `_gameplaySceneName` to
   `"Gameplay"`).
3. "Quit" button -> `MainMenuController.OnQuit()`.

## 4. Create the Gameplay scene

1. Drop a `PlayerSpawnPoint` in the scene with your Player prefab referenced.
2. The Player prefab needs at least:
   - `Player/InputHandler` (with an InputActionAsset assigned).
   - `Player/PlayerStats` (with a `PlayerStatsConfig` SO).
   - `Player/PlayerController3D` (Rigidbody) **or** `PlayerCharacterController3D`.
   - `Combat/HealthComponent`.
   - `Misc/PlayerDeathHook` (publishes the death event).
   - `"Player"` tag.
3. Camera: add a `CameraFollow` or `OrbitCamera`. Also add a
   `CameraTargetSwitcher` so it auto-follows the player on spawn.
4. Add a `Misc/GameOverWatcher` somewhere (switches to GameState.GameOver on
   `PlayerDiedEvent`).
5. Add an objective (`Misc/WinTrigger`) to trigger victory.

## 5. Enemies

1. Create an Enemy prefab with:
   - `Combat/HealthComponent`.
   - `Entities/SimpleAI` or `RangedAI` or `FlyingAI`.
   - An `EnemyConfig` SO.
   - `"Enemy"` tag.
   - `Misc/EnemyKilledHook` to publish the death event.
   - `Combat/DespawnOnDeath` to return to the pool.
2. Register the prefab in `ObjectPool` with a key (e.g. `"Enemy_Slime"`).
3. Drop a `Entities/EnemySpawner` or `WaveSpawner` in the scene to make enemies
   appear.

## 6. HUD UI

1. In the UIManager, configure a HUD panel with:
   - `UI/HealthBarUI` connected to the player's HealthComponent.
   - `UI/ScoreUI` listening to `ScoreChangedEvent`.
   - `UI/HUDController` that auto-binds to the player on every spawn.

## 7. Audio

1. Add an `AudioMixer` to the project, expose `MasterVol`, `MusicVol`, `SfxVol`.
2. Create an `AudioDatabase` SO and fill it with your `SoundClipConfig` entries.
3. Assign the database to the `AudioManager`.
4. To play a sound on event, add an `Audio/SfxOnEvent` listening to e.g.
   `EnemyKilled` -> id `"sfx_explosion"`.

## 8. Save

To save end-of-level state:

1. Add a `SaveManager` (already in the persistent prefabs).
2. On objects to save, add `SaveableTransform`, `SaveableHealth`,
   `SaveableScore` as needed.
3. Call `SaveManager.Instance.Save(0)` when you want to persist.
4. At the start of the Gameplay scene, call `SaveManager.Instance.Load(0)` if
   the slot exists.

## 9. Test in 5 minutes

- Open the Boot scene and hit Play.
- Check that the menu shows up, that the "Play" button loads Gameplay.
- Check that the player spawns, that the enemy detects you and hits you.
- Check that damage numbers appear (DamageNumberSpawner) and that the screen
  shake fires (CameraShakeOnEvent).
- Check that death triggers GameOver.

If everything works: you're good to go, iterate on game content.
