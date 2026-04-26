# AI

Several abstraction levels depending on the desired complexity.

## 1. Simple scripted AI

`SimpleAI` (in Entities): Idle -> Patrol -> Chase -> Attack. Plenty for jam
mobs.

Setup:
1. Components: `HealthComponent` + `SimpleAI` + `EnemyConfig` SO.
2. Fill in `_waypoints` (optional) and `_target` (the player).
3. Transitions are based on distance and target presence.

## 2. Generic state machine

For custom behaviors:

```csharp
var sm = new StateMachine();
sm.Register(new IdleState());
sm.Register(new ChaseState(transform, player.transform, speed: 4f, stopDistance: 1.2f));
sm.Register(new AttackState(transform, player.transform, range: 1.5f, damage: 8f, cooldown: 1f));
sm.TransitionTo<IdleState>();
// in Update
sm.Tick(Time.deltaTime);
```

Built-in states: Idle, Patrol, Chase, Attack, Flee, Dead.

## 3. NavMesh

For solid 3D navigation:

1. Bake a NavMesh in the scene (`Window > AI > Navigation`).
2. On the Enemy prefab, add a `NavMeshAgent`.
3. Use `NavMeshAgentController` or `NavMeshChase`:

```csharp
var ctrl = enemy.GetComponent<NavMeshAgentController>();
ctrl.GoTo(player.transform.position);
if (ctrl.HasReached(0.3f)) { /* ... */ }
```

`NavMeshWander`: roams in a radius. Useful for passive NPCs / mobs.

## 4. Behavior Tree

For bosses / complex AI:

```csharp
public sealed class GoblinBT : BTRunner
{
    [SerializeField] private Transform _target;

    protected override BTNode BuildTree()
    {
        return new BTSelector()
            .Add(new BTSequence()
                .Add(new BTCondition(() => _target != null))
                .Add(new BTAction(() => { /* attack */ return BTStatus.Success; }))
            )
            .Add(new BTAction(() => { /* idle */ return BTStatus.Success; }));
    }
}
```

Available nodes: `BTSequence`, `BTSelector`, `BTAction`, `BTCondition`.

## Detection

| Script | Detection |
|---|---|
| `FieldOfView` | Cone (radius + angle + line of sight) |
| `LineOfSight.Has(from, to, mask)` | Direct ray test |

`FieldOfView`:

```csharp
if (fov.VisibleTarget != null) chase.GoTo(fov.VisibleTarget.position);
```

## Aggro

`AggroSystem`: on every damage taken, sets target to the attacker. Decays
after `_decaySeconds`. Lets neutral enemies turn hostile when hit.

## Recommended pattern for a mob

- `HealthComponent`
- `EnemyConfig` SO
- `SimpleAI` (or variant)
- `AggroSystem`
- Optional: `NavMeshAgent` + `NavMeshChase` for proper pathfinding
- `Misc/EnemyKilledHook`
- `Combat/DespawnOnDeath`
- `Items/LootDropper` with a LootTable
