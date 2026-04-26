using UnityEngine;

namespace GameJamToolkit.LocalMultiplayer
{
    /// <summary>
    /// Lays out N cameras in horizontal/vertical/quad split-screen rects.
    /// Provide one Camera per local player.
    /// </summary>
    public sealed class SplitScreenSetup : MonoBehaviour
    {
        public enum Layout { Horizontal, Vertical, Quad }
        [SerializeField] private Camera[] _cameras;
        [SerializeField] private Layout _layout = Layout.Horizontal;

        public void Apply()
        {
            Debug.Assert(_cameras != null && _cameras.Length > 0, "[SplitScreenSetup.Apply] _cameras is empty"); // R5
            int n = _cameras.Length;
            int max = n; // R2
            for (int i = 0; i < max; i++)
            {
                var cam = _cameras[i];
                if (cam == null) continue;
                cam.rect = ComputeRect(i, n);
            }
        }

        private Rect ComputeRect(int i, int n)
        {
            switch (_layout)
            {
                case Layout.Horizontal: return new Rect(i / (float)n, 0f, 1f / n, 1f);
                case Layout.Vertical: return new Rect(0f, i / (float)n, 1f, 1f / n);
                case Layout.Quad:
                {
                    float x = (i % 2) * 0.5f;
                    float y = (i / 2) * 0.5f;
                    return new Rect(x, y, 0.5f, 0.5f);
                }
            }
            return new Rect(0f, 0f, 1f, 1f);
        }
    }
}
