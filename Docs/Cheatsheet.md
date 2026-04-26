# Cheatsheet

Quick snippets for the most frequent operations.

## EventBus

```csharp
EventBus.Subscribe<MyEvent>(Handler);
EventBus.Unsubscribe<MyEvent>(Handler);
EventBus.Publish(new MyEvent { Foo = 42 });
EventBus.Clear();
```

## ObjectPool

```csharp
var go = ObjectPool.Instance.Get("Bullet", pos, rot);
ObjectPool.Instance.Return("Bullet", go);
ObjectPool.Instance.HasPool("Bullet");
ObjectPool.Instance.CountAvailable("Bullet");
```

## GameManager

```csharp
GameManager.Instance.ChangeState(GameState.Gameplay);
GameManager.Instance.TogglePause();
GameManager.Instance.TriggerWin();
GameManager.Instance.TriggerGameOver();
```

## SceneLoader

```csharp
SceneLoader.Instance.LoadScene("Gameplay");
SceneLoader.Instance.ReloadCurrent();
SceneLoader.Instance.IsLoading;
SceneLoader.Instance.Progress;
```

## ScoreManager

```csharp
ScoreManager.Instance.Add(100);
ScoreManager.Instance.Reset();
int hi = ScoreManager.Instance.HighScore;
```

## Damage

```csharp
DamageHandler.TryDamage(target, amount: 10f, source: gameObject);

DamageHandler.TryDamage(target, new DamageInfo {
    Amount = 25f,
    Type = DamageType.Fire,
    Source = gameObject,
    HitPoint = hit.point,
    HitNormal = hit.normal,
    Knockback = 6f,
    IsCritical = true
});
```

## Health

```csharp
health.TakeDamage(info);
health.Heal(20f);
health.Kill();
health.IsAlive;
health.Ratio;
```

## Audio

```csharp
AudioManager.Instance.PlaySfx("sfx_jump");
AudioManager.Instance.PlaySfxAt("sfx_explosion", worldPos);
AudioManager.Instance.PlayMusic(myClip, fadeSeconds: 1.5f);
```

## VFX

```csharp
VFXSpawner.Spawn("Explosion", pos, Quaternion.identity);
DecalSpawner.Spawn("BulletHole", raycastHit, yOffset: 0.01f);
ScreenFlash.ActiveInstance.Flash(Color.red);
CameraShake.ActiveInstance.Shake(0.4f);
```

## Camera

```csharp
cameraFollow.SetTarget(player.transform);
orbit.SetTarget(player.transform);
cameraRig.SetMode(CameraRig.Mode.Orbit);
TimeScaleController.SlowDown(0.2f);
TimeScaleController.Restore();
```

## Save

```csharp
SaveManager.Instance.Save(0);
SaveManager.Instance.Load(0);
SaveManager.Instance.SlotExists(0);
SaveManager.Instance.DeleteSlot(0);
```

## Localization

```csharp
LocalizationManager.Instance.SetLanguage("en");
string s = LocalizationManager.Instance.Get("main_menu.play");
```

## Inventory

```csharp
inventory.TryAdd(item, count: 3);
inventory.TryRemove(slotIndex: 2, count: 1);
inventory.Has(item, count: 1);
```

## Quest

```csharp
QuestManager.Instance.Begin(myQuest);
QuestManager.Instance.Abandon(myQuest);
```

## Achievement

```csharp
AchievementManager.Instance.Unlock("first_kill");
bool b = AchievementManager.Instance.IsUnlocked("first_kill");
```

## UI

```csharp
NotificationUI.ActiveInstance.Push("Item picked up!");
TooltipUI.ActiveInstance.Show("Description");
TooltipUI.ActiveInstance.Hide();
screenFader.FadeOut(0.4f);
screenFader.FadeIn(0.4f);
```

## Time

```csharp
GameClock.Instance.StartClock();
GameClock.Instance.StopClock();
GameClock.Instance.ResetClock();
float t = GameClock.Instance.ElapsedSeconds;
```

## Cooldown / Timer

```csharp
var cd = Cooldown.Of(1.5f);
if (cd.TryConsume()) { /* fire ! */ }

var t = new Timer();
t.Start(2f);
t.OnComplete += () => Debug.Log("done");
// in Update: t.Tick(Time.deltaTime);
```

## Math

```csharp
float r = MathHelper.Remap(value, 0, 100, -1, 1);
Vector3 snapped = MathHelper.SnapTo(v, 0.5f);
float t = Easing.QuadOut(0.7f);
int idx = WeightedRandom.PickIndex(weights);
Vector3 p = BezierCurve.Quadratic(p0, p1, p2, t);
```

## Extensions

```csharp
Vector3 v = transform.position.WithY(0f);
Vector2 xz = v.XZ();
gameObject.SetLayerRecursive(LayerMask.NameToLayer("Player"));
list.Shuffle();
T element = list.RandomElement();
Color c = ColorExtensions.FromHex("#ff8800");
```
