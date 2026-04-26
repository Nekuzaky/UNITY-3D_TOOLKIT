using System.Collections.Generic;
using UnityEngine;
using GameJamToolkit.Utils;

namespace GameJamToolkit.CardGame
{
    /// <summary>Ordered deck of cards. Draw + shuffle + reshuffle from discard.</summary>
    public sealed class Deck : MonoBehaviour
    {
        [SerializeField] private List<CardDefinition> _drawList = new List<CardDefinition>();
        [SerializeField] private List<CardDefinition> _discardList = new List<CardDefinition>();

        public int RemainingInDraw => _drawList.Count;
        public int RemainingInDiscard => _discardList.Count;

        public void Shuffle() { _drawList.Shuffle(); }

        public void AddToDraw(CardDefinition card)
        {
            Debug.Assert(card != null, "[Deck.AddToDraw] card is null"); // R5
            if (card != null) _drawList.Add(card);
        }

        public CardDefinition Draw()
        {
            if (_drawList.Count == 0) ReshuffleDiscard();
            if (_drawList.Count == 0) return null;
            int last = _drawList.Count - 1;
            var card = _drawList[last];
            _drawList.RemoveAt(last);
            return card;
        }

        public void Discard(CardDefinition card)
        {
            Debug.Assert(card != null, "[Deck.Discard] card is null"); // R5
            if (card != null) _discardList.Add(card);
        }

        private void ReshuffleDiscard()
        {
            int max = _discardList.Count; // R2
            for (int i = 0; i < max; i++) _drawList.Add(_discardList[i]);
            _discardList.Clear();
            Shuffle();
        }
    }
}
