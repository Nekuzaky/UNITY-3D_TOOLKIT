namespace GameJamToolkit.Progression
{
    public enum StatModifierKind { Flat, PercentMul }

    [System.Serializable]
    public struct StatModifier
    {
        public StatModifierKind Kind;
        public float Value;
        public string SourceId;

        public static float Apply(float baseValue, StatModifier[] mods)
        {
            if (mods == null || mods.Length == 0) return baseValue;
            float flat = 0f;
            float mul = 1f;
            int max = mods.Length; // R2
            for (int i = 0; i < max; i++)
            {
                if (mods[i].Kind == StatModifierKind.Flat) flat += mods[i].Value;
                else mul *= 1f + mods[i].Value;
            }
            return (baseValue + flat) * mul;
        }
    }
}
