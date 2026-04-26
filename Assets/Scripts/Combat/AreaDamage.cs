using UnityEngine;

namespace GameJamToolkit.Combat
{
    /// <summary>Damages every IDamageable in a radius when activated.</summary>
    public sealed class AreaDamage : MonoBehaviour
    {
        [SerializeField] private float _radius = 3f;
        [SerializeField] private float _damage = 25f;
        [SerializeField] private DamageType _type = DamageType.Physical;
        [SerializeField] private LayerMask _mask = ~0;
        [SerializeField] private GameObject _owner;

        public void Detonate(Vector3 origin)
        {
            Debug.Assert(_radius > 0f, "[AreaDamage.Detonate] _radius <= 0"); // R5
            Debug.Assert(_damage > 0f, "[AreaDamage.Detonate] _damage <= 0"); // R5

            var hits = Physics.OverlapSphere(origin, _radius, _mask);
            int max = hits.Length; // R2
            for (int i = 0; i < max; i++)
            {
                if (hits[i] == null) continue;
                Vector3 p = hits[i].ClosestPoint(origin);
                float falloff = 1f - Mathf.Clamp01(Vector3.Distance(origin, p) / _radius);
                DamageHandler.TryDamage(hits[i].gameObject, new DamageInfo
                {
                    Amount = _damage * falloff,
                    Type = _type,
                    Source = _owner != null ? _owner : gameObject,
                    HitPoint = p,
                    HitNormal = (p - origin).normalized
                });
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.4f, 0f, 0.4f);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
