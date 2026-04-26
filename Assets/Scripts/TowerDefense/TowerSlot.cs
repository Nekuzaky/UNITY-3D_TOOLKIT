using UnityEngine;
using UnityEngine.Events;

namespace GameJamToolkit.TowerDefense
{
    /// <summary>Slot where a tower can be built. Holds the current tower if any.</summary>
    public sealed class TowerSlot : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;
        [SerializeField] private UnityEvent _onTowerBuilt;
        [SerializeField] private UnityEvent _onTowerRemoved;

        public GameObject CurrentTower { get; private set; }
        public bool IsEmpty => CurrentTower == null;

        public bool TryBuild(GameObject towerPrefab)
        {
            Debug.Assert(towerPrefab != null, "[TowerSlot.TryBuild] towerPrefab is null"); // R5
            if (!IsEmpty) return false;
            CurrentTower = Instantiate(towerPrefab, transform.position, transform.rotation, transform); // R3 exempt: tower commit
            if (_highlight != null) _highlight.SetActive(false);
            _onTowerBuilt?.Invoke();
            return true;
        }

        public void Sell()
        {
            if (CurrentTower == null) return;
            Destroy(CurrentTower);
            CurrentTower = null;
            _onTowerRemoved?.Invoke();
        }

        private void OnMouseEnter() { if (_highlight != null && IsEmpty) _highlight.SetActive(true); }
        private void OnMouseExit() { if (_highlight != null) _highlight.SetActive(false); }
    }
}
