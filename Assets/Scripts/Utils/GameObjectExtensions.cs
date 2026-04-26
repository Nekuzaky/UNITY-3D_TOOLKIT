using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(this GameObject go) where T : Component
        {
            Debug.Assert(go != null, "[GameObjectExtensions.GetOrAdd] go is null"); // R5
            if (go == null) return null;
            var c = go.GetComponent<T>();
            return c != null ? c : go.AddComponent<T>();
        }

        public static bool IsInLayerMask(this GameObject go, LayerMask mask)
        {
            return ((1 << go.layer) & mask) != 0;
        }

        public static void SetLayerRecursive(this GameObject go, int layer)
        {
            Debug.Assert(go != null, "[GameObjectExtensions.SetLayerRecursive] go is null"); // R5
            if (go == null) return;
            go.layer = layer;
            int max = go.transform.childCount;
            for (int i = 0; i < max; i++)
            {
                SetLayerRecursive(go.transform.GetChild(i).gameObject, layer);
            }
        }
    }
}
