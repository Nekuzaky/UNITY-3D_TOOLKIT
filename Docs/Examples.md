# Examples

Three micro-games you can prototype in a few hours with the toolkit.

## Example 1: Wave Survival

Waves of enemies, the player attacks them, score climbs. Game over on death.

**Ingredients:**
- 1 Player (PlayerController3D + HealthComponent + InputHandler + MeleeWeapon).
- 1 Enemy prefab (HealthComponent + SimpleAI + EnemyKilledHook + DespawnOnDeath).
- 1 WaveSpawner with a WaveConfig of 5 increasing waves.
- 1 ScoreUI + 1 HealthBarUI in the HUD.
- 1 GameOverWatcher.

**Loop:**
1. The player attacks with the MeleeWeapon (attack input).
2. The active hitbox deals damage. HealthComponent.OnDied -> KillReward adds
   to the score + EnemyKilledEvent.
3. WaveSpawner spawns more enemies per the WaveConfig.
4. When the player's HealthComponent reaches 0, PlayerDeathHook publishes
   PlayerDiedEvent. GameOverWatcher switches to GameState.GameOver. UIManager
   shows the GameOver panel.

## Example 2: Dungeon Crawler

Top-down, corridors, NavMesh enemies, chests.

**Ingredients:**
- 1 Player (PlayerCharacterController3D + Inventory).
- 1 TopDownCamera.
- Several ChestInteractable with LootTable assets.
- 1 NavMesh baked in the scene.
- Enemy with NavMeshChase + NavMeshAgent + AggroSystem.
- Several DoorInteractable requiring KeyItems.

**Loop:**
1. Player walks around, opens chests (PlayerInteraction + ChestInteractable).
2. Loot drops as items (LootTable.Roll). Auto-pickup via ItemPickup.
3. Melee combat against NavMesh enemies (FieldOfView + NavMeshChase).
4. A key opens the door to the boss.
5. Boss with BossBase + BossPhase for scripted phases.

## Example 3: Platformer

Jump, dash, double-jump, checkpoints, lava.

**Ingredients:**
- 1 Player (PlayerController3D + PlayerJump with coyote/buffer + PlayerDash).
- 1 CameraFollow.
- 1 KillVolume below the level (infinite fall).
- Several PlayerCheckpoint at respawn points.
- 1 PlayerRespawner.
- BounceZone for trampolines.
- DamageZone for lava.
- 1 WinTrigger at the level end.

**Loop:**
1. Player navigates with jump + dash.
2. Falling off the level or into lava -> death.
3. Respawn at the last PlayerCheckpoint via PlayerRespawner.
4. Reaching the WinTrigger -> GameState.Win.

## Common wiring

For any of these prototypes:

- `_Bootstrap` scene with the singletons (GameManager, ObjectPool, AudioManager,
  SceneLoader, UIManager, ScoreManager, GlobalConfigProvider, SaveManager).
- ObjectPool pre-populated with: Bullet, Enemy, Pickup, Explosion, DamageNumber.
- AudioDatabase with: sfx_jump, sfx_dash, sfx_hit, sfx_pickup, sfx_explosion,
  sfx_win, sfx_lose.
- AudioMixer with MasterVol / MusicVol / SfxVol exposed.
- AchievementManager with a few baseline achievements.

With this foundation, you can iterate on game content without touching the
systems.
