using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.CameraTools
{
    /// <summary>Fades Renderers between the camera and the target (opaque wall -> transparent).</summary>
    public sealed class CameraOcclusionFader : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private LayerMask _occluderMask = ~0;
        [SerializeField] [Range(0f, 1f)] private float _occludedAlpha = 0.4f;

        private readonly List<Renderer> _previousList = new List<Renderer>();
        private readonly List<Renderer> _currentList = new List<Renderer>();

        private void LateUpdate()
        {
            if (_target == null) return;
            _currentList.Clear();
            Vector3 dir = _target.position - transform.position;
            float dist = dir.magnitude;
            var hits = Physics.RaycastAll(transform.position, dir.normalized, dist, _occluderMask, QueryTriggerInteraction.Ignore);
            int max = hits.Length; // R2
            for (int i = 0; i < max; i++)
            {
                var r = hits[i].collider.GetComponentInChildren<Renderer>();
                if (r != null) _currentList.Add(r);
            }
            ApplyAlpha(_currentList, _occludedAlpha);
            // Restore previous occluders no longer in the list
            int prevMax = _previousList.Count;
            for (int i = 0; i < prevMax; i++)
            {
                if (_previousList[i] != null && !_currentList.Contains(_previousList[i]))
                    SetAlpha(_previousList[i], 1f);
            }
            _previousList.Clear();
            _previousList.AddRange(_currentList);
        }

        private static void ApplyAlpha(List<Renderer> list, float a)
        {
            int max = list.Count;
            for (int i = 0; i < max; i++) SetAlpha(list[i], a);
        }

        private static void SetAlpha(Renderer r, float a)
        {
            if (r == null || r.material == null) return;
            var c = r.material.color;
            c.a = a;
            r.material.color = c;
        }
    }
}
