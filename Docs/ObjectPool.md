# ObjectPool

## Why

`Instantiate` and `Destroy` are expensive: GC allocation, component init,
physics teardown. In gameplay (bullets, particles, spawned enemies), this is
unacceptable. The pool re-uses instances by deactivating them rather than
destroying them.

## Configuration

1. Drop a `Core/ObjectPool` into your persistent singleton GameObject.
2. In the inspector, edit the `_entryList`:
   - **Key**: unique pool name (e.g. `"Bullet"`, `"Slime"`).
   - **Prefab**: prefab to pool (must have a `Poolable`, or one will be added).
   - **InitialSize**: number of instances pre-spawned at boot.
   - **MaxSize**: hard cap (above this, returned objects are destroyed).

Example:

| Key | Prefab | Initial | Max |
|---|---|---|---|
| Bullet | Bullet.prefab | 32 | 256 |
| Slime | Slime.prefab | 8 | 64 |
| Explosion | Explosion.prefab | 6 | 32 |
| ItemPickup | ItemPickup.prefab | 16 | 128 |

## Usage

### Get

```csharp
var go = ObjectPool.Instance.Get("Bullet", spawnPos, spawnRot);
if (go == null) return;
// ... configure the GO (e.g. projectile.Launch)
```

### Return

Three ways:

1. **Timed auto-return** (recommended for bullets, VFX): set
   `_autoReturnSeconds` on the prefab's `Poolable`.
2. **Manual**: `ObjectPool.Instance.Return("Bullet", go)`.
3. **Helper**: `go.GetComponent<Poolable>().ReturnToPool()`.

## Pre-warm

The pool pre-spawns `InitialSize` instances at startup. To avoid spawn spikes
in-game (CPU + GC stutter), bump `InitialSize` to cover the highest expected
peak.

## Inspect the pool

```csharp
ObjectPool.Instance.HasPool("Bullet");        // bool
ObjectPool.Instance.CountAvailable("Bullet"); // int (free instances)
```

## When to skip the pool

Pooling is unnecessary for:
- Scene initialization (level generator, static decor).
- Weapon equipping (one-shot at equip time).
- Scripted cinematics.

In those cases, `Instantiate` is fine. Document with `// R3 exempt: <reason>`.

## Common pitfalls

- **Forgetting to reset components on Get.** A reused projectile keeps its
  state. Solution: re-init in `OnEnable` or in the `Launch()` method.
- **Returning an already-disabled GO.** Check `Poolable.IsActiveInPool` before.
- **Spawning from an unknown key.** `ObjectPool.Get` returns `null` and logs a
  warning. Always check `if (go == null)`.
- **Capacity reached.** If `MaxSize` is too low, the pool destroys the
  excess returns. Increase `MaxSize` or lower spawn frequency.
