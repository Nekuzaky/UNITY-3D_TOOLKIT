# Modules

Detailed view of each module and its main scripts.

## Core

Anything with no business dependency.

| Script | Role |
|---|---|
| `GameManager` | Source of truth for `GameState`. Persistent singleton. |
| `GameState` | Enum (Boot / MainMenu / Gameplay / Paused / GameOver / Win / Cutscene). |
| `EventBus` | Strongly-typed event bus. All cross-module communication. |
| `ObjectPool` | Pool indexed by string key. All gameplay instantiation goes through it. |
| `Poolable` | Component on poolable prefabs (optional auto-return). |
| `PoolEntry` | Pool definition exposed in the `ObjectPool` inspector. |
| `SceneLoader` | Async scene loading with events. |
| `Singleton<T>` / `PersistentSingleton<T>` | Generic singleton bases. |
| `Bootstrapper` | Spawns persistent prefabs at startup. |
| `ServiceLocator` | Service registry (singleton alternative for testing). |
| `TickSystem` | Centralized tick (Update / FixedUpdate / LateUpdate). |
| `CoroutineRunner` | Run coroutines from non-MonoBehaviour callers. |
| `GlobalConfig` / `GlobalConfigProvider` | Project config (FPS, gravity, masks). |
| `ScoreManager` | Current score + high score (PlayerPrefs). |
| `GameClock` | Game clock independent of timeScale. |
| `AppQuitter` | `Quit()` cross-platform (Editor + build). |
| `Events/` | EventBus event definitions (`PlayerDied`, `EnemyKilled`, ...). |

## Player

Generic player controllers, meant to be derived for the jam.

| Script | Role |
|---|---|
| `InputHandler` | Reads the New Input System, exposes axes + buttons. |
| `PlayerStats` / `PlayerStatsConfig` | Runtime stats + config SO. |
| `PlayerControllerBase` | Abstract controller base. |
| `PlayerController3D` | Camera-relative 3D Rigidbody controller. |
| `PlayerCharacterController3D` | CharacterController variant, snappier. |
| `PlayerJump` | Extended jump: double-jump, coyote time, jump buffer. |
| `PlayerDash` | Directional dash with cooldown. |
| `PlayerStamina` | Regenerative stamina. |
| `PlayerLook` | First-person look (yaw + pitch). |
| `PlayerCrouch` | CharacterController crouch. |
| `PlayerInteraction` | IInteractable detection + event publishing. |
| `PlayerAnimator` | Generic Animator bridge (Speed, Grounded, Jump). |
| `PlayerInputBuffer` | Input buffer for combos. |
| `PlayerStateMachine` / `PlayerStateBase` + `States/` | Player state machine. |
| `PlayerSpawnPoint` / `PlayerRespawner` | Spawn and checkpoint respawn. |
| `PlayerCheckpoint` | Saves the last safe position. |
| `PlayerInventoryHook` | Marks the active inventory holder. |

## Combat

Everything related to damage and weapons.

| Script | Role |
|---|---|
| `IDamageable` / `IHealable` | Interfaces. |
| `DamageType` | Enum (Physical / Fire / Ice / Lightning / Poison / True). |
| `DamageInfo` | Struct carrying all hit details. |
| `HealthComponent` | Universal HP component with per-type resistances. |
| `HitboxController` | Active hitbox dealing damage on trigger enter. |
| `DamageHandler` | Static helper to apply damage. |
| `Knockback` / `KnockbackHook` | Knockback force + auto-wiring. |
| `WeaponBase` / `MeleeWeapon` / `RangedWeapon` | Weapon bases. |
| `ProjectileBase` / `BulletProjectile` / `HomingProjectile` / `GrenadeProjectile` | Projectiles. |
| `AreaDamage` / `DamageZone` / `HealZone` | Area-of-effect zones. |
| `DamageOverTime` | One-shot damage-over-time effect. |
| `StatusEffectBase` + `Burn` / `Poison` / `Stun` / `Freeze` | Status effects. |
| `ComboSystem` | Combo counter. |
| `WeaponConfig` | Config SO. |
| `InvincibilityFlash` | Post-hit blink. |
| `KillReward` | Score + EnemyKilledEvent on death. |
| `DespawnOnDeath` | Pool return or destroy after a delay. |

## Entities

Enemies, AI, spawners.

| Script | Role |
|---|---|
| `EnemyBase` | Abstract base (Health + Config + Target). |
| `EnemyConfig` | Config SO. |
| `StateMachine` / `StateBase` + `States/` | Idle, Patrol, Chase, Attack, Flee, Dead. |
| `SimpleAI` / `RangedAI` / `FlyingAI` / `TurretAI` | AI variants. |
| `AggroSystem` | Aggro on the last attacker. |
| `BossBase` / `BossPhase` | Multi-phase boss. |
| `TargetingSystem` / `TargetByTag` | Target resolution. |
| `EnemyHealthBarHook` | Local health bar. |
| `EnemyDeathExplosion` | AreaDamage on death. |
| `EnemySpawner` / `WaveSpawner` / `WaveConfig` | Spawn and waves. |
| `NavMeshChase` | Chase via NavMeshAgent. |
| `Lifetime` | Timed auto-despawn. |
| `EnemyAnimator` | Bridge Agent.velocity -> Animator.Speed. |
| `PatrolPath` | Visualized waypoint list. |

## UI

HUD and menus.

| Script | Role |
|---|---|
| `UIManager` | Switches panels per GameState. |
| `HUDController` | Auto-binds widgets to the player. |
| `HealthBarUI` / `StaminaBarUI` | Stat bars. |
| `ScoreUI` / `HighScoreUI` / `TimerUI` / `WaveUI` | Counters. |
| `ScreenFader` | Full-screen fade in/out. |
| `MenuButton` | UnityEvent wrapper. |
| `MainMenu` / `Pause` / `GameOver` / `Win` Controllers | Menu buttons. |
| `LoadingScreenUI` | Loading progress slider. |
| `DamageNumberPopup` / `DamageNumberSpawner` | Floating damage numbers. |
| `BillboardUI` | Forces 3D UI to face the camera. |
| `TooltipUI` / `TooltipTrigger` | Global tooltip. |
| `NotificationUI` | Notification queue. |
| `DialogueBoxUI` | Typing-effect dialog box. |
| `CrosshairUI` | Crosshair colored by what's targeted. |
| `InteractionPromptUI` | "Press E" when an Interactable is in range. |
| `PauseInputBinding` | Maps an InputAction to TogglePause. |

## Audio

| Script | Role |
|---|---|
| `AudioManager` | Singleton with SFX pool + music crossfade. |
| `SoundClipConfig` / `AudioDatabase` | Definition SO + index. |
| `SfxOnEvent` | Auto SFX on EventBus event. |
| `MusicTrigger` | Plays music on trigger enter. |
| `PlayMusicOnStateChange` | Music per GameState. |
| `VolumeController` | Slider <-> AudioMixer (saved in PlayerPrefs). |
| `FootstepPlayer` | Steps based on speed. |
| `AmbientPlayer` | Looping ambient track. |
| `OneShotAudio` | Static helper for PlayClipAtPoint. |
| `PitchedRandomizer` | Pitch + volume variation. |

## Camera

| Script | Role |
|---|---|
| `CameraFollow` / `OrbitCamera` / `FirstPersonCamera` / `TopDownCamera` | Modes. |
| `CameraRig` | Switches between modes. |
| `CameraTargetSwitcher` | Re-binds to the player on spawn. |
| `CameraBounds` | Clamps inside a box. |
| `CameraShake` / `CameraShakeOnEvent` | Trauma + event listening. |
| `CameraZoom` | Dynamic FOV (scroll). |
| `CameraTrigger` | Camera switch on trigger. |
| `CameraOcclusionFader` | Fades walls between camera and target. |
| `HitstopController` | Mini slowdown on impact. |

## Utils

| Script | Role |
|---|---|
| `Timer` / `Cooldown` / `Stopwatch` / `TimedAction` | Timing. |
| `MathHelper` / `Easing` / `BezierCurve` / `WeightedRandom` | Math. |
| `VectorExtensions` / `TransformExtensions` / `GameObjectExtensions` / `ListExtensions` / `StringExtensions` / `ColorExtensions` | Extensions. |
| `LayerMaskHelper` / `PlayerPrefsExtensions` | Helpers. |
| `Rotator` / `Bobber` / `LookAt` / `Billboard` | Procedural animations. |
| `AutoDestroy` / `DontDestroyOnLoadComponent` | Lifecycle. |
| `GizmoDrawer` / `DebugDraw` / `FrameRateMonitor` | Debug. |
| `RandomGenerator` | Seedable RNG. |
| `ToggleOnState` | Toggle GameObject by GameState. |

## Data

| Script | Role |
|---|---|
| `ISaveable` / `SaveData` / `SaveManager` | JSON save. |
| `SaveableTransform` / `SaveableHealth` / `SaveableScore` | Implementations. |
| `LocalizationKey` / `LocalizationTable` / `LocalizationManager` / `LocalizedText` | I18N. |
| `DifficultyConfig` / `DifficultyManager` | Runtime multipliers. |
| `LevelConfig` / `LevelDatabase` / `LevelProgress` | Level index. |

## Physics

| Script | Role |
|---|---|
| `BounceZone` / `PushZone` / `GravityZone` / `WaterFloat` | Force zones. |
| `KillVolume` / `TeleportVolume` | Special triggers. |
| `TriggerOnce` / `TriggerEvents` | UnityEvents on trigger. |
| `GroundCheck` | Raycast ground detection. |
| `RaycastHelper` | Physics.Raycast wrappers. |
| `PhysicsTimer` | UnityEvent after a delay in FixedTime. |

## Items

| Script | Role |
|---|---|
| `ItemBase` / `ConsumableItem` / `WeaponItem` / `KeyItem` | SO definitions. |
| `ItemRarity` / `ItemDatabase` | Enum + index. |
| `Inventory` / `InventorySlot` | Grid inventory. |
| `InventoryUI` / `InventorySlotUI` | Inventory UI. |
| `ItemPickup` / `LootTable` / `LootDropper` | Pickup and loot. |
| `HotbarController` / `EquipmentManager` | Quick select + equip. |

## Interaction

| Script | Role |
|---|---|
| `IInteractable` / `InteractableBase` | Base. |
| `Door` / `Chest` / `Lever` / `PressurePlate` / `Teleporter` / `Dialogue` / `Pickup` / `Button` | Variants. |
| `InteractableHighlight` | Material highlight when targeted. |

## VFX

| Script | Role |
|---|---|
| `VFXSpawner` | Static helper to pop a pooled VFX. |
| `VFXOnEvent` | Reacts to EventBus events. |
| `VFXParticleAutoReturn` | Returns to pool when ParticleSystem ends. |
| `TrailEffect` | TrailRenderer driven by speed. |
| `HitFlash` / `ScreenFlash` | Impact flashes. |
| `DecalSpawner` / `LineRenderEffect` | Decals + beams. |
| `PostProcessPulse` | Pulse via material property. |

## Spawning / Procedural

| Script | Role |
|---|---|
| `SpawnPoint` | Marker. |
| `TimedSpawner` / `TriggerSpawner` / `AreaSpawner` / `PathSpawner` | Spawners. |
| `TileGenerator` / `RoomGenerator` / `RandomScatter` | Procedural. |
| `DespawnNotifier` | Helper to track a pooled GO's lifetime. |

## AI / Pathfinding

| Script | Role |
|---|---|
| `WaypointPath` / `WaypointFollower` | Patrol. |
| `FieldOfView` / `LineOfSight` | Detection. |
| `NavMeshAgentController` / `NavMeshWander` | NavMesh wrappers. |
| `BehaviorTree/` (BTNode, BTSequence, BTSelector, BTAction, BTCondition, BTRunner) | Mini Behavior Tree. |

## Quest

| Script | Role |
|---|---|
| `QuestState` | Enum. |
| `QuestObjective` + `Kill` / `Collect` / `Reach` | Objectives. |
| `QuestBase` / `QuestManager` | Definition + tracking. |
| `QuestStartTrigger` / `QuestUI` | Start and display. |

## Time

| Script | Role |
|---|---|
| `DayNightCycle` | Day/night cycle (light + ambient). |
| `TimeOfDayEvents` | UnityEvents at specific times. |
| `WeatherSystem` | Clear/Rain/Storm cycle. |
| `GameTimer` | Countdown timer. |
| `TimeScaleController` | Slow-mo / pause helpers. |

## Misc

| Script | Role |
|---|---|
| `AchievementBase` / `AchievementManager` / `AchievementHooks` / `AchievementToastUI` | Achievements. |
| `LogManager` | Logs with module + level. |
| `DebugMenu` / `CheatCodes` | Developer tools. |
| `PerformanceOverlay` | FPS + memory. |
| `GameOverWatcher` / `WinTrigger` | End state. |
| `PlayerDeathHook` / `EnemyKilledHook` | Bridge HealthComponent -> EventBus. |
| `ScreenshotCapture` | Capture on key press. |
