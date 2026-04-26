using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildren(this Transform t)
        {
            Debug.Assert(t != null, "[TransformExtensions.DestroyAllChildren] t null"); // R5
            if (t == null) return;
            int max = t.childCount; // R2
            for (int i = max - 1; i >= 0; i--)
            {
                var child = t.GetChild(i);
                if (Application.isPlaying) Object.Destroy(child.gameObject);
                else Object.DestroyImmediate(child.gameObject);
            }
        }

        public static void ResetLocal(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static Transform FindRecursive(this Transform t, string childName)
        {
            if (t == null || string.IsNullOrEmpty(childName)) return null;
            if (t.name == childName) return t;
            int max = t.childCount;
            for (int i = 0; i < max; i++)
            {
                var found = FindRecursive(t.GetChild(i), childName);
                if (found != null) return found;
            }
            return null;
        }
    }
}
