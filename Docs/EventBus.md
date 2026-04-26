# EventBus

## Why

Decouple modules. The Audio module shouldn't know about Combat, but should be
able to react to damage (play a hit sound). The EventBus lets Audio listen to
`DamageDealtEvent` without referencing Combat.

## Usage

Define an event (always a `struct` in `Core/Events/`):

```csharp
namespace GameJamToolkit.Core.Events
{
    public struct MyCustomEvent
    {
        public int Value;
        public Vector3 Position;
    }
}
```

Subscribe:

```csharp
private void OnEnable()
{
    EventBus.Subscribe<MyCustomEvent>(HandleMyEvent);
}

private void OnDisable()
{
    EventBus.Unsubscribe<MyCustomEvent>(HandleMyEvent); // CRUCIAL
}

private void HandleMyEvent(MyCustomEvent evt)
{
    Debug.Log(evt.Value);
}
```

Publish:

```csharp
EventBus.Publish(new MyCustomEvent { Value = 42, Position = transform.position });
```

## Built-in events

Defined in `Core/Events/`:

| Event | Fields | Published by |
|---|---|---|
| `GameStateChangedEvent` | Previous, Current | GameManager |
| `GameplayStartedEvent` | Level | (project-side) |
| `GameplayEndedEvent` | Victory, DurationSeconds | (project-side) |
| `PauseToggledEvent` | IsPaused | GameManager |
| `SceneLoadStartedEvent` | SceneName | SceneLoader |
| `SceneLoadCompletedEvent` | SceneName | SceneLoader |
| `PlayerDiedEvent` | Player, Position, Score | PlayerDeathHook |
| `EnemyKilledEvent` | Enemy, Position, ScoreReward | EnemyKilledHook / KillReward |
| `DamageDealtEvent` | Source, Target, Amount, HitPoint | HealthComponent |
| `HealEvent` | Target, Amount | HealthComponent |
| `ScoreChangedEvent` | Previous, Current, Delta | ScoreManager |
| `InteractionRequestedEvent` | Source, Target | PlayerInteraction |
| `PlayerSpawnedEvent` | Player, Position | PlayerSpawnPoint |
| `CheckpointReachedEvent` | CheckpointId, Position | PlayerCheckpoint |
| `ItemPickedUpEvent` | Picker, ItemId, Amount | ItemPickup / PickupInteractable |

## Best practices

- Events are `struct` (zero allocation, fast).
- Always `Unsubscribe` symmetrically with `Subscribe` (otherwise the delegate
  leaks).
- Don't run heavy logic inside the handler; delegate if needed.
- For multiple custom events, follow the same pattern (one file per event).

## Reset between scenes

If you want to purge all subscriptions (e.g. when leaving a session), call
`EventBus.Clear()`. Use sparingly: it breaks subscriptions of persistent
modules (UI, Audio, etc.).
