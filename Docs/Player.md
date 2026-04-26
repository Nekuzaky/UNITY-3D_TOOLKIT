# Player

## Architecture

```
PlayerControllerBase (abstract)
├── PlayerController3D            (Rigidbody, physics-driven, can be knocked back)
└── PlayerCharacterController3D   (CharacterController, kinematic, snappier)
```

Pick one OR the other based on the desired feel.

Optional components (mix and match):

| Component | Purpose |
|---|---|
| `InputHandler` | Reads inputs (axes + buttons), centralized |
| `PlayerStats` | Runtime stats, fed by a `PlayerStatsConfig` |
| `PlayerJump` | Jump with coyote time + buffer + double-jump |
| `PlayerDash` | Directional dash with optional invuln |
| `PlayerStamina` | Regenerative stamina |
| `PlayerLook` | First-person look (yaw + pitch) |
| `PlayerCrouch` | CharacterController crouch |
| `PlayerInteraction` | IInteractable detection + event publishing |
| `PlayerAnimator` | Generic Animator bridge |
| `PlayerInputBuffer` | Input buffer (combos) |
| `PlayerCheckpoint` (on the checkpoint) | Saves last safe position |
| `PlayerRespawner` | Repositions on respawn |
| `PlayerSpawnPoint` (on the spawn) | Spawns the Player prefab on scene start |
| `PlayerInventoryHook` | Marks the active inventory |

## Minimum setup

Player prefab:
1. Tag `"Player"`.
2. Components:
   - `Rigidbody` (freezeRotation true, useGravity true)
   - `CapsuleCollider`
   - `InputHandler` + InputActionAsset
   - `PlayerStats` + `PlayerStatsConfig` SO
   - `PlayerController3D`
   - `Combat/HealthComponent`
   - `Combat/Knockback` + `KnockbackHook`
   - `Misc/PlayerDeathHook`

## InputActionAsset

The project already contains `Assets/InputSystem_Actions.inputactions`. Default
expected actions:

| Action | Type | Default |
|---|---|---|
| Move | Value Vector2 | WASD / Left stick |
| Jump | Button | Space / South |
| Sprint | Button | Shift / North |
| Attack | Button | LMB / West |
| Interact | Button | E / East |
| Dash | Button | LCtrl / RB |
| Pause | Button | Esc / Start |

Names can be changed in `InputHandler` if needed.

## Optional state machine

For more structured behavior:

```csharp
var sm = gameObject.AddComponent<PlayerStateMachine>();
sm.Register(new PlayerIdleState());
sm.Register(new PlayerRunState());
sm.Register(new PlayerJumpState());
sm.TransitionTo<PlayerIdleState>();
```

States live in `Player/States/`. Subclass to add logic.

## Stamina cost for Sprint / Dash

`PlayerStamina` is not auto-wired. To drain stamina while sprint is held:

```csharp
// in a custom routine
if (_input.SprintHeld && !_stamina.Consume(20f * Time.deltaTime)) {
    // not enough stamina, cancel sprint
}
```

## HUD wiring

`HUDController` listens to `PlayerSpawnedEvent` and auto-binds HealthBarUI /
StaminaBarUI to the spawned player.

To publish the event on a manual spawn:

```csharp
EventBus.Publish(new PlayerSpawnedEvent { Player = playerGO, Position = playerGO.transform.position });
```
