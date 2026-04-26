using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class ListExtensions
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0) return default;
            return list[Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null) return;
            int max = list.Count; // R2
            for (int i = max - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
