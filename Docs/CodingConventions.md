# Coding conventions

The rules below apply to every new project script and to extensions of the
toolkit.

## Naming

| Type | Convention | Example |
|---|---|---|
| `namespace` | PascalCase | `GameJamToolkit.Combat` |
| `class` / `struct` / `enum` | PascalCase | `HealthComponent` |
| Method | PascalCase | `TakeDamage`, `Heal` |
| Public field | PascalCase | `public float MaxHealth` |
| Public property | PascalCase | `public float Current { get; private set; }` |
| Private field | _camelCase | `private float _maxHealth` |
| Protected field | _camelCase | `protected Rigidbody _rigidbody` |
| Local variable | camelCase | `int score = 0;` |
| Array (private) | _camelCases | `private Enemy[] _enemies` |
| List (private) | _camelCaseList | `private List<Enemy> _enemyList` |
| Dictionary (private) | _camelCaseDict | `private Dictionary<string, int> _scoreDict` |

## Script structure

```csharp
namespace GameJamToolkit.Module
{
    public class MyClass : MonoBehaviour
    {
        // 1. Constants
        private const int MaxItems = 16;

        // 2. Serialized fields
        [SerializeField] private float _moveSpeed;

        // 3. Private fields
        private Vector3 _currentVelocity;

        // 4. Public properties
        public float MoveSpeed => _moveSpeed;

        // 5. Events / Actions
        public event Action OnSomething;

        // 6. Unity Lifecycle
        private void Awake() { }
        private void Start() { }
        private void Update() { }
        private void OnDestroy() { }

        // 7. Public methods
        public void DoSomething() { }

        // 8. Private methods
        private void HelperMethod() { }
    }
}
```

## Defensive programming (R5)

Every non-trivial method starts by validating its preconditions:

```csharp
private void ApplyDamage(GameObject target, float amount)
{
    Debug.Assert(target != null, "[ApplyDamage] target null"); // R5
    Debug.Assert(amount > 0f, "[ApplyDamage] amount <= 0"); // R5
    if (target == null || amount <= 0f) return;

    // ... logic
}
```

`Debug.Assert` is free in release builds (compiled out). In the editor it
explodes the console when a precondition is violated, which saves hours of
debugging.

## Bounded loops (R2)

```csharp
int max = list.Count; // R2: fixed bound
for (int i = 0; i < max; i++)
{
    // ...
}
```

Avoid `while (true)`, `goto`, and any loop whose bound is not provably finite.

## ObjectPool (R3)

```csharp
// FORBIDDEN in gameplay
Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
Destroy(bulletGO);

// CORRECT
ObjectPool.Instance.Get("Bullet", spawnPos, Quaternion.identity);
ObjectPool.Instance.Return("Bullet", bulletGO);
```

Exception: scene initialization, weapon equipping, procedural generation.
Document with `// R3 exempt: <reason>`.

## EventBus

All cross-module communication goes through the EventBus:

```csharp
EventBus.Subscribe<MyEvent>(HandleMyEvent);
EventBus.Unsubscribe<MyEvent>(HandleMyEvent); // always in OnDisable / OnDestroy
EventBus.Publish(new MyEvent { Foo = 42 });
```

Events are lightweight `struct` types defined in `Core/Events/`.

## ScriptableObjects

For externalized config (stats, audio, levels, etc.). No hardcoded values.

```csharp
[CreateAssetMenu(menuName = "GameJamToolkit/Module/MyConfig", fileName = "MyConfig")]
public class MyConfig : ScriptableObject
{
    [Min(0f)] public float Speed = 5f;
}
```

## Comments

- No WHAT comments (the code already says what it does).
- WHY comments only, for non-obvious decisions.
- `///` headers for IntelliSense doc (short class/method description).
- Annotate rule violations (`// R3 exempt: ...`).

The `// R5`, `// R2`, etc. comments inside the toolkit code mark the spots
where the rule is intentionally enforced.

## Clean compilation (R10)

The project must compile with no warnings. If a warning cannot be resolved
(e.g. explicit use of an obsolete API for compatibility), document it.

## Tests

Not required for a game jam, but the Unity test framework is available
(`com.unity.test-framework`). Generic and utility classes are the easiest to
test (Timer, Cooldown, WeightedRandom, MathHelper).
