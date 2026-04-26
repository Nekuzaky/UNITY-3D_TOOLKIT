namespace GameJamToolkit.Core.Events
{
    public struct GameStateChangedEvent
    {
        public GameState Previous;
        public GameState Current;
    }
}
