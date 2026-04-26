namespace GameJamToolkit.Core
{
    /// <summary>
    /// Global game states. Owned by <see cref="GameManager"/>.
    /// Boot -> MainMenu -> Gameplay <-> Paused -> GameOver / Win
    /// </summary>
    public enum GameState
    {
        Boot = 0,
        MainMenu = 1,
        Loading = 2,
        Gameplay = 3,
        Paused = 4,
        GameOver = 5,
        Win = 6,
        Cutscene = 7
    }
}
