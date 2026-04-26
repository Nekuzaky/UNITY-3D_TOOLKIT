# Genre coverage

Quick reference: which modules to use for each 3D game genre.

## Action / Adventure

Core, Player, Combat, Entities, Camera, UI, Audio, VFX, Items, Interaction,
Quest, Misc.

## Racing / Driving

Core, Vehicles (`CarController`), Racing (`LapTracker`, `RaceManager`,
`RaceCheckpoint`, `RaceUI`), Camera (`CameraFollow`), Audio, UI.

## Flying / Air combat

Core, Vehicles (`PlaneController`), Combat (`RangedWeapon`, `BulletProjectile`),
Camera (`OrbitCamera` or `FirstPersonCamera`), UI, Audio.

## Boat / Naval

Core, Vehicles (`BoatController`), Physics (`WaterFloat`), Audio, UI.

## Hover / Spaceship

Core, Vehicles (`HoverController` for ground hover, `PlaneController` tweaked),
Combat, UI.

## Tactical / Turn-based / XCOM-like

Core, TurnBased (`TurnManager`, `TurnActorBase`, `ActionPoints`), Grid3D
(`GridSettings`, `GridOccupancy`, `GridPathfinder`, `GridMover`), Combat for
damage, UI.

## Roguelike / Dungeon crawler

Core, Player, Combat, Entities, Items, Interaction, Spawning (`RoomGenerator`,
`AreaSpawner`, `RandomScatter`), AI, Quest, Misc.

## Puzzle (Portal-like)

Core, Player, Puzzle (`Grabbable`, `PlayerGrab`, `WeightSwitch`, `LockChain`,
`ResetZone`, `ColorChannel`/`ColorReceiver`, `PuzzleSolver`), Interaction, UI.

## Stealth (Hitman, Splinter Cell)

Core, Player, Combat, Stealth (`SuspicionMeter`, `NoiseEmitter`,
`NoiseListener`, `HidingSpot`, `StealthVisibility`), AI (`FieldOfView`,
`LineOfSight`), Audio.

## Survival / Crafting

Core, Player, Combat, Survival (`Need`, `NeedsManager`, `WarmthZone`,
`NeedDamageOnDeplete`), Crafting (`Recipe`, `CraftingStation`, `ResourceNode`),
Items, Time (`DayNightCycle`, `WeatherSystem`).

## RPG / Adventure RPG

Core, Player, Combat, Entities, Quest, Items, Progression (`ExperienceSystem`,
`LevelTable`, `SkillTree`, `SkillNode`, `StatModifier`), Data
(`SaveableHealth`, `SaveableScore`), UI (`DialogueBoxUI`).

## RTS / Strategy

Core, RTS (`Selectable`, `SelectionManager`, `OrderHandler`, `ResourceEconomy`,
`BuildingPlacer`), AI (`NavMeshAgentController`), Camera (`TopDownCamera`),
Combat, Spawning.

## Tower Defense

Core, TowerDefense (`TowerSlot`, `TowerConfig`, `TowerShooter`), Entities
(`WaveSpawner`, `NavMeshChase`), AI (`WaypointPath`, `WaypointFollower`),
Combat, RTS (`ResourceEconomy` for cost), UI.

## Rhythm

Core, Rhythm (`BeatClock`, `NoteSpawner`, `TimingJudge`, `RhythmScore`),
Audio, UI.

## Card / Deck-building

Core, CardGame (`CardDefinition`, `Deck`, `Hand`, `CardPlayer`, `HandLayout`),
TurnBased (`TurnManager`), Combat (for cards that deal damage), UI.

## Sports

Core, Player, Sports (`Goal`, `BallController`, `MatchScore`, `GoalToScore`),
Camera (`CameraFollow`), Time (`GameTimer`), UI.

## Local multiplayer / Couch co-op

Core, LocalMultiplayer (`LocalPlayerManager`, `SplitScreenSetup`,
`LocalScoreBoard`), Player, Camera, UI.

## Visual novel / Narrative 3D

Core, UI (`DialogueBoxUI`, `LocalizedText`), Quest, Data (`LocalizationManager`).

---

## How to combine genres

Modules are intentionally independent. A "stealth racing" game would use
Vehicles + Stealth + Audio + Camera, with the Core/Pool/EventBus glue
underneath. A "deckbuilder dungeon crawler" combines CardGame + Spawning +
Quest + Combat. Pick what you need; ignore the rest.
