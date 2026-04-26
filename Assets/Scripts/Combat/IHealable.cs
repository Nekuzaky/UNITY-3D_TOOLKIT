namespace GameJamToolkit.Combat
{
    /// <summary>Implemented by any object that can be healed.</summary>
    public interface IHealable
    {
        void Heal(float amount);
    }
}
