# UI

## UIManager

Switches panels per `GameState`. Configure references in the inspector:

| Panel | GameState |
|---|---|
| `_hudPanel` | Gameplay (also visible during Paused) |
| `_mainMenuPanel` | MainMenu |
| `_pausePanel` | Paused (overlaid on HUD) |
| `_gameOverPanel` | GameOver |
| `_winPanel` | Win |
| `_loadingPanel` | Loading |

`UIManager` automatically listens to `GameStateChangedEvent`.

## HUD

`HUDController` binds HealthBarUI + StaminaBarUI to the player on spawn.
To add other widgets, extend HUDController or add them in parallel.

## Score / Timer

- `ScoreUI`: listens to `ScoreChangedEvent`.
- `TimerUI`: reads `GameClock.Instance.ElapsedSeconds`.
- `HighScoreUI`: reads `ScoreManager.Instance.HighScore`.

## Menus

Every menu exposes its methods publicly for OnClick binding:

```csharp
public void OnPlay() { /* ... */ }
public void OnQuit() { /* ... */ }
```

Wire in the Button inspector -> OnClick -> select the method.

## Notifications / Tooltips

```csharp
NotificationUI.ActiveInstance.Push("Quest completed!");
TooltipUI.ActiveInstance.Show("Description...");
TooltipUI.ActiveInstance.Hide();
```

## Damage Numbers

Configure the `"DamageNumber"` pool in ObjectPool. Add a `DamageNumberSpawner`
(scene) listening to `DamageDealtEvent` and spawning a `DamageNumberPopup` at
the impact point. The popup floats up and returns to the pool after `_lifetime`.

## Loading Screen

`LoadingScreenUI` shows automatically on `SceneLoadStartedEvent` and hides on
`SceneLoadCompletedEvent`. Reads `SceneLoader` progress.

## Dialogue

```csharp
_dialogueBox.Show(new[] { "Hello.", "I'm an NPC.", "Goodbye." });
```

Automatic typing effect. Click to advance.

## Localization

For localized UI text, use `LocalizedText`:

1. Put `LocalizedText` on the TMP_Text.
2. Fill in the `_key` (e.g. `"main_menu.play"`).
3. The text updates on language switch (`LocalizationManager.SetLanguage("en")`).

## Crosshair / Interaction Prompt

For first-person:
- `CrosshairUI`: crosshair colored by what's targeted.
- `InteractionPromptUI`: "Press E" when an Interactable is in range.

## Pause

To bind the Pause key:
- Add a `PauseInputBinding` mapping an `InputAction` to `GameManager.TogglePause`.

## Best practices

- All UI components have a serializable `_label` (`TMP_Text`) or `_slider`.
  No tag/path-based auto-find.
- UIs subscribe to the EventBus, no `FindObjectOfType` at runtime.
- 3D UIs (healthbar above an enemy) use `BillboardUI` or `Utils/LookAt` to
  face the camera.
