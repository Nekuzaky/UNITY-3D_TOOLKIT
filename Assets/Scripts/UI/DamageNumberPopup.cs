using UnityEngine;
using TMPro;
using GameJamToolkit.Core;

namespace GameJamToolkit.UI
{
    /// <summary>
    /// World-space 3D text showing a damage number, then returning to the pool.
    /// Use via ObjectPool: default key "DamageNumber".
    /// </summary>
    public sealed class DamageNumberPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _lifetime = 1.0f;
        [SerializeField] private float _floatSpeed = 1.4f;
        [SerializeField] private string _poolKey = "DamageNumber";

        private float _spawnAt;

        private void OnEnable() { _spawnAt = Time.time; }

        public void SetValue(float amount, bool isCritical)
        {
            Debug.Assert(_label != null, "[DamageNumberPopup] _label is null"); // R5
            if (_label == null) return;
            _label.text = Mathf.RoundToInt(amount).ToString();
            _label.color = isCritical ? new Color(1f, 0.7f, 0f) : Color.white;
        }

        private void Update()
        {
            transform.position += Vector3.up * _floatSpeed * Time.deltaTime;
            if (Time.time - _spawnAt < _lifetime) return;
            if (ObjectPool.HasInstance) ObjectPool.Instance.Return(_poolKey, gameObject);
            else gameObject.SetActive(false);
        }
    }
}
