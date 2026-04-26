# Combat

## HealthComponent

The heart of the system. Every "destructible" object (player, enemy, breakable
wall) carries one.

Main fields:
- `_maxHealth`: max HP
- `_invincibilityAfterHit`: post-hit invuln window (default 0.1s)
- `_physicalMult / _fireMult / ...`: per-DamageType multipliers
  (0 = immune, 1 = normal, 2 = double damage)

Events:
- `OnHealthChanged(current, max)`: for the health bar
- `OnDamageTaken(DamageInfo)`: for knockback, hit flash
- `OnDied()`: for despawn, loot, score

## DamageInfo

Every hit carries a `DamageInfo`:

```csharp
new DamageInfo
{
    Amount = 10f,
    Type = DamageType.Fire,
    Source = attackerGO,
    HitPoint = hit.point,
    HitNormal = hit.normal,
    Knockback = 5f,
    IsCritical = false
}
```

Quick damage application:

```csharp
DamageHandler.TryDamage(target, amount: 10f, source: gameObject);
DamageHandler.TryDamage(target, info: dmgInfo);
```

## Hitbox

A hitbox is a trigger Collider + a `HitboxController`:

- `_damage`, `_damageType`, `_knockback`
- `_targetMask`: target layers
- `_tickInterval`: if the hitbox stays active (DoT zone), delay before
  re-applying to the same target
- `_owner`: prevents self-damage

Toggle via `Activate()` / `Deactivate()` (see `MeleeWeapon`).

## Weapons

```
WeaponBase (abstract)
├── MeleeWeapon  -> activates a child HitboxController for N seconds
└── RangedWeapon -> fires a ProjectileBase via ObjectPool
```

To add a new weapon type: derive `WeaponBase`, override `FireInternal`.

## Projectiles

```
ProjectileBase (abstract)
├── BulletProjectile  -> forward raycast, despawn on hit or lifetime
├── HomingProjectile  -> finds the closest tagged target, steers
└── GrenadeProjectile -> ballistic, explodes on contact or timeout
```

All go through `ObjectPool`. The pool key is provided to `Launch()`.

## Status Effects

```
StatusEffectBase (abstract)
├── StatusEffectBurn   -> periodic Fire damage
├── StatusEffectPoison -> periodic Poison damage
├── StatusEffectStun   -> disables MonoBehaviours for the duration
└── StatusEffectFreeze -> applies high drag on the Rigidbody
```

To apply: `gameObject.AddComponent<StatusEffectBurn>().Apply(source, duration, tickInterval)`.

## Combo

`ComboSystem`: counter that increments on kill, resets to 0 after
`_resetSeconds`. Multiplier is exposed for score / damage scaling.

## Auto-wiring

- `KnockbackHook`: connects HealthComponent.OnDamageTaken -> Knockback.Apply.
- `InvincibilityFlash`: blinks on every hit.
- `KillReward`: adds score + publishes `EnemyKilledEvent` on death.
- `DespawnOnDeath`: pool return after delay.
- `EnemyDeathExplosion`: triggers an AreaDamage on death.

## Recommended pattern for an enemy

For a standard mob:

1. `HealthComponent` (with resistances)
2. `Knockback` + `KnockbackHook`
3. `InvincibilityFlash`
4. `KillReward`
5. `DespawnOnDeath` (or `Combat/AreaDamage` for explosive mobs)
6. `EnemyKilledHook` (publishes EnemyKilledEvent)
7. `LootDropper` (loot via LootTable)

All of this auto-wires with HealthComponent. The designer just tweaks the
inspector values.
