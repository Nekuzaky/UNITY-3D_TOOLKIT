# Camera

## Available modes

| Script | Use case |
|---|---|
| `CameraFollow` | Follows a target with offset, exponential smoothing |
| `OrbitCamera` | Third-person orbit (yaw + pitch via mouse/stick) |
| `FirstPersonCamera` | Snaps onto a child pivot (use `PlayerLook` for rotation) |
| `TopDownCamera` | Top-down (RTS, dungeon crawler) |

Pick one active mode. To switch dynamically between modes, use
`CameraRig.SetMode`.

## Bounds

`CameraBounds` clamps the camera in a box (min/max positions). Useful to limit
scrolling in closed arenas.

## Shake

```csharp
CameraShake.ActiveInstance.Shake(trauma: 0.4f);
```

`trauma` is a 0..1 parameter that accumulates and decays. Multiple Shake()
calls in parallel add up. Decay is configurable.

`CameraShakeOnEvent` automatically triggers a shake on:
- `PlayerDamage` (damage to the player)
- `EnemyKilled`
- `PlayerDied`

## Hitstop

`HitstopController`: very brief `Time.timeScale` slowdown on impacts. Adds
weight to big hits. Auto-triggers on `EnemyKilled` and `PlayerDied`, or call
`Trigger()` manually.

## Zoom (FOV)

`CameraZoom` changes the FOV based on an `InputAction` (scroll wheel by
default). Min/max + smoothing configurable.

## Switch by trigger

`CameraTrigger`: on volume enter, activate another Camera and disable the
current one. For shot changes in corridors / cinematic panels.

## Occlusion fader

`CameraOcclusionFader` makes Renderers between the camera and its target
transparent. Avoids walls blocking the view. Only works with materials that
support transparency (otherwise `material.color.a` is ignored).

## Auto-bind on Player

`CameraTargetSwitcher` listens to `PlayerSpawnedEvent` and calls `SetTarget`
on every referenced camera mode. No manual drag-and-drop.

## Recommended pattern

1. Main Camera with:
   - `CameraFollow` (or another mode)
   - `CameraBounds`
   - `CameraShake`
   - `CameraZoom`
2. A "CameraSystems" GameObject with:
   - `CameraTargetSwitcher`
   - `CameraShakeOnEvent`
   - `HitstopController`
3. The camera auto-binds to the player on spawn.
