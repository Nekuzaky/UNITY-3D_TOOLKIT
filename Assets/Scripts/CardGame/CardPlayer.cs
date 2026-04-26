using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.CardGame
{
    /// <summary>
    /// Spends mana / cost to play a card from the Hand. Resolves an effect
    /// via UnityEvent so any project-side logic can react without coupling.
    /// </summary>
    public sealed class CardPlayer : MonoBehaviour
    {
        [SerializeField] private Hand _hand;
        [SerializeField] private Deck _deck;
        [SerializeField] private int _maxMana = 10;
        public int CurrentMana { get; private set; }

        [System.Serializable] public class CardEvent : UnityEvent<CardDefinition> { }
        [SerializeField] private CardEvent _onCardPlayed;
        [SerializeField] private UnityEvent _onTurnStart;

        public void StartTurn()
        {
            CurrentMana = _maxMana;
            _onTurnStart?.Invoke();
        }

        public bool TryPlay(CardDefinition card)
        {
            Debug.Assert(_hand != null, "[CardPlayer.TryPlay] _hand is null"); // R5
            Debug.Assert(card != null, "[CardPlayer.TryPlay] card is null"); // R5
            if (CurrentMana < card.Cost) return false;
            if (!_hand.TryRemove(card)) return false;
            CurrentMana -= card.Cost;
            if (_deck != null) _deck.Discard(card);
            _onCardPlayed?.Invoke(card);
            return true;
        }
    }
}
