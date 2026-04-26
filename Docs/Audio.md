# Audio

## AudioManager

Persistent singleton. Pool of N AudioSource for SFX, two music sources for
crossfade.

Configuration:
- `_database`: an `AudioDatabase` SO containing all sounds.
- `_sfxGroup` / `_musicGroup`: exposed `AudioMixerGroup` references.
- `_sfxPoolSize`: number of concurrent SFX channels (default 16).

## SoundClipConfig

A sound clip is a SO:

```
Id : "sfx_explosion"
Clips : [explosion_01, explosion_02, explosion_03]   // randomly picked
Volume : 0.8
PitchRange : (0.95, 1.05)
SpatialBlend : 1   (3D)
Loop : false
```

To avoid repetition, add 2-3 alternative clips.

## AudioDatabase

ScriptableObject aggregating all `SoundClipConfig` entries. Builds an
`Id -> SoundClip` cache in `OnEnable`.

## Play a sound

```csharp
AudioManager.Instance.PlaySfx("sfx_explosion");                  // 2D
AudioManager.Instance.PlaySfxAt("sfx_explosion", worldPosition); // 3D
```

## Play music

```csharp
AudioManager.Instance.PlayMusic(myClip, fadeSeconds: 1.5f);
```

The two music sources linearly crossfade.

## SfxOnEvent

Instead of coupling Audio to other modules, drop a `SfxOnEvent` component that
listens to an EventBus event:

| Trigger | Event listened |
|---|---|
| PlayerDied | PlayerDiedEvent |
| EnemyKilled | EnemyKilledEvent |
| DamageDealt | DamageDealtEvent |
| ScoreChanged | ScoreChangedEvent |
| CheckpointReached | CheckpointReachedEvent |
| ItemPickedUp | ItemPickedUpEvent |

## PlayMusicOnStateChange

Plays a different music per `GameState`. Configure the inspector list:

```
Entry { State: MainMenu, Clip: menu_loop, FadeSeconds: 1.5 }
Entry { State: Gameplay, Clip: combat_loop, FadeSeconds: 1 }
Entry { State: GameOver, Clip: defeat_sting, FadeSeconds: 0.5 }
```

## VolumeController

UI slider driving an AudioMixer parameter. Linear -> dB mapping is included
(0 = -80dB, 1 = 0dB).

Configure a project AudioMixer with exposed parameters:
- `MasterVol`, `MusicVol`, `SfxVol`

Wire one `VolumeController` per slider, with the matching `_exposedParam`.

## Footstep

`FootstepPlayer` plays an SFX at an interval inversely proportional to the
controller's speed. Drop on the Player prefab with the right sound id.

## OneShotAudio

Quick static helper:

```csharp
OneShotAudio.PlayAt(myClip, worldPos, volume: 1f);
```

Uses `AudioSource.PlayClipAtPoint`. No pool, no variation. For prototyping.
