using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.CardGame
{
    /// <summary>Cards in the player's hand. Pure data; UI separate.</summary>
    public sealed class Hand : MonoBehaviour
    {
        [SerializeField] private int _maxCards = 7;
        private readonly List<CardDefinition> _cards = new List<CardDefinition>();
        public int Count => _cards.Count;
        public IReadOnlyList<CardDefinition> Cards => _cards;
        public event Action OnChanged;

        public bool TryAdd(CardDefinition card)
        {
            Debug.Assert(card != null, "[Hand.TryAdd] card is null"); // R5
            if (_cards.Count >= _maxCards) return false;
            _cards.Add(card);
            OnChanged?.Invoke();
            return true;
        }

        public bool TryRemove(CardDefinition card)
        {
            bool removed = _cards.Remove(card);
            if (removed) OnChanged?.Invoke();
            return removed;
        }

        public CardDefinition GetAt(int index)
        {
            Debug.Assert(index >= 0 && index < _cards.Count, "[Hand.GetAt] index out of range"); // R5
            return (index >= 0 && index < _cards.Count) ? _cards[index] : null;
        }
    }
}
