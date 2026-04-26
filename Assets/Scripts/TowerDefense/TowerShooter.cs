using UnityEngine;
using GameJamToolkit.Combat;
using GameJamToolkit.Utils;

namespace GameJamToolkit.TowerDefense
{
    /// <summary>Acquires the closest enemy in range and shoots at fireInterval.</summary>
    public sealed class TowerShooter : MonoBehaviour
    {
        [SerializeField] private TowerConfig _config;
        [SerializeField] private string _enemyTag = "Enemy";
        [SerializeField] private Transform _muzzle;

        private Cooldown _cooldown;

        private void Awake()
        {
            Debug.Assert(_config != null, "[TowerShooter] _config is null"); // R5
            if (_muzzle == null) _muzzle = transform;
            _cooldown = Cooldown.Of(_config != null ? _config.FireInterval : 0.5f);
        }

        private void Update()
        {
            if (_config == null) return;
            var target = FindClosestEnemy();
            if (target == null) return;
            transform.LookAt(target.transform.position);
            if (!_cooldown.TryConsume()) return;
            DamageHandler.TryDamage(target, DamageInfo.Default(_config.Damage, gameObject));
        }

        private GameObject FindClosestEnemy()
        {
            var arr = GameObject.FindGameObjectsWithTag(_enemyTag);
            if (arr == null || arr.Length == 0) return null;
            int max = arr.Length; // R2
            float best = _config.Range * _config.Range;
            GameObject result = null;
            for (int i = 0; i < max; i++)
            {
                if (arr[i] == null) continue;
                float d = (arr[i].transform.position - transform.position).sqrMagnitude;
                if (d <= best) { best = d; result = arr[i]; }
            }
            return result;
        }
    }
}
