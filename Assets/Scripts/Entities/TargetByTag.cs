using UnityEngine;

namespace GameJamToolkit.Entities
{
    /// <summary>Wires an AI onto a target identified by tag.</summary>
    public sealed class TargetByTag : MonoBehaviour
    {
        [SerializeField] private string _tag = "Player";
        [SerializeField] private EnemyBase _enemy;

        private void Start()
        {
            if (_enemy == null) _enemy = GetComponent<EnemyBase>();
            Debug.Assert(_enemy != null, "[TargetByTag] _enemy null"); // R5
            var go = GameObject.FindGameObjectWithTag(_tag);
            if (go != null && _enemy != null) _enemy.SetTarget(go.transform);
        }
    }
}
