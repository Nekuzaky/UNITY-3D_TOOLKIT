using UnityEngine;

namespace GameJamToolkit.CardGame
{
    /// <summary>A card definition (data only). Effects are decoupled.</summary>
    [CreateAssetMenu(menuName = "GameJamToolkit/CardGame/Card", fileName = "Card")]
    public sealed class CardDefinition : ScriptableObject
    {
        public string CardId = "card_default";
        public string DisplayName = "Card";
        [TextArea] public string Description;
        public Sprite Art;
        [Min(0)] public int Cost;
        [Min(0)] public int Power;
        [Min(0)] public int Toughness;
    }
}
