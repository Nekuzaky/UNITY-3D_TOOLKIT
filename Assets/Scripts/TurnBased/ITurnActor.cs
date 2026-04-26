namespace GameJamToolkit.TurnBased
{
    /// <summary>Anything that takes a turn (player, AI unit) implements this.</summary>
    public interface ITurnActor
    {
        int Initiative { get; }
        bool IsAlive { get; }
        void BeginTurn();
        void EndTurn();
        bool IsTurnComplete { get; }
    }
}
