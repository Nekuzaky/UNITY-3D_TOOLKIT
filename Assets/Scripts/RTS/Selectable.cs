using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.RTS
{
    /// <summary>Marks a unit as selectable. Stores team id + selected state.</summary>
    public sealed class Selectable : MonoBehaviour
    {
        [SerializeField] private int _teamId;
        [SerializeField] private GameObject _selectedRing;
        [SerializeField] private UnityEvent _onSelected;
        [SerializeField] private UnityEvent _onDeselected;

        public int TeamId => _teamId;
        public bool IsSelected { get; private set; }

        public void SetSelected(bool value)
        {
            if (value == IsSelected) return;
            IsSelected = value;
            if (_selectedRing != null) _selectedRing.SetActive(value);
            if (value) _onSelected?.Invoke();
            else _onDeselected?.Invoke();
        }
    }
}
