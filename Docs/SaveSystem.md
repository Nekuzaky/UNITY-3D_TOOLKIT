# SaveSystem

Simple JSON save, sufficient for a game jam. No encryption, no advanced
versioning.

## Architecture

- `ISaveable`: any object that wants to be saved implements it.
- `SaveManager`: singleton, aggregates ISaveable objects and writes/reads the file.
- `SaveData`: serialized container (`JsonUtility`).

## Implement a Saveable

```csharp
public sealed class MySaveable : MonoBehaviour, ISaveable
{
    [SerializeField] private string _saveKey = "myKey";
    public string SaveKey => _saveKey;

    [Serializable] private class State { public int Foo; public float Bar; }

    private void OnEnable() { if (SaveManager.HasInstance) SaveManager.Instance.Register(this); }
    private void OnDisable() { if (SaveManager.HasInstance) SaveManager.Instance.Unregister(this); }

    public string SerializeState() { return JsonUtility.ToJson(new State { Foo = 42, Bar = 3.14f }); }
    public void DeserializeState(string state)
    {
        if (string.IsNullOrEmpty(state)) return;
        var s = JsonUtility.FromJson<State>(state);
        // ... apply s.Foo, s.Bar
    }
}
```

## Built-in saveables

| Script | Saves |
|---|---|
| `SaveableTransform` | Position, rotation, scale |
| `SaveableHealth` | Current HP, max HP |
| `SaveableScore` | Current score, high score |

## Save / Load

```csharp
SaveManager.Instance.Save(slot: 0);
SaveManager.Instance.Load(slot: 0);
SaveManager.Instance.SlotExists(0);
SaveManager.Instance.DeleteSlot(0);
```

File: `Application.persistentDataPath/save_<slot>.json`.

## When to save

- At the end of a level.
- On checkpoint reached (subscribe to `CheckpointReachedEvent`).
- On quit (`OnApplicationQuit` or similar).

## Limitations

- No automatic migration between save versions: if the format changes, an old
  save must be regenerated. Fine for a jam.
- No encryption: easy to edit by hand. Fine for a jam, not for production.
- `JsonUtility` doesn't support Dictionary. Use lists of key/value pairs if
  needed (see `LocalizationTable.Entry`).

## Save the inventory

No built-in saveable for the inventory. To do it:

```csharp
public sealed class SaveableInventory : MonoBehaviour, ISaveable
{
    [SerializeField] private Items.Inventory _inventory;
    [SerializeField] private Items.ItemDatabase _database;
    [SerializeField] private string _saveKey = "inventory";
    public string SaveKey => _saveKey;

    [Serializable] private class State { public string[] Ids; public int[] Counts; }

    public string SerializeState()
    {
        int n = _inventory.Capacity;
        var s = new State { Ids = new string[n], Counts = new int[n] };
        for (int i = 0; i < n; i++)
        {
            var slot = _inventory.GetSlot(i);
            s.Ids[i] = slot.Item != null ? slot.Item.ItemId : null;
            s.Counts[i] = slot.Count;
        }
        return JsonUtility.ToJson(s);
    }

    public void DeserializeState(string state)
    {
        if (string.IsNullOrEmpty(state) || _database == null) return;
        var s = JsonUtility.FromJson<State>(state);
        if (s == null) return;
        // ... clear inventory, then re-add via ItemDatabase.Get(id)
    }
}
```
