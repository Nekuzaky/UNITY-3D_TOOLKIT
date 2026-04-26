using UnityEngine;

namespace GameJamToolkit.VFX
{
    /// <summary>Draws a line (laser, link, projectile beam) between two points for N seconds.</summary>
    [RequireComponent(typeof(LineRenderer))]
    public sealed class LineRenderEffect : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 0.1f;
        private LineRenderer _line;
        private float _despawnAt;

        private void Awake() { _line = GetComponent<LineRenderer>(); Debug.Assert(_line != null, "[LineRenderEffect] _line null"); }

        public void Show(Vector3 from, Vector3 to)
        {
            Debug.Assert(_line != null, "[LineRenderEffect.Show] _line null"); // R5
            if (_line == null) return;
            _line.SetPosition(0, from);
            _line.SetPosition(1, to);
            _line.enabled = true;
            _despawnAt = Time.time + _lifetime;
        }

        private void Update()
        {
            if (!_line.enabled) return;
            if (Time.time >= _despawnAt) _line.enabled = false;
        }
    }
}
