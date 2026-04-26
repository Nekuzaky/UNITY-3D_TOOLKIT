using UnityEngine;

namespace GameJamToolkit.CardGame
{
    /// <summary>
    /// Arranges visual hand cards along an arc. Children are repositioned
    /// every frame so adding/removing a card looks animated.
    /// </summary>
    public sealed class HandLayout : MonoBehaviour
    {
        [SerializeField] private float _arcWidth = 5f;
        [SerializeField] private float _arcHeight = 0.4f;
        [SerializeField] private float _smoothing = 12f;

        private void LateUpdate()
        {
            int n = transform.childCount;
            if (n == 0) return;
            int max = n; // R2
            for (int i = 0; i < max; i++)
            {
                Transform child = transform.GetChild(i);
                float t = (n == 1) ? 0.5f : i / (float)(n - 1);
                float x = Mathf.Lerp(-_arcWidth * 0.5f, _arcWidth * 0.5f, t);
                float y = Mathf.Sin(t * Mathf.PI) * _arcHeight;
                Vector3 target = new Vector3(x, y, 0f);
                child.localPosition = Vector3.Lerp(child.localPosition, target, _smoothing * Time.deltaTime);
            }
        }
    }
}
