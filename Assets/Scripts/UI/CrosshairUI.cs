using UnityEngine;
using UnityEngine.UI;

namespace GameJamToolkit.UI
{
    /// <summary>Centered crosshair. Color changes based on what the camera is targeting.</summary>
    public sealed class CrosshairUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _enemyColor = Color.red;
        [SerializeField] private LayerMask _enemyMask = ~0;
        [SerializeField] private float _maxDistance = 30f;

        private Camera _camera;

        private void Update()
        {
            if (_camera == null) _camera = Camera.main;
            if (_camera == null || _image == null) return;
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            bool hit = Physics.Raycast(ray, out _, _maxDistance, _enemyMask, QueryTriggerInteraction.Ignore);
            _image.color = hit ? _enemyColor : _normalColor;
        }
    }
}
